using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductStore.API.DBFirst.Configs;
using ProductStore.API.DBFirst.ViewModels;
using ProductStore.API.DBFirst.ViewModels.Aws;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.AwsService.S3Service
{
    public class S3FileService:IS3FileService
    {
        private readonly ILogger<IS3FileService> _logger;
        private readonly IAmazonS3 _amazonS3Client;
        private readonly TransferUtility _transferUtility;
        private readonly AWSS3ConfigKeys _config;
        private static String accessKey = "YOUR_ACCESS_KEY_ID";
        private static String accessSecret = "YOUR_SECRET_ACCESS_KEY";
        private static String bucket = "YOUR_S3_BUCKET";
        public S3FileService(ILogger<IS3FileService> logger, IAmazonS3 amazonS3Client, TransferUtility transferUtility, IOptions<AWSS3ConfigKeys> config)
        {
            _logger = logger;
            _amazonS3Client = amazonS3Client;
            _transferUtility = transferUtility;
            _config = config.Value;
        }
        public async Task<bool> CreateBucketAsync(string bucketName)
        {
            try
            {
                _logger.LogInformation("Creating Amazon S3 bucket...");
                //var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_amazonS3Client, bucketName);
                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.APSoutheast1);

                var bucketRequest = new PutBucketRequest()
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };

                var response = await client.PutBucketAsync(bucketRequest);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    _logger.LogError("Something went wrong while creating AWS S3 bucket.", response);
                    return false;
                }

                _logger.LogInformation("Amazon S3 bucket created successfully");
                return true;
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError("Something went wrong", ex);
                throw;
            }
        }

        public AwsS3Result<string> GenerateAwsFileUrl(string bucketName, string key, bool useRegion = true)
        {
            // URL patterns: Virtual hosted style and path style
            // Virtual hosted style
            // 1. http://[bucketName].[regionName].amazonaws.com/[key]
            // 2. https://[bucketName].s3.amazonaws.com/[key]

            // Path style: DEPRECATED
            // 3. http://s3.[regionName].amazonaws.com/[bucketName]/[key]
            string publicUrl = string.Empty;
            if (useRegion)
            {
                publicUrl = $"https://{bucketName}.{_config.AwsRegion}.{_config.AwsS3BaseUrl}/{key}";
            }
            else
            {
                publicUrl = $"https://{bucketName}.{_config.AwsS3BaseUrl}/{key}";
            }
            return new AwsS3Result<string>
            {
                Status = true,
                Data = publicUrl
            };
        }

        public async Task<AwsS3Result<string>> UploadImageToS3BucketAsync(AWSS3Model requestDto)
        {
            try
            {
                var file = requestDto.File;
                string bucketName = requestDto.BucketName;

                if (!IsValidImageFile(file))
                {
                    _logger.LogInformation("Invalid file");
                    return new AwsS3Result<string>
                    {
                        Status = false,
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                // Rename file to random string to prevent injection and similar security threats
                var trustedStorageName = RenameFile(requestDto.File);
                var trustedFileName = WebUtility.HtmlEncode(file.FileName);

                // Create the image object to be uploaded in memory
                var transferUtilityRequest=CreateImageObject(trustedFileName, trustedStorageName, file, bucketName, requestDto.Metatags);


                await _transferUtility.UploadAsync(transferUtilityRequest);

                // Retrieve Url
                var ImageUrl = GenerateAwsFileUrl(bucketName, trustedStorageName).Data;

                _logger.LogInformation("File uploaded to Amazon S3 bucket successfully");
                return new AwsS3Result<string>
                {
                    Status = true,
                    Data = ImageUrl
                };
            }
            catch (Exception ex) when (ex is NullReferenceException)
            {
                _logger.LogError("File data not contained in form", ex);
                throw;
            }
            catch (Exception ex) when (ex is AmazonS3Exception)
            {
                _logger.LogError("Something went wrong during file upload", ex);
                throw;
            }
        }

        private TransferUtilityUploadRequest CreateImageObject(string trustedFileName, string trustedStorageName, IFormFile file, string bucketName, Dictionary<string, string> Metactags)
        {
            var transferUtilityRequest = new TransferUtilityUploadRequest()
            {
                InputStream = file.OpenReadStream(),
                Key = trustedStorageName,
                BucketName = bucketName,
                CannedACL = S3CannedACL.PublicRead, // Ensure the file is read-only to allow users view their pictures
                PartSize = 6291456
            };

            // Add metatags which can include the original file name and other decriptions
            var metaTags = Metactags;
            if (metaTags != null && metaTags.Count() > 0)
            {
                foreach (var tag in metaTags)
                {
                    transferUtilityRequest.Metadata.Add(tag.Key, tag.Value);
                }
            }

            transferUtilityRequest.Metadata.Add("originalFileName", trustedFileName);
            return transferUtilityRequest;
        }

        private string RenameFile(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var randomFileName = Path.GetRandomFileName();
            var trustedStorageName = "files/" + randomFileName + ext;
            return trustedStorageName;
        }

        private bool IsValidImageFile(IFormFile file)
        {

            // Check file length
            if (file.Length < 0)
            {
                return false;
            }

            // Check file extension to prevent security threats associated with unknown file types
            string[] permittedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains<string>(ext))
            {
                return false;
            }

            // Check if file size is greater than permitted limit
            if (file.Length > _config.FileSize) // 6MB
            {
                return false;
            }

            return true;
        }
    }
}
