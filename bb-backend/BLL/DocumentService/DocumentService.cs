using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.Configs;

namespace BLL.DocumentService;

public class DocumentService : IDocumentService
{
    private readonly string _staticFilesPath;

    private readonly ILogger<DocumentService> _logger;

    public DocumentService(IOptions<StaticConfig> staticConfig, ILogger<DocumentService> logger)
    {
        _logger = logger;
        _staticFilesPath = staticConfig.Value.StaticFilesPath;
    }

    private void EnsureSubFolderExist(string subfolder)
    {
        var path = Path.Combine(_staticFilesPath, subfolder);
        if (Directory.Exists(path)) return;
        _logger.LogWarning($"SubFolder \"{subfolder}\" Is Missing, Creating");
        Directory.CreateDirectory(path);
    }

    public async Task<string> Create(string filename, string folder, byte[] data)
    {
        EnsureSubFolderExist(folder);

        var guid = Guid.NewGuid()
            .ToString("N");
        var guidFileName = guid + filename.Substring(filename.LastIndexOf('.'));
        var folderPath = Path.Combine(_staticFilesPath, folder);
        var filePath = Path.Combine(folderPath, guidFileName);

        await using var fileStream = new FileStream(filePath, FileMode.CreateNew);
        await fileStream.WriteAsync(data.AsMemory(0, data.Length));
        return guidFileName;
    }
}