namespace ProgrammersToolKit.Services.Interfaces
{
    public interface ICodeRunnerService
    {
        Task<string> RunCodeAsync(string code, string language);
        Task<string> RunCodeWithInputAsync(string code, string language, string input);
    }
}
