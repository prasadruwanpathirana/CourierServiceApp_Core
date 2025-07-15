namespace CourierServiceApp.Presentation
{
    public static class InputValidator
    {
        public static T TryParse<T>(string input, string fieldName)
        {
            try
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch (Exception)
            {
                throw new ArgumentException($"Invalid input for {fieldName}: '{input}'");
            }
        }

        public static void ValidateInput(string[]? parts, int expectedLength, string context)
        {
            if (parts == null || parts.Length < expectedLength)
            {
                throw new ArgumentException($"Invalid input format for {context}. Expected {expectedLength} values.");
            }
        }
    }
}
