

using Auth_service.domain.entity;
using Auth_service.domain.interfaces;
using User_service.infrastructure.Dbcontext;

namespace Auth_service.application.services
{
    public class UserService(UserDbContext db) : IUserService<User>
    {
        public User getById(int id, string password)
        {
            return db.Users.FirstOrDefault(p => p.id == id && p.IsPasswordEqual(password));
        }
        public bool DeleteById(int id, string password)
        {
            User? UserToDelete = db.Users.FirstOrDefault(u => u.id == id);
            if (UserToDelete != null && UserToDelete.IsPasswordEqual(password))
            {
                db.Users.Remove(UserToDelete);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Add(User user)
        {
            if(user != null && (db.Users.FirstOrDefault(u => u.id == user.id && u.GetEmail() == user.GetEmail()) == null) )
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(User user, string password)
        {
            if (user != null && user.IsPasswordEqual(password))
            {
                db.Users.Update(user);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public int getId(string password, string email)
        {
            User? user = db.Users.FirstOrDefault(p => p.IsPasswordEqual(password) && p.GetEmail() == email);
            if (user != null )
            {
                return user.id;
            }
            return 0;
        }
    }
}
