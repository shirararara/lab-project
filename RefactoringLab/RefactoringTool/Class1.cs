namespace RefactoringTool;

public class RefactoringService
{
    public string RenameMethod(string code, string oldName, string newName)
    {
        return code;
    }

    public bool MethodExists(string code, string methodName)
    {
        return false;
    }

    public string AddParameter(string code, string methodName, string type, string paramName)
    {
        return code;
    }

    public string UpdateMethodCalls(string code, string methodName, string defaultValue)
    {
        return code;
    }

    public bool HasParameter(string code, string methodName, string paramName)
    {
        return false;
    }

    public bool IsValidParameterName(string name)
    {
        return false;
    }
}
