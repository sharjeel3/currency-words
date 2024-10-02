using Words.Server.Extensions;
using Words.Server.Services.Translation;

namespace Words.Server.Services;

public class CurrencyService(TranslateHundredsStrategy translateHundredsStrategy,
    TranslateThousandsStrategy translateThousandsStrategy,
    TranslateCentsStrategy translateCentsStrategy) 
        : ICurrencyService
{
    public string GetWords(double number)
    {
        var parts = number.ToThousandChunks();

        var dollarsChunks = parts.Dollars;

        IEnumerable<string> result = ParseAndTranslateChunks(dollarsChunks);

        var dollarsStr = string.Join(", ", result).ToUpper();
        var currencyUnit = dollarsStr == "ONE" ? " dollar" : " dollars";
        dollarsStr += currencyUnit;

        var cents = parts.Cents;
        if (!string.IsNullOrEmpty(cents))
        {
            var centsStr = translateCentsStrategy.Translate(new CurrencyToken
            {
                Value = Int32.Parse(cents)
            }, 10);

            dollarsStr = dollarsStr + " and " + centsStr;
        }

        return dollarsStr.ToUpper();
    }

    private IEnumerable<string> ParseAndTranslateChunks(IEnumerable<string> dollarChunks)
    {
        IEnumerable<string> result = new List<string>();

        for (var i = 0; i < dollarChunks.Count(); i++)
        {
            var c = dollarChunks.ElementAt(i);
            var numeric = Int32.Parse(dollarChunks.ElementAt(i));

            if (i > 0 && numeric == 0) { continue; }

            var remainingItems = dollarChunks.Count() - (i + 1);
            var zeros = Math.Pow(10, c.Length - 1 + remainingItems * 3);

            var tok = new CurrencyToken
            {
                Value = numeric
            };

            string res;

            var unit = zeros switch
            {
                >= 1000000000000 => 1000000000000,
                >= 1000000000 => 1000000000,
                >= 1000000 => 1000000,
                >= 1000 => 1000,
                >= 100 => 100,
                >= 10 => 10,
                _ => 0
            };

            if (zeros >= 1000)
            {
                res = translateThousandsStrategy.Translate(tok, unit);
            }
            else
            {
                res = translateHundredsStrategy.Translate(tok, unit);
            }

            result = result.Append(res);
        }

        return result;
    }
}
