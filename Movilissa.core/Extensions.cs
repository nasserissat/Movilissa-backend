using System.Text;

namespace Movilissa.core;

public static class Extensions
{
    public static string[] PascalCaseToStrings(this string text)
    {
        var words = new List<string>();
        var word = new StringBuilder();
        foreach (var ch in text)
        {
            if (char.IsUpper(ch))
            {
                if (word.Length > 0)
                {
                    words.Add(word.ToString());
                    word.Clear();
                }
                word.Append(ch);
            }
            else word.Append(ch);
        }
        if (word.Length > 0)
            words.Add(word.ToString());
        return words.ToArray();
    }
    static string Decapitalize(this string text, params string[] words) =>
        words.Aggregate(text, (current, word) => current.Replace($" {word}", $" {word.ToLower()}"));
    
    public static string PascalCaseToTitleCase(this string text, string language = "es")
    {
        var capitalized = string.Join(' ', text.PascalCaseToStrings()).Trim();
        return language switch
        {
            "en" => capitalized.Decapitalize("Of", "And", "A", "An", "The", "But", "For", "At", "By", "To"),
            "es" => capitalized.Decapitalize("Un", "Una", "La", "Los", "Las", "El", "Y", "Pero", "Para", "En", "Por", "Desde", "Hasta", "Del", "De", "A"),
            _ => capitalized
        };
    }
    public static string PascalCaseWithInitialsToTitleCase(this string text, string language = "es")
    {
        var capitalized = string.Join(' ', text.PascalCaseWithInitialsToStrings()).Trim();
        return language switch
        {
            "en" => capitalized.Decapitalize("Of", "And", "A", "An", "The", "But", "For", "At", "By", "To"),
            "es" => capitalized.Decapitalize("Un", "Una", "La", "Los", "Las", "El", "Y", "Pero", "Para", "En", "Por", "Desde", "Hasta", "Del", "De", "A"),
            _ => capitalized
        };
    }
    static bool IsUpper(this string text) => text.All(ch => char.IsUpper(ch));
    public static string[] PascalCaseWithInitialsToStrings(this string text)
    {
        var words = new List<string>();
        var word = new StringBuilder();
        foreach (var ch in text)
        {
            if (char.IsUpper(ch))
            {
                if (word.Length > 0 && !word.ToString().IsUpper())
                {
                    words.Add(word.ToString());
                    word.Clear();
                }
                word.Append(ch);
            }
            else
            {
                if (word.ToString().IsUpper() && word.Length > 1)
                {
                    words.Add(word.ToString().Substring(0, word.Length - 1));
                    word.Remove(0, word.Length - 1);
                }
                word.Append(ch);
            }
        }
        if (word.Length > 0)
            words.Add(word.ToString());
        return words.ToArray();
    }

}