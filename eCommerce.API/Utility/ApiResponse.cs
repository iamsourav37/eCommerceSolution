namespace eCommerce.API.Utility
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; } = false;
        public int StatusCode { get; set; } = 500;
        public object Data { get; set; } = null;
        public string[] Errors { get; set; } = ["Internal Server Error"];

        public void SetResponse(bool isSuccess, int statusCode, object data, string[] errors)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Data = data;
            Errors = errors;
        }
    }
}
