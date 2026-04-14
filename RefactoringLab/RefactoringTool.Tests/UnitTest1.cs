using Xunit;
using RefactoringTool;

public class RefactoringTests
{
    private readonly RefactoringService _refactoring = new RefactoringService();

    // ===== RenameMethod =====

    [Fact]
    public void RenameMethod_SimpleCase()
    {
        string code = "void foo() {}";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("void bar() {}", result);
    }

    [Fact]
    public void RenameMethod_WithParameters()
    {
        string code = "int sum(int a, int b) { return a + b; }";
        var result = _refactoring.RenameMethod(code, "sum", "add");
        Assert.Equal("int add(int a, int b) { return a + b; }", result);
    }

    [Fact]
    public void RenameMethod_MultipleCalls()
    {
        string code = "foo(); foo();";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("bar(); bar();", result);
    }

    [Fact]
    public void RenameMethod_InsideClass()
    {
        string code = "class A { void foo() {} }";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("class A { void bar() {} }", result);
    }

    [Fact]
    public void RenameMethod_WithReturnType()
    {
        string code = "int foo() { return 1; }";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("int bar() { return 1; }", result);
    }

    [Fact]
    public void RenameMethod_MethodCallWithArguments()
    {
        string code = "foo(5, 10);";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("bar(5, 10);", result);
    }

    [Fact]
    public void RenameMethod_MethodInExpression()
    {
        string code = "int x = foo() + 5;";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("int x = bar() + 5;", result);
    }

    [Fact]
    public void RenameMethod_NestedCalls()
    {
        string code = "foo(foo());";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("bar(bar());", result);
    }

    [Fact]
    public void RenameMethod_MultipleLines()
    {
        string code = "foo();\nfoo();";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("bar();\nbar();", result);
    }

    [Fact]
    public void RenameMethod_RenameToLongerName()
    {
        string code = "void foo() { foo(); }";
        var result = _refactoring.RenameMethod(code, "foo", "myMethod");
        Assert.Equal("void myMethod() { myMethod(); }", result);
    }

    // ===== MethodExists =====

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

    // ===== AddParameter =====

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

    // ===== UpdateMethodCalls =====

    [Fact]
    public void UpdateMethodCalls_AddsDefaultArgument()
    {
        string code = "void foo(){} void bar(){ foo(); }";
        string result = _refactoring.UpdateMethodCalls(code, "foo", "0");
        Assert.Contains("foo(0)", result);
    }

    // ===== HasParameter =====

    [Fact]
    public void HasParameter_WhenParameterExists_ReturnsTrue()
    {
        string code = "void Calculate(int value) { }";
        bool result = _refactoring.HasParameter(code, "Calculate", "value");
        Assert.True(result);
    }

    // ===== IsValidParameterName =====

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
    // ===== RenameVariable =====

[Fact]
public void RenameVariable_SimpleCase()
{
    string code = "int foo = 5;";
    var result = _refactoring.RenameVariable(code, "foo", "bar");
    Assert.Equal("int bar = 5;", result);
}

[Fact]
public void RenameVariable_UsedInExpression()
{
    string code = "int x = foo + 5;";
    var result = _refactoring.RenameVariable(code, "foo", "bar");
    Assert.Equal("int x = bar + 5;", result);
}

[Fact]
public void RenameVariable_MultipleTimes()
{
    string code = "foo = 1; foo = 2;";
    var result = _refactoring.RenameVariable(code, "foo", "bar");
    Assert.Equal("bar = 1; bar = 2;", result);
}

[Fact]
public void RenameVariable_InsideMethod()
{
    string code = "void Test() { int foo = 0; foo++; }";
    var result = _refactoring.RenameVariable(code, "foo", "bar");
    Assert.Equal("void Test() { int bar = 0; bar++; }", result);
}

[Fact]
public void RenameVariable_NoMatch()
{
    string code = "int x = 5;";
    var result = _refactoring.RenameVariable(code, "foo", "bar");
    Assert.Equal("int x = 5;", result);
}

[Fact]
public void RenameVariable_PartialMatchShouldNotChange()
{
    string code = "int fooBar = 5;";
    var result = _refactoring.RenameVariable(code, "foo", "bar");
    Assert.Equal("int fooBar = 5;", result);
}

[Fact]
public void RenameVariable_UsedAsArgument()
{
    string code = "Print(foo);";
    var result = _refactoring.RenameVariable(code, "foo", "bar");
    Assert.Equal("Print(bar);", result);
}

[Fact]
public void RenameVariable_MultipleLines()
{
    string code = "int foo = 0;\nfoo = foo + 1;";
    var result = _refactoring.RenameVariable(code, "foo", "bar");
    Assert.Equal("int bar = 0;\nbar = bar + 1;", result);
}

[Fact]
public void RenameVariable_RenameToLongerName()
{
    string code = "int x = 5; int y = x + 1;";
    var result = _refactoring.RenameVariable(code, "x", "myVar");
    Assert.Equal("int myVar = 5; int y = myVar + 1;", result);
}

[Fact]
public void RenameVariable_InsideIfCondition()
{
    string code = "if (foo > 0) { foo = 0; }";
    var result = _refactoring.RenameVariable(code, "foo", "bar");
    Assert.Equal("if (bar > 0) { bar = 0; }", result);
}
}
