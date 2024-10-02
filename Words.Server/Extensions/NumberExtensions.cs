namespace Words.Server.Extensions;

public static class NumberExtensions
{
    public static CurrencyPart ToThousandChunks(this double value)
    {
        var numberStr = value.ToString();

        var parts = numberStr.Split(".");

        string digits = parts[0];
        string cents = "";

        if (parts.Length > 1)
        {
            cents = parts[1];
        }

        var chars = digits
            .ToCharArray()
            .Reverse();
        
        var thousandsChunks = chars.Chunk(3).Select(x =>
        {
            var chunk = x.Reverse().Select(u => u.ToString());
            return string.Join("", chunk);
        }).Reverse();

        return new CurrencyPart
        {
            Dollars = thousandsChunks,
            Cents = cents
        };
    }
}

public class CurrencyPart
{
    public IEnumerable<string> Dollars { get; set; }
    public string Cents { get; set; }
}
