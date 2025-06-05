using Clubify.Data;
using Clubify.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Clubify.Services
{
    public interface ILicenseService
    {
        Task<List<LicenseTierDto>> GetLicenseTiersAsync();
    }

    public class LicenseService : ILicenseService
    {
        private readonly ApplicationDbContext _context;

        public LicenseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LicenseTierDto>> GetLicenseTiersAsync()
        {
            return await _context.LicenseTiers
                .OrderBy(t => t.MemberCap)
                .Select(t => new LicenseTierDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    MemberCap = t.MemberCap,
                    MonthlyPrice = t.MonthlyPrice,
                    Notes = t.Notes
                })
                .ToListAsync();
        }
    }
}