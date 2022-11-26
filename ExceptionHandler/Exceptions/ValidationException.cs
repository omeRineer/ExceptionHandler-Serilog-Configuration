namespace ExceptionHandler.Exceptions
{
    public class ValidationException:Exception
    {
        public string[] Errors { get; set; }
    }
}
