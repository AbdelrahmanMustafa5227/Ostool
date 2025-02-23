using System.Net;

namespace Ostool.Application.Helpers
{


    public class Error
    {
        public static Error Null = new Error("Null Value", HttpStatusCode.BadRequest);
        public static Error NotFound = new Error("Could not find resource with specified Id", HttpStatusCode.NotFound , "Resourse Not Found");

        public Error(string message = "An Error Has Ocurred",
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            string title = "Fatal Error")
        {
            Message = message;
            StatusCode = statusCode;
            Title = title;
        }

        public string Message { get; set; }
        public string Title { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}