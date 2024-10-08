﻿using LegalTrace.BLL.Controllers.UserTaskControllers;
using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.UserTaskApiControllers
{
    public class InsertUserTask
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public InsertUserTask(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();

        }
        public async Task<IActionResult> Insert(UserTaskInsertDTO userTask)
        {
            var userTaskCreator = new AddUserTasksController(_context);
            var dataModified = await userTaskCreator.AddUserTask(userTask);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(201, dataModified, "Create Task Successful"));
            
            if(dataModified == -1)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(400, "Error trying to insert an User Task", "Insert rejected"));
            if(dataModified == -2)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(400, "Client or User is Disabled", "Insert rejected"));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to Insert an User Task"));

        }
    }
}
