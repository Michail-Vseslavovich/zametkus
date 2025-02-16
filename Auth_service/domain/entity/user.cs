using System.ComponentModel.DataAnnotations;

namespace Auth_service.domain.entity
{
    public class User
    {
        [Key]
        public readonly int id;

        [Required] public string Name;
        [Required] private string Email;
        [Required] private string Password;

        public string GetEmail()
        {
            return Email;
        }      
        public bool IsPasswordEqual(string other)
        {
            return Password == other;
        }
        public bool Changepassword(string other)
        {
            if (Password.Length > 8 && Password.Length < 20)
            {
                Password = other;
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
