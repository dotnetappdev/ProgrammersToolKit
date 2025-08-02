namespace ProgrammersToolKit.Services.Interfaces
{
    public interface IEncryptionToolService
    {
        string Encrypt(string input, string key, string algorithm);
        string Decrypt(string input, string key, string algorithm);
    }
}
