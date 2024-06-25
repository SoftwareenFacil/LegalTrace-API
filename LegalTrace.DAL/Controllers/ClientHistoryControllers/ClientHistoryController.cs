using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;


namespace LegalTrace.DAL.Controllers.ClientHistoryControllers
{
    public class ClientHistoryController : IClientHistoryController
    {
        private AppDbContext _context;
        public ClientHistoryController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }


        public async Task<ClientHistory?> GetClientHistoryById(int id)
        {
            var history = await _context.ClientHistory.FirstOrDefaultAsync(u => u.Id == id);
            return history;
        }

        public async Task<List<ClientHistory>> GetClientHistoryBy(int id,int clientId, DateTime? createdFrom, DateTime? createdTo)
        {
            if (id != 0)
            {
                var history = await _context.ClientHistory.FirstOrDefaultAsync(u => u.Id == id);
                return history == null ? new List<ClientHistory>() : new List<ClientHistory> { history };
                
            }
            else if(clientId != 0 && createdFrom != null && createdTo != null)
            {
                var history = await _context.ClientHistory.Where(x => x.ClientId == clientId
                && x.Created >= DateTime.SpecifyKind(createdFrom.Value, DateTimeKind.Utc) 
                && x.Created <= DateTime.SpecifyKind(createdTo.Value, DateTimeKind.Utc))
                    .Take(100).ToListAsync();
                return history;
            }
            else
            {
                var query = _context.ClientHistory.AsQueryable();

                if (createdFrom.HasValue)
                    query = query.Where(u => u.Created >= DateTime.SpecifyKind(createdFrom.Value, DateTimeKind.Utc))
                                 .OrderBy(u => u.Created);
                if (createdTo.HasValue)
                    query = query.Where(u => u.Created <= DateTime.SpecifyKind(createdTo.Value, DateTimeKind.Utc))
                                 .OrderBy(u => u.Created);

                return await query.Take(100).ToListAsync();

            }

        }

        public async Task<int> InsertClientHistory(ClientHistory history)
        {
            await _context.ClientHistory.AddAsync(history);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateClientHistory(ClientHistory updatedHistory)
        {
            var history = await _context.ClientHistory.FirstOrDefaultAsync(h => h.Id == updatedHistory.Id);
            if (history != null)
            {
                history.Title = updatedHistory.Title;
                history.Description = updatedHistory.Description;
                history.EventDate = updatedHistory.EventDate;
                history.Updated = updatedHistory.Updated;

                if (await _context.SaveChangesAsync() > 0)
                    return true;
            }
            return false;
        }

        public async Task<bool> DeleteClientHistory(int id)
        {
            var history = await _context.ClientHistory.FirstOrDefaultAsync(h => h.Id == id);
            if (history != null)
            {
                _context.ClientHistory.Remove(history);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
