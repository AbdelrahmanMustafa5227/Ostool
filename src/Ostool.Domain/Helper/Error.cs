namespace Ostool.Domain.Helper
{
    

    public class Error
    {
        public static Error Null = new Error("Null Value");

        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; set; } = "An Error Has Ocurred";

    }
}
