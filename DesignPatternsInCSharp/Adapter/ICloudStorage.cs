using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Adapter
{
    public interface ICloudStorage
    {
        Task<bool> UploadFileAsync(string fileName, byte[] content);
        Task<byte[]> DownloadFileAsync(string fileName);
        Task<bool> DeleteFileAsync(string fileName);
        Task<List<string>> ListFilesAsync();
    }

    // API گوگل درایو (شبیه‌سازی شده)
    public class GoogleDriveApi
    {
        public string UploadToDrive(string name, Stream data)
        {
            Console.WriteLine($"[Google Drive] آپلود: {name}");
            return Guid.NewGuid().ToString(); // File ID
        }

        public Stream DownloadFromDrive(string fileId)
        {
            Console.WriteLine($"[Google Drive] دانلود: {fileId}");
            return new MemoryStream(new byte[] { 1, 2, 3 });
        }

        public void RemoveFile(string fileId)
        {
            Console.WriteLine($"[Google Drive] حذف: {fileId}");
        }
    }

    // API Dropbox (شبیه‌سازی شده)
    public class DropboxClient
    {
        public async Task<string> Upload(string path, byte[] fileData)
        {
            await Task.Delay(100); // شبیه‌سازی عملیات async
            Console.WriteLine($"[Dropbox] آپلود به: {path}");
            return "success";
        }

        public async Task<byte[]> Download(string path)
        {
            await Task.Delay(100);
            Console.WriteLine($"[Dropbox] دانلود از: {path}");
            return new byte[] { 4, 5, 6 };
        }

        public async Task Delete(string path)
        {
            await Task.Delay(100);
            Console.WriteLine($"[Dropbox] حذف: {path}");
        }
    }

    // API Azure Blob Storage (شبیه‌سازی شده)
    public class AzureBlobService
    {
        public bool UploadBlob(string containerName, string blobName, byte[] data)
        {
            Console.WriteLine($"[Azure Blob] آپلود به Container: {containerName}/{blobName}");
            return true;
        }

        public byte[] DownloadBlob(string containerName, string blobName)
        {
            Console.WriteLine($"[Azure Blob] دانلود از: {containerName}/{blobName}");
            return new byte[] { 7, 8, 9 };
        }
    }

    // Adapter برای Google Drive
    public class GoogleDriveAdapter : ICloudStorage
    {
        private readonly GoogleDriveApi _api;
        private readonly Dictionary<string, string> _fileMap; // name -> fileId

        public GoogleDriveAdapter(GoogleDriveApi api)
        {
            _api = api;
            _fileMap = new Dictionary<string, string>();
        }

        public async Task<bool> UploadFileAsync(string fileName, byte[] content)
        {
            await Task.Run(() =>
            {
                using var stream = new MemoryStream(content);
                string fileId = _api.UploadToDrive(fileName, stream);
                _fileMap[fileName] = fileId;
            });
            return true;
        }

        public async Task<byte[]> DownloadFileAsync(string fileName)
        {
            return await Task.Run(() =>
            {
                if (!_fileMap.ContainsKey(fileName))
                    throw new FileNotFoundException();

                string fileId = _fileMap[fileName];
                using var stream = _api.DownloadFromDrive(fileId);
                using var ms = new MemoryStream();
                stream.CopyTo(ms);
                return ms.ToArray();
            });
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            await Task.Run(() =>
            {
                if (_fileMap.ContainsKey(fileName))
                {
                    _api.RemoveFile(_fileMap[fileName]);
                    _fileMap.Remove(fileName);
                }
            });
            return true;
        }

        public async Task<List<string>> ListFilesAsync()
        {
            return await Task.FromResult(_fileMap.Keys.ToList());
        }
    }

    // Adapter برای Dropbox
    public class DropboxAdapter : ICloudStorage
    {
        private readonly DropboxClient _client;
        private const string BasePath = "/MyApp/";

        public DropboxAdapter(DropboxClient client)
        {
            _client = client;
        }

        public async Task<bool> UploadFileAsync(string fileName, byte[] content)
        {
            string path = BasePath + fileName;
            string result = await _client.Upload(path, content);
            return result == "success";
        }

        public async Task<byte[]> DownloadFileAsync(string fileName)
        {
            string path = BasePath + fileName;
            return await _client.Download(path);
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            string path = BasePath + fileName;
            await _client.Delete(path);
            return true;
        }

        public async Task<List<string>> ListFilesAsync()
        {
            // در پیاده‌سازی واقعی، لیست فایل‌ها را از Dropbox می‌گیریم
            return await Task.FromResult(new List<string>());
        }
    }

    // Adapter برای Azure Blob
    public class AzureBlobAdapter : ICloudStorage
    {
        private readonly AzureBlobService _service;
        private const string ContainerName = "mycontainer";

        public AzureBlobAdapter(AzureBlobService service)
        {
            _service = service;
        }

        public async Task<bool> UploadFileAsync(string fileName, byte[] content)
        {
            return await Task.Run(() =>
                _service.UploadBlob(ContainerName, fileName, content)
            );
        }

        public async Task<byte[]> DownloadFileAsync(string fileName)
        {
            return await Task.Run(() =>
                _service.DownloadBlob(ContainerName, fileName)
            );
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            // Azure Blob حذف را پشتیبانی می‌کند
            await Task.CompletedTask;
            return true;
        }

        public async Task<List<string>> ListFilesAsync()
        {
            return await Task.FromResult(new List<string>());
        }
    }

    // سیستم مدیریت فایل
    public class FileManager
    {
        private readonly ICloudStorage _storage;

        public FileManager(ICloudStorage storage)
        {
            _storage = storage;
        }

        public async Task BackupFileAsync(string fileName, byte[] content)
        {
            Console.WriteLine($"\n🔄 در حال پشتیبان‌گیری از {fileName}...");
            bool success = await _storage.UploadFileAsync(fileName, content);

            if (success)
                Console.WriteLine("✅ پشتیبان‌گیری موفق");
            else
                Console.WriteLine("❌ خطا در پشتیبان‌گیری");
        }

        public async Task RestoreFileAsync(string fileName)
        {
            Console.WriteLine($"\n🔄 در حال بازیابی {fileName}...");
            byte[] content = await _storage.DownloadFileAsync(fileName);
            Console.WriteLine($"✅ فایل بازیابی شد ({content.Length} بایت)");
        }
    }

}
