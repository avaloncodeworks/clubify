namespace Clubify.Services
{
    public static class EmailTemplates
    {
        public static string WelcomeEmail(string userName) =>
            $"Welcome to Clubify, {userName}!\n\n" +
            "Thank you for joining our club community. We’re excited to have you on board.\n\n" +
            "Cheers,\nThe Clubify Team";

        public static string EventReminder(string eventName, DateTime eventDate) =>
            $"Hello!\n\n" +
            $"This is a reminder for the upcoming event: {eventName} happening on {eventDate:MMMM dd, yyyy}.\n\n" +
            "We look forward to seeing you there!\n\n" +
            "Cheers,\nThe Clubify Team";

        public static string MembershipRenewalReminder(string userName, DateTime renewalDate) =>
            $"Hello {userName},\n\n" +
            $"Just a quick reminder that your membership is due for renewal on {renewalDate:MMMM dd, yyyy}.\n\n" +
            "Please log in to your account to renew and continue enjoying Clubify benefits.\n\n" +
            "Thank you,\nThe Clubify Team";
    }
}
