using Xunit;
using RefactoringTool;

public class RefactoringServiceTests
{
    private readonly RefactoringService _service = new();

    // 1. Базова заміна числа на константу
    [Fact]
    public void ReplaceMagicNumber_AddsConstantAndReplacesNumber()
    {
        string code = "if (x > 42) return 42;";
        string result = _service.ReplaceMagicNumber(code, "42", "MAX_SIZE");
        Assert.Contains("const int MAX_SIZE = 42;", result);
        Assert.Contains("MAX_SIZE", result);
        Assert.DoesNotContain("42", result);
    }

    // 2. Порожній код
    [Fact]
    public void ReplaceMagicNumber_EmptyCode()
    {
        string result = _service.ReplaceMagicNumber("", "42", "MAX_SIZE");
        Assert.Equal("const int MAX_SIZE = 42;\n", result);
    }

    // 3. Числа немає в коді
    [Fact]
    public void ReplaceMagicNumber_NumberNotInCode()
    {
        string code = "int x = 10;";
        string result = _service.ReplaceMagicNumber(code, "42", "MAX_SIZE");
        Assert.Equal("const int MAX_SIZE = 42;\nint x = 10;", result);
    }

    // 4. Число зустрічається кілька разів
    [Fact]
    public void ReplaceMagicNumber_ReplacesAllOccurrences()
    {
        string code = "int a = 100; int b = 100; int c = 100;";
        string result = _service.ReplaceMagicNumber(code, "100", "LIMIT");
        Assert.DoesNotContain("100", result);
        Assert.Equal(3, result.Split("LIMIT").Length - 1 - 1);
    }

    // 5. Константа додається на початок файлу
    [Fact]
    public void ReplaceMagicNumber_ConstantIsAtTheTop()
    {
        string code = "int x = 99;";
        string result = _service.ReplaceMagicNumber(code, "99", "MAX_VAL");
        Assert.StartsWith("const int MAX_VAL = 99;", result);
    }

    // 6. Інше число та інша назва константи
    [Fact]
    public void ReplaceMagicNumber_DifferentNumberAndName()
    {
        string code = "for (int i = 0; i < 255; i++)";
        string result = _service.ReplaceMagicNumber(code, "255", "MAX_BYTE");
        Assert.Contains("const int MAX_BYTE = 255;", result);
        Assert.Contains("MAX_BYTE", result);
        Assert.DoesNotContain("255", result);
    }

    // 7. Код з кількома рядками
    [Fact]
    public void ReplaceMagicNumber_MultilineCode()
    {
        string code = "int a = 10;\nint b = 10;\nreturn 10;";
        string result = _service.ReplaceMagicNumber(code, "10", "TEN");
        Assert.Contains("const int TEN = 10;", result);
        Assert.DoesNotContain("= 10", result);
    }

    // 8. Між константою та кодом є перенос рядка
    [Fact]
    public void ReplaceMagicNumber_HasNewlineBetweenConstantAndCode()
    {
        string code = "int x = 5;";
        string result = _service.ReplaceMagicNumber(code, "5", "FIVE");
        Assert.Contains("\n", result);
    }

    // 9. Назва константи присутня в результаті
    [Fact]
    public void ReplaceMagicNumber_ResultContainsConstantName()
    {
        string code = "int timeout = 30;";
        string result = _service.ReplaceMagicNumber(code, "30", "TIMEOUT_SECONDS");
        Assert.Contains("TIMEOUT_SECONDS", result);
    }

    // 10. Оголошення константи має правильний формат
    [Fact]
    public void ReplaceMagicNumber_ConstantHasCorrectFormat()
    {
        string code = "int x = 7;";
        string result = _service.ReplaceMagicNumber(code, "7", "DAYS_IN_WEEK");
        Assert.StartsWith("const int DAYS_IN_WEEK = 7;", result);
    }
}
