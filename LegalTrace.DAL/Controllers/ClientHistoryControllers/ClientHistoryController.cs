using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;


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

        public async Task<List<ClientHistory>> GetClientHistoryBy(int id)
        {
            if (id == 0)
            {
                return await _context.ClientHistory.Take(100).ToListAsync();
            }
            else
            {
                var history = await _context.ClientHistory.FirstOrDefaultAsync(u => u.Id == id);
                return history == null ? new List<ClientHistory>() : new List<ClientHistory> { history };
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
