using System.Text.RegularExpressions;

namespace CourseWorkResult.Controllers.Validation
{
    static class ValidateData
    {
        public static bool CheckString(string data, out string errorMessage)
        {
            if (!Regex.IsMatch(data, @"^[А-ЯЁа-яёA-Za-z ]{4,20}"))
            {
                errorMessage = "Строка должна содержать только символы русского и английского алфавита и иметь длину от 4 до 20 символов!";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
