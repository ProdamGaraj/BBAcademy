namespace BLL.DocumentService;

public interface IDocumentService
{
    Task<string> Create(string filename, string folder, byte[] data);
}