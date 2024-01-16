namespace DiIocRehearsal.Contracts
{
    public interface IFileRepository
    {
        Task<string> WriteAsync(string name, string base64);
        Task<byte[]> ReadAsync(string fileName);
    }
}
