// ApplicationUser.cs
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? LastPasswordChange { get; set; }
    public bool IsActive { get; set; } = true;
    public string TimeZone { get; set; }
    public string Language { get; set; } = "fa-IR";

    // برای لاگین‌های چند دستگاهی
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    public virtual ICollection<LoginHistory> LoginHistories { get; set; }
}

// JwtSettings.cs
public class JwtSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpiration { get; set; } = 15; // دقیقه
    public int RefreshTokenExpirationDays { get; set; } = 7;
}

// RefreshToken.cs
public class RefreshToken
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedByIp { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string RevokedByIp { get; set; }
    public string ReplacedByToken { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt != null;
    public bool IsActive => !IsRevoked && !IsExpired;

    public virtual ApplicationUser User { get; set; }
}

// LoginHistory.cs
public class LoginHistory
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public string DeviceType { get; set; }
    public string Browser { get; set; }
    public string Platform { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public DateTime LoginTime { get; set; }
    public bool IsSuccessful { get; set; }
    public string FailureReason { get; set; }

    public virtual ApplicationUser User { get; set; }
}