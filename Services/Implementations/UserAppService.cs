using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTestProject.Models;
using UserTestProject.Persistence;

namespace UserTestProject.Services.Implementations
{
    public class UserAppService : IUserAppService
    {
        private IGenericRepository<User> _userRepository = null;
        private IEmailService _emailService = null;
        private static string _emailBody = @"
             Hi {0}, \n\n
             Welcome to our site. Thank you for joining.\n\n
             Sincerely, \n
             Admin
                    ";

        private static string _emailSubject = "Welcome to our Angular Site";
        private static string _emailBodyHtml = @"
             Hi {0}, <br />

            <p>
             Welcome to our site. Thank you for joining.
            </p>

             Sincerely, <br />
             Admin
                    ";

        public UserAppService(IGenericRepository<User> userRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<User> AddUser(User user)
        {
            var users = await _userRepository.Get(filter: a => a.UserName.ToLower().Equals(user.UserName.ToLower()));
            if ( users.Any() )
            {
                return user;
            }
            else
            {
                _userRepository.Insert(user);
                var result = await _userRepository.Save();

                // send an email
                if (result > 0)
                {
                    var emailResult = await _emailService.SendEmail(user.UserName
                        , string.Format(_emailBody, user.FirstName)
                        , _emailSubject
                        , string.Format(_emailBodyHtml, user.FirstName));
                }

                return user;
            }            
        }

        public async Task<int> DeleteUser(int id)
        {
            _userRepository.Delete(id);
            return await _userRepository.Save();
        }

        public async Task<User> EditUser(int id, User user)
        {
            var entity = await _userRepository.GetById(id);
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.UserName = user.UserName;
            entity.Password = user.Password;

            _userRepository.Update(entity);
            await _userRepository.Save();

            return entity;
        }

        public async Task<User> GetUser(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> ValidateLogin(string username, string password)
        {
            var user = await _userRepository.Get(filter: a => a.UserName.ToLower().Equals(username.ToLower()) 
                                                 && a.Password.Equals(password));

            return user?.FirstOrDefault();
        }
    }
}
