namespace SWCE.Web1.Models.Base
{
    public class ModelResponse<T> where T : class
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public T data { get; set; }

        public static ModelResponse<T> Success(T data, string message = "")
        {
            return new ModelResponse<T> { isSuccess = true, data = data, message = message };
        }

        public static ModelResponse<T> Failure(string message)
        {
            return new ModelResponse<T> { isSuccess = false, message = message };
        }
    }
}
