using Xunit;
using RefactoringTool;

public class RefactoringTests
{
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
