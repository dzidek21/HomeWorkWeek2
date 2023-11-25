using PasswordManager.Domain;
using PassworManager.App.Helpers;
using PassworManager.App.Service;

namespace PassworManager.App.Manager
{
    public class PasswordManagerApp
    {
        private readonly PasswordService _passwordService;
        private List<Password> passwords = new List<Password>();
        static char[] passwordCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        string? password;

        public PasswordManagerApp(PasswordService passwordService)
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
    }
}
