using TektonApp.Common;

namespace TektonApp.Application.Dtos.Response.Helpers
{
    public class ServiceResultDto
    {
        public int StatusCode { get; set; }
        public BaseDto Result { get; set; }
    }
}
