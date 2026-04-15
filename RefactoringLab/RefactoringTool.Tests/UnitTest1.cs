using Xunit;
using RefactoringTool;

public class RefactoringTests
{
    private readonly RefactoringService _refactoring = new RefactoringService();

 // =========================================================
    // ВАРІАНТ 8 — Перейменування методу (Rename Method)
    // =========================================================

    // 1. Перевірка базового сценарію: заміна назви простого порожнього методу
    [Fact]
    public void RenameMethod_SimpleCase()
    {
        string code = "void foo() {}";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("void bar() {}", result);
    }

    // 2. Перевірка збереження параметрів у дужках при зміні назви методу
    [Fact]
    public void RenameMethod_WithParameters()
    {
        string code = "int sum(int a, int b) { return a + b; }";
        var result = _refactoring.RenameMethod(code, "sum", "add");
        Assert.Equal("int add(int a, int b) { return a + b; }", result);
    }

    // 3. Перевірка заміни всіх входжень, якщо метод викликається кілька разів у рядку
    [Fact]
    public void RenameMethod_MultipleCalls()
    {
        string code = "foo(); foo();";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("bar(); bar();", result);
    }

    // 4. Перевірка роботи рефакторингу всередині структури класу
    [Fact]
    public void RenameMethod_InsideClass()
    {
        string code = "class A { void foo() {} }";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("class A { void bar() {} }", result);
    }

    // 5. Перевірка того, що тип повернення методу (напр. int) залишається незмінним
    [Fact]
    public void RenameMethod_WithReturnType()
    {
        string code = "int foo() { return 1; }";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("int bar() { return 1; }", result);
    }

    // 6. Перевірка коректності заміни назви у викликах з реальними аргументами
    [Fact]
    public void RenameMethod_MethodCallWithArguments()
    {
        string code = "foo(5, 10);";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("bar(5, 10);", result);
    }

    // 7. Перевірка заміни назви методу, коли він є частиною математичного виразу
    [Fact]
    public void RenameMethod_MethodInExpression()
    {
        string code = "int x = foo() + 5;";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("int x = bar() + 5;", result);
    }

    // 8. Перевірка складних випадків: вкладені виклики методу в самого себе (foo(foo()))
    [Fact]
    public void RenameMethod_NestedCalls()
    {
        string code = "foo(foo());";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("bar(bar());", result);
    }

    // 9. Перевірка коректної обробки багаторядкового коду з переносами \n
    [Fact]
    public void RenameMethod_MultipleLines()
    {
        string code = "foo();\nfoo();";
        var result = _refactoring.RenameMethod(code, "foo", "bar");
        Assert.Equal("bar();\nbar();", result);
    }

    // 10. Перевірка стабільності при рекурсії та заміні імені на значно довше
    [Fact]
    public void RenameMethod_RenameToLongerName()
    {
        string code = "void foo() { foo(); }";
        var result = _refactoring.RenameMethod(code, "foo", "myMethod");
        Assert.Equal("void myMethod() { myMethod(); }", result);
    }

   // =========================================================
    // ВАРІАНТ 9 — Додавання параметра (Add Parameter)
    // =========================================================

    // 1. Перевірка позитивного сценарію: метод існує в коді
    [Fact]
    public void MethodExists_WhenMethodPresent_ReturnsTrue()
    {
        string code = "void Calculate() { }";
        bool result = _refactoring.MethodExists(code, "Calculate");
        Assert.True(result);
    }

    // 2. Перевірка негативного сценарію: пошук методу, якого немає в коді
    [Fact]
    public void MethodExists_WhenMethodAbsent_ReturnsFalse()
    {
        string code = "void Calculate() { }";
        bool result = _refactoring.MethodExists(code, "Print");
        // Заглушка возвращает False. Ожидаем True, чтобы тест упал.
        Assert.True(result, "Тест провален специально: нет реализации MethodExists");
    }

    // 3. Перевірка стійкості до порожнього вхідного тексту
    [Fact]
    public void MethodExists_WhenSourceCodeEmpty_ReturnsFalse()
    {
        bool result = _refactoring.MethodExists("", "Calculate");
        // Заглушка возвращает False. Ожидаем True, чтобы тест упал.
        Assert.True(result, "Тест провален специально: нет реализации MethodExists для пустой строки");
    }

    // 4. Перевірка правильності формування нового підпису (сигнатури) методу
    [Fact]
    public void AddParameter_MethodSignatureIsCorrect()
    {
        string code = "void Print() { }";
        string result = _refactoring.AddParameter(code, "Print", "string", "message");
        Assert.Contains("void Print(string message)", result);
    }

    // 5. Перевірка факту додавання типу та назви параметра в дужки методу
    [Fact]
    public void AddParameter_AddsParameterToMethod()
    {
        string code = "void Calculate() { }";
        string result = _refactoring.AddParameter(code, "Calculate", "int", "value");
        Assert.Contains("int value", result);
    }

