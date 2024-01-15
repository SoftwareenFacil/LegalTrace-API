using Microsoft.AspNetCore.Mvc;
using LegalTrace.Models;

namespace LegalTrace.Controllers.Services
{
    public class ResponseService
    {
        public IActionResult CreateResponse<T>(ApiResponse<T> apiResponse)
        {
            if (apiResponse.Success)
                return new OkObjectResult(apiResponse);
            else if (apiResponse.Data != null && !string.IsNullOrEmpty(apiResponse.Message))
                return new BadRequestObjectResult(apiResponse);
            else if (apiResponse.Data == null)
                return new NotFoundObjectResult(apiResponse);
            return new ObjectResult(apiResponse) { StatusCode = 500 };

        }
    }
}
