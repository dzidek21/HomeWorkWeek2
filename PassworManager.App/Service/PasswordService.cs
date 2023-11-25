using PasswordManager.Domain;
using PassworManager.App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassworManager.App.Service
{
    public class PasswordService : IService<Password>
    {
        public List<Password> Passwords { get; set; }
        public PasswordService()
        {
            Passwords = new();
        }

        public void AddNewPassword(Password newPassword)
        {
            Passwords.Add(newPassword);
        }

        public List<Password> GetAllPasswords()
        {
            return Passwords;
        }

        public Password GetPasswordByName(string webSiteName)
        {
            var pass = Passwords.FirstOrDefault(x => x.Website == webSiteName);
            return pass;
        }

        public void DeletePassword(Password passwordToDelete)
        {
            Passwords.Remove(passwordToDelete);
        }

        public void UpdatePassword(Password passwordToUpdate)
        {
            var pass = Passwords.FirstOrDefault(x => x.Website == passwordToUpdate.Website);
            if (pass!=null)
            {
                pass = passwordToUpdate;
            }
        }
    }
}
