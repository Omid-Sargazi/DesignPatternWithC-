using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region DbSetها

        // احراز هویت و کاربران
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<TwoFactorRecoveryCode> TwoFactorRecoveryCodes { get; set; }
        public DbSet<TwoFactorValidationHistory> TwoFactorValidationHistories { get; set; }

        // پروفایل و تنظیمات
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        // امنیت و دسترسی
        public DbSet<SecurityLog> SecurityLogs { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        // جلسات و دستگاه‌ها
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Device> Devices { get; set; }

        // آمار و گزارشات
        public DbSet<AuditLog> AuditLogs { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // اعمال کانفیگ‌های از کلاس‌های جداگانه
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // کانفیگ پیش‌فرض برای ApplicationUser
            ConfigureApplicationUser(builder);

            // کانفیگ پیش‌فرض برای ApplicationRole
            ConfigureApplicationRole(builder);

            // کانفیگ سایر موجودیت‌ها
            ConfigureRefreshToken(builder);
            ConfigureLoginHistory(builder);
            ConfigureTwoFactorRecoveryCode(builder);
            ConfigureUserProfile(builder);
            ConfigureUserSetting(builder);
            ConfigureNotification(builder);
            ConfigureSecurityLog(builder);
            ConfigureApiKey(builder);
            ConfigurePermission(builder);
            ConfigureUserPermission(builder);
            ConfigureUserSession(builder);
            ConfigureDevice(builder);
            ConfigureAuditLog(builder);

            // تنظیمات نامگذاری جدول‌ها
            ConfigureTableNames(builder);

            // تنظیمات ایندکس‌ها
            ConfigureIndexes(builder);

            // تنظیمات داده‌های اولیه
            SeedData(builder);
        }

        #region متدهای کانفیگ

        private void ConfigureApplicationUser(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(entity =>
            {
                //entity.ToTable("Users");

                // ویژگی‌های اضافی
                entity.Property(u => u.FirstName)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(u => u.LastName)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(u => u.FullName)
                    .HasMaxLength(201);
                    //.HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

                entity.Property(u => u.AvatarUrl)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(u => u.CreatedAt)
                    ////.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(u => u.LastLogin)
                    .IsRequired(false);

                entity.Property(u => u.LastPasswordChange)
                    .IsRequired(false);

                entity.Property(u => u.IsActive);
                    ////.HasDefaultValue(true);

                entity.Property(u => u.TimeZone)
                    .HasMaxLength(50)
                    ////.HasDefaultValue("Asia/Tehran")
                    .IsRequired(false);

                entity.Property(u => u.Language)
                    .HasMaxLength(10);
                    ////.HasDefaultValue("fa-IR");

                entity.Property(u => u.DateOfBirth)
                    .IsRequired(false);

                entity.Property(u => u.Gender)
                    .HasMaxLength(10)
                    .IsRequired(false);

                entity.Property(u => u.Address)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(u => u.City)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(u => u.Country)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(u => u.PostalCode)
                    .HasMaxLength(20)
                    .IsRequired(false);

                entity.Property(u => u.Bio)
                    .HasMaxLength(1000)
                    .IsRequired(false);

                entity.Property(u => u.Website)
                    .HasMaxLength(200)
                    .IsRequired(false);

                entity.Property(u => u.SocialMediaLinks)
                    ////.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                // ایندکس‌ها
                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.HasIndex(u => u.NormalizedUserName)
                    .IsUnique();
                    //.HasDatabaseName("UserNameIndex");

                    entity.HasIndex(u => u.NormalizedEmail);
                    //.HasDatabaseName("EmailIndex");

                entity.HasIndex(u => u.PhoneNumber)
                    .IsUnique(false);

                entity.HasIndex(u => new { u.FirstName, u.LastName });

                entity.HasIndex(u => u.CreatedAt);

                entity.HasIndex(u => u.LastLogin);

                entity.HasIndex(u => u.IsActive);

                // رابطه‌ها
                entity.HasMany(u => u.RefreshTokens)
                    .WithOne(rt => rt.User)
                    .HasForeignKey(rt => rt.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.LoginHistories)
                    .WithOne(lh => lh.User)
                    .HasForeignKey(lh => lh.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.TwoFactorRecoveryCodes)
                    .WithOne()
                    .HasForeignKey(tfrc => tfrc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Notifications)
                    .WithOne(n => n.User)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.SecurityLogs)
                    .WithOne(sl => sl.User)
                    .HasForeignKey(sl => sl.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.ApiKeys)
                    .WithOne(ak => ak.User)
                    .HasForeignKey(ak => ak.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.UserPermissions)
                    .WithOne(up => up.User)
                    .HasForeignKey(up => up.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.UserSessions)
                    .WithOne(us => us.User)
                    .HasForeignKey(us => us.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Devices)
                    .WithOne(d => d.User)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.AuditLogs)
                    .WithOne(al => al.User)
                    .HasForeignKey(al => al.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }

        private void ConfigureApplicationRole(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>(entity =>
            {
                ////entity.ToTable("Roles");

                entity.Property(r => r.Description)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(r => r.CreatedAt)
                    ////.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(r => r.UpdatedAt)
                    .IsRequired(false);

                entity.Property(r => r.IsSystemRole);
                    ////.HasDefaultValue(false);

                entity.Property(r => r.Permissions)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                // ایندکس‌ها
                entity.HasIndex(r => r.NormalizedName)
                    .IsUnique();
                    //.HasDatabaseName("RoleNameIndex");

                entity.HasIndex(r => r.IsSystemRole);

                // رابطه‌ها
                entity.HasMany(r => r.RolePermissions)
                    .WithOne(rp => rp.Role)
                    .HasForeignKey(rp => rp.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureRefreshToken(ModelBuilder builder)
        {
            builder.Entity<RefreshToken>(entity =>
            {
                ////entity.ToTable("RefreshTokens");

                entity.HasKey(rt => rt.Id);

                entity.Property(rt => rt.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(rt => rt.Token)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(rt => rt.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(rt => rt.ExpiresAt)
                    .IsRequired();

                entity.Property(rt => rt.CreatedAt)
                    ////.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(rt => rt.CreatedByIp)
                    .HasMaxLength(45)
                    .IsRequired(false);

                entity.Property(rt => rt.RevokedAt)
                    .IsRequired(false);

                entity.Property(rt => rt.RevokedByIp)
                    .HasMaxLength(45)
                    .IsRequired(false);

                entity.Property(rt => rt.ReplacedByToken)
                    .HasMaxLength(500)
                    .IsRequired(false);

                // ایندکس‌ها
                entity.HasIndex(rt => rt.UserId);
                entity.HasIndex(rt => rt.Token);
                entity.HasIndex(rt => rt.ExpiresAt);
                entity.HasIndex(rt => rt.CreatedAt);
                entity.HasIndex(rt => new { rt.UserId, rt.IsActive });

                // رابطه‌ها
                entity.HasOne(rt => rt.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(rt => rt.UserId);
            });
        }

        private void ConfigureLoginHistory(ModelBuilder builder)
        {
            builder.Entity<LoginHistory>(entity =>
            {
                ////entity.ToTable("LoginHistories");

                entity.HasKey(lh => lh.Id);

                entity.Property(lh => lh.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(lh => lh.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(lh => lh.IpAddress)
                    .HasMaxLength(45)
                    .IsRequired(false);

                entity.Property(lh => lh.UserAgent)
                    ////.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(lh => lh.DeviceType)
                    .HasMaxLength(50)
                    .IsRequired(false);

                entity.Property(lh => lh.Browser)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(lh => lh.Platform)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(lh => lh.Country)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(lh => lh.City)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(lh => lh.LoginTime)
                    ////.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(lh => lh.IsSuccessful)
                    .IsRequired();

                entity.Property(lh => lh.FailureReason)
                    .HasMaxLength(500)
                    .IsRequired(false);

                // ایندکس‌ها
                entity.HasIndex(lh => lh.UserId);
                entity.HasIndex(lh => lh.IpAddress);
                entity.HasIndex(lh => lh.LoginTime);
                entity.HasIndex(lh => lh.IsSuccessful);
                entity.HasIndex(lh => new { lh.UserId, lh.LoginTime });

                // رابطه‌ها
                entity.HasOne(lh => lh.User)
                    .WithMany(u => u.LoginHistories)
                    .HasForeignKey(lh => lh.UserId);
            });
        }

        private void ConfigureTwoFactorRecoveryCode(ModelBuilder builder)
        {
            builder.Entity<TwoFactorRecoveryCode>(entity =>
            {
                ////entity.ToTable("TwoFactorRecoveryCodes");

                entity.HasKey(tfrc => tfrc.Id);

                entity.Property(tfrc => tfrc.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(tfrc => tfrc.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(tfrc => tfrc.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(tfrc => tfrc.IsUsed);
                    ////.HasDefaultValue(false);

                entity.Property(tfrc => tfrc.GeneratedAt)
                    ////.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(tfrc => tfrc.ExpiresAt)
                    .IsRequired();

                entity.Property(tfrc => tfrc.UsedAt)
                    .IsRequired(false);

                // ایندکس‌ها
                entity.HasIndex(tfrc => tfrc.UserId);
                entity.HasIndex(tfrc => tfrc.Code);
                entity.HasIndex(tfrc => tfrc.ExpiresAt);
                entity.HasIndex(tfrc => tfrc.IsUsed);
                entity.HasIndex(tfrc => new { tfrc.UserId, tfrc.Code, tfrc.IsUsed });
            });
        }

        private void ConfigureTwoFactorValidationHistory(ModelBuilder builder)
        {
            builder.Entity<TwoFactorValidationHistory>(entity =>
            {
                ////entity.ToTable("TwoFactorValidationHistories");

                entity.HasKey(tfvh => tfvh.Id);

                entity.Property(tfvh => tfvh.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(tfvh => tfvh.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(tfvh => tfvh.ValidatedAt)
                    ////.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(tfvh => tfvh.IpAddress)
                    .HasMaxLength(45)
                    .IsRequired(false);

                entity.Property(tfvh => tfvh.IsSuccessful)
                    .IsRequired();

                // ایندکس‌ها
                entity.HasIndex(tfvh => tfvh.UserId);
                entity.HasIndex(tfvh => tfvh.ValidatedAt);
                entity.HasIndex(tfvh => tfvh.IsSuccessful);
            });
        }

        private void ConfigureUserProfile(ModelBuilder builder)
        {
            builder.Entity<UserProfile>(entity =>
            {
                //entity.ToTable("UserProfiles");

                entity.HasKey(up => up.Id);

                entity.Property(up => up.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(up => up.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(up => up.Title)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(up => up.Company)
                    .HasMaxLength(200)
                    .IsRequired(false);

                entity.Property(up => up.Department)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(up => up.JobTitle)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(up => up.Skills)
                    ////.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(up => up.Interests)
                    ////.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(up => up.Education)
                    ////.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(up => up.Experience)
                    ////.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(up => up.Certifications)
                    ////.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(up => up.Preferences)
                    ////.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(up => up.CreatedAt)
                    ////.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(up => up.UpdatedAt)
                    .IsRequired(false);

                // ایندکس‌ها
                entity.HasIndex(up => up.UserId)
                    .IsUnique();

                // رابطه‌ها
                entity.HasOne(up => up.User)
                    .WithOne()
                    .HasForeignKey<UserProfile>(up => up.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureUserSetting(ModelBuilder builder)
        {
            builder.Entity<UserSetting>(entity =>
            {
                ////entity.ToTable("UserSettings");

                entity.HasKey(us => us.Id);

                entity.Property(us => us.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(us => us.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(us => us.SettingKey)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(us => us.SettingValue)
                    //./HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(us => us.DataType)
                    .HasMaxLength(50);
                    //.HasDefaultValue("string");

                entity.Property(us => us.Category)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(us => us.UpdatedAt)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAddOrUpdate();

                // ایندکس‌ها
                entity.HasIndex(us => us.UserId);
                entity.HasIndex(us => us.SettingKey);
                entity.HasIndex(us => us.Category);
                entity.HasIndex(us => new { us.UserId, us.SettingKey })
                    .IsUnique();

                // رابطه‌ها
                entity.HasOne(us => us.User)
                    .WithMany()
                    .HasForeignKey(us => us.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureNotification(ModelBuilder builder)
        {
            builder.Entity<Notification>(entity =>
            {
                //entity.ToTable("Notifications");

                entity.HasKey(n => n.Id);

                entity.Property(n => n.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(n => n.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(n => n.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(n => n.Message)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired();

                entity.Property(n => n.Type)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(n => n.Priority)
                    .HasMaxLength(20);
                    //.HasDefaultValue("Normal");

                    entity.Property(n => n.IsRead);
                    //.HasDefaultValue(false);

                    entity.Property(n => n.IsArchived);
                    //.HasDefaultValue(false);

                entity.Property(n => n.CreatedAt)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(n => n.ReadAt)
                    .IsRequired(false);

                entity.Property(n => n.Data)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(n => n.ActionUrl)
                    .HasMaxLength(500)
                    .IsRequired(false);

                // ایندکس‌ها
                entity.HasIndex(n => n.UserId);
                entity.HasIndex(n => n.Type);
                entity.HasIndex(n => n.Priority);
                entity.HasIndex(n => n.IsRead);
                entity.HasIndex(n => n.CreatedAt);
                entity.HasIndex(n => new { n.UserId, n.IsRead, n.CreatedAt });

                // رابطه‌ها
                entity.HasOne(n => n.User)
                    .WithMany(u => u.Notifications)
                    .HasForeignKey(n => n.UserId);
            });
        }

        private void ConfigureSecurityLog(ModelBuilder builder)
        {
            builder.Entity<SecurityLog>(entity =>
            {
                //entity.ToTable("SecurityLogs");

                entity.HasKey(sl => sl.Id);

                entity.Property(sl => sl.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(sl => sl.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(sl => sl.EventType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(sl => sl.Severity)
                    .HasMaxLength(20);
                    //.HasDefaultValue("Info");

                entity.Property(sl => sl.IpAddress)
                    .HasMaxLength(45)
                    .IsRequired(false);

                entity.Property(sl => sl.UserAgent)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(sl => sl.Details)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(sl => sl.AdditionalData)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(sl => sl.CreatedAt)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                // ایندکس‌ها
                entity.HasIndex(sl => sl.UserId);
                entity.HasIndex(sl => sl.EventType);
                entity.HasIndex(sl => sl.Severity);
                entity.HasIndex(sl => sl.CreatedAt);
                entity.HasIndex(sl => new { sl.UserId, sl.EventType, sl.CreatedAt });

                // رابطه‌ها
                entity.HasOne(sl => sl.User)
                    .WithMany(u => u.SecurityLogs)
                    .HasForeignKey(sl => sl.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureApiKey(ModelBuilder builder)
        {
            builder.Entity<ApiKey>(entity =>
            {
                //entity.ToTable("ApiKeys");

                entity.HasKey(ak => ak.Id);

                entity.Property(ak => ak.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(ak => ak.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(ak => ak.Key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(ak => ak.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(ak => ak.Description)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(ak => ak.Scopes)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(ak => ak.IsActive);
                    //.HasDefaultValue(true);

                entity.Property(ak => ak.LastUsed)
                    .IsRequired(false);

                entity.Property(ak => ak.ExpiresAt)
                    .IsRequired(false);

                entity.Property(ak => ak.CreatedAt)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                // ایندکس‌ها
                entity.HasIndex(ak => ak.UserId);
                entity.HasIndex(ak => ak.Key)
                    .IsUnique();
                entity.HasIndex(ak => ak.IsActive);
                entity.HasIndex(ak => ak.ExpiresAt);

                // رابطه‌ها
                entity.HasOne(ak => ak.User)
                    .WithMany(u => u.ApiKeys)
                    .HasForeignKey(ak => ak.UserId);
            });
        }

        private void ConfigurePermission(ModelBuilder builder)
        {
            builder.Entity<Permission>(entity =>
            {
                //entity.ToTable("Permissions");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Description)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(p => p.Category)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(p => p.Module)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(p => p.CreatedAt)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                // ایندکس‌ها
                entity.HasIndex(p => p.Name)
                    .IsUnique();
                entity.HasIndex(p => p.Category);
                entity.HasIndex(p => p.Module);
            });
        }

        private void ConfigureUserPermission(ModelBuilder builder)
        {
            builder.Entity<UserPermission>(entity =>
            {
                //entity.ToTable("UserPermissions");

                entity.HasKey(up => new { up.UserId, up.PermissionId });

                entity.Property(up => up.GrantedAt)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(up => up.GrantedBy)
                    .HasMaxLength(450)
                    .IsRequired(false);

                entity.Property(up => up.ExpiresAt)
                    .IsRequired(false);

                // ایندکس‌ها
                entity.HasIndex(up => up.UserId);
                entity.HasIndex(up => up.PermissionId);
                entity.HasIndex(up => up.ExpiresAt);

                // رابطه‌ها
                entity.HasOne(up => up.User)
                    .WithMany(u => u.UserPermissions)
                    .HasForeignKey(up => up.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(up => up.Permission)
                    .WithMany()
                    .HasForeignKey(up => up.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureRolePermission(ModelBuilder builder)
        {
            builder.Entity<RolePermission>(entity =>
            {
                //entity.ToTable("RolePermissions");

                entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });

                entity.Property(rp => rp.GrantedAt)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                // رابطه‌ها
                entity.HasOne(rp => rp.Role)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(rp => rp.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.Permission)
                    .WithMany()
                    .HasForeignKey(rp => rp.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureUserSession(ModelBuilder builder)
        {
            builder.Entity<UserSession>(entity =>
            {
                //entity.ToTable("UserSessions");

                entity.HasKey(us => us.Id);

                entity.Property(us => us.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(us => us.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(us => us.SessionToken)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(us => us.IpAddress)
                    .HasMaxLength(45)
                    .IsRequired(false);

                entity.Property(us => us.UserAgent)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(us => us.DeviceInfo)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(us => us.Location)
                    .HasMaxLength(200)
                    .IsRequired(false);

                entity.Property(us => us.LoginTime)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(us => us.LastActivity)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(us => us.ExpiresAt)
                    .IsRequired();

                entity.Property(us => us.IsActive);
                    //.HasDefaultValue(true);

                // ایندکس‌ها
                entity.HasIndex(us => us.UserId);
                entity.HasIndex(us => us.SessionToken)
                    .IsUnique();
                entity.HasIndex(us => us.IsActive);
                entity.HasIndex(us => us.ExpiresAt);
                entity.HasIndex(us => us.LastActivity);

                // رابطه‌ها
                entity.HasOne(us => us.User)
                    .WithMany(u => u.UserSessions)
                    .HasForeignKey(us => us.UserId);
            });
        }

        private void ConfigureDevice(ModelBuilder builder)
        {
            builder.Entity<Device>(entity =>
            {
                //entity.ToTable("Devices");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(d => d.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(d => d.DeviceId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(d => d.Type)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(d => d.Model)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(d => d.OS)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(d => d.Browser)
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(d => d.IpAddress)
                    .HasMaxLength(45)
                    .IsRequired(false);

                entity.Property(d => d.Location)
                    .HasMaxLength(200)
                    .IsRequired(false);

                entity.Property(d => d.IsTrusted);
                    //.HasDefaultValue(false);

                    entity.Property(d => d.IsActive);
                    //.HasDefaultValue(true);

                entity.Property(d => d.LastLogin)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                entity.Property(d => d.CreatedAt)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                // ایندکس‌ها
                entity.HasIndex(d => d.UserId);
                entity.HasIndex(d => d.DeviceId);
                entity.HasIndex(d => d.IsTrusted);
                entity.HasIndex(d => d.IsActive);
                entity.HasIndex(d => new { d.UserId, d.DeviceId })
                    .IsUnique();

                // رابطه‌ها
                entity.HasOne(d => d.User)
                    .WithMany(u => u.Devices)
                    .HasForeignKey(d => d.UserId);
            });
        }

        private void ConfigureAuditLog(ModelBuilder builder)
        {
            builder.Entity<AuditLog>(entity =>
            {
                //entity.ToTable("AuditLogs");

                entity.HasKey(al => al.Id);

                entity.Property(al => al.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(al => al.UserId)
                    .HasMaxLength(450)
                    .IsRequired(false);

                entity.Property(al => al.TableName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(al => al.EntityId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(al => al.Action)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(al => al.OldValues)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(al => al.NewValues)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(al => al.ChangedColumns)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(al => al.IpAddress)
                    .HasMaxLength(45)
                    .IsRequired(false);

                entity.Property(al => al.UserAgent)
                    //.HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(al => al.Timestamp)
                    //.HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                // ایندکس‌ها
                entity.HasIndex(al => al.UserId);
                entity.HasIndex(al => al.TableName);
                entity.HasIndex(al => al.EntityId);
                entity.HasIndex(al => al.Action);
                entity.HasIndex(al => al.Timestamp);
                entity.HasIndex(al => new { al.TableName, al.EntityId, al.Timestamp });

                // رابطه‌ها
                entity.HasOne(al => al.User)
                    .WithMany(u => u.AuditLogs)
                    .HasForeignKey(al => al.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }

        #endregion

        #region تنظیمات عمومی

        private void ConfigureTableNames(ModelBuilder builder)
        {
            // نام‌گذاری جداول Identity
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }

        private void ConfigureIndexes(ModelBuilder builder)
        {
            // ایندکس‌های ترکیبی برای کارایی بیشتر
            builder.Entity<LoginHistory>()
                .HasIndex(lh => new { lh.UserId, lh.LoginTime, lh.IsSuccessful });

            builder.Entity<RefreshToken>()
                .HasIndex(rt => new { rt.UserId, rt.IsActive, rt.ExpiresAt });

            builder.Entity<Notification>()
                .HasIndex(n => new { n.UserId, n.IsRead, n.CreatedAt });

            builder.Entity<SecurityLog>()
                .HasIndex(sl => new { sl.UserId, sl.EventType, sl.CreatedAt });

            builder.Entity<AuditLog>()
                .HasIndex(al => new { al.TableName, al.EntityId, al.Timestamp });
        }

        private void SeedData(ModelBuilder builder)
        {
            // نقش‌های پیش‌فرض
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Description = "دسترسی کامل به سیستم",
                    IsSystemRole = true,
                    CreatedAt = DateTime.UtcNow,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "مدیر سیستم",
                    IsSystemRole = true,
                    CreatedAt = DateTime.UtcNow,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER",
                    Description = "کاربر عادی",
                    IsSystemRole = true,
                    CreatedAt = DateTime.UtcNow,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Moderator",
                    NormalizedName = "MODERATOR",
                    Description = "ناظر سیستم",
                    IsSystemRole = true,
                    CreatedAt = DateTime.UtcNow,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            };

            builder.Entity<ApplicationRole>().HasData(roles);

            // دسترسی‌های پیش‌فرض
            var permissions = new List<Permission>
            {
                // دسترسی‌های کاربری
                new Permission { Id = 1, Name = "User.View", Description = "مشاهده کاربران", Category = "User", Module = "UserManagement" },
                new Permission { Id = 2, Name = "User.Create", Description = "ایجاد کاربر", Category = "User", Module = "UserManagement" },
                new Permission { Id = 3, Name = "User.Edit", Description = "ویرایش کاربر", Category = "User", Module = "UserManagement" },
                new Permission { Id = 4, Name = "User.Delete", Description = "حذف کاربر", Category = "User", Module = "UserManagement" },
                
                // دسترسی‌های نقش
                new Permission { Id = 5, Name = "Role.View", Description = "مشاهده نقش‌ها", Category = "Role", Module = "UserManagement" },
                new Permission { Id = 6, Name = "Role.Create", Description = "ایجاد نقش", Category = "Role", Module = "UserManagement" },
                new Permission { Id = 7, Name = "Role.Edit", Description = "ویرایش نقش", Category = "Role", Module = "UserManagement" },
                new Permission { Id = 8, Name = "Role.Delete", Description = "حذف نقش", Category = "Role", Module = "UserManagement" },
                
                // دسترسی‌های سیستم
                new Permission { Id = 9, Name = "System.Settings.View", Description = "مشاهده تنظیمات سیستم", Category = "System", Module = "SystemManagement" },
                new Permission { Id = 10, Name = "System.Settings.Edit", Description = "ویرایش تنظیمات سیستم", Category = "System", Module = "SystemManagement" },
                
                // دسترسی‌های لاگ
                new Permission { Id = 11, Name = "Log.View", Description = "مشاهده لاگ‌ها", Category = "Log", Module = "SystemManagement" },
                new Permission { Id = 12, Name = "Log.Export", Description = "خروجی گرفتن از لاگ‌ها", Category = "Log", Module = "SystemManagement" },
            };

            builder.Entity<Permission>().HasData(permissions);

            // نقش‌های پیش‌فرض به دسترسی‌ها
            var rolePermissions = new List<RolePermission>();
            var superAdminRole = roles.First(r => r.Name == "SuperAdmin");
            var adminRole = roles.First(r => r.Name == "Admin");

            foreach (var permission in permissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    RoleId = superAdminRole.Id,
                    PermissionId = permission.Id,
                    GrantedAt = DateTime.UtcNow
                });

                if (permission.Name.StartsWith("User.") || permission.Name.StartsWith("Role."))
                {
                    rolePermissions.Add(new RolePermission
                    {
                        RoleId = adminRole.Id,
                        PermissionId = permission.Id,
                        GrantedAt = DateTime.UtcNow
                    });
                }
            }

            builder.Entity<RolePermission>().HasData(rolePermissions);

            // کاربر ادمین پیش‌فرض
            var adminUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                FirstName = "مدیر",
                LastName = "سیستم",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = "AQAAAAIAAYagAAAAEK5mWu7UOQK9Nzj2qvCcpH8QpSUpYy8Q4oOGA3c7kHqMNtDDK7e89kHq1KpRrXJwZg==" // Password@123
            };

            builder.Entity<ApplicationUser>().HasData(adminUser);

            // تخصیص نقش به کاربر ادمین
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUser.Id,
                    RoleId = superAdminRole.Id
                }
            );

            // تنظیمات پیش‌فرض سیستم
            var systemSettings = new List<UserSetting>
            {
                new UserSetting
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = adminUser.Id,
                    SettingKey = "Theme",
                    SettingValue = "dark",
                    DataType = "string",
                    Category = "Appearance",
                    UpdatedAt = DateTime.UtcNow
                },
                new UserSetting
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = adminUser.Id,
                    SettingKey = "Language",
                    SettingValue = "fa-IR",
                    DataType = "string",
                    Category = "Localization",
                    UpdatedAt = DateTime.UtcNow
                },
                new UserSetting
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = adminUser.Id,
                    SettingKey = "Notifications.Email",
                    SettingValue = "true",
                    DataType = "boolean",
                    Category = "Notifications",
                    UpdatedAt = DateTime.UtcNow
                },
                new UserSetting
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = adminUser.Id,
                    SettingKey = "Notifications.SMS",
                    SettingValue = "false",
                    DataType = "boolean",
                    Category = "Notifications",
                    UpdatedAt = DateTime.UtcNow
                }
            };

            builder.Entity<UserSetting>().HasData(systemSettings);
        }

        #endregion

        #region متدهای کمکی

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            var currentTime = DateTime.UtcNow;
            var userId = GetCurrentUserId();

            foreach (var entry in entries)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = currentTime;
                    entity.CreatedBy = userId;
                }

                entity.UpdatedAt = currentTime;
                entity.UpdatedBy = userId;
            }
        }

        private string GetCurrentUserId()
        {
            // این متد باید با توجه به سیستم احراز هویت شما پیاده‌سازی شود
            // به طور موقت مقدار پیش‌فرض برگردانید
            return "system";
        }

        #endregion
    }

    #region اینترفیس‌ها

    public interface IAuditable
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }

    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
        string DeletedBy { get; set; }
    }

    #endregion

    #region مدل‌های کامل

    public class ApplicationUser : IdentityUser, IAuditable
    {
        // اطلاعات شخصی
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string AvatarUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }

        // اطلاعات تماس
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        // اطلاعات پروفایل
        public string Bio { get; set; }
        public string Website { get; set; }
        public string SocialMediaLinks { get; set; } // JSON

        // تنظیمات
        public string TimeZone { get; set; }
        public string Language { get; set; }

        // امنیت
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool IsActive { get; set; }

        // لاگینگ
        public int FailedLoginAttempts { get; set; }
        public DateTime? LockoutEnd { get; set; }

        // Audit
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        // روابط
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
        public virtual ICollection<TwoFactorRecoveryCode> TwoFactorRecoveryCodes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<SecurityLog> SecurityLogs { get; set; }
        public virtual ICollection<ApiKey> ApiKeys { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<UserSession> UserSessions { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }

    public class ApplicationRole : IdentityRole, IAuditable
    {
        public string Description { get; set; }
        public bool IsSystemRole { get; set; }
        public string Permissions { get; set; } // JSON
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }

    public class RefreshToken
    {
        public string Id { get; set; }
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

    public class LoginHistory
    {
        public string Id { get; set; }
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

    public class TwoFactorRecoveryCode
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? UsedAt { get; set; }
    }

    public class TwoFactorValidationHistory
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime ValidatedAt { get; set; }
        public string IpAddress { get; set; }
        public bool IsSuccessful { get; set; }
    }

    public class UserProfile : IAuditable
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string Skills { get; set; } // JSON
        public string Interests { get; set; } // JSON
        public string Education { get; set; } // JSON
        public string Experience { get; set; } // JSON
        public string Certifications { get; set; } // JSON
        public string Preferences { get; set; } // JSON
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class UserSetting
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string DataType { get; set; }
        public string Category { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class Notification
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public bool IsRead { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public string Data { get; set; } // JSON
        public string ActionUrl { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class SecurityLog
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string EventType { get; set; }
        public string Severity { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Details { get; set; }
        public string AdditionalData { get; set; } // JSON
        public DateTime CreatedAt { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class ApiKey
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Scopes { get; set; } // JSON
        public bool IsActive { get; set; }
        public DateTime? LastUsed { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Module { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }

    public class UserPermission
    {
        public string UserId { get; set; }
        public int PermissionId { get; set; }
        public DateTime GrantedAt { get; set; }
        public string GrantedBy { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Permission Permission { get; set; }
    }

    public class RolePermission
    {
        public string RoleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime GrantedAt { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual Permission Permission { get; set; }
    }

    public class UserSession
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SessionToken { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string DeviceInfo { get; set; } // JSON
        public string Location { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LastActivity { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsActive { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class Device
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string OS { get; set; }
        public string Browser { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }
        public bool IsTrusted { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class AuditLog
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TableName { get; set; }
        public string EntityId { get; set; }
        public string Action { get; set; }
        public string OldValues { get; set; } // JSON
        public string NewValues { get; set; } // JSON
        public string ChangedColumns { get; set; } // JSON
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    #endregion
}