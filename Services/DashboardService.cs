using Clubify.Data;
using Clubify.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Clubify.Services
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync(int clubId);
    }

    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(int clubId)
        {
            var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == clubId);
            if (club == null)
            {
                throw new InvalidOperationException("Club not found.");
            }

            var totalEvents = await _context.Events
                .CountAsync(e => e.ClubId == clubId);

            var totalMembers = await _context.Memberships
                .CountAsync(m => m.ClubId == clubId);

            var recentEvents = await _context.Events
                .Where(e => e.ClubId == clubId)
                .OrderByDescending(e => e.StartDate)
                .Take(5)
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartDate = e.StartDate
                })
                .ToListAsync();

            var recentMemberships = await _context.Memberships
                .Include(m => m.User)
                .Where(m => m.ClubId == clubId)
                .OrderByDescending(m => m.JoinedOn)
                .Take(5)
                .Select(m => new MembershipDto
                {
                    Id = m.Id,
                    Email = m.User.Email,
                    JoinedOn = m.JoinedOn,
                    IsApproved = m.IsApproved,
                    IsRejected = m.IsRejected
                })
                .ToListAsync();

            return new DashboardSummaryDto
            {
                ClubName = club.Name,
                TotalEvents = totalEvents,
                TotalMembers = totalMembers,
                RecentEvents = recentEvents,
                RecentMemberships = recentMemberships
            };
        }
    }
}
