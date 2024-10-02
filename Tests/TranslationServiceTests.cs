using Words.Server.Services.Translation;
using FluentAssertions;

namespace Tests;

public class TranslationServiceTests
{
    [Fact]
    public void GetDictionaryLookup__ShouldReturnCurrencyLookup()
    {
        // Arrange
        var service = new TranslationService();
        var token = new CurrencyToken { Value = 1000 };

        // Act
        var words = service.GetDictionaryLookup(token);

        // Assert
        words.Should().Be("thousand");
    }

    [Fact]
    public void GetDictionaryLookup__WhenCalledWithBadInput__ShouldReturnEmptyResponse()
    {
        // Arrange
        var service = new TranslationService();
        var token = new CurrencyToken { Value = 1234 };

        // Act
        var words = service.GetDictionaryLookup(token);

        // Assert
        words.Should().Be("");
    }

    [Fact]
    public void TranslateNumber__WhenCalledWithHundreds__ShouldReturnWords()
    {
        // Arrange
        var service = new TranslationService();
        var token = new CurrencyToken { Value = 121 };

        // Act
        var words = service.TranslateNumber(token);

        // Assert
        words.Should().Be("one hundred and twenty-one");
    }

    [Fact]
    public void TranslateNumber__WhenCalledWithTens__ShouldReturnWords()
    {
        // Arrange
        var service = new TranslationService();
        var token = new CurrencyToken { Value = 34 };

        // Act
        var words = service.TranslateNumber(token);

        // Assert
        words.Should().Be("thirty-four");
    }

    [Fact]
    public void TranslateNumber__WhenCalledWithSingleDigit__ShouldReturnWords()
    {
        // Arrange
        var service = new TranslationService();
        var token = new CurrencyToken { Value = 6 };

        // Act
        var words = service.TranslateNumber(token);

        // Assert
        words.Should().Be("six");
    }
}
