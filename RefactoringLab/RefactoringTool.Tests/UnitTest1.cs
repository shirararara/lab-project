using Xunit;
using RefactoringTool; 
public class RefactoringTests
{
  [Fact]
public void RenameMethod_SimpleCase()
{
    var tool = new RefactoringService();
    string code = "void foo() {}";
    var result = tool.RenameMethod(code, "foo", "bar");
    Assert.Equal("void bar() {}", result);
}

[Fact]
public void RenameMethod_WithParameters()
{
    var tool = new RefactoringService();
    string code = "int sum(int a, int b) { return a + b; }";
    var result = tool.RenameMethod(code, "sum", "add");
    Assert.Equal("int add(int a, int b) { return a + b; }", result);
}

[Fact]
public void RenameMethod_MultipleCalls()
{
    var tool = new RefactoringService();
    string code = "foo(); foo();";
    var result = tool.RenameMethod(code, "foo", "bar");
    Assert.Equal("bar(); bar();", result);
}

[Fact]
public void RenameMethod_InsideClass()
{
    var tool = new RefactoringService();
    string code = "class A { void foo() {} }";
    var result = tool.RenameMethod(code, "foo", "bar");
    Assert.Equal("class A { void bar() {} }", result);
}

[Fact]
public void RenameMethod_WithReturnType()
{
    var tool = new RefactoringService();
    string code = "int foo() { return 1; }";
    var result = tool.RenameMethod(code, "foo", "bar");
    Assert.Equal("int bar() { return 1; }", result);
}

[Fact]
public void RenameMethod_MethodCallWithArguments()
{
    var tool = new RefactoringService();
    string code = "foo(5, 10);";
    var result = tool.RenameMethod(code, "foo", "bar");
    Assert.Equal("bar(5, 10);", result);
}

[Fact]
public void RenameMethod_MethodInExpression()
{
    var tool = new RefactoringService();
    string code = "int x = foo() + 5;";
    var result = tool.RenameMethod(code, "foo", "bar");
    Assert.Equal("int x = bar() + 5;", result);
}

[Fact]
public void RenameMethod_NestedCalls()
{
    var tool = new RefactoringService();
    string code = "foo(foo());";
    var result = tool.RenameMethod(code, "foo", "bar");
    Assert.Equal("bar(bar());", result);
}

[Fact]
public void RenameMethod_MultipleLines()
{
    var tool = new RefactoringService();
    string code = "foo();\nfoo();";
    var result = tool.RenameMethod(code, "foo", "bar");
    Assert.Equal("bar();\nbar();", result);
}

[Fact]
public void RenameMethod_RenameToLongerName()
{
    var tool = new RefactoringService();
    string code = "void foo() { foo(); }";
    var result = tool.RenameMethod(code, "foo", "myMethod");
    Assert.Equal("void myMethod() { myMethod(); }", result);
}
}