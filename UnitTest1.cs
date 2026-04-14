using Xunit;
using RefactoringTool;

public class RefactoringTests
{
    private readonly RefactoringService _refactoring = new RefactoringService();

    [Fact]
    public void MethodExists_WhenMethodPresent_ReturnsTrue()
    {
        string code = "void Calculate() { }";
        bool result = _refactoring.MethodExists(code, "Calculate");
        Assert.True(result);
    }

    [Fact]
    public void MethodExists_WhenMethodAbsent_ReturnsFalse()
    {
        string code = "void Calculate() { }";
        bool result = _refactoring.MethodExists(code, "Print");
        Assert.False(result);
    }

    [Fact]
    public void MethodExists_WhenSourceCodeEmpty_ReturnsFalse()
    {
        bool result = _refactoring.MethodExists("", "Calculate");
        Assert.False(result);
    }

    [Fact]
    public void AddParameter_MethodSignatureIsCorrect()
    {
        string code = "void Print() { }";
        string result = _refactoring.AddParameter(code, "Print", "string", "message");
        Assert.Contains("void Print(string message)", result);
    }

    [Fact]
    public void AddParameter_AddsParameterToMethod()
    {
        string code = "void Calculate() { }";
        string result = _refactoring.AddParameter(code, "Calculate", "int", "value");
        Assert.Contains("int value", result);
    }

    [Fact]
    public void AddParameter_MethodAlreadyHasParams_AddsNewParam()
    {
        string code = "void Calculate(int x) { }";
        string result = _refactoring.AddParameter(code, "Calculate", "int", "y");
        Assert.Contains("int y", result);
    }

    [Fact]
    public void UpdateMethodCalls_AddsDefaultArgument()
    {
        string code = "void foo(){} void bar(){ foo(); }";
        string result = _refactoring.UpdateMethodCalls(code, "foo", "0");
        Assert.Contains("foo(0)", result);
    }

    [Fact]
    public void HasParameter_WhenParameterExists_ReturnsTrue()
    {
        string code = "void Calculate(int value) { }";
        bool result = _refactoring.HasParameter(code, "Calculate", "value");
        Assert.True(result);
    }

    [Fact]
    public void IsValidParameterName_ValidName_ReturnsTrue()
    {
        bool result = _refactoring.IsValidParameterName("myParam");
        Assert.True(result);
    }

    [Fact]
    public void IsValidParameterName_StartsWithDigit_ReturnsFalse()
    {
        bool result = _refactoring.IsValidParameterName("1param");
        Assert.False(result);
    }
}
