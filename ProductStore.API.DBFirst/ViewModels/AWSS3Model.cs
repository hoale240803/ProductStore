using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.API.DBFirst.ViewModels
{
    public class AWSS3Model
    {
        [Required]
        public string BucketName { get; set; }

        [Required]
        public IFormFile File { get; set; }

        public Dictionary<string, string> Metatags { get; set; }
    }
}