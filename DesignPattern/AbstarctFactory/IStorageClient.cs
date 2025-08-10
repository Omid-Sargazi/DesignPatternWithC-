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

    public interface IStorageSuiteFactory
    {
        IStorageClient CreateClient();
        IUrlSigner CreateSigner();
        IMetadataReader CreateMetadata();
    }

    public sealed class S3Client : IStorageClient
    {
        public Task DeleteAsync(string key, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> DownloadAsync(string key, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task UploadAsync(string key, Stream content, string contentType, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class S3Signer : IUrlSigner
    {
        public Uri GetSignedReadUrl(string key, TimeSpan ttl)
        {
            throw new NotImplementedException();
        }

        public Uri GetSignedWriteUrl(string key, TimeSpan ttl)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class S3Metadata : IMetadataReader
    {
        public Task<IDictionary<string, string>> GetMetadataAsync(string key, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class S3SuiteFactory : IStorageSuiteFactory
    {
        public IStorageClient CreateClient()
        {
            return new S3Client();
        }

        public IMetadataReader CreateMetadata()
        {
            return new S3Metadata();
        }

        public IUrlSigner CreateSigner()
        {
            return new S3Signer();
        }
    }

    public class AzureBlobClient : IStorageClient
    {
        public Task UploadAsync(string key, Stream content, string contentType, CancellationToken ct = default) => Task.CompletedTask;
        public Task<Stream> DownloadAsync(string key, CancellationToken ct = default) => Task.FromResult<Stream>(Stream.Null);
        public Task DeleteAsync(string key, CancellationToken ct = default) => Task.CompletedTask;
    }

    public class AzureBlobSigner : IUrlSigner
    {
        public Uri GetSignedReadUrl(string key, TimeSpan ttl) => new($"https://azure.fake/{key}?read");
        public Uri GetSignedWriteUrl(string key, TimeSpan ttl) => new($"https://azure.fake/{key}?write");
    }

    public class AzureBlobMetadata : IMetadataReader
    {
        public Task<IDictionary<string, string>> GetMetadataAsync(string key, CancellationToken ct = default)
         => Task.FromResult<IDictionary<string, string>>(new Dictionary<string, string> { { "provider", "azure" } });
    }

    public sealed class AzureSuiteFactory : IStorageSuiteFactory
    {
        public IStorageClient CreateClient() => new AzureBlobClient();
        public IUrlSigner CreateSigner() => new AzureBlobSigner();
        public IMetadataReader CreateMetadata() => new AzureBlobMetadata();
    }
}
