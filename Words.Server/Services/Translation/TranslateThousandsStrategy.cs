namespace Words.Server.Services.Translation;

public class TranslateThousandsStrategy(ITranslationService translationService) 
    : TranslateStrategyBase
{
    public override string Translate(CurrencyToken currencyToken, 
        double denominator)
    {
        var units = translationService.GetDictionaryLookup(new CurrencyToken
        {
            Value = denominator
        });
        string words = translationService.TranslateNumber(currencyToken);

        return words + " " + units;
    }
}
