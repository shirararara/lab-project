
namespace RefactoringTool;

public class RefactoringService
{
    // ===== Антон Приймич =====
    public string RenameMethod(string code, string oldName, string newName)
    {
        return code.Replace(oldName, newName);
    }
    // ====================================

    // ===== Бобиль Микола =====
    public string ReplaceMagicNumber(string code, string magicNumber, string constantName)
    {
        string constant = $"const int {constantName} = {magicNumber};";
        string newCode = code.Replace(magicNumber, constantName);
        return constant + "\n" + newCode;
    }
    // ========================================================================
}
