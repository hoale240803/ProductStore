namespace ProductStore.API.DBFirst.ViewModels.Aws
{
    public class AwsS3Result<T> where T : class
    {
        public bool Status { get; set; } = false;
        public int StatusCode { get; set; } = 200;
        public T Data { get; set; }
    }
}