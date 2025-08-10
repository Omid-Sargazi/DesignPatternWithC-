using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.AbstarctFactory
{
    public interface IStorageClient
    {
        Task UploadAsync(string key, Stream content, string contentType, CancellationToken ct = default);
        Task<Stream> DownloadAsync(string key, CancellationToken ct = default);
        Task DeleteAsync(string key, CancellationToken ct = default);
    }

    public interface IUrlSigner
    {
        Uri GetSignedReadUrl(string key, TimeSpan ttl);
        Uri GetSignedWriteUrl(string key, TimeSpan ttl);
    }

    public interface IMetadataReader
    {
        Task<IDictionary<string, string>> GetMetadataAsync(string key, CancellationToken ct = default);
    }

}