    // 6. Перевірка додавання нового параметра до вже існуючого списку параметрів (через кому)
    [Fact]
    public void AddParameter_MethodAlreadyHasParams_AddsNewParam()
    {
        string code = "void Calculate(int x) { }";
        string result = _refactoring.AddParameter(code, "Calculate", "int", "y");
        Assert.Contains("int y", result);
    }

    // 7. Перевірка оновлення викликів методу: додавання аргументу за замовчуванням
    [Fact]
    public void UpdateMethodCalls_AddsDefaultArgument()
    {
        string code = "void foo(){} void bar(){ foo(); }";
        string result = _refactoring.UpdateMethodCalls(code, "foo", "0");
        Assert.Contains("foo(0)", result);
    }

    // 8. Перевірка пошуку конкретного параметра за назвою у сигнатурі методу
    [Fact]
    public void HasParameter_WhenParameterExists_ReturnsTrue()
    {
        string code = "void Calculate(int value) { }";
        bool result = _refactoring.HasParameter(code, "Calculate", "value");
        Assert.True(result);
    }

    // 9. Валідація назви: перевірка, що стандартна назва змінної вважається коректною
    [Fact]
    public void IsValidParameterName_ValidName_ReturnsTrue()
    {
        bool result = _refactoring.IsValidParameterName("myParam");
        Assert.True(result);
    }

  // 10. Валідація назви: перевірка заборони назв, що починаються з цифри (правила C#)
    [Fact]
    public void IsValidParameterName_StartsWithDigit_ReturnsFalse()
    {
        bool result = _refactoring.IsValidParameterName("1param");
        // Заглушка возвращает False. Ожидаем True, чтобы тест упал.
        Assert.True(result, "Тест провален специально: нет реализации IsValidParameterName");
    }

   // =========================================================
    // ВАРІАНТ 1 — Перейменування змінної (Rename Variable)
    // =========================================================

    // 1. Базовий сценарій: перейменування змінної при її оголошенні та ініціалізації
    [Fact]
    public void RenameVariable_SimpleCase()
    {    
        string code = "int foo = 5;";
        var result = _refactoring.RenameVariable(code, "foo", "bar");
        Assert.Equal("int bar = 5;", result);
    }

    // 2. Перевірка заміни назви змінної, коли вона використовується всередині математичного виразу
    [Fact]
    public void RenameVariable_UsedInExpression()
    {
        string code = "int x = foo + 5;";
        var result = _refactoring.RenameVariable(code, "foo", "bar");
        Assert.Equal("int x = bar + 5;", result);
    }

    // 3. Перевірка того, що всі входження змінної в рядку будуть замінені
    [Fact]
    public void RenameVariable_MultipleTimes()
    {
        string code = "foo = 1; foo = 2;";
        var result = _refactoring.RenameVariable(code, "foo", "bar");
        Assert.Equal("bar = 1; bar = 2;", result);
    }

    // 4. Перевірка коректності рефакторингу локальної змінної всередині тіла методу
    [Fact]
    public void RenameVariable_InsideMethod()
    {
        string code = "void Test() { int foo = 0; foo++; }";
        var result = _refactoring.RenameVariable(code, "foo", "bar");
        Assert.Equal("void Test() { int bar = 0; bar++; }", result);
    }

    // 5. Перевірка стабільності: якщо змінної з такою назвою немає, код не повинен змінитися
    [Fact]
    public void RenameVariable_NoMatch()
    {
        string code = "int x = 5;";
        var result = _refactoring.RenameVariable(code, "foo", "bar");
        // Заглушка возвращает ту же строку "int x = 5;". Ожидаем другой результат, чтобы тест упал.
        Assert.NotEqual("int x = 5;", result); 
    }

    // 6. Критична перевірка: назва не повинна змінюватися, якщо вона є лише частиною іншого слова (напр. fooBar)
    [Fact]
    public void RenameVariable_PartialMatchShouldNotChange()
    {
        string code = "int fooBar = 5;";
        var result = _refactoring.RenameVariable(code, "foo", "bar");
        // Заглушка возвращает ту же строку. Меняем ожидание на "сломанное", чтобы тест упал.
        Assert.Equal("ОЖИДАЕМ_ПРОВАЛ_НЕТ_КОДА", result);
    }

    // 7. Перевірка заміни назви змінної, коли вона передається як аргумент у виклик методу
    [Fact]
    public void RenameVariable_UsedAsArgument()
    {
        string code = "Print(foo);";
        var result = _refactoring.RenameVariable(code, "foo", "bar");
        Assert.Equal("Print(bar);", result);
    }

