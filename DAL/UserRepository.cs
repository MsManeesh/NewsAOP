using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace DAL
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e UserRepository by inheriting IUserRepository

    // UserRepository class is used to implement all Data access operations
    public class UserRepository:IUserRepository
    {
        private readonly NewsDbContext context;
        public UserRepository(NewsDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<bool> AddUser(UserProfile user)
        {
            UserProfile exist = await context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            if (exist == null)
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteUser(UserProfile user)
        {
            UserProfile exist = await context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            if (exist != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<UserProfile> GetUser(string userId)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

        }

        public async Task<bool> UpdateUser(UserProfile user)
        {
            UserProfile exist = await context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            if (exist != null)
            {
                foreach (UserProfile x in context.Users.ToList())
                {
                    if (x.UserId == user.UserId)
                    {
                        x.FirstName = user.FirstName;
                        x.LastName = user.LastName;
                        x.Contact = user.Contact;
                        x.CreatedAt = user.CreatedAt;
                        x.Email = user.Email;
                        await context.SaveChangesAsync();
                        break;
                    }
                }

                return true;
            }
            else
                return false;
        }
        // Implement AddUser method which should be used to save a new user.
        // Implement DeleteUser method which should be used to delete an existing user.
        // Implement GetUser method which should be used to get a userprofile complete detail by userId.
        // Implement UpdateUser method which should be used to update an existing user.
    }
}
