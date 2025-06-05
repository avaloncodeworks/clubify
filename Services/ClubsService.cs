using Clubify.Data;
using Clubify.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Clubify.Services
{
    public interface IClubsService
    {
        Task<LicenseUsageDto?> GetLicenseUsageAsync(int clubId);
        Task<bool> UpgradeLicenseAsync(int clubId, int newTierId);
    }

    public class ClubsService : IClubsService
    {
        private readonly ApplicationDbContext _context;

        public ClubsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LicenseUsageDto?> GetLicenseUsageAsync(int clubId)
        {
            var club = await _context.Clubs
                .Include(c => c.LicenseTier)
                .FirstOrDefaultAsync(c => c.Id == clubId);

            if (club == null || club.LicenseTier == null)
                return null;

            var approvedMemberCount = await _context.Memberships
                .CountAsync(m => m.ClubId == clubId && m.IsApproved && !m.IsRejected);

            var tier = club.LicenseTier;

            return new LicenseUsageDto
            {
                TierName = tier.Name,
                MemberCap = tier.MemberCap,
                MonthlyPrice = tier.MonthlyPrice,
                AnnualPrice = tier.AnnualPrice,
                CurrentMembers = approvedMemberCount,
                CurrentTierId = tier.Id
            };
        }

        public async Task<bool> UpgradeLicenseAsync(int clubId, int newTierId)
        {
            var club = await _context.Clubs
                .Include(c => c.LicenseTier)
                .FirstOrDefaultAsync(c => c.Id == clubId);

            if (club == null)
                return false;

            var newTier = await _context.LicenseTiers.FindAsync(newTierId);
            if (newTier == null)
                return false;

            club.LicenseTierId = newTier.Id;
            club.LicenseTier = newTier;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}