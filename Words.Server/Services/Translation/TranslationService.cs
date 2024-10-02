namespace Words.Server.Services.Translation;

public class TranslationService : ITranslationService
{
    private readonly Dictionary<double, string> _englishDictionary = new()
    {
        { 0, "zero" },
        { 1, "one" },
        { 2, "two" },
        { 3, "three" },
        { 4, "four" },
        { 5, "five" },
        { 6, "six" },
        { 7, "seven" },
        { 8, "eight" },
        { 9, "nine" },
        { 10, "ten" },
        { 11, "eleven" },
        { 12, "twelve" },
        { 13, "thirteen" },
        { 14, "fourteen" },
        { 15, "fifteen" },
        { 16, "sixteen" },
        { 17, "seventeen" },
        { 18, "eighteen" },
        { 19, "ninteen" },
        { 20, "twenty" },
        { 30, "thirty" },
        { 40, "forty" },
        { 50, "fifty" },
        { 60, "sixty" },
        { 70, "seventy" },
        { 80, "eighty" },
        { 90, "ninety" },
        { 100, "hundred" },
        { 1000, "thousand" },
        { 1000000, "million" },
        { 1000000000, "billion" },
        { 1000000000000, "trillion" },
    };
    
    public string GetDictionaryLookup(CurrencyToken token)
    {
        if (_englishDictionary.TryGetValue(token.Value, out _))
            return _englishDictionary[token.Value];

        return "";
    }

    public string TranslateNumber(CurrencyToken token)
    {
        string r;

        if (token.Value.ToString().Length > 2)
            r = TranslateHundreds(token);
        else if (token.Value.ToString().Length > 1)
            r = TranslateTens(token);
        else
            r = TranslateSingles(token);

        return r;
    }

    private string TranslateHundreds(CurrencyToken token)
    {
        var denominator = 100;
        var factor = _englishDictionary[denominator];

        var remainder = token.Value % denominator;
        var unit = token.Value - remainder;
        var leading = unit / denominator;

        var start = leading == 0 ? "" : _englishDictionary[leading] + " " + factor;

        var ending = "";

        if (remainder > 0)
        {
            var separator = leading == 0 ? "and " : " and ";
            ending = separator + TranslateTens(new CurrencyToken
            {
                Value = remainder,
            });
        }

        return start + ending;
    }

    private string TranslateTens(CurrencyToken token)
    {
        var val = token.Value;
        if (val <= 20)
        {
            return TranslateSingles(token);
        }

        var denominator = 10;
        var remainder = val % denominator;
        var factor = val - remainder;

        var start = _englishDictionary[factor];

        var ending = "";

        if (remainder > 0)
        {
            ending = "-" + _englishDictionary[remainder];
        }

        return start + ending;
    }

    private string TranslateSingles(CurrencyToken token)
    {
        return _englishDictionary[token.Value];
    }
}

public class CurrencyToken
{
    public double Value { get; set; }
}
