using PasswordManager.Domain;
using PassworManager.App.Interface;
using PassworManager.App.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassworManager.App.Service
{
    public class PasswordService : IService<Password>
    {
        private static string FILEPATH = @"C:\testFile.txt";
        FileManager fileManager = new FileManager(FILEPATH);
        public List<Password> Passwords { get; set; }
        public PasswordService()
        {
            Passwords = new List<Password>();
            Passwords = fileManager.ReadFile();
            if (Passwords==null)
            {
                Passwords = new List<Password>();
                fileManager.SaveListToFile(Passwords);
            }
        }

        public void AddNewPassword(Password newPassword)
        {
            Passwords = fileManager.ReadFile();
            if (Passwords==null)
            {
                Passwords.Add(newPassword);
                fileManager.SaveListToFile(Passwords);
            }
            Passwords.Add(newPassword);
            fileManager.SaveListToFile(Passwords);
        }

        public List<Password> GetAllPasswords()
        {
            Passwords = fileManager.ReadFile();
            return Passwords;
        }

        public Password GetPasswordByName(string webSiteName)
        {
            var passFile=fileManager.ReadFile();
            var pass = passFile.FirstOrDefault(x => x.Website == webSiteName);
            return pass;
        }

        public void DeletePassword(Password passwordToDelete)
        {
            Passwords = fileManager.ReadFile();
            var del = Passwords.FirstOrDefault(x => x.Website == passwordToDelete.Website);
            Passwords.Remove(del);
            fileManager.SaveListToFile(Passwords);
        }

        public void UpdatePassword(Password passwordToUpdate)
        {
            Passwords = fileManager.ReadFile();
            var pass = Passwords.FirstOrDefault(x => x.Website == passwordToUpdate.Website);
            if (pass!=null)
            {
                pass = passwordToUpdate;
            }
            fileManager.SaveListToFile(Passwords);
        }
    }
}
