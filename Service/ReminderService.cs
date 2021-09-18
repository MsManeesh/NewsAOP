using DAL;
using Entities;
using Service.Exceptions;
using System.Threading.Tasks;

namespace Service
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e ReminderService by inheriting IReminderService
    public class ReminderService:IReminderService
    {
        /*
       * ReminderRepository should  be injected through constructor injection. 
       * Please note that we should not create ReminderRepository object using the new keyword
       */
        IReminderRepository _reminderRepo;
        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepo = reminderRepository;
        }

        
        /* Implement all the methods of respective interface asynchronously*/

        // Implement AddReminder method which should be used to save a new reminder.    
        public async Task<Reminder> AddReminder(Reminder reminder)
        {
            Reminder exist = await _reminderRepo.GetReminderByNewsId(reminder.NewsId);
            if (exist == null)
            {
                return await _reminderRepo.AddReminder(reminder);
            }
            else
                throw new ReminderAlreadyExistsException($"This news: {reminder.NewsId} already have a reminder");
        }
        // Implement GetReminderByNewsId method which should be used to get all reminder by newsId.
        public async Task<Reminder> GetReminderByNewsId(int newsId)
        {
            Reminder reminder = await _reminderRepo.GetReminderByNewsId(newsId);
            if (reminder != null)
            {
                return reminder;
            }
            else
                throw new ReminderNotFoundException($"No reminder found for news: {newsId}");
        }
        // Implement RemoveReminder method which method should be used to delete an existing reminder withits Id
        public async Task<bool> RemoveReminder(int reminderId)
        {
            Reminder reminder = await _reminderRepo.GetReminder(reminderId);
            if (reminder != null)
            {
                return await _reminderRepo.RemoveReminder(reminder);

            }
            else
                throw new ReminderNotFoundException($"No reminder found with id: {reminderId}");
        }
        // Throw your own custom Exception whereever its required in AddReminder,GetReminderByNewsId and RemoveReminder 
        // functionalities
    }
}
