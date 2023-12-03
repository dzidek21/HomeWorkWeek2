using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassworManager.App.Interface
{
    public interface IService<T>
    {
        List<T> Passwords { get; set; }
        List<T> GetAllPasswords();
        void AddNewPassword(T newPassword);
        void DeletePassword(T passwordToDelete);
        void UpdatePassword(T passwordToUpdate, int id);
        T GetPasswordByName(string webSiteName);
    }
}
