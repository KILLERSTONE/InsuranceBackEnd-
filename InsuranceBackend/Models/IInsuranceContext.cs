using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceBackend.Models;

public partial class IInsuranceContext : DbContext
{
    public IInsuranceContext()
    {
    }

    public IInsuranceContext(DbContextOptions<IInsuranceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CardInfo> CardInfos { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardInfo>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__card_inf__BDF201DD2D044738");

            entity.ToTable("card_info");

            entity.HasIndex(e => e.CardNo, "CHECK_UNIQUE_CARD").IsUnique();

            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.CardNo).HasColumnName("card_no");
            entity.Property(e => e.CardOwner)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("card_owner");
            entity.Property(e => e.SecurityCode).HasColumnName("security_code");
            entity.Property(e => e.ValidThrough)
                .HasColumnType("date")
                .HasColumnName("valid_through");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__policies__47DA3F0310D9D6A6");

            entity.ToTable("policies");

            entity.HasIndex(e => e.PolicyName, "UNIQUE_POLICY_NAME").IsUnique();
                
            entity.Property(e => e.PolicyId).HasColumnName("policy_id");
            entity.Property(e => e.PolicyDetail).HasColumnName("policy_detail");
            entity.Property(e => e.PolicyFrom)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("policy_from");
            entity.Property(e => e.PolicyInsurer)
                .HasMaxLength(255)
                .HasColumnName("policy_insurer");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(255)
                .HasColumnName("policy_name");
            entity.Property(e => e.PolicyTo)
                .HasColumnType("date")
                .HasColumnName("policy_to");
            entity.Property(e => e.PolicyTpa)
                .HasMaxLength(255)
                .HasColumnName("policy_tpa");

            entity.Property(e => e.User_id).HasColumnName("user_id"); 

            entity.HasOne<UserInfo>() 
                .WithMany() 
                .HasForeignKey(e => e.User_id) 
                .HasConstraintName("FK_Policy_UserInfo"); 
        });

            modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user_inf__B9BE370F5BC06F65");

            entity.ToTable("user_info");

            entity.HasIndex(e => e.Email, "CHECK_UNIQUE_EMAIL").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    public virtual async Task<int?> UserLogin(string username, string password)
    {
        var usernameParam = new SqlParameter("@username", SqlDbType.NVarChar, 255)
        {
            Value = username
        };

        var passwordParam = new SqlParameter("@password", SqlDbType.NVarChar, 255)
        {
            Value = password
        };

        var userIdParam = new SqlParameter("@userId", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        await Database.ExecuteSqlRawAsync("EXEC user_login @username, @password, @userId OUTPUT",
                                            usernameParam, passwordParam, userIdParam);

        if (userIdParam.Value != DBNull.Value && userIdParam.Value != null)
        {
            return Convert.ToInt32(userIdParam.Value);
        }
        else
        {
            return null;
        }
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
