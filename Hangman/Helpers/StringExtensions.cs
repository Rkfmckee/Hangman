namespace Hangman.Helpers
{
    public static class StringExtensions
    {
        public static string ReplaceCharAt(this string original, int position, char replaceWith)
        {
            return original.Remove(position, 1).Insert(position, replaceWith.ToString());
        }
    }
}
