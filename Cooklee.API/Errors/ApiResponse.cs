namespace Cooklee.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request.",
                401 => "Unauthorized.",
                404 => "Resources not Found.",
                405 => "Method not Allowed.",
                415 => "Unsupported Media.",
                500 => "The server encountered an unexpected condition that prevented it from fulfilling the request.",
                _ => null
            };
        }
    }
}
