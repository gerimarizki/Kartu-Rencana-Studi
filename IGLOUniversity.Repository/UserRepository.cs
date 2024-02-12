using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Repository
{
    public class UserRepository
    {
        private static UserRepository instance = new UserRepository();
        public static UserRepository GetRepository()
        {
            return instance;
        }
        public bool IsAuthentication(string username, string password)
        {
            using (var _context = new IGLOUniversityContext())
            {
                var user = _context.Users.SingleOrDefault(u => u.UserName == username);
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public string GetRole(string username)
        {
            using (var context = new IGLOUniversityContext())
            {
                var user = context.Users.SingleOrDefault(u => u.UserName == username);
                var isAdmin = user.IsAdmin;
                if (isAdmin)
                {
                    return "Administrator";
                }
                return user.Status;
            }
        }
        public IQueryable<User> GetAll()
        {
            var context = new IGLOUniversityContext();
            return context.Users;
        }
        public User GetSingle(int id)
        {
            var user = new User();
            using (var context = new IGLOUniversityContext())
            {
                user = context.Users.SingleOrDefault(u => u.Id == id);
            }
            return user;
        }
        public bool Insert(User model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var isUserExist = context.Users.Any(u => u.UserName == model.UserName);
                    if (isUserExist) { return false; }
                    var isMahasiswaExist = context.Users.Any(m => m.MahasiswaId == model.MahasiswaId);
                    if (!model.IsAdmin)
                    {
                        if (isMahasiswaExist) { return false; }
                        if (model.MahasiswaId == null) { return false; }
                    }
                    context.Users.Add(model);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(User model)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var user = context.Users.SingleOrDefault(u => u.Id == model.Id);
                    if (user == null) { return false; }

                    HelperFunction.MappingModel(user, model);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var user = context.Users.SingleOrDefault(u => u.Id == id);
                    
                    context.Users.Remove(user);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public int GetId(string userName)
        {
            int? id;
            using (var context = new IGLOUniversityContext())
            {
                id = context.Users.SingleOrDefault(u => u.UserName == userName).MahasiswaId;
            }
            return (int)id;
        }
        public bool UpdatePassword(int id, string password)
        {
            try
            {
                using (var context = new IGLOUniversityContext())
                {
                    var user = context.Users.SingleOrDefault(u => u.Id == id);
                    user.Password = password;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            
        }
    }
}
