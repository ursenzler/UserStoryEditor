namespace UserStoryEditor.WebApi.Specs
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;

    public static class ToJsonExtensions
    {
        public static string ToJson(this decimal number)
            => number.ToString(CultureInfo.InvariantCulture);

        public static string ToJson(this Guid guid)
            => $"'{guid.ToString().ToUpper()}'";

        public static string ToJson(this int value)
            => $"{value}";

        public static string ToJson(this string value)
            => $"'{value}'";

        public static string ToJsonVariableName(this string input)
        {
            var first = input.Substring(0, 1);
            return $"{first.ToLower()}{input.Substring(1, input.Length - 1)}";
        }

        public static string ToJson(this HttpResponseMessage response)
            => response.Content.ReadAsStringAsync().Result;

        public static string ToResponseString(this HttpResponseMessage response)
            => response.Content.ReadAsStringAsync().Result.Trim('\"');

        public static string ToLower(this bool value)
            => value.ToString().ToLower();

        public static string ToJsonArray(this IEnumerable<string> parts)
        {
            return $"[ {string.Join(", ", parts)} ]";
        }

        public static string ToJsonObject(this IEnumerable<KeyValuePair<string, string>> parts)
        {
            return $"{{ {string.Join(", ", parts.Select(kvp => $"{kvp.Key}: {kvp.Value}"))} }}";
        }

        public static string ToJsonDictionary<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> keyFunc,
            Func<T, string> valueFunc)
        {
            return enumerable
                .ToDictionary(keyFunc, valueFunc)
                .ToJsonObject();
        }
    }
}