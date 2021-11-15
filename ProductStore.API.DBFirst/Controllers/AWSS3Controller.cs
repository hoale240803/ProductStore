using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductStore.API.DBFirst.Services.AwsService.S3Service;
using ProductStore.API.DBFirst.ViewModels;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AWSS3Controller:ControllerBase
    {
        private readonly IS3FileService _uploadService;

        public AWSS3Controller(IS3FileService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AWSS3Model requestDto)
        {
            var result = await _uploadService.UploadImageToS3BucketAsync(requestDto);
            return StatusCode(result.StatusCode);
        }

        [HttpPost("s3/bucket")]
        public async Task<IActionResult> CreateS3BucketAsync(string bucketName)
        {
            await _uploadService.CreateBucketAsync(bucketName);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}