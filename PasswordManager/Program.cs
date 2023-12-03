using PasswordManager.Domain;
using PassworManager.App.Helpers;
using PassworManager.App.Manager;
using PassworManager.App.Service;

namespace PasswordManager;

class Program
{
    
    static void Main(string[] args)
    {
        Console.WriteLine(ViewMessages.LogoGenerator());
        PasswordService passwordService = new PasswordService();
        PasswordManagerApp passwordManager = new PasswordManagerApp(passwordService);
        
        Console.WriteLine("Witaj w Locker v: 1.0.0");
        ConsoleKeyInfo action;
        do
        {
            ViewMessages.StartUpMenu();
            action = Console.ReadKey();
            switch (action.KeyChar)
            {
                case '1':
                    passwordManager.AddNewPassword();
                    break;
                case '2':
                    ViewMessages.PasswordsListView(passwordManager.GetAllPasswords());
                    break;
                case '3':
                    passwordManager.GetPasswordByWebName();
                    break;
                case '4':
                    passwordManager.EditPassword();
                    break;
                case '5':
                    passwordManager.RemovePassword();
                    break;
                case '6':
                    Environment.Exit(0);
                    break;
            }
        } while (action.KeyChar != '6');
    }
}