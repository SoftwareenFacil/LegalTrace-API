﻿using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.UserTaskApiControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserTaskApiController : ControllerBase
    {
        private AppDbContext _context;
        public UserTaskApiController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTasksBy(int? id,string? title, int? userId, int? clientId, DateTime? dueDate, DateTime? createdFrom, DateTime? createdTo, bool? repeatable, bool? vigency, bool? finished)
        {
            var userTaskGetter = new GetUserTasks(_context);
            return await userTaskGetter.GetUserTaskBy(id, title, userId, clientId, dueDate, repeatable, vigency, finished, createdFrom, createdTo);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUserTask([FromBody] UserTaskInsertDTO userTask)
        {
            var add = new InsertUserTask(_context);
            return await add.Insert(userTask);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserTask([FromBody] UserTaskEditDTO userEdited)
        {
            var updater = new UpdateUserTask(_context);
            return await updater.Update(userEdited);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserTaskVigency(int id)
        {
            var updater = new UpdateUserTask(_context);
            return await updater.UpdateVigency(id);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserTask(int id)
        {
            var deleter = new DeleteUserTask(_context);
            return await deleter.Delete(id);
        }
        [HttpPost]
        public async Task<IActionResult> CheckRepetitiveTasks()
        {
            var checker = new CheckRepetitiveTasks(_context);
            return await checker.Check();
        }
    }
    
}
