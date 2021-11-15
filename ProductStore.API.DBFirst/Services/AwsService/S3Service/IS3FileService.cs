using ProductStore.API.DBFirst.ViewModels;
using ProductStore.API.DBFirst.ViewModels.Aws;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.AwsService.S3Service
{
    public interface IS3FileService
    {
        Task<bool> CreateBucketAsync(string bucketName);

        AwsS3Result<string> GenerateAwsFileUrl(string bucketName, string key, bool useRegion = true);

        Task<AwsS3Result<string>> UploadImageToS3BucketAsync(AWSS3Model requestDto);
    }
}