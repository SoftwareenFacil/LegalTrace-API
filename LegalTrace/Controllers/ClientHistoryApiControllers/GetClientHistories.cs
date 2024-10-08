﻿using LegalTrace.BLL.Controllers.ClientHistoryControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ClientHistoryApiControllers
{
    public class GetClientHistories
    {
        private AppDbContext _context;
        private readonly ResponseService _responseService;
        public GetClientHistories(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> GetBy(int id, DateTime? createdFrom, DateTime? createdTo)
        {
            var clientHistoryGetter = new GetClientHistoryController(_context);
            var clientHistories = await clientHistoryGetter.GetClientHistory(id, 0, createdFrom, createdTo);
            if (clientHistories.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200,clientHistories, "Success when searching for client histories"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no client histories with these parameters"));
        }
    }
}
