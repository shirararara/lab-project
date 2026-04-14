using Xunit;
using Core;
using System;

namespace Tests
{
    public class UnitTest1
    {
        private AddParameterRefactoring _refactoring;

        public UnitTest1()
        {
            _refactoring = new AddParameterRefactoring();
        }

        // 1. Метод існує в коді
        [Fact]
        public void MethodExists_WhenMethodPresent_ReturnsTrue()
        {
            string code = "void Calculate() { }";
            bool result = _refactoring.MethodExists(code, "Calculate");
            Assert.True(result);
        }

        // 2. Метод не існує в коді
        [Fact]
        public void MethodExists_WhenMethodAbsent_ReturnsFalse()
        {
            string code = "void Calculate() { }";
            bool result = _refactoring.MethodExists(code, "Print");
            Assert.False(result);
        }

        // 3. Метод існує але код порожній
        [Fact]
        public void MethodExists_WhenSourceCodeEmpty_ReturnsFalse()
        {
            bool result = _refactoring.MethodExists("", "Calculate");
            Assert.False(result);
        }

        // 4. Додавання параметра — сигнатура змінюється коректно
        [Fact]
        public void AddParameter_MethodSignatureIsCorrect()
        {
            string code = "void Print() { }";
            string result = _refactoring.AddParameter(code, "Print", "string", "message");
            Assert.Contains("void Print(string message)", result);
        }

        // 5. Додавання параметра — параметр присутній у результаті
        [Fact]
        public void AddParameter_AddsParameterToMethod()
        {
            string code = "void Calculate() { }";
            string result = _refactoring.AddParameter(code, "Calculate", "int", "value");
            Assert.Contains("int value", result);
        }

        // 6. Додавання параметра до методу з існуючими параметрами
        [Fact]
        public void AddParameter_MethodAlreadyHasParams_AddsNewParam()
        {
            string code = "void Calculate(int x) { }";
            string result = _refactoring.AddParameter(code, "Calculate", "int", "y");
            Assert.Contains("int y", result);
        }

        // 7. Оновлення викликів методу — аргумент додається
        [Fact]
        public void UpdateMethodCalls_AddsDefaultArgument()
        {
            string code = "void foo(){} void bar(){ foo(); }";
            string result = _refactoring.UpdateMethodCalls(code, "foo", "0");
            Assert.Contains("foo(0)", result);
        }

        // 8. Перевірка наявності параметра — параметр існує
        [Fact]
        public void HasParameter_WhenParameterExists_ReturnsTrue()
        {
            string code = "void Calculate(int value) { }";
            bool result = _refactoring.HasParameter(code, "Calculate", "value");
            Assert.True(result);
        }

        // 9. Валідне імя параметра
        [Fact]
        public void IsValidParameterName_ValidName_ReturnsTrue()
        {
            bool result = _refactoring.IsValidParameterName("myParam");
            Assert.True(result);
        }

        // 10. Невалідне імя — починається з цифри
        [Fact]
        public void IsValidParameterName_StartsWithDigit_ReturnsFalse()
        {
            bool result = _refactoring.IsValidParameterName("1param");
            Assert.False(result);
        }
    }
}