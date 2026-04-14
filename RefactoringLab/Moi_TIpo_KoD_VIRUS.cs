using Xunit;
using RefactoringTool;

public class RefactoringService
{
    private readonly RefactoringService _service = new();

    [Fact]
    public void ReplaceMagicNumber_AddsConstantAndReplacesNumber()
    {
        string code = "if (x > 42) return 42;";
        string result = _service.ReplaceMagicNumber(code, "42", "MAX_SIZE");

        Assert.Contains("const int MAX_SIZE = 42;", result);
        Assert.Contains("MAX_SIZE", result);
        Assert.DoesNotContain("42", result);
    }

    [Fact]
    public void ReplaceMagicNumber_EmptyCode()
    {
        string result = _service.ReplaceMagicNumber("", "42", "MAX_SIZE");
        Assert.Equal("const int MAX_SIZE = 42;\n", result);
    }

    [Fact]
    public void ReplaceMagicNumber_NumberNotInCode()
    {
        string code = "int x = 10;";
        string result = _service.ReplaceMagicNumber(code, "42", "MAX_SIZE");
        Assert.Equal("const int MAX_SIZE = 42;\nint x = 10;", result);
    }
}