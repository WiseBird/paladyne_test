using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using Paladyne.Angularjs.BL.Includes;
using Paladyne.Angularjs.BL.Models;
using Paladyne.Angularjs.DAL;
using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.BL.Services
{
    public interface IUserService
    {
        User GetById(string id);
        User GetByIdEx(string id, Include<User> include);
        User GetByName(string name);
        User GetByNameEx(string name, Include<User> include);
        User GetByNameAndPassword(string name, string password);
        User GetByNameAndPasswordEx(string name, string password, Include<User> include);

        bool ExistsByName(string name);

        List<User> GetAll(Include<User> include);

        void Create(CreateUser model, IValidationErrors errors);
        void Update(UpdateUserData model, IValidationErrors errors);
    }

    public class UserService : BaseService, IUserService
    {
        public UserManager<User> UserManager { get; set; }

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager)
            : base(unitOfWork)
        {
            UserManager = userManager;
        }

        public User GetById(string id)
        {
            return GetByIdEx(id, new Include<User>());
        }
        public User GetByIdEx(string id, Include<User> include)
        {
            return UnitOfWork.Users.Include(include).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
        public User GetByName(string name)
        {
            return GetByNameEx(name, new Include<User>());
        }
        public User GetByNameEx(string name, Include<User> include)
        {
            return UnitOfWork.Users.Include(include).AsNoTracking().FirstOrDefault(x => x.UserName == name);
        }
        public bool ExistsByName(string name)
        {
            return UnitOfWork.Users.Any(x => x.UserName == name);
        }
        public User GetByNameAndPassword(string name, string password)
        {
            return GetByNameAndPasswordEx(name, password, new Include<User>());
        }
        public User GetByNameAndPasswordEx(string name, string password, Include<User> include)
        {
            var user = UnitOfWork.Users.Include(include).FirstOrDefault(x => x.UserName == name);
            if (user == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher();
            var result = passwordHasher.VerifyHashedPassword(user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            user.LastLogin = DateTime.Now;
            UnitOfWork.SaveChanges();

            return user;
        }

        public List<User> GetAll(Include<User> include)
        {
            return UnitOfWork.Users.AsNoTracking().Include(include).ToList();
        }

        public void Create(CreateUser model, IValidationErrors errors)
        {
            if (!model.Validate(errors))
            {
                return;
            }

            if (UnitOfWork.Users.Any(x => x.UserName == model.UserName))
            {
                errors.Add("UserName", "UserName is already taken");
                return;
            }

            var user = model.MapTo<User>();
            foreach (var userModule in user.UserModules)
            {
                userModule.UserId = user.Id;
            }

            var result = UserManager.Create(user, model.Password);
            if (!result.Succeeded)
            {
                errors.AddErrorsFromResult(result);
                return;
            }
        }

        public void Update(UpdateUserData model, IValidationErrors errors)
        {
            if (!model.Validate(errors))
            {
                return;
            }

            var user = UnitOfWork.Users.Include(x => x.UserModules).FirstOrDefault(x => x.Id == model.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            model.MapTo(user);
            foreach (var userModule in user.UserModules)
            {
                var modelUserModule = model.Modules.FirstOrDefault(x => x.Id == userModule.ModuleId);
                if (modelUserModule == null)
                {
                    continue;
                }

                modelUserModule.MapTo(userModule);
            }

            UnitOfWork.SaveChanges();
        }
    }
}
