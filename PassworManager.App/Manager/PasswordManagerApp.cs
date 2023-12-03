using PasswordManager.Domain;
using PassworManager.App.Helpers;
using PassworManager.App.Interface;
using PassworManager.App.Service;

namespace PassworManager.App.Manager
{
    
    public class PasswordManagerApp
    {
        
        private IService<Password> _passwordService;
        private List<Password> passwords = new List<Password>();
        static char[] passwordCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        string? password;
        

        public PasswordManagerApp(IService<Password> passwordService)
        {
            _passwordService = passwordService;
        }

        public void AddNewPassword()
        {
            Password newPassword = new Password();
            Console.WriteLine("\nWpisz nazwe strony:");
            newPassword.Website = Console.ReadLine();
            Console.WriteLine("Wpisz login:");
            newPassword.UserName = Console.ReadLine();
            do
            {
                Console.WriteLine("1: Wpisz haslo:");
                Console.WriteLine("2: Generuj haslo:");
                var action = Console.ReadKey();
                Console.WriteLine("\n");
                switch (action.KeyChar)
                {
                    case '1':
                        newPassword.UserPassword = Console.ReadLine();
                        break;
                    case '2':
                        newPassword.UserPassword = GeneratedPassword();
                        break;
                    default:
                        ViewMessages.ShowError("Brak takiej akcji");
                        break;
                }
            } while (newPassword.UserPassword == null);

            ViewMessages.ShowPassword(newPassword);
            Console.WriteLine($"---Dodano pomyslnie--\n");
            _passwordService.AddNewPassword(newPassword);
            
        }

        private string? GeneratedPassword()
        {
            do
            {
                Console.WriteLine("\n--Z ilu znakow chcesz wygenerowac haslo?--\n --Podaj liczbe od 8 do 20--");
                int charactersCount;
                var userInput = Console.ReadLine();
                Int32.TryParse(userInput, out charactersCount);
                switch (charactersCount)
                {
                    case < 8:
                        ViewMessages.ShowError("Haslo musi skladac sie z conajmniej 8 znakow");
                        break;
                    case > 20:
                        ViewMessages.ShowError("Haslo musi skladac sie maksymalnie z 20 znakow");
                        break;
                    default:
                        password = GetRandomCharacters(charactersCount);
                        break;
                }
            } while (password == null);

            return password;
        }

        private string GetRandomCharacters(int charactersCount)
        {
            List<char> randomCharacters = new List<char>();
            for (int i = 0; i < charactersCount; i++)
            {
                var randomNb = new Random().Next(passwordCharacters.Length);
                randomCharacters.Add(passwordCharacters[randomNb]);
            }

            return new string(randomCharacters.ToArray());
        }

        public List<Password> GetAllPasswords()
        {
            return _passwordService.GetAllPasswords();
        }

        public void GetPasswordByWebName()
        {
            Password passwordToFind;
            do
            {
                Console.WriteLine("\n--Podaj nazwe strony do znalezienia--");
                string website = Console.ReadLine();
                passwordToFind = _passwordService.GetPasswordByName(website);
                if (passwordToFind == null)
                {
                    ViewMessages.ShowError("Nie znaleziono hasla");
                    website = Console.ReadLine();
                }
            } while (passwordToFind == null);

            ViewMessages.ShowPassword(passwordToFind);
        }

        public void RemovePassword()
        {
            Password passwordToFind;
            do
            {
                Console.WriteLine("\n--Podaj nazwe strony do znalezienia:--");
                var website = Console.ReadLine();
                passwordToFind = _passwordService.GetPasswordByName(website);
                if (passwordToFind == null)
                {
                    ViewMessages.ShowError("Nie znaleziono hasla");
                    website = Console.ReadLine();
                }
            } while (passwordToFind == null);

            ViewMessages.ShowPassword(passwordToFind);
            _passwordService.DeletePassword(passwordToFind);
            Console.WriteLine("--Usunieto--");
        }
        public void EditPassword()
        {
            Password passwordToFind;
            do
            {
                Console.WriteLine("\n--Podaj nazwę strony do edycji:--");
                var website = Console.ReadLine();
                passwordToFind = _passwordService.GetPasswordByName(website);
                if (passwordToFind == null)
                {
                    ViewMessages.ShowError("Nie znaleziono hasla");
                    website = Console.ReadLine();
                }
            } while (passwordToFind == null);
            ViewMessages.ShowPassword(passwordToFind);
            ConsoleKeyInfo action;
            Console.WriteLine("\n1: Edytuj nazwę strony");
            Console.WriteLine("2: Edytuj login");
            Console.WriteLine("3: Edytuj hasło\n");
            action = Console.ReadKey();
            switch (action.KeyChar)
            {
                case '1':
                    Console.WriteLine("--Podaj nową nazwe strony:--");
                    passwordToFind.Website = Console.ReadLine();
                    _passwordService.UpdatePassword(passwordToFind, passwordToFind.Id);
                    break;
                case '2':
                    Console.WriteLine("--Podaj nowy login:--");
                    
                    passwordToFind.UserName = Console.ReadLine();
                    _passwordService.UpdatePassword(passwordToFind, passwordToFind.Id);
                    break;
                case '3':
                    do
                    {
                        Console.WriteLine("\n1: Wpisz haslo:");
                        Console.WriteLine("2: Generuj haslo:");
                        ConsoleKeyInfo passwordAction = Console.ReadKey();
                        Console.WriteLine("\n");
                        switch (passwordAction.KeyChar)
                        {
                            case '1':
                                Console.WriteLine("--Podaj nowe hasło:--");
                                passwordToFind.UserPassword = Console.ReadLine();
                                _passwordService.UpdatePassword(passwordToFind, passwordToFind.Id);
                                break;
                            case '2':
                                passwordToFind.UserPassword = GeneratedPassword();
                                _passwordService.UpdatePassword(passwordToFind, passwordToFind.Id);
                                break;
                            default:
                                ViewMessages.ShowError("Brak takiej akcji");
                                break;
                        }
                    } while (passwordToFind.UserPassword == null);

                    break;
                default:
                    ViewMessages.ShowError("Brak takiej akcji");
                    break;
            }

            Console.WriteLine("--Edytowano--");
            ViewMessages.ShowPassword(passwordToFind);
        }
        //Testing
        public Password GetPasswordByName(string name)
        {
            return _passwordService.GetPasswordByName(name);
        }
    }
}
