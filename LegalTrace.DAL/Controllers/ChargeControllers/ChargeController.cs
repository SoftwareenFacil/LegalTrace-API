using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LegalTrace.DAL.Controllers.ChargeControllers
{
    public class ChargeController : IChargeController
    {
        private AppDbContext _context;
        public ChargeController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<Charge> GetChargeById(int id)
        {
            var response = await _context.Charges.Where(chargeAux => chargeAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }

        public async Task<List<Charge>> GetChargeBy(int? id, int? clientId, DateTime? date, DateTime? dateTo, string? title, int? amount, int? type)
        {
            if (id.HasValue)
            {
                if (id.Value == 0)
                {
                    return await _context.Charges.Take(100).ToListAsync();
                }
                else
                {
                    var charge = await _context.Charges.FirstOrDefaultAsync(u => u.Id == id.Value);
                    return charge == null ? new List<Charge>() : new List<Charge> { charge };
                }
            }

            var query = _context.Charges.AsQueryable();

            if (clientId.HasValue)
                query = query.Where(charge => charge.ClientId == clientId.Value);

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(u => EF.Functions.Like(u.Title, $"%{title}%"));

            if (date.HasValue)
            {
                var dateOnlyStart = date.Value.Date;
                query = query.Where(charge => charge.Created >= DateTime.SpecifyKind(dateOnlyStart, DateTimeKind.Utc));
            }
            if (dateTo.HasValue)
            {
                query = query.Where(charge => charge.Created < DateTime.SpecifyKind(dateTo.Value, DateTimeKind.Utc));
            }

            if (amount.HasValue)
            {
                var lowerBound = amount.Value - 10; 
                var upperBound = amount.Value + 10;
                query = query.Where(charge => charge.Amount >= lowerBound && charge.Amount <= upperBound);
            }
            if (type.HasValue)
                query = query.Where(charge => charge.ChargeType == (ChargeType)type);


            return await query.Take(100).ToListAsync();
        }

        public async Task<bool> DeleteCharge(int id)
        {
            var charge = await _context.Charges.Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();
            if (charge != null)
            {
                _context.Charges.Remove(charge);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<int> InsertCharge(Charge charge)
        {
            await _context.Charges.AddAsync(charge);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCharge(Charge charge)
        {
            var response = await _context.Charges.Where(u => u.Id.Equals(charge.Id)).FirstOrDefaultAsync();
            if (response != null)
            {
                response.ClientId = charge.ClientId;
                response.Title = charge.Title;
                response.Description = charge.Description;
                response.Created = charge.Created;
                response.Amount = charge.Amount;
                response.FileLink = charge.FileLink;
                response.Updated = charge.Updated;

                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }
    }
}
