using Clubify.Data;
using Clubify.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Clubify.Services
{
    public interface IMembershipService
    {
        Task<List<MembershipDto>> GetMembershipsAsync(int clubId, string? search = null, string? status = null, string? firstName = null, string? lastName = null);
        Task<MembershipDto?> GetMembershipAsync(int id);
        Task<bool> UpdateMembershipAsync(int id, MembershipDto updatedMember);
        Task<bool> DeleteMembershipAsync(int id);
        Task<bool> ApproveMembershipAsync(int id);
        Task<bool> RejectMembershipAsync(int id);
    }

    public class MembershipService : IMembershipService
    {
        private readonly ApplicationDbContext _context;

        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public MembershipService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<MembershipDto>> GetMembershipsAsync(int clubId, string? search = null, string? status = null, string? firstName = null, string? lastName = null)
        {
            await using var _context = _contextFactory.CreateDbContext();

            var query = _context.Memberships
                .Include(m => m.User)
                .Where(m => m.ClubId == clubId && !m.IsRejected)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(m => m.User.Email.ToLower().Contains(lowerSearch));
            }

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                var lowerFirstName = firstName.ToLower();
                query = query.Where(m => m.User.FirstName.ToLower().Contains(lowerFirstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                var lowerLastName = lastName.ToLower();
                query = query.Where(m => m.User.LastName.ToLower().Contains(lowerLastName));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                switch (status.ToLower())
                {
                    case "approved":
                        query = query.Where(m => m.IsApproved);
                        break;
                    case "pending":
                        query = query.Where(m => !m.IsApproved);
                        break;
                }
            }

            return await query
                .OrderByDescending(m => m.JoinedOn)
                .Select(m => new MembershipDto
                {
                    Id = m.Id,
                    Email = m.User.Email,
                    FirstName = m.User.FirstName,
                    LastName = m.User.LastName,
                    Street = m.User.Street,
                    City = m.User.City,
                    PostalCode = m.User.PostalCode,
                    Country = m.User.Country,
                    DateOfBirth = m.User.DateOfBirth,
                    Gender = m.User.Gender,
                    EmergencyContactName = m.User.EmergencyContactName,
                    EmergencyContactPhone = m.User.EmergencyContactPhone,
                    MembershipType = m.User.MembershipType,
                    Notes = m.User.Notes,
                    ContactPhoneNumber = m.User.ContactPhoneNumber,
                    JoinedOn = m.JoinedOn,
                    IsApproved = m.IsApproved,
                    IsRejected = m.IsRejected,
                    ExpiryDate = m.ExpiryDate,
                    IsActive = m.IsActive
                })
                .ToListAsync();
        }

        public async Task<MembershipDto?> GetMembershipAsync(int id)
        {
            await using var _context = _contextFactory.CreateDbContext();

            var membership = await _context.Memberships
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null) return null;

            return new MembershipDto
            {
                Id = membership.Id,
                Email = membership.User.Email,
                FirstName = membership.User.FirstName,
                LastName = membership.User.LastName,
                Street = membership.User.Street,
                City = membership.User.City,
                PostalCode = membership.User.PostalCode,
                Country = membership.User.Country,
                DateOfBirth = membership.User.DateOfBirth,
                Gender = membership.User.Gender,
                EmergencyContactName = membership.User.EmergencyContactName,
                EmergencyContactPhone = membership.User.EmergencyContactPhone,
                MembershipType = membership.User.MembershipType,
                Notes = membership.User.Notes,
                ContactPhoneNumber = membership.User.ContactPhoneNumber,
                JoinedOn = membership.JoinedOn,
                IsApproved = membership.IsApproved,
                IsRejected = membership.IsRejected,
                ExpiryDate = membership.ExpiryDate,
                IsActive = membership.IsActive
            };
        }

        public async Task<bool> UpdateMembershipAsync(int id, MembershipDto updatedMember)
        {
            await using var _context = _contextFactory.CreateDbContext();

            var membership = await _context.Memberships
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null) return false;

            membership.ExpiryDate = updatedMember.ExpiryDate;
            membership.IsActive = updatedMember.IsActive;
            membership.User.FirstName = updatedMember.FirstName;
            membership.User.LastName = updatedMember.LastName;
            membership.User.Street = updatedMember.Street;
            membership.User.City = updatedMember.City;
            membership.User.PostalCode = updatedMember.PostalCode;
            membership.User.Country = updatedMember.Country;
            membership.User.DateOfBirth = updatedMember.DateOfBirth;
            membership.User.Gender = updatedMember.Gender;
            membership.User.EmergencyContactName = updatedMember.EmergencyContactName;
            membership.User.EmergencyContactPhone = updatedMember.EmergencyContactPhone;
            membership.User.MembershipType = updatedMember.MembershipType;
            membership.User.Notes = updatedMember.Notes;
            membership.User.ContactPhoneNumber = updatedMember.ContactPhoneNumber;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMembershipAsync(int id)
        {
            await using var _context = _contextFactory.CreateDbContext();

            var membership = await _context.Memberships.FindAsync(id);
            if (membership == null) return false;

            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveMembershipAsync(int id)
        {
            await using var _context = _contextFactory.CreateDbContext();

            var membership = await _context.Memberships.FindAsync(id);
            if (membership == null) return false;

            membership.IsApproved = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectMembershipAsync(int id)
        {
            await using var _context = _contextFactory.CreateDbContext();

            var membership = await _context.Memberships.FindAsync(id);
            if (membership == null) return false;

            membership.IsRejected = true;
            membership.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}