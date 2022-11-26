namespace ExceptionHandler.DTO.ExceptionDetail
{
    public class ValidationExceptionResult:ExceptionResult
    {
        public ValidationExceptionResult(string type, string description, string[] errors) : base(type,description)
        {
            Errors = errors;
        }

        public string[] Errors { get; set; }
    }
}
