using Clubify.Services;

public class NullEmailService : IEmailService
{
    public Task SendEmailAsync(string toEmail, string subject, string body)
    {
        Console.WriteLine("========== EMAIL SIMULATION ==========");
        Console.WriteLine($"To: {toEmail}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body:\n{body}");
        Console.WriteLine("======================================");
        return Task.CompletedTask;
    }
}
