
namespace Talabat.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode , string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            //string message = string.Empty;
            //switch (statusCode)
            //{
            //    case 401:
            //        message = "gergg";
            //        break;
            //    default:
            //        message = null;
            //        break;
            //}


            // switch expression
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are Not",
                404 => "Resource was not found",
                500 => "Errors are the path to the dark side. Errors Lead to anger. Anger lead to hate. Hate lead to career shift",
                _   => null
            };
        }
    }
}
