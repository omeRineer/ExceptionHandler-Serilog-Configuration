
using Newtonsoft.Json;
using System.Text.Json;

namespace ExceptionHandler.DTO.ExceptionDetail
{
    public class ExceptionResult
    {
        public ExceptionResult(string type, string description)
        {
            Description = description;
            Type = type;
        }

        public string Type { get; set; }
        public string Description { get; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
