using System.Text.RegularExpressions;



public class Validations{
public static bool IsPasswordValid(string password)
{
        // Password must be at least 8 characters, with one special character, one lowercase letter, and one uppercase letter
        const string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
        return Regex.IsMatch(password, pattern);
}
}