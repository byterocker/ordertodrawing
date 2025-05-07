namespace OrderToDrawing.Extensions
{
    public static class StringExtensions
    {
        public static bool IsMatch(this string source, string pattern, char singleWildcard, char multipleWildcard)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(pattern))
                return false;

            if (pattern.Length == 1 && pattern[0] == multipleWildcard)
                return true;

            int i = 0, j = 0;
            int cp = 0, mp = 0;
            while (i < source.Length)
            {
                if (j < pattern.Length && (pattern[j] == source[i] || pattern[j] == singleWildcard))
                {
                    i++;
                    j++;
                }
                else if (j < pattern.Length && pattern[j] == multipleWildcard)
                {
                    mp = ++j;
                    cp = i;
                }
                else if (mp != 0)
                {
                    j = mp;
                    i = ++cp;
                }
                else
                {
                    return false;
                }
            }

            while (j < pattern.Length && pattern[j] == multipleWildcard)
            {
                j++;
            }

            return j == pattern.Length;
        }
    }
}
