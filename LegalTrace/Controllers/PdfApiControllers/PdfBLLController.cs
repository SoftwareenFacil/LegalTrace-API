﻿using LegalTrace.BLL.Controllers.PdfControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Controllers.ClientHistoryControllers;
using LegalTrace.BLL.Controllers.UserTaskControllers;
using LegalTrace.BLL.Controllers.ClientControllers;
using LegalTrace.BLL.Controllers;

namespace LegalTrace.Controllers.PdfApiControllers
{
    public class PdfBLLController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        private readonly string _logoLoc;
        public PdfBLLController(AppDbContext dbContext, string logoLoc)
        {
            _context = dbContext;
            _responseService = new ResponseService();
            _logoLoc = logoLoc;
        }
        public async Task<IActionResult> GetMonthlyMovementsFromClient(int clientid, DateTime month)
        {
            var historyGet = new GetClientHistoryController(_context);
            var clientHistory = await historyGet.GetClientHistory(0, clientid, month, month.AddMonths(1));
            var TasksGet = new GetUserTasksController(_context);
            var clientTasks = await TasksGet.GetUserTaskBy(null, null, null, clientid, null, null, null, null, month, month.AddMonths(1));
            var chargeGet = new ChargesController(_context, null, null);
            var clientCharges = await chargeGet.GetChargeBy(null, clientid, month, month.AddMonths(1), null, null, null);
            if (clientHistory.Count > 0 || clientTasks.Count() > 0 || clientCharges.Count() > 0)
            {
                var PDFer = new PDFReportsController(_logoLoc);
                var stream = PDFer.drawClientHistoryReport(clientHistory, clientTasks, clientCharges, clientid, month);

                if (!(stream.Length > 0))
                {
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "The Pdf File is not found"));
                }
                return File(stream.ToArray(), "application/pdf", "Reporte_" + DateTime.Now.ToShortDateString() + ".pdf");
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no movements for this client"));
        }
        public async Task<IActionResult> GetClientsWithNoMovementsInMonth(DateTime month)
        {
            var clientsWithNoMovements = new GetClientsController(_context);
            var getter = await clientsWithNoMovements.GetClientsWithNoMovements(month, month.AddMonths(1));
            if (getter.Count > 0)
            {
                var PDFer = new PDFReportsController(_logoLoc);
                var stream = PDFer.drawClientswithNoMovementsReport(getter);

                if (!(stream.Length > 0))
                {
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "The Pdf File is not found"));
                }
                return File(stream.ToArray(), "application/pdf", "Sin_Movimientos_" + DateTime.Now.ToShortDateString() + ".pdf");
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no users with these parameters"));
        }
    }
}
