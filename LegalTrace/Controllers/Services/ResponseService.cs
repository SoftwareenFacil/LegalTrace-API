using Microsoft.AspNetCore.Mvc;
using LegalTrace.Models;

namespace LegalTrace.Controllers.Services
{
    public class ResponseService
    {
        public IActionResult CreateResponse<T>(ApiResponse<T> apiResponse)
        {
            if (apiResponse.Code == 200)
                return new OkObjectResult(apiResponse);
            else if (apiResponse.Code == 201)
                return new CreatedAtRouteResult(null, apiResponse);
            else if (apiResponse.Code == 400)
                return new BadRequestObjectResult(apiResponse);
            else if (apiResponse.Code == 404)
                return new NotFoundObjectResult(apiResponse);
            else
                return new ObjectResult(apiResponse) { StatusCode = 500 };
        }
    }
}
