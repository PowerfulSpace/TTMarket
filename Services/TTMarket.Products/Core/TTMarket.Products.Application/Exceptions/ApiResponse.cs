using System.Text.Json;

namespace TTMarket.Products.Application.Exceptions
{
    public class ApiResponse
    {
        readonly int _statusCode;
        readonly string _message;

        /// <summary>
        /// Create a response message for an exception
        /// </summary>
        /// <param name="statusCode">Status code of exception</param>
        /// <param name="message">Message of exception</param>
        public ApiResponse(int statusCode, string message = null)
        {
            _statusCode = statusCode;
            _message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        /// <summary>
        /// Status code of exception
        /// </summary>
        /// <value>Integer</value>
        public int StatusCode { get => _statusCode; }
        /// <summary>
        /// Message of exception
        /// </summary>
        /// <value>String</value>
        public string Message { get => _message; }

        private string GetDefaultMessageForStatusCode(int statusCode)
            => statusCode switch
            {
                400 => "You have made bad request.",
                401 => "You're not Authorized.",
                404 => "Resource was not found.",
                500 => "Something go wrong please tell about it your software developer.",
                _ => null
            };

        /// <summary>
        /// Represent an exception in json format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => JsonSerializer.Serialize(this);
    }
}