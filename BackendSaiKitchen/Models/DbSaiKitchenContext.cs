using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class DbSaiKitchenContext : DbContext
    {
        public DbSaiKitchenContext()
        {
        }

        public DbSaiKitchenContext(DbContextOptions<DbSaiKitchenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accesory> Accesories { get; set; }
        public virtual DbSet<Appliance> Appliances { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<BranchRole> BranchRoles { get; set; }
        public virtual DbSet<BranchType> BranchTypes { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<ContactStatus> ContactStatuses { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerBranch> CustomerBranches { get; set; }
        public virtual DbSet<Design> Designs { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<InquiryStatus> InquiryStatuses { get; set; }
        public virtual DbSet<InquiryWorkscope> InquiryWorkscopes { get; set; }
        public virtual DbSet<KitchenDesignInfo> KitchenDesignInfos { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<MeasurementDetail> MeasurementDetails { get; set; }
        public virtual DbSet<MeasurementDetailInfo> MeasurementDetailInfos { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationCategory> NotificationCategories { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionLevel> PermissionLevels { get; set; }
        public virtual DbSet<PermissionRole> PermissionRoles { get; set; }
        public virtual DbSet<RoleHead> RoleHeads { get; set; }
        public virtual DbSet<RoleType> RoleTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<WardrobeDesignInformation> WardrobeDesignInformations { get; set; }
        public virtual DbSet<WayOfContact> WayOfContacts { get; set; }
        public virtual DbSet<WorkScope> WorkScopes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\;Database=DbSaiKitchen;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Accesory>(entity =>
            {
                entity.HasKey(e => e.AccesoriesId);

                entity.Property(e => e.AccesoriesName).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.WardrobeDesignInfo)
                    .WithMany(p => p.Accesories)
                    .HasForeignKey(d => d.WardrobeDesignInfoId)
                    .HasConstraintName("FK_Accesories_WardrobeDesignInformation");
            });

            modelBuilder.Entity<Appliance>(entity =>
            {
                entity.HasKey(e => e.AppliancesId);

                entity.Property(e => e.AppliancesName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.KitchenDesignInfo)
                    .WithMany(p => p.Appliances)
                    .HasForeignKey(d => d.KitchenDesignInfoId)
                    .HasConstraintName("FK_Appliances_KitchenDesignInfo");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.BranchAddress).HasMaxLength(500);

                entity.Property(e => e.BranchContact).HasMaxLength(50);

                entity.Property(e => e.BranchName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.BranchType)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.BranchTypeId)
                    .HasConstraintName("FK_Branch_BranchType");
            });

            modelBuilder.Entity<BranchRole>(entity =>
            {
                entity.ToTable("BranchRole");

                entity.Property(e => e.BranchRoleDescription).HasMaxLength(500);

                entity.Property(e => e.BranchRoleName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.RoleType)
                    .WithMany(p => p.BranchRoles)
                    .HasForeignKey(d => d.RoleTypeId)
                    .HasConstraintName("FK_BranchRole_RoleType");
            });

            modelBuilder.Entity<BranchType>(entity =>
            {
                entity.ToTable("BranchType");

                entity.Property(e => e.BranchTypeId).ValueGeneratedNever();

                entity.Property(e => e.BranchTypeName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("Building");

                entity.Property(e => e.BuildingAddress).HasMaxLength(500);

                entity.Property(e => e.BuildingCondition).HasMaxLength(500);

                entity.Property(e => e.BuildingFloor).HasMaxLength(500);

                entity.Property(e => e.BuildingTypeOfUnit).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<ContactStatus>(entity =>
            {
                entity.ToTable("ContactStatus");

                entity.Property(e => e.ContactStatusId).ValueGeneratedNever();

                entity.Property(e => e.ContactStatusName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.CustomerCity).HasMaxLength(50);

                entity.Property(e => e.CustomerContact).HasMaxLength(50);

                entity.Property(e => e.CustomerCountry).HasMaxLength(50);

                entity.Property(e => e.CustomerEmail).HasMaxLength(50);

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.CustomerNationalId).HasMaxLength(500);

                entity.Property(e => e.CustomerNationality).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Customer_Branch");

                entity.HasOne(d => d.ContactStatus)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ContactStatusId)
                    .HasConstraintName("FK_Customer_ContactStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Customer_User");

                entity.HasOne(d => d.WayofContact)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.WayofContactId)
                    .HasConstraintName("FK_Customer_WayOfContact");
            });

            modelBuilder.Entity<CustomerBranch>(entity =>
            {
                entity.ToTable("CustomerBranch");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.CustomerBranches)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_CustomerBranch_Branch");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerBranches)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CustomerBranch_Customer");
            });

            modelBuilder.Entity<Design>(entity =>
            {
                entity.ToTable("Design");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.DesignApprovedon).HasMaxLength(50);

                entity.Property(e => e.DesignDescription).HasMaxLength(500);

                entity.Property(e => e.DesignName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.DesignApprovedByNavigation)
                    .WithMany(p => p.DesignDesignApprovedByNavigations)
                    .HasForeignKey(d => d.DesignApprovedBy)
                    .HasConstraintName("FK_Design_User1");

                entity.HasOne(d => d.DesignTakenByNavigation)
                    .WithMany(p => p.DesignDesignTakenByNavigations)
                    .HasForeignKey(d => d.DesignTakenBy)
                    .HasConstraintName("FK_Design_UserTaken");

                entity.HasOne(d => d.DesignCreatedByNavigation)
                    .WithMany(p => p.DesignDesignCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Design_User");

                entity.HasOne(d => d.InquiryWorkscope)
                    .WithMany(p => p.Designs)
                    .HasForeignKey(d => d.InquiryWorkscopeId)
                    .HasConstraintName("FK_Design_InquiryWorkscope");
            });

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.HasKey(e => e.FeesId);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.FeesAmount).HasMaxLength(500);

                entity.Property(e => e.FeesDescription).HasMaxLength(500);

                entity.Property(e => e.FeesName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("File");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.FileImage).HasColumnType("image");

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.FileUrl).HasColumnName("FileURL");

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Design)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.DesignId)
                    .HasConstraintName("FK_File_Design");

                entity.HasOne(d => d.Measurement)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.MeasurementId)
                    .HasConstraintName("FK_File_Measurement");
            });

            modelBuilder.Entity<Inquiry>(entity =>
            {
                entity.ToTable("Inquiry");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.InquiryCode).HasMaxLength(4000);

                entity.Property(e => e.InquiryDescription).HasMaxLength(500);

                entity.Property(e => e.InquiryDueDate).HasMaxLength(50);

                entity.Property(e => e.InquiryEndDate).HasMaxLength(50);

                entity.Property(e => e.InquiryName).HasMaxLength(500);

                entity.Property(e => e.InquiryStartDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.AddedByNavigation)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.AddedBy)
                    .HasConstraintName("FK_Inquiry_User");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Inquiry_Branch");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK_Inquiry_bUILDING");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Inquiry_Customer");
            });

            modelBuilder.Entity<InquiryStatus>(entity =>
            {
                entity.ToTable("InquiryStatus");

                entity.Property(e => e.InquiryStatusId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.InquiryStatusDescription).HasMaxLength(500);

                entity.Property(e => e.InquiryStatusName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<InquiryWorkscope>(entity =>
            {
                entity.ToTable("InquiryWorkscope");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.DesignScheduleDate).HasMaxLength(50);

                entity.Property(e => e.MeasurementScheduleDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.DesignAssignedToNavigation)
                    .WithMany(p => p.InquiryWorkscopeDesignAssignedToNavigations)
                    .HasForeignKey(d => d.DesignAssignedTo)
                    .HasConstraintName("FK_InquiryWorkscope_User1");

                entity.HasOne(d => d.Inquiry)
                    .WithMany(p => p.InquiryWorkscopes)
                    .HasForeignKey(d => d.InquiryId)
                    .HasConstraintName("FK_InquiryMeasurement_Inquiry");

                entity.HasOne(d => d.InquiryStatus)
                    .WithMany(p => p.InquiryWorkscopes)
                    .HasForeignKey(d => d.InquiryStatusId)
                    .HasConstraintName("FK_InquiryWorkscope_InquiryStatus");

                entity.HasOne(d => d.MeasurementAssignedToNavigation)
                    .WithMany(p => p.InquiryWorkscopeMeasurementAssignedToNavigations)
                    .HasForeignKey(d => d.MeasurementAssignedTo)
                    .HasConstraintName("FK_InquiryWorkscope_User");

                entity.HasOne(d => d.Workscope)
                    .WithMany(p => p.InquiryWorkscopes)
                    .HasForeignKey(d => d.WorkscopeId)
                    .HasConstraintName("FK_InquiryMeasurement_WorkScope");
            });

            modelBuilder.Entity<KitchenDesignInfo>(entity =>
            {
                entity.HasKey(e => e.Kdiid);

                entity.ToTable("KitchenDesignInfo");

                entity.Property(e => e.Kdiid).HasColumnName("KDIId");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.KdibaseUnitHeight)
                    .HasMaxLength(50)
                    .HasColumnName("KDIBaseUnitHeight");

                entity.Property(e => e.KdiboardModelCarcass)
                    .HasMaxLength(50)
                    .HasColumnName("KDIBoardModelCarcass");

                entity.Property(e => e.KdiboardModelCarcassColor)
                    .HasMaxLength(50)
                    .HasColumnName("KDIBoardModelCarcassColor");

                entity.Property(e => e.KdiboardModelDoorShutterColor)
                    .HasMaxLength(10)
                    .HasColumnName("KDIBoardModelDoorShutterColor")
                    .IsFixedLength(true);

                entity.Property(e => e.KdiboradModelDoorShutter)
                    .HasMaxLength(50)
                    .HasColumnName("KDIBoradModelDoorShutter");

                entity.Property(e => e.KdikitchenType)
                    .HasMaxLength(50)
                    .HasColumnName("KDIKitchenType");

                entity.Property(e => e.Kdinotes).HasColumnName("KDINotes");

                entity.Property(e => e.KdiwallUnitHeight)
                    .HasMaxLength(50)
                    .HasColumnName("KDIWallUnitHeight");

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log");

                entity.Property(e => e.Ip)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IP");

                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.Properties).HasColumnType("xml");

                entity.Property(e => e.UserName).HasMaxLength(200);
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.ToTable("Measurement");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.MeasurementApprovedOn).HasMaxLength(50);

                entity.Property(e => e.MeasurementComment).HasMaxLength(500);

                entity.Property(e => e.MeasurementName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Fees)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.FeesId)
                    .HasConstraintName("FK_Measurement_Fees");

                entity.HasOne(d => d.InquiryWorkscope)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.InquiryWorkscopeId)
                    .HasConstraintName("FK_Measurement_InquiryWorkscope");

                entity.HasOne(d => d.KitchenDesignInfo)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.KitchenDesignInfoId)
                    .HasConstraintName("FK_Measurement_KitchenDesignInfo");

                entity.HasOne(d => d.MeasurementApprovedByNavigation)
                    .WithMany(p => p.MeasurementMeasurementApprovedByNavigations)
                    .HasForeignKey(d => d.MeasurementApprovedBy)
                    .HasConstraintName("FK_Measurement_User1");

                entity.HasOne(d => d.MeasurementDetail)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.MeasurementDetailId)
                    .HasConstraintName("FK_Measurement_MeasurementDetail");

                entity.HasOne(d => d.MeasurementTakenByNavigation)
                    .WithMany(p => p.MeasurementMeasurementTakenByNavigations)
                    .HasForeignKey(d => d.MeasurementTakenBy)
                    .HasConstraintName("FK_Measurement_User");

                entity.HasOne(d => d.WardrobeDesignInfo)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.WardrobeDesignInfoId)
                    .HasConstraintName("FK_Measurement_WardrobeDesignInformation");
            });

            modelBuilder.Entity<MeasurementDetail>(entity =>
            {
                entity.ToTable("MeasurementDetail");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailCeilingHeight).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailCielingDiameter).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailCornishDiameter).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailCornishHeight).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailDescription).HasMaxLength(500);

                entity.Property(e => e.MeasurementDetailDoorHeight).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailName).HasMaxLength(500);

                entity.Property(e => e.MeasurementDetailPlinthDiameter).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailPlinthHeight).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailSkirtingDiameter).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailSkirtingHeight).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailSpotLightFromWall).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<MeasurementDetailInfo>(entity =>
            {
                entity.ToTable("MeasurementDetailInfo");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailInfoDistanceHeight).HasMaxLength(50);

                entity.Property(e => e.MeasurementDetailInfoDistanceHff)
                    .HasMaxLength(50)
                    .HasColumnName("MeasurementDetailInfoDistanceHFF");

                entity.Property(e => e.MeasurementDetailInfoDistanceLl)
                    .HasMaxLength(50)
                    .HasColumnName("MeasurementDetailInfoDistanceLL");

                entity.Property(e => e.MeasurementDetailInfoDistanceRr)
                    .HasMaxLength(50)
                    .HasColumnName("MeasurementDetailInfoDistanceRR");

                entity.Property(e => e.MeasurementDetailInfoName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.MeasurementDetail)
                    .WithMany(p => p.MeasurementDetailInfos)
                    .HasForeignKey(d => d.MeasurementDetailId)
                    .HasConstraintName("FK_MeasurementDetailInfo_MeasurementDetail");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.NotificationAcceptAction).HasMaxLength(500);

                entity.Property(e => e.NotificationContent).HasMaxLength(500);

                entity.Property(e => e.NotificationDeclineAction).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Notification_Branch");

                entity.HasOne(d => d.NotificationCategory)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationCategoryId)
                    .HasConstraintName("FK_Notification_NotificationCategory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Notification_User");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserRoleId)
                    .HasConstraintName("FK_Notification_UserRole");
            });

            modelBuilder.Entity<NotificationCategory>(entity =>
            {
                entity.ToTable("NotificationCategory");

                entity.Property(e => e.NotificationCategoryId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.NotificationCategoryName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.PermissionId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PermissionName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<PermissionLevel>(entity =>
            {
                entity.ToTable("PermissionLevel");

                entity.Property(e => e.PermissionLevelId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PermissionLevelName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<PermissionRole>(entity =>
            {
                entity.ToTable("PermissionRole");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("('true')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('false')");

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.BranchRole)
                    .WithMany(p => p.PermissionRoles)
                    .HasForeignKey(d => d.BranchRoleId)
                    .HasConstraintName("FK_PermissionRole_BranchRole");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionRoles)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_PermissionRole_Permission");

                entity.HasOne(d => d.PermissionLevel)
                    .WithMany(p => p.PermissionRoles)
                    .HasForeignKey(d => d.PermissionLevelId)
                    .HasConstraintName("FK_PermissionRole_PermissionLevel");
            });

            modelBuilder.Entity<RoleHead>(entity =>
            {
                entity.ToTable("RoleHead");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.EmployeeRole)
                    .WithMany(p => p.RoleHeads)
                    .HasForeignKey(d => d.EmployeeRoleId)
                    .HasConstraintName("FK_RoleHead_BranchRole");
            });

            modelBuilder.Entity<RoleType>(entity =>
            {
                entity.ToTable("RoleType");

                entity.Property(e => e.RoleTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.RoleTypeName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.Property(e => e.UserEmail).HasMaxLength(50);

                entity.Property(e => e.UserFcmtoken)
                    .HasMaxLength(500)
                    .HasColumnName("UserFCMToken");

                entity.Property(e => e.UserMobile).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserPassword).HasMaxLength(50);

                entity.Property(e => e.UserProfileImageUrl)
                    .HasMaxLength(500)
                    .HasColumnName("UserProfileImageURL");

                entity.Property(e => e.UserToken).HasMaxLength(500);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Role_Branch");

                entity.HasOne(d => d.BranchRole)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.BranchRoleId)
                    .HasConstraintName("FK_Role_BranchRole");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRole_User");
            });

            modelBuilder.Entity<WardrobeDesignInformation>(entity =>
            {
                entity.HasKey(e => e.Wdiid);

                entity.ToTable("WardrobeDesignInformation");

                entity.Property(e => e.Wdiid).HasColumnName("WDIId");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.Property(e => e.WdiboardModel)
                    .HasMaxLength(50)
                    .HasColumnName("WDIBoardModel");

                entity.Property(e => e.WdiceilingHeight)
                    .HasMaxLength(50)
                    .HasColumnName("WDICeilingHeight");

                entity.Property(e => e.WdiclosetHeight)
                    .HasMaxLength(50)
                    .HasColumnName("WDIClosetHeight");

                entity.Property(e => e.WdiclosetType)
                    .HasMaxLength(50)
                    .HasColumnName("WDIClosetType");

                entity.Property(e => e.WdidoorDesign)
                    .HasMaxLength(50)
                    .HasColumnName("WDIDoorDesign");

                entity.Property(e => e.WdidoorMaterial)
                    .HasMaxLength(50)
                    .HasColumnName("WDIDoorMaterial");

                entity.Property(e => e.WdihandleDesign)
                    .HasMaxLength(50)
                    .HasColumnName("WDIHandleDesign");

                entity.Property(e => e.Wdinotes).HasColumnName("WDINotes");

                entity.Property(e => e.WdiselectedColor)
                    .HasMaxLength(50)
                    .HasColumnName("WDISelectedColor");

                entity.Property(e => e.WdistorageDoor).HasColumnName("WDIStorageDoor");
            });

            modelBuilder.Entity<WayOfContact>(entity =>
            {
                entity.ToTable("WayOfContact");

                entity.Property(e => e.WayOfContactId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.Property(e => e.WayOfContactName).HasMaxLength(50);
            });

            modelBuilder.Entity<WorkScope>(entity =>
            {
                entity.ToTable("WorkScope");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.Property(e => e.WorkScopeDescription).HasMaxLength(500);

                entity.Property(e => e.WorkScopeName).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
