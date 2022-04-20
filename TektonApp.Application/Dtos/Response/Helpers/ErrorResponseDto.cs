using TektonApp.Common;

namespace TektonApp.Application.Dtos.Response.Helpers
{
    public class ErrorResponseDto : BaseDto
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
