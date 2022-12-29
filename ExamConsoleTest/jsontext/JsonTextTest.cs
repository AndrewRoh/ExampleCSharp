using System;
using System.Globalization;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace  Extentions
{
    public static class StringExtensions
    {
        public static string? ToCamelCase2(this string? str) => str is null
            ? null
            : new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() }.GetResolvedPropertyName(str);

        public static string? ToSnakeCase2(this string? str) => str is null
            ? null
            : new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() }.GetResolvedPropertyName(str);
    

        public static string ToSnakeCase(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (text.Length < 2)
            {
                return text;
            }

            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char
                    c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}
