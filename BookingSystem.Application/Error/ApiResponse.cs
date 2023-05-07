namespace BookingSystem.Application.Error
{
    public class ApiResponse
    {
        public int StatueCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statueCode, string message = null)
        {
            StatueCode = statueCode;
            Message = string.IsNullOrEmpty(message) ? GetDefaultMessageForStatusCode(statueCode) : message;
        }
        private string GetDefaultMessageForStatusCode(int statusCode)
            => statusCode switch
            {
                200 => "success",
                400 => "bad request, you have made",
                401 => "you are not authorized",
                404 => "resources not found",
                500 => "server error",
                _ => "un handled error status code",
            };
    }
}
