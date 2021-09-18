using DAL;
using Entities;
using Service.Exceptions;
using System.Threading.Tasks;
namespace Service
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e UserService by inheriting IUserService
    public class UserService:IUserService
    {
        /*
      * UserRepository should  be injected through constructor injection. 
      * Please note that we should not create USerRepository object using the new keyword
      */
        IUserRepository _userRepo;
        public UserService(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public async Task<bool> AddUser(UserProfile user)
        {
            bool flag = await _userRepo.AddUser(user);
            if (flag)
                return true;
            else
                throw new UserAlreadyExistsException($"{user.UserId} already exists");
        }

        public async Task<bool> DeleteUser(string userId)
        {
            UserProfile user = await _userRepo.GetUser(userId);
            if (user != null)
            {
                bool flag = await _userRepo.DeleteUser(user);
                return flag;
            }
            else
                throw new UserNotFoundException($"{userId} doesn't exist");
        }

        public async Task<UserProfile> GetUser(string userId)
        {
            UserProfile user = await _userRepo.GetUser(userId);
            if (user != null)
            {
                return user;
            }
            else
                throw new UserNotFoundException($"{userId} doesn't exist");
        }

        public async Task<bool> UpdateUser(string userId, UserProfile user)
        {
            UserProfile exist = await _userRepo.GetUser(userId);
            if (exist != null)
            {
                bool flag = await _userRepo.UpdateUser(user);
                return flag;
            }
            else
                throw new UserNotFoundException($"{userId} doesn't exist");
        }
        // UserService class is used to implement all the functionalities declared in the interface

        /* Implement all the methods of respective interface asynchronously*/

        // Implement AddUser method which should be used to save a new user.   
        // Implement DeleteUser method which should be used to delete an existing user.
        // Implement GetUser method which should be used to get a userprofile complete detail by userId.
        // Implement UpdateUser method which should be used to update an existing user.
        // Throw your own custom Exception whereever its required in AddUser,DeleteUser,GetUser and UpdateUser 
        // functionalities
    }
}
