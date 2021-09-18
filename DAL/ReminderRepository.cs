using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e ReminderRepository by inheriting IReminderRepository
    //ReminderRepository class is used to implement all Data access operations
    public class ReminderRepository:IReminderRepository
    {
        private readonly NewsDbContext context;
        
        public ReminderRepository(NewsDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<Reminder> AddReminder(Reminder reminder)
        {
            context.Reminders.Add(reminder);
            await context.SaveChangesAsync();
            return reminder;
        }

        public async Task<Reminder> GetReminder(int reminderId)
        {
            return await context.Reminders.FirstOrDefaultAsync(x => x.ReminderId == reminderId);
        }

        public async Task<Reminder> GetReminderByNewsId(int newsId)
        {
            return await context.Reminders.FirstOrDefaultAsync(x => x.NewsId == newsId);
        }

        public async Task<bool> RemoveReminder(Reminder reminder)
        {
            context.Reminders.Remove(reminder);
            await context.SaveChangesAsync();
            return true;
        }
        //Implement the methods of interface Asynchronously.
        // Implement AddReminder method which should be used to save a new reminder.    
        // Implement RemoveReminder method which method should be used to delete an existing reminder.
        // Implement GetReminderByNewsId method which should be used to get all reminder by newsId.
        // Implement GetReminder method which should be used to get a reminder by reminderId.
        // Implement UpdateReminder method which should be used to update an existing reminder.
    }
}
