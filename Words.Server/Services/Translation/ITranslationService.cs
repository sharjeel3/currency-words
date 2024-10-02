namespace Words.Server.Services.Translation;

public interface ITranslationService
{
    public string TranslateNumber(CurrencyToken token);
    public string GetDictionaryLookup(CurrencyToken token);
}
