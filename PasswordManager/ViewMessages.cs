namespace PasswordManager;

public static class ViewMessages
{
    public static void ShowError(string errorMessage)
    {
        Console.WriteLine($"\n----{errorMessage}----");
        Console.WriteLine("\n----Sprobuj jeszcze raz----");
    }

    public static void ShowPassword(Password passwordToShow)
    {
        Console.WriteLine("\n*******");
        Console.WriteLine($"\nNazwa strony: {passwordToShow.Website}\n" +
                          $"Login: {passwordToShow.UserName}\n" +
                          $"Haslo: {passwordToShow.UserPassword}\n");
        Console.WriteLine("\n*******");
    }

    public static void StartUpMenu()
    {
        Console.WriteLine("\n1: Dodaj haslo \n2: Pokaz liste hasel \n3: Szukaj \n4: Usun haslo \n5: Wyjscie");
    }
}