    // 8. Перевірка коректної обробки багаторядкового коду (заміна на різних рядках)
    [Fact]
    public void RenameVariable_MultipleLines()
    {
        string code = "int foo = 0;\nfoo = foo + 1;";
        var result = _refactoring.RenameVariable(code, "foo", "bar");
        Assert.Equal("int bar = 0;\nbar = bar + 1;", result);
    }

    // 9. Перевірка заміни ідентифікатора на довшу назву (збереження цілісності виразів)
    [Fact]
    public void RenameVariable_RenameToLongerName()
    {
        string code = "int x = 5; int y = x + 1;";
        var result = _refactoring.RenameVariable(code, "x", "myVar");
        Assert.Equal("int myVar = 5; int y = myVar + 1;", result);
    }

    // 10. Перевірка заміни змінної, що використовується в умові оператора if
    [Fact]
    public void RenameVariable_InsideIfCondition()
    {
        string code = "if (foo > 0) { foo = 0; }";
        var result = _refactoring.RenameVariable(code, "foo", "bar");
        Assert.Equal("if (bar > 0) { bar = 0; }", result);
    }
    // =========================================================
    // ВАРІАНТ 2 — Заміна магічного числа символічною константою
    // =========================================================
    // 1. Базова заміна числа на константу
    [Fact]
    public void ReplaceMagicNumber_AddsConstantAndReplacesNumber()
    {
        string code = "if (x > 42) return 42;";
        string result = _refactoring.ReplaceMagicNumber(code, "42", "MAX_SIZE");
        Assert.Contains("const int MAX_SIZE = 42;", result);
        Assert.Contains("MAX_SIZE", result);
        Assert.DoesNotContain("42", result);
    }

    // 2. Порожній код
    [Fact]
    public void ReplaceMagicNumber_EmptyCode()
    {
        string result = _refactoring.ReplaceMagicNumber("", "42", "MAX_SIZE");
        Assert.Equal("const int MAX_SIZE = 42;\n", result);
    }

    // 3. Числа немає в коді
    [Fact]
    public void ReplaceMagicNumber_NumberNotInCode()
    {
        string code = "int x = 10;";
        string result = _refactoring.ReplaceMagicNumber(code, "42", "MAX_SIZE");
        Assert.Equal("const int MAX_SIZE = 42;\nint x = 10;", result);
    }

    // 4. Число зустрічається кілька разів
    [Fact]
    public void ReplaceMagicNumber_ReplacesAllOccurrences()
    {
        string code = "int a = 100; int b = 100; int c = 100;";
        string result = _refactoring.ReplaceMagicNumber(code, "100", "LIMIT");
        Assert.DoesNotContain("100", result);
        Assert.Equal(3, result.Split("LIMIT").Length - 1 - 1);
    }

    // 5. Константа додається на початок файлу
    [Fact]
    public void ReplaceMagicNumber_ConstantIsAtTheTop()
    {
        string code = "int x = 99;";
        string result = _refactoring.ReplaceMagicNumber(code, "99", "MAX_VAL");
        Assert.StartsWith("const int MAX_VAL = 99;", result);
    }

    // 6. Інше число та інша назва константи
    [Fact]
    public void ReplaceMagicNumber_DifferentNumberAndName()
    {
        string code = "for (int i = 0; i < 255; i++)";
        string result = _refactoring.ReplaceMagicNumber(code, "255", "MAX_BYTE");
        Assert.Contains("const int MAX_BYTE = 255;", result);
        Assert.Contains("MAX_BYTE", result);
        Assert.DoesNotContain("255", result);
    }

    // 7. Код з кількома рядками
    [Fact]
    public void ReplaceMagicNumber_MultilineCode()
    {
        string code = "int a = 10;\nint b = 10;\nreturn 10;";
        string result = _refactoring.ReplaceMagicNumber(code, "10", "TEN");
        Assert.Contains("const int TEN = 10;", result);
        Assert.DoesNotContain("= 10", result);
    }

    // 8. Між константою та кодом є перенос рядка
    [Fact]
    public void ReplaceMagicNumber_HasNewlineBetweenConstantAndCode()
    {
        string code = "int x = 5;";
        string result = _refactoring.ReplaceMagicNumber(code, "5", "FIVE");
        Assert.Contains("\n", result);
    }

    // 9. Назва константи присутня в результаті
    [Fact]
    public void ReplaceMagicNumber_ResultContainsConstantName()
    {
        string code = "int timeout = 30;";
        string result = _refactoring.ReplaceMagicNumber(code, "30", "TIMEOUT_SECONDS");
        Assert.Contains("TIMEOUT_SECONDS", result);
    }

    // 10. Оголошення константи має правильний формат
    [Fact]
    public void ReplaceMagicNumber_ConstantHasCorrectFormat()
    {
        string code = "int x = 7;";
        string result = _refactoring.ReplaceMagicNumber(code, "7", "DAYS_IN_WEEK");
        Assert.StartsWith("const int DAYS_IN_WEEK = 7;", result);
    }
}
