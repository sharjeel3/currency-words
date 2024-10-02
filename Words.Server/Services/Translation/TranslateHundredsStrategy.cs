namespace Words.Server.Services.Translation;

public class TranslateHundredsStrategy(ITranslationService translationService) 
    : TranslateStrategyBase
{
    public override string Translate(CurrencyToken currencyToken, 
        double denominator)
    {
        string words = translationService.TranslateNumber(currencyToken);

        return words;
    }
}
