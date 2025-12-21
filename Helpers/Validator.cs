namespace management_delegate.Helpers
{
    public static class Validator
    {
        public static bool ValidateUsername(string username)
        {
            return !string.IsNullOrWhiteSpace(username) && username.Length >= 3 && username.Length <= 16;
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6 || password.Length > 16)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);

            return hasUpper && hasLower && hasDigit;
        }
    }
}

