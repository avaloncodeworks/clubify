namespace Clubify.Data.Models
{
    public class DashboardSummaryDto
    {
        public string ClubName { get; set; }
        public int TotalEvents { get; set; }
        public int TotalMembers { get; set; }
        public List<EventDto> RecentEvents { get; set; }
        public List<MembershipDto> RecentMemberships { get; set; }
    }

    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class MembershipDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? MembershipType { get; set; }
        public string? Notes { get; set; }
        public string? ContactPhoneNumber { get; set; }
        public DateTime JoinedOn { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class LicenseUsageDto
    {
        public string TierName { get; set; } = string.Empty;
        public int MemberCap { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal AnnualPrice { get; set; }
        public int CurrentMembers { get; set; }
        public int CurrentTierId { get; set; }

        public bool IsAtCap => CurrentMembers >= MemberCap;

        public bool IsNearCap
        {
            get
            {
                if (MemberCap == 0) return false;
                var threshold = (int)(MemberCap * 0.9);
                return CurrentMembers >= threshold && CurrentMembers < MemberCap;
            }
        }

        public bool CanUpgrade => MemberCap < int.MaxValue;
    }

    public class UpgradeLicenseRequest
    {
        public int ClubId { get; set; }
        public int NewTierId { get; set; }
    }

    public class LicenseTierDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MemberCap { get; set; }
        public decimal MonthlyPrice { get; set; }
        public string? Notes { get; set; }
    }
}
