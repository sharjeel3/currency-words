using Words.Server.Services;
using FluentAssertions;
using Words.Server.Services.Translation;

namespace Tests;

public class CurrencyServiceTests
{
    private readonly ICurrencyService _currencyService;

    public CurrencyServiceTests()
    {
        ITranslationService translationService = new TranslationService();
        TranslateHundredsStrategy translateHundredsStrategy = new TranslateHundredsStrategy(translationService);
        TranslateThousandsStrategy translateThousandsStrategy = new TranslateThousandsStrategy(translationService);
        TranslateCentsStrategy translateCentsStrategy = new TranslateCentsStrategy(translationService);

        _currencyService = new CurrencyService(translateHundredsStrategy,
            translateThousandsStrategy, translateCentsStrategy);
    }

    [Theory]
    [InlineData(23, "TWENTY-THREE DOLLARS")]
    [InlineData(123, "ONE HUNDRED AND TWENTY-THREE DOLLARS")]
    [InlineData(123.45, "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS")]
    public void GetWords__ShouldCorrectlyConvertSomeCommonNumbers(double input, 
        string expected)
    {
        // Assert
        var actual = _currencyService.GetWords(input);

        // Act
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(23000, "TWENTY-THREE THOUSAND DOLLARS")]
    [InlineData(45001, "FORTY-FIVE THOUSAND, ONE DOLLARS")]
    [InlineData(212555.2, "TWO HUNDRED AND TWELVE THOUSAND, FIVE HUNDRED AND FIFTY-FIVE DOLLARS AND TWO CENTS")]
    [InlineData(1200700, "ONE MILLION, TWO HUNDRED THOUSAND, SEVEN HUNDRED DOLLARS")]
    [InlineData(15200007100, "FIFTEEN BILLION, TWO HUNDRED MILLION, SEVEN THOUSAND, ONE HUNDRED DOLLARS")]
    public void GetWords__ShouldCorrectlyConvertThousandsOrBiggerNumbers(double input,
        string expected)
    {
        // Assert
        var actual = _currencyService.GetWords(input);

        // Act
        actual.Should().Be(expected);
    }

    [Fact]
    public void GetWords__ShouldHandleExponentials()
    {
        // Arrange
        double input = 10e3;
        string expected = "TEN THOUSAND DOLLARS";

        // Assert
        var actual = _currencyService.GetWords(input);

        // Act
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(1.22e2, "ONE HUNDRED AND TWENTY-TWO DOLLARS")]
    [InlineData(1.4578e2, "ONE HUNDRED AND FORTY-FIVE DOLLARS AND SEVENTY-EIGHT CENTS")]
    public void GetWords__ShouldHandleExponentialsWithFractions(double input,
        string expected)
    {
        // Assert
        var actual = _currencyService.GetWords(input);

        // Act
        actual.Should().Be(expected);
    }
}
