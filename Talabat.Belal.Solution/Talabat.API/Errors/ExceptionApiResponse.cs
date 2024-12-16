namespace Talabat.API.Errors
{
    public class ExceptionApiResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ExceptionApiResponse(int statusCode , string message = null , string details = null ) 
            : base( statusCode , message )
        {
            Details = details;   
        }
    }
}
