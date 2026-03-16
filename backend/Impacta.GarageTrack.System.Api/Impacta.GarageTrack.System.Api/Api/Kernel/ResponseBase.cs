namespace Impacta.GarageTrack.System.Api.Api.Kernel
{
    public class ResponseBase<T>
    {
        public ResponseBase(T data)
        {
            Data = data;
            Success = true;
        }

        public ResponseBase(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }

        public ResponseBase(string errorMessage, IEnumerable<string> validationErrors)
        {
            Success = false;
            ErrorMessage = errorMessage;
            ValidationErrors = validationErrors;
        }

        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public IEnumerable<string>? ValidationErrors { get; set; }
    }
}
