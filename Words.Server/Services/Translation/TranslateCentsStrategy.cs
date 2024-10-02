namespace Words.Server.Services.Translation;

public class TranslateCentsStrategy(ITranslationService translationService) 
    : TranslateStrategyBase
{
    public override string Translate(CurrencyToken currencyToken, 
        double denominator)
    {
        var cents = translationService
            .TranslateNumber(currencyToken).ToUpper();

        var centsUnit = cents == "ONE" ? " cent" : " cents";

        return cents + centsUnit;
    }
}
