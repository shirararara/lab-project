using Xunit;
using Core;
using System;


public class RefactoringTests
{
         // 1. Ìåòîä ³ñíóº â êîä³
        [Fact]
        public void MethodExists_WhenMethodPresent_ReturnsTrue()
        {
            string code = "void Calculate() { }";
            bool result = _refactoring.MethodExists(code, "Calculate");
            Assert.True(result);
        }

        // 2. Ìåòîä íå ³ñíóº â êîä³
        [Fact]
        public void MethodExists_WhenMethodAbsent_ReturnsFalse()
        {
            string code = "void Calculate() { }";
            bool result = _refactoring.MethodExists(code, "Print");
            Assert.False(result);
        }

        // 3. Ìåòîä ³ñíóº àëå êîä ïîðîæí³é
        [Fact]
        public void MethodExists_WhenSourceCodeEmpty_ReturnsFalse()
        {
            bool result = _refactoring.MethodExists("", "Calculate");
            Assert.False(result);
        }

        // 4. Äîäàâàííÿ ïàðàìåòðà — ñèãíàòóðà çì³íþºòüñÿ êîðåêòíî
        [Fact]
        public void AddParameter_MethodSignatureIsCorrect()
        {
            string code = "void Print() { }";
            string result = _refactoring.AddParameter(code, "Print", "string", "message");
            Assert.Contains("void Print(string message)", result);
        }

        // 5. Äîäàâàííÿ ïàðàìåòðà — ïàðàìåòð ïðèñóòí³é ó ðåçóëüòàò³
        [Fact]
        public void AddParameter_AddsParameterToMethod()
        {
            string code = "void Calculate() { }";
            string result = _refactoring.AddParameter(code, "Calculate", "int", "value");
            Assert.Contains("int value", result);
        }

        // 6. Äîäàâàííÿ ïàðàìåòðà äî ìåòîäó ç ³ñíóþ÷èìè ïàðàìåòðàìè
        [Fact]
        public void AddParameter_MethodAlreadyHasParams_AddsNewParam()
        {
            string code = "void Calculate(int x) { }";
            string result = _refactoring.AddParameter(code, "Calculate", "int", "y");
            Assert.Contains("int y", result);
        }

        // 7. Îíîâëåííÿ âèêëèê³â ìåòîäó — àðãóìåíò äîäàºòüñÿ
        [Fact]
        public void UpdateMethodCalls_AddsDefaultArgument()
        {
            string code = "void foo(){} void bar(){ foo(); }";
            string result = _refactoring.UpdateMethodCalls(code, "foo", "0");
            Assert.Contains("foo(0)", result);
        }

        // 8. Ïåðåâ³ðêà íàÿâíîñò³ ïàðàìåòðà — ïàðàìåòð ³ñíóº
        [Fact]
        public void HasParameter_WhenParameterExists_ReturnsTrue()
        {
            string code = "void Calculate(int value) { }";
            bool result = _refactoring.HasParameter(code, "Calculate", "value");
            Assert.True(result);
        }

        // 9. Âàë³äíå ³ìÿ ïàðàìåòðà
        [Fact]
        public void IsValidParameterName_ValidName_ReturnsTrue()
        {
            bool result = _refactoring.IsValidParameterName("myParam");
            Assert.True(result);
        }

        // 10. Íåâàë³äíå ³ìÿ — ïî÷èíàºòüñÿ ç öèôðè
        [Fact]
        public void IsValidParameterName_StartsWithDigit_ReturnsFalse()
        {
            bool result = _refactoring.IsValidParameterName("1param");
            Assert.False(result);
        }
}
