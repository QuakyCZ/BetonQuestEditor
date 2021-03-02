namespace BetonQuestEditorApp.Model.Compiler
{
    public interface IStatement
    {
        string Compile(CompilerContext context);
    }
}
