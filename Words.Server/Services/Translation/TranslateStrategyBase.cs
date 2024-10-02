namespace Words.Server.Services.Translation;

public abstract class TranslateStrategyBase
{
    public abstract string Translate(CurrencyToken currencyToken, 
        double denominator);
}
