using Microsoft.Extensions.Caching.Memory;
using System.Net;
using Newtonsoft.Json;

namespace API.Services
{
    public interface IGeoLocationService
    {
        Task<LocationInfo> GetLocationAsync(string ipAddress);
        Task<bool> IsIpValidAsync(string ipAddress);
        Task<string> GetISPAsync(string ipAddress);
    }

    public class LocationInfo
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string TimeZone { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ISP { get; set; }
        public bool IsProxy { get; set; }
        public bool IsHosting { get; set; }
    }

    public class GeoLocationService : IGeoLocationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GeoLocationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(24);

        public GeoLocationService(
            HttpClient httpClient,
            ILogger<GeoLocationService> logger,
            IConfiguration configuration,
            IMemoryCache cache)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<LocationInfo> GetLocationAsync(string ipAddress)
        {
            try
            {
                // بررسی کش
                var cacheKey = $"GeoLocation_{ipAddress}";
                if (_cache.TryGetValue<LocationInfo>(cacheKey, out var cachedLocation))
                {
                    return cachedLocation;
                }

                // بررسی IPهای خصوصی
                if (IsPrivateIp(ipAddress))
                {
                    return new LocationInfo
                    {
                        Country = "محلی",
                        City = "شبکه داخلی",
                        ISP = "شبکه خصوصی"
                    };
                }

                // استفاده از سرویس IPInfo
                var apiToken = _configuration["IPInfo:Token"];
                var apiUrl = $"https://ipinfo.io/{ipAddress}/json?token={apiToken}";

                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var ipInfo = JsonConvert.DeserializeObject<IPInfoResponse>(content);

                    var location = new LocationInfo
                    {
                        Country = ipInfo.Country,
                        CountryCode = ipInfo.CountryCode,
                        Region = ipInfo.Region,
                        City = ipInfo.City,
                        TimeZone = ipInfo.Timezone,
                        ISP = ipInfo.Org,
                        IsProxy = await IsProxyAsync(ipAddress),
                        IsHosting = IsHostingProvider(ipInfo.Org)
                    };

                    if (!string.IsNullOrEmpty(ipInfo.Loc))
                    {
                        var locParts = ipInfo.Loc.Split(',');
                        if (locParts.Length == 2)
                        {
                            location.Latitude = double.Parse(locParts[0]);
                            location.Longitude = double.Parse(locParts[1]);
                        }
                    }

                    // ذخیره در کش
                    _cache.Set(cacheKey, location, _cacheDuration);

                    return location;
                }

                _logger.LogWarning("خطا در دریافت اطلاعات موقعیت برای IP: {IpAddress}", ipAddress);
                return GetDefaultLocation();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در دریافت اطلاعات جغرافیایی برای IP: {IpAddress}", ipAddress);
                return GetDefaultLocation();
            }
        }

        public async Task<bool> IsIpValidAsync(string ipAddress)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ipAddress))
                    return false;

                if (!IPAddress.TryParse(ipAddress, out var ip))
                    return false;

                // بررسی IPهای رزرو شده و خصوصی
                if (ip.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                    return false;

                if (IsPrivateIp(ipAddress) || IsReservedIp(ipAddress))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetISPAsync(string ipAddress)
        {
            var location = await GetLocationAsync(ipAddress);
            return location?.ISP ?? "نامشخص";
        }

        private bool IsPrivateIp(string ipAddress)
        {
            try
            {
                var ip = IPAddress.Parse(ipAddress);

                // 10.0.0.0 - 10.255.255.255
                if (ip.GetAddressBytes()[0] == 10)
                    return true;

                // 172.16.0.0 - 172.31.255.255
                if (ip.GetAddressBytes()[0] == 172 && ip.GetAddressBytes()[1] >= 16 && ip.GetAddressBytes()[1] <= 31)
                    return true;

                // 192.168.0.0 - 192.168.255.255
                if (ip.GetAddressBytes()[0] == 192 && ip.GetAddressBytes()[1] == 168)
                    return true;

                // 127.0.0.0 - 127.255.255.255
                if (ip.GetAddressBytes()[0] == 127)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsReservedIp(string ipAddress)
        {
            try
            {
                var ip = IPAddress.Parse(ipAddress);

                // بررسی محدوده‌های رزرو شده
                var bytes = ip.GetAddressBytes();

                // 0.0.0.0/8
                if (bytes[0] == 0)
                    return true;

                // 100.64.0.0/10
                if (bytes[0] == 100 && bytes[1] >= 64 && bytes[1] <= 127)
                    return true;

                // 169.254.0.0/16
                if (bytes[0] == 169 && bytes[1] == 254)
                    return true;

                // 224.0.0.0/4
                if (bytes[0] >= 224 && bytes[0] <= 239)
                    return true;

                // 240.0.0.0/4
                if (bytes[0] >= 240)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> IsProxyAsync(string ipAddress)
        {
            try
            {
                var apiKey = _configuration["ProxyCheck:ApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                    return false;

                var url = $"http://proxycheck.io/v2/{ipAddress}?key={apiKey}&vpn=1&asn=1";
                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<ProxyCheckResponse>(response);

                return result?.Status == "ok" && result.Data.ContainsKey(ipAddress) &&
                       result.Data[ipAddress].Proxy == "yes";
            }
            catch
            {
                return false;
            }
        }

        private bool IsHostingProvider(string isp)
        {
            if (string.IsNullOrEmpty(isp))
                return false;

            var hostingKeywords = new[]
            {
                "hosting", "server", "datacenter", "cloud", "aws", "azure",
                "google cloud", "digitalocean", "linode", "vultr", "ovh"
            };

            return hostingKeywords.Any(keyword =>
                isp.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        private LocationInfo GetDefaultLocation()
        {
            return new LocationInfo
            {
                Country = "نامشخص",
                City = "نامشخص",
                ISP = "نامشخص"
            };
        }
    }

    public class IPInfoResponse
    {
        public string Ip { get; set; }
        public string Hostname { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Loc { get; set; }
        public string Org { get; set; }
        public string Postal { get; set; }
        public string Timezone { get; set; }
    }

    public class ProxyCheckResponse
    {
        public string Status { get; set; }
        public Dictionary<string, ProxyCheckData> Data { get; set; }
    }

    public class ProxyCheckData
    {
        public string Proxy { get; set; }
        public string Type { get; set; }
    }
}