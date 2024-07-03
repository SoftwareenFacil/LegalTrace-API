﻿using DinkToPdf.Contracts;
using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.BLL.Controllers.PdfControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Controllers.ClientHistoryControllers;
using LegalTrace.PDF.Controllers;
using LegalTrace.Controllers.UserTaskApiControllers;
using LegalTrace.DAL.Controllers.ChargeControllers;
using LegalTrace.BLL.Controllers.ChargeControllers;
using LegalTrace.BLL.Controllers.UserTaskControllers;
using LegalTrace.DAL.Models;
using LegalTrace.BLL.Controllers.ClientControllers;

namespace LegalTrace.Controllers.PdfApiControllers
{
    public class PdfBLLController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        private readonly string _TempFolder;
        public PdfBLLController(AppDbContext dbContext, string tempFolder)
        {
            _context = dbContext;
            _responseService = new ResponseService();
            _TempFolder = tempFolder;
        }
        public async Task<IActionResult> GetMonthlyMovementsFromClient(int clientid, DateTime month)
        {
            var historyGet = new GetClientHistoryController(_context);
            var clientHistory = await historyGet.GetClientHistory(0, clientid, month, month.AddMonths(1));
            var TasksGet = new GetUserTasksController(_context);
            var clientTasks = await TasksGet.GetUserTaskBy(null, null, null, clientid, null, null, null, null, month, month.AddMonths(1));
            var chargeGet = new GetChargesController(_context);
            var clientCharges = await chargeGet.GetChargeBy(null, clientid, month, month.AddMonths(1), null, null, null);
            if (clientHistory.Count > 0 || clientTasks.Count() > 0 || clientCharges.Count() > 0)
            {
                var PDFer = new PDFReportsController();
                var fileLocation = PDFer.drawClientHistoryReport(clientHistory, clientTasks, clientCharges, clientid, month, _TempFolder);

                if (!System.IO.File.Exists(fileLocation))
                {
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "The Pdf File is not found"));
                }
                var fileBytes = await System.IO.File.ReadAllBytesAsync(fileLocation);
                return File(fileBytes, "application/pdf", "Reporte_" + DateTime.Now.ToShortDateString() + ".pdf");
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no movements for this client"));
        }
        public async Task<IActionResult> GetClientsWithNoMovementsInMonth(DateTime month)
        {
            var clientsWithNoMovements = new GetClientsController(_context);
            var getter = await clientsWithNoMovements.GetClientsWithNoMovements(month, month.AddMonths(1));
            if (getter.Count > 0)
            {
                var PDFer = new PDFReportsController();
                var fileLocation = PDFer.drawClientswithNoMovementsReport(getter, _TempFolder);
                
                if (!System.IO.File.Exists(fileLocation))
                {
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "The Pdf File is not found"));
                }
                var fileBytes = await System.IO.File.ReadAllBytesAsync(fileLocation);
                return File(fileBytes, "application/pdf", "Sin_Movimientos_" + DateTime.Now.ToShortDateString() + ".pdf");
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no users with these parameters"));
        }
    }
}
