using LokesNewsletterFunction.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LokesNewsletterFunction.Functions
{
    public class NewsletterFunction
    {
        private readonly RecipientService _recipientService;
        private readonly EmailService _emailService;
        private readonly ILogger<NewsletterFunction> _logger;

        public NewsletterFunction(RecipientService recipientService, EmailService emailService, ILogger<NewsletterFunction> logger)
        {
            _recipientService = recipientService;
            _emailService = emailService;
            _logger = logger;
        }

        [Function("SendNewsletter")]
        //public async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo timerInfo)
        public async Task RunAsync([TimerTrigger("0 0 0 * * 1")] TimerInfo timerInfo)
        {
            _logger.LogInformation($"Newsletter function triggered at: {System.DateTime.UtcNow}");

            var emails = await _recipientService.GetActiveEmailsAsync();

            foreach (var email in emails)
            {
                await _emailService.SendEmailAsync(email, "Your Newsletter", "<h1>Hello!</h1><p>This is your newsletter.</p>");
            }
        }
    }
}
