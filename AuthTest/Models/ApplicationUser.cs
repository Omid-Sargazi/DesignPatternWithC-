// ApplicationUser.cs

using Microsoft.AspNetCore.Identity;

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

