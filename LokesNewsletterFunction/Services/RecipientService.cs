using Microsoft.EntityFrameworkCore;
using LokesNewsletterFunction.Data;
using LokesNewsletterFunction.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LokesNewsletterFunction.Services
{
    public class RecipientService
    {
        private readonly ApplicationDbContext _dbContext;

        public RecipientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetActiveEmailsAsync()
        {
            var today = DateTime.UtcNow;

            var emails = await _dbContext.Subscriptions
                .Include(s => s.User)
                .Where(s => s.PaymentComplete && s.Expires >= today && s.User != null)
                .Select(s => s.User.Email)
                .Where(email => !string.IsNullOrWhiteSpace(email))
                .Distinct()
                .ToListAsync();

            return emails!;
        }
    }
}
