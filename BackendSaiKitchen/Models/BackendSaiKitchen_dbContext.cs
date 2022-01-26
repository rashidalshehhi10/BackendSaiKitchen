using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class BackendSaiKitchen_dbContext : DbContext
    {
        public BackendSaiKitchen_dbContext()
        {
        }

        public BackendSaiKitchen_dbContext(DbContextOptions<BackendSaiKitchen_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accesory> Accesories { get; set; }
        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Appliance> Appliances { get; set; }
        public virtual DbSet<ApplianceAccessory> ApplianceAccessories { get; set; }
        public virtual DbSet<ApplianceAccessoryType> ApplianceAccessoryTypes { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<BranchRole> BranchRoles { get; set; }
        public virtual DbSet<BranchType> BranchTypes { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<CalendarEvent> CalendarEvents { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommercialProject> CommercialProjects { get; set; }
        public virtual DbSet<ContactStatus> ContactStatuses { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerBranch> CustomerBranches { get; set; }
        public virtual DbSet<Design> Designs { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<FineAuthority> FineAuthorities { get; set; }
        public virtual DbSet<FineAuthorityObject> FineAuthorityObjects { get; set; }
        public virtual DbSet<FineBackupExcludeEntity> FineBackupExcludeEntities { get; set; }
        public virtual DbSet<FineBackupNode> FineBackupNodes { get; set; }
        public virtual DbSet<FineBaseMessage> FineBaseMessages { get; set; }
        public virtual DbSet<FineBaseOutput> FineBaseOutputs { get; set; }
        public virtual DbSet<FineBlockIp> FineBlockIps { get; set; }
        public virtual DbSet<FineConfClassname> FineConfClassnames { get; set; }
        public virtual DbSet<FineConfEntity> FineConfEntities { get; set; }
        public virtual DbSet<FineConfXmlentity> FineConfXmlentities { get; set; }
        public virtual DbSet<FineCustomRole> FineCustomRoles { get; set; }
        public virtual DbSet<FineDepRole> FineDepRoles { get; set; }
        public virtual DbSet<FineDepartment> FineDepartments { get; set; }
        public virtual DbSet<FineExtraProperty> FineExtraProperties { get; set; }
        public virtual DbSet<FineFavoriteEntry> FineFavoriteEntries { get; set; }
        public virtual DbSet<FineHomepageExpand> FineHomepageExpands { get; set; }
        public virtual DbSet<FineInternational> FineInternationals { get; set; }
        public virtual DbSet<FineLabel> FineLabels { get; set; }
        public virtual DbSet<FineLabelIndex> FineLabelIndices { get; set; }
        public virtual DbSet<FineLastLogin> FineLastLogins { get; set; }
        public virtual DbSet<FineLoginLock> FineLoginLocks { get; set; }
        public virtual DbSet<FineMobileDevice> FineMobileDevices { get; set; }
        public virtual DbSet<FineMobilePushMessage> FineMobilePushMessages { get; set; }
        public virtual DbSet<FineOutputClass> FineOutputClasses { get; set; }
        public virtual DbSet<FineOutputClientNotice> FineOutputClientNotices { get; set; }
        public virtual DbSet<FineOutputEmail> FineOutputEmails { get; set; }
        public virtual DbSet<FineOutputFtp> FineOutputFtps { get; set; }
        public virtual DbSet<FineOutputMount> FineOutputMounts { get; set; }
        public virtual DbSet<FineOutputPlatformMsg> FineOutputPlatformMsgs { get; set; }
        public virtual DbSet<FineOutputPrint> FineOutputPrints { get; set; }
        public virtual DbSet<FineOutputSftp> FineOutputSftps { get; set; }
        public virtual DbSet<FineOutputSm> FineOutputSms { get; set; }
        public virtual DbSet<FineParamTemplate> FineParamTemplates { get; set; }
        public virtual DbSet<FinePost> FinePosts { get; set; }
        public virtual DbSet<FinePrintOffset> FinePrintOffsets { get; set; }
        public virtual DbSet<FinePrintOffsetIpRelate> FinePrintOffsetIpRelates { get; set; }
        public virtual DbSet<FineProcessExpand> FineProcessExpands { get; set; }
        public virtual DbSet<FineProcessMessage> FineProcessMessages { get; set; }
        public virtual DbSet<FineRemoteDesignAuth> FineRemoteDesignAuths { get; set; }
        public virtual DbSet<FineReportExpand> FineReportExpands { get; set; }
        public virtual DbSet<FineScheduleOutput> FineScheduleOutputs { get; set; }
        public virtual DbSet<FineScheduleRecord> FineScheduleRecords { get; set; }
        public virtual DbSet<FineScheduleTask> FineScheduleTasks { get; set; }
        public virtual DbSet<FineScheduleTaskLog> FineScheduleTaskLogs { get; set; }
        public virtual DbSet<FineScheduleTaskParam> FineScheduleTaskParams { get; set; }
        public virtual DbSet<FineSoftDatum> FineSoftData { get; set; }
        public virtual DbSet<FineSwiftColIdxConf> FineSwiftColIdxConfs { get; set; }
        public virtual DbSet<FineSwiftConfigEntity> FineSwiftConfigEntities { get; set; }
        public virtual DbSet<FineSwiftDaysRecord> FineSwiftDaysRecords { get; set; }
        public virtual DbSet<FineSwiftFilekey> FineSwiftFilekeys { get; set; }
        public virtual DbSet<FineSwiftMetadatum> FineSwiftMetadata { get; set; }
        public virtual DbSet<FineSwiftSegLocation> FineSwiftSegLocations { get; set; }
        public virtual DbSet<FineSwiftSegment> FineSwiftSegments { get; set; }
        public virtual DbSet<FineSwiftServiceInfo> FineSwiftServiceInfos { get; set; }
        public virtual DbSet<FineSwiftTabIdxConf> FineSwiftTabIdxConfs { get; set; }
        public virtual DbSet<FineSwiftTablePath> FineSwiftTablePaths { get; set; }
        public virtual DbSet<FineSystemMessage> FineSystemMessages { get; set; }
        public virtual DbSet<FineTenant> FineTenants { get; set; }
        public virtual DbSet<FineUsageDatum> FineUsageData { get; set; }
        public virtual DbSet<FineUser> FineUsers { get; set; }
        public virtual DbSet<FineUserRoleMiddle> FineUserRoleMiddles { get; set; }
        public virtual DbSet<FineVc> FineVcs { get; set; }
        public virtual DbSet<FineWorkflow> FineWorkflows { get; set; }
        public virtual DbSet<FineWorkflowLog> FineWorkflowLogs { get; set; }
        public virtual DbSet<FineWorkflowNode> FineWorkflowNodes { get; set; }
        public virtual DbSet<FineWorkflowStashDatum> FineWorkflowStashData { get; set; }
        public virtual DbSet<FineWorkflowTask> FineWorkflowTasks { get; set; }
        public virtual DbSet<FineWorkflowTaskImpl> FineWorkflowTaskImpls { get; set; }
        public virtual DbSet<FineWriteStash> FineWriteStashes { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<InquiryStatus> InquiryStatuses { get; set; }
        public virtual DbSet<InquiryWorkscope> InquiryWorkscopes { get; set; }
        public virtual DbSet<ItemColor> ItemColors { get; set; }
        public virtual DbSet<JobOrder> JobOrders { get; set; }
        public virtual DbSet<JobOrderDetail> JobOrderDetails { get; set; }
        public virtual DbSet<KitchenDesignInfo> KitchenDesignInfos { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<MeasurementDetail> MeasurementDetails { get; set; }
        public virtual DbSet<MeasurementDetailInfo> MeasurementDetailInfos { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<NewsletterFrequency> NewsletterFrequencies { get; set; }
        public virtual DbSet<NewsletterType> NewsletterTypes { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationCategory> NotificationCategories { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMode> PaymentModes { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionLevel> PermissionLevels { get; set; }
        public virtual DbSet<PermissionRole> PermissionRoles { get; set; }
        public virtual DbSet<ProjectDetail> ProjectDetails { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public virtual DbSet<Promo> Promos { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<PromotionType> PromotionTypes { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public virtual DbSet<PurchaseStatus> PurchaseStatuses { get; set; }
        public virtual DbSet<QrtzBlobTrigger> QrtzBlobTriggers { get; set; }
        public virtual DbSet<QrtzCalendar> QrtzCalendars { get; set; }
        public virtual DbSet<QrtzCronTrigger> QrtzCronTriggers { get; set; }
        public virtual DbSet<QrtzFiredTrigger> QrtzFiredTriggers { get; set; }
        public virtual DbSet<QrtzJobDetail> QrtzJobDetails { get; set; }
        public virtual DbSet<QrtzLock> QrtzLocks { get; set; }
        public virtual DbSet<QrtzPausedTriggerGrp> QrtzPausedTriggerGrps { get; set; }
        public virtual DbSet<QrtzSchedulerState> QrtzSchedulerStates { get; set; }
        public virtual DbSet<QrtzSimpleTrigger> QrtzSimpleTriggers { get; set; }
        public virtual DbSet<QrtzSimpropTrigger> QrtzSimpropTriggers { get; set; }
        public virtual DbSet<QrtzTrigger> QrtzTriggers { get; set; }
        public virtual DbSet<Quotation> Quotations { get; set; }
        public virtual DbSet<RoleHead> RoleHeads { get; set; }
        public virtual DbSet<RoleType> RoleTypes { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<TermsAndCondition> TermsAndConditions { get; set; }
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
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
                optionsBuilder.UseSqlServer("Server=backendsaikitchendbserver.database.windows.net,1433;Database=BackendSaiKitchen_db;User Id=SaiAdmin;Password=SaiKitchen123;MultipleActiveResultSets=true;");
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

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("Apartment");

                entity.Property(e => e.ApartmentName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.HasOne(d => d.CommercialProject)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.CommercialProjectId)
                    .HasConstraintName("FK_Apartment_CommercialProject");
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

            modelBuilder.Entity<ApplianceAccessory>(entity =>
            {
                entity.ToTable("ApplianceAccessory");

                entity.Property(e => e.ApplianceAccesoryDescription).HasMaxLength(500);

                entity.Property(e => e.ApplianceAccessoryName).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.ApplianceAccessoryType)
                    .WithMany(p => p.ApplianceAccessories)
                    .HasForeignKey(d => d.ApplianceAccessoryTypeId)
                    .HasConstraintName("FK_ApplianceAccessory_ApplianceAccessoryType");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ApplianceAccessories)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_ApplianceAccessory_Brand");

                entity.HasOne(d => d.UnitOfMeasurement)
                    .WithMany(p => p.ApplianceAccessories)
                    .HasForeignKey(d => d.UnitOfMeasurementId)
                    .HasConstraintName("FK_ApplianceAccessory_UnitOfMeasurement");
            });

            modelBuilder.Entity<ApplianceAccessoryType>(entity =>
            {
                entity.ToTable("ApplianceAccessoryType");

                entity.Property(e => e.ApplianceAccessoryTypeId).ValueGeneratedNever();

                entity.Property(e => e.ApplianceAccessoryTypeDescription).HasMaxLength(500);

                entity.Property(e => e.ApplianceAccessoryTypeName).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
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

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandDescription).HasMaxLength(500);

                entity.Property(e => e.BrandName).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.ApplianceAccessoryType)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.ApplianceAccessoryTypeId)
                    .HasConstraintName("FK_Brand_ApplianceAccessoryType");
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

            modelBuilder.Entity<CalendarEvent>(entity =>
            {
                entity.ToTable("CalendarEvent");

                entity.Property(e => e.CalendarEventDate).HasMaxLength(500);

                entity.Property(e => e.CalendarEventName).HasMaxLength(500);

                entity.Property(e => e.CalendarEventOnClickUrl)
                    .HasMaxLength(500)
                    .HasColumnName("CalendarEventOnClickURL");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.CalendarEvents)
                    .HasForeignKey(d => d.EventTypeId)
                    .HasConstraintName("FK_CalendarEvent_EventType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CalendarEvents)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_CalendarEvent_User");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentAddedon).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.CommentAddedByNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CommentAddedBy)
                    .HasConstraintName("FK_Comment_User");

                entity.HasOne(d => d.Inquiry)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.InquiryId)
                    .HasConstraintName("FK_Comment_Inquiry");

                entity.HasOne(d => d.InquiryStatus)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.InquiryStatusId)
                    .HasConstraintName("FK_Comment_InquiryStatus");
            });

            modelBuilder.Entity<CommercialProject>(entity =>
            {
                entity.ToTable("CommercialProject");

                entity.Property(e => e.CommercialProjectName).HasMaxLength(50);

                entity.Property(e => e.CommercialProjectNo).HasMaxLength(50);

                entity.Property(e => e.CommercialProjectStartDate).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.ProjectStatus)
                    .WithMany(p => p.CommercialProjects)
                    .HasForeignKey(d => d.ProjectStatusId)
                    .HasConstraintName("FK_CommercialProject_ProjectStatus");
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

                entity.Property(e => e.CustomerAssignedDate).HasMaxLength(500);

                entity.Property(e => e.CustomerContact).HasMaxLength(50);

                entity.Property(e => e.CustomerEmail).HasMaxLength(50);

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.CustomerNextMeetingDate).HasMaxLength(50);

                entity.Property(e => e.CustomerWhatsapp).HasMaxLength(50);

                entity.Property(e => e.EscalatedOn).HasMaxLength(50);

                entity.Property(e => e.EscalationRequestedOn).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Customer_Branch");

                entity.HasOne(d => d.ContactStatus)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ContactStatusId)
                    .HasConstraintName("FK_Customer_ContactStatus");

                entity.HasOne(d => d.CustomerAssignedByNavigation)
                    .WithMany(p => p.CustomerCustomerAssignedByNavigations)
                    .HasForeignKey(d => d.CustomerAssignedBy)
                    .HasConstraintName("FK_Customer_User2");

                entity.HasOne(d => d.CustomerAssignedToNavigation)
                    .WithMany(p => p.CustomerCustomerAssignedToNavigations)
                    .HasForeignKey(d => d.CustomerAssignedTo)
                    .HasConstraintName("FK_Customer_User1");

                entity.HasOne(d => d.EscalatedByNavigation)
                    .WithMany(p => p.CustomerEscalatedByNavigations)
                    .HasForeignKey(d => d.EscalatedBy)
                    .HasConstraintName("FK_Customer_User4");

                entity.HasOne(d => d.EscalationRequestedByNavigation)
                    .WithMany(p => p.CustomerEscalationRequestedByNavigations)
                    .HasForeignKey(d => d.EscalationRequestedBy)
                    .HasConstraintName("FK_Customer_User3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CustomerUsers)
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

                entity.Property(e => e.DesignAddedDate).HasMaxLength(50);

                entity.Property(e => e.DesignApprovedon).HasMaxLength(50);

                entity.Property(e => e.DesignCustomerReviewDate).HasMaxLength(50);

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
                    .HasConstraintName("FK_Design_User");

                entity.HasOne(d => d.InquiryWorkscope)
                    .WithMany(p => p.Designs)
                    .HasForeignKey(d => d.InquiryWorkscopeId)
                    .HasConstraintName("FK_Design_InquiryWorkscope");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.ToTable("EventType");

                entity.Property(e => e.EventTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.EventTypeDescription).HasMaxLength(500);

                entity.Property(e => e.EventTypeName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.HasKey(e => e.FeesId);

                entity.Property(e => e.FeesId).ValueGeneratedNever();

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

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.Paymentid)
                    .HasConstraintName("FK_File_Payment");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("FK_File_PurchaseOrder");

                entity.HasOne(d => d.PurchaseRequest)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.PurchaseRequestId)
                    .HasConstraintName("FK_File_PurchaseRequest");

                entity.HasOne(d => d.Quotation)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.QuotationId)
                    .HasConstraintName("FK_File_Quotation");
            });

            modelBuilder.Entity<FineAuthority>(entity =>
            {
                entity.ToTable("fine_authority", "db_owner");

                entity.HasIndex(e => new { e.RoleId, e.RoleType, e.AuthorityEntityId, e.AuthorityEntityType, e.Authority, e.AuthorityType, e.TenantId }, "UK1tsntnb9o3bajn025123mdv6n")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Authority).HasColumnName("authority");

                entity.Property(e => e.AuthorityEntityId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("authorityEntityId");

                entity.Property(e => e.AuthorityEntityType).HasColumnName("authorityEntityType");

                entity.Property(e => e.AuthorityType).HasColumnName("authorityType");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("roleId");

                entity.Property(e => e.RoleType).HasColumnName("roleType");

                entity.Property(e => e.TenantId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tenantId")
                    .HasDefaultValueSql("('default')");
            });

            modelBuilder.Entity<FineAuthorityObject>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TenantId })
                    .HasName("PK__fine_aut__69528B0DC4EE532D");

                entity.ToTable("fine_authority_object", "db_owner");

                entity.HasIndex(e => e.ParentId, "IDX5mjcl597oenehothib9qcqll0");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.TenantId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tenantId")
                    .HasDefaultValueSql("('default')");

                entity.Property(e => e.CoverId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("coverId");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.DeviceType).HasColumnName("deviceType");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("displayName");

                entity.Property(e => e.ExpandId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("expandId");

                entity.Property(e => e.ExpandType).HasColumnName("expandType");

                entity.Property(e => e.FullPath)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("fullPath");

                entity.Property(e => e.Icon)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("icon");

                entity.Property(e => e.MobileIcon)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("mobileIcon");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("parentId");

                entity.Property(e => e.Path)
                    .IsUnicode(false)
                    .HasColumnName("path");

                entity.Property(e => e.SortIndex).HasColumnName("sortIndex");
            });

            modelBuilder.Entity<FineBackupExcludeEntity>(entity =>
            {
                entity.ToTable("fine_backup_exclude_entity", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<FineBackupNode>(entity =>
            {
                entity.ToTable("fine_backup_node", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.BackupEndTime).HasColumnName("backupEndTime");

                entity.Property(e => e.BackupModule)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("backupModule");

                entity.Property(e => e.BackupName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("backupName");

                entity.Property(e => e.BackupSize).HasColumnName("backupSize");

                entity.Property(e => e.BackupTime).HasColumnName("backupTime");

                entity.Property(e => e.Detail)
                    .IsUnicode(false)
                    .HasColumnName("detail");

                entity.Property(e => e.SavePath)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("savePath");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<FineBaseMessage>(entity =>
            {
                entity.ToTable("fine_base_message", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateTime).HasColumnName("createTime");

                entity.Property(e => e.Datetime).HasColumnName("datetime");

                entity.Property(e => e.Message)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.Readed).HasColumnName("readed");

                entity.Property(e => e.Toasted).HasColumnName("toasted");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Url)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.Property(e => e.UrlType).HasColumnName("urlType");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userId");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<FineBaseOutput>(entity =>
            {
                entity.ToTable("fine_base_output", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.ActionName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("actionName");

                entity.Property(e => e.ExecuteByUser).HasColumnName("executeByUser");

                entity.Property(e => e.OutputId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("outputId");

                entity.Property(e => e.ResultUrl)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("resultURL");

                entity.Property(e => e.RunType).HasColumnName("runType");
            });

            modelBuilder.Entity<FineBlockIp>(entity =>
            {
                entity.ToTable("fine_block_ip", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateTime).HasColumnName("createTime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ip");

                entity.Property(e => e.RejectedVisits).HasColumnName("rejectedVisits");
            });

            modelBuilder.Entity<FineConfClassname>(entity =>
            {
                entity.ToTable("fine_conf_classname", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("className");
            });

            modelBuilder.Entity<FineConfEntity>(entity =>
            {
                entity.ToTable("fine_conf_entity", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Value)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<FineConfXmlentity>(entity =>
            {
                entity.ToTable("fine_conf_xmlentity", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<FineCustomRole>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TenantId })
                    .HasName("PK__fine_cus__69528B0DB19C2D39");

                entity.ToTable("fine_custom_role", "db_owner");

                entity.HasIndex(e => e.Alias, "IDX8j76wg0qwjp47arr0ebdrnpir");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.TenantId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tenantId")
                    .HasDefaultValueSql("('default')");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("alias");

                entity.Property(e => e.CreationType).HasColumnName("creationType");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Enable).HasColumnName("enable");

                entity.Property(e => e.LastOperationType).HasColumnName("lastOperationType");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<FineDepRole>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TenantId })
                    .HasName("PK__fine_dep__69528B0D37BD7114");

                entity.ToTable("fine_dep_role", "db_owner");

                entity.HasIndex(e => new { e.DepartmentId, e.PostId, e.TenantId }, "UKcoywjvt5e0xfyijblktkppk8a")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.TenantId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tenantId")
                    .HasDefaultValueSql("('default')");

                entity.Property(e => e.CreationType).HasColumnName("creationType");

                entity.Property(e => e.DepartmentId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("departmentId");

                entity.Property(e => e.FullPath)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("fullPath");

                entity.Property(e => e.LastOperationType).HasColumnName("lastOperationType");

                entity.Property(e => e.PostId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("postId");
            });

            modelBuilder.Entity<FineDepartment>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TenantId })
                    .HasName("PK__fine_dep__69528B0DB3EC25BF");

                entity.ToTable("fine_department", "db_owner");

                entity.HasIndex(e => e.Alias, "IDX168i0npv0xbhrx2m6e0a7psde");

                entity.HasIndex(e => e.ParentId, "IDX24umvif3uqbghqi2vrbet1b9x");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.TenantId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tenantId")
                    .HasDefaultValueSql("('default')");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("alias");

                entity.Property(e => e.CreationType).HasColumnName("creationType");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Enable).HasColumnName("enable");

                entity.Property(e => e.FullPath)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("fullPath");

                entity.Property(e => e.LastOperationType).HasColumnName("lastOperationType");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("parentId");
            });

            modelBuilder.Entity<FineExtraProperty>(entity =>
            {
                entity.ToTable("fine_extra_property", "db_owner");

                entity.HasIndex(e => new { e.RelatedId, e.Type, e.Name }, "UKfuxlugi6j4kgugdms45dj5bbv")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.RelatedId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("relatedId");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Value)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<FineFavoriteEntry>(entity =>
            {
                entity.ToTable("fine_favorite_entry", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.EntryId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("entryId");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userId");
            });

            modelBuilder.Entity<FineHomepageExpand>(entity =>
            {
                entity.ToTable("fine_homepage_expand", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.AndroidPadHomePage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("androidPadHomePage");

                entity.Property(e => e.AndroidPhoneHomePage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("androidPhoneHomePage");

                entity.Property(e => e.IPadHomePage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("iPadHomePage");

                entity.Property(e => e.IPhoneHomePage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("iPhoneHomePage");

                entity.Property(e => e.PcHomePage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("pcHomePage");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<FineInternational>(entity =>
            {
                entity.ToTable("fine_international", "db_owner");

                entity.HasIndex(e => e.Language, "IDXbngrhvl1j6sxn5nvl5r1kqptn");

                entity.HasIndex(e => e.I18nKey, "IDXnvn0kpgsb7pn809px6jlbaend");

                entity.HasIndex(e => new { e.I18nKey, e.Language }, "UKrkdhiekxlpiq8jcjmul4d42v")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.I18nKey)
                    .HasMaxLength(255)
                    .HasColumnName("i18nKey");

                entity.Property(e => e.I18nValue)
                    .HasMaxLength(1000)
                    .HasColumnName("i18nValue");

                entity.Property(e => e.Language)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("language");
            });

            modelBuilder.Entity<FineLabel>(entity =>
            {
                entity.ToTable("fine_label", "db_owner");

                entity.HasIndex(e => new { e.LabelName, e.RelatedType }, "UK8p617fevaacro56m5bsuldppl")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.LabelName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("labelName");

                entity.Property(e => e.RelatedType).HasColumnName("relatedType");
            });

            modelBuilder.Entity<FineLabelIndex>(entity =>
            {
                entity.ToTable("fine_label_index", "db_owner");

                entity.HasIndex(e => new { e.LabelId, e.RelatedId }, "UKgoornoptbrpar9say768gues0")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.LabelId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("labelId");

                entity.Property(e => e.RelatedId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("relatedId");
            });

            modelBuilder.Entity<FineLastLogin>(entity =>
            {
                entity.ToTable("fine_last_login", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Ip)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ip");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userId");
            });

            modelBuilder.Entity<FineLoginLock>(entity =>
            {
                entity.ToTable("fine_login_lock", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.ErrorTime).HasColumnName("errorTime");

                entity.Property(e => e.LockObject)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("lockObject");

                entity.Property(e => e.LockObjectValue)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("lockObjectValue");

                entity.Property(e => e.LockTime).HasColumnName("lockTime");

                entity.Property(e => e.Locked).HasColumnName("locked");

                entity.Property(e => e.UnlockTime).HasColumnName("unlockTime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userId");
            });

            modelBuilder.Entity<FineMobileDevice>(entity =>
            {
                entity.ToTable("fine_mobile_device", "db_owner");

                entity.HasIndex(e => new { e.Username, e.MacAddress }, "UKnueghd2ow0ntb4efvtyyufs2r")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.DeviceName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("deviceName");

                entity.Property(e => e.MacAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("macAddress");

                entity.Property(e => e.Passed).HasColumnName("passed");

                entity.Property(e => e.UpdateDate).HasColumnName("updateDate");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<FineMobilePushMessage>(entity =>
            {
                entity.ToTable("fine_mobile_push_message", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("groupId");

                entity.Property(e => e.MediaId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("mediaId");

                entity.Property(e => e.MsgType).HasColumnName("msgType");

                entity.Property(e => e.Terminal).HasColumnName("terminal");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<FineOutputClass>(entity =>
            {
                entity.ToTable("fine_output_class", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("className");
            });

            modelBuilder.Entity<FineOutputClientNotice>(entity =>
            {
                entity.ToTable("fine_output_client_notice", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Addressee)
                    .IsUnicode(false)
                    .HasColumnName("addressee");

                entity.Property(e => e.Content)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.CustomizeLink)
                    .IsUnicode(false)
                    .HasColumnName("customizeLink");

                entity.Property(e => e.LinkOpenType).HasColumnName("linkOpenType");

                entity.Property(e => e.MediaId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("mediaId");

                entity.Property(e => e.Subject)
                    .IsUnicode(false)
                    .HasColumnName("subject");

                entity.Property(e => e.Terminal).HasColumnName("terminal");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<FineOutputEmail>(entity =>
            {
                entity.ToTable("fine_output_email", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.AddLink).HasColumnName("addLink");

                entity.Property(e => e.BccAddress)
                    .IsUnicode(false)
                    .HasColumnName("bccAddress");

                entity.Property(e => e.BodyContent)
                    .IsUnicode(false)
                    .HasColumnName("bodyContent");

                entity.Property(e => e.CcAddress)
                    .IsUnicode(false)
                    .HasColumnName("ccAddress");

                entity.Property(e => e.CustomAddress)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("customAddress");

                entity.Property(e => e.CustomBccAddress)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("customBccAddress");

                entity.Property(e => e.CustomCcAddress)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("customCcAddress");

                entity.Property(e => e.PreviewAttach).HasColumnName("previewAttach");

                entity.Property(e => e.PreviewWidget).HasColumnName("previewWidget");

                entity.Property(e => e.Subject)
                    .IsUnicode(false)
                    .HasColumnName("subject");

                entity.Property(e => e.UseAttach).HasColumnName("useAttach");
            });

            modelBuilder.Entity<FineOutputFtp>(entity =>
            {
                entity.ToTable("fine_output_ftp", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.FtpMode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ftpMode");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Port)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("port");

                entity.Property(e => e.SavePath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("savePath");

                entity.Property(e => e.ServerAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("serverAddress");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<FineOutputMount>(entity =>
            {
                entity.ToTable("fine_output_mount", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FolderEntryId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("folderEntryID");

                entity.Property(e => e.FolderEntryName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("folderEntryName");

                entity.Property(e => e.FolderEntryStr)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("folderEntryStr");
            });

            modelBuilder.Entity<FineOutputPlatformMsg>(entity =>
            {
                entity.ToTable("fine_output_platform_msg", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.LinkOpenType).HasColumnName("linkOpenType");

                entity.Property(e => e.Subject)
                    .IsUnicode(false)
                    .HasColumnName("subject");
            });

            modelBuilder.Entity<FineOutputPrint>(entity =>
            {
                entity.ToTable("fine_output_print", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.PrinterName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("printerName");
            });

            modelBuilder.Entity<FineOutputSftp>(entity =>
            {
                entity.ToTable("fine_output_sftp", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Port)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("port");

                entity.Property(e => e.PrivateKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("privateKey");

                entity.Property(e => e.SavePath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("savePath");

                entity.Property(e => e.ServerAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("serverAddress");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<FineOutputSm>(entity =>
            {
                entity.ToTable("fine_output_sms", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.SmsParam)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("smsParam");

                entity.Property(e => e.TemplateId).HasColumnName("templateID");
            });

            modelBuilder.Entity<FineParamTemplate>(entity =>
            {
                entity.ToTable("fine_param_template", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Templateid)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("templateid");

                entity.Property(e => e.Tpgroup)
                    .IsUnicode(false)
                    .HasColumnName("tpgroup");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<FinePost>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TenantId })
                    .HasName("PK__fine_pos__69528B0D96ACA94D");

                entity.ToTable("fine_post", "db_owner");

                entity.HasIndex(e => e.Alias, "IDX1nbisecalp694watgjcqti6sk");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.TenantId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tenantId")
                    .HasDefaultValueSql("('default')");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("alias");

                entity.Property(e => e.CreationType).HasColumnName("creationType");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Enable).HasColumnName("enable");

                entity.Property(e => e.LastOperationType).HasColumnName("lastOperationType");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<FinePrintOffset>(entity =>
            {
                entity.ToTable("fine_print_offset", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CptName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cptName");

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ip");

                entity.Property(e => e.OffsetX)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("offsetX");

                entity.Property(e => e.OffsetY)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("offsetY");

                entity.Property(e => e.Sign)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sign");
            });

            modelBuilder.Entity<FinePrintOffsetIpRelate>(entity =>
            {
                entity.ToTable("fine_print_offset_ip_relate", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.ChildIp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("childIP");

                entity.Property(e => e.MotherId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("motherID");
            });

            modelBuilder.Entity<FineProcessExpand>(entity =>
            {
                entity.ToTable("fine_process_expand", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.ProcessType).HasColumnName("processType");
            });

            modelBuilder.Entity<FineProcessMessage>(entity =>
            {
                entity.ToTable("fine_process_message", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.AllTaskId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("allTaskId");

                entity.Property(e => e.DeadLine).HasColumnName("deadLine");

                entity.Property(e => e.Processed).HasColumnName("processed");

                entity.Property(e => e.TaskId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskId");
            });

            modelBuilder.Entity<FineRemoteDesignAuth>(entity =>
            {
                entity.ToTable("fine_remote_design_auth", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("path");

                entity.Property(e => e.PathType).HasColumnName("pathType");

                entity.Property(e => e.RoleType)
                    .HasColumnName("roleType")
                    .HasDefaultValueSql("((3))");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userId");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userName");
            });

            modelBuilder.Entity<FineReportExpand>(entity =>
            {
                entity.ToTable("fine_report_expand", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.ShowType).HasColumnName("showType");

                entity.Property(e => e.TransmitParameters)
                    .IsUnicode(false)
                    .HasColumnName("transmitParameters");
            });

            modelBuilder.Entity<FineScheduleOutput>(entity =>
            {
                entity.ToTable("fine_schedule_output", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.BaseName)
                    .IsUnicode(false)
                    .HasColumnName("baseName");

                entity.Property(e => e.CreateAttachByUsername).HasColumnName("createAttachByUsername");

                entity.Property(e => e.Formats)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("formats");
            });

            modelBuilder.Entity<FineScheduleRecord>(entity =>
            {
                entity.ToTable("fine_schedule_record", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Creator)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("creator");

                entity.Property(e => e.DetailMessage)
                    .IsUnicode(false)
                    .HasColumnName("detailMessage");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("filePath");

                entity.Property(e => e.LogMessage)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("logMessage");

                entity.Property(e => e.LogTime).HasColumnName("logTime");

                entity.Property(e => e.LogType).HasColumnName("logType");

                entity.Property(e => e.NextFireTime).HasColumnName("nextFireTime");

                entity.Property(e => e.RunType).HasColumnName("runType");

                entity.Property(e => e.TaskId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskId");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskName");
            });

            modelBuilder.Entity<FineScheduleTask>(entity =>
            {
                entity.ToTable("fine_schedule_task", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.BackupFilePath)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("backupFilePath");

                entity.Property(e => e.ConditionParameter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("conditionParameter");

                entity.Property(e => e.Creator)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("creator");

                entity.Property(e => e.Editable).HasColumnName("editable");

                entity.Property(e => e.FileClearCount).HasColumnName("fileClearCount");

                entity.Property(e => e.NextFireTime).HasColumnName("nextFireTime");

                entity.Property(e => e.OutputStr)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("outputStr");

                entity.Property(e => e.PreFireTime).HasColumnName("preFireTime");

                entity.Property(e => e.RepeatTime).HasColumnName("repeatTime");

                entity.Property(e => e.RepeatTimes).HasColumnName("repeatTimes");

                entity.Property(e => e.ScheduleOutput)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("scheduleOutput");

                entity.Property(e => e.SendBackupFile).HasColumnName("sendBackupFile");

                entity.Property(e => e.ShowType).HasColumnName("showType");

                entity.Property(e => e.TaskCondition)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("taskCondition");

                entity.Property(e => e.TaskDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("taskDescription");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskName");

                entity.Property(e => e.TaskParameter)
                    .IsUnicode(false)
                    .HasColumnName("taskParameter");

                entity.Property(e => e.TaskState).HasColumnName("taskState");

                entity.Property(e => e.TaskType).HasColumnName("taskType");

                entity.Property(e => e.TemplatePath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("templatePath");

                entity.Property(e => e.TriggerGroup)
                    .IsUnicode(false)
                    .HasColumnName("triggerGroup");

                entity.Property(e => e.UserGroup)
                    .IsUnicode(false)
                    .HasColumnName("userGroup");
            });

            modelBuilder.Entity<FineScheduleTaskLog>(entity =>
            {
                entity.ToTable("fine_schedule_task_log", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.LogTime).HasColumnName("logTime");

                entity.Property(e => e.LogType).HasColumnName("logType");

                entity.Property(e => e.TaskId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskId");

                entity.Property(e => e.TaskLog)
                    .IsUnicode(false)
                    .HasColumnName("taskLog");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskName");
            });

            modelBuilder.Entity<FineScheduleTaskParam>(entity =>
            {
                entity.ToTable("fine_schedule_task_param", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Param)
                    .IsUnicode(false)
                    .HasColumnName("param");

                entity.Property(e => e.TaskId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskId");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskName");
            });

            modelBuilder.Entity<FineSoftDatum>(entity =>
            {
                entity.ToTable("fine_soft_data", "db_owner");

                entity.HasIndex(e => new { e.DeletedName, e.Type }, "UKinws3an4js1ibprri9efepxni")
                    .IsUnique();

                entity.HasIndex(e => new { e.DeletedId, e.Type }, "UKkarmvav9qpl2gngebfncpes8f")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.DeletedId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("deletedId");

                entity.Property(e => e.DeletedName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("deletedName");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<FineSwiftColIdxConf>(entity =>
            {
                entity.HasKey(e => new { e.ColumnName, e.TableKey })
                    .HasName("PK__fine_swi__71649D4AAD1A7841");

                entity.ToTable("fine_swift_col_idx_conf", "db_owner");

                entity.Property(e => e.ColumnName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("columnName");

                entity.Property(e => e.TableKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tableKey");

                entity.Property(e => e.RequireGlobalDict).HasColumnName("requireGlobalDict");

                entity.Property(e => e.RequireIndex).HasColumnName("requireIndex");
            });

            modelBuilder.Entity<FineSwiftConfigEntity>(entity =>
            {
                entity.HasKey(e => e.ConfigKey)
                    .HasName("PK__fine_swi__439A5A81AD688CF3");

                entity.ToTable("fine_swift_config_entity", "db_owner");

                entity.Property(e => e.ConfigKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("configKey");

                entity.Property(e => e.ConfigValue)
                    .IsUnicode(false)
                    .HasColumnName("configValue");
            });

            modelBuilder.Entity<FineSwiftDaysRecord>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__fine_swi__0492148DE10E7EC3");

                entity.ToTable("fine_swift_days_record", "db_owner");

                entity.Property(e => e.TaskId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("task_id");

                entity.Property(e => e.TaskCondition)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("task_condition");

                entity.Property(e => e.TaskResult)
                    .HasMaxLength(4096)
                    .IsUnicode(false)
                    .HasColumnName("task_result");

                entity.Property(e => e.TaskType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("task_type");
            });

            modelBuilder.Entity<FineSwiftFilekey>(entity =>
            {
                entity.HasKey(e => new { e.SegmentId, e.TimeKey })
                    .HasName("PK__fine_swi__5790AD0BE451A0C0");

                entity.ToTable("fine_swift_filekey", "db_owner");

                entity.Property(e => e.SegmentId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("segmentId");

                entity.Property(e => e.TimeKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("timeKey");
            });

            modelBuilder.Entity<FineSwiftMetadatum>(entity =>
            {
                entity.ToTable("fine_swift_metadata", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Fields)
                    .IsUnicode(false)
                    .HasColumnName("fields");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.Property(e => e.SchemaName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("schemaName");

                entity.Property(e => e.SwiftSchema)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("swiftSchema");

                entity.Property(e => e.TableName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tableName");
            });

            modelBuilder.Entity<FineSwiftSegLocation>(entity =>
            {
                entity.HasKey(e => new { e.ClusterId, e.SegmentId })
                    .HasName("PK__fine_swi__6EAA7451C1A834B5");

                entity.ToTable("fine_swift_seg_location", "db_owner");

                entity.Property(e => e.ClusterId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("clusterId");

                entity.Property(e => e.SegmentId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("segmentId");

                entity.Property(e => e.SourceKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("sourceKey");
            });

            modelBuilder.Entity<FineSwiftSegment>(entity =>
            {
                entity.ToTable("fine_swift_segments", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.SegmentOrder).HasColumnName("segmentOrder");

                entity.Property(e => e.SegmentOwner)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("segmentOwner");

                entity.Property(e => e.SegmentUri)
                    .IsUnicode(false)
                    .HasColumnName("segmentUri");

                entity.Property(e => e.StoreType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("storeType");

                entity.Property(e => e.SwiftSchema)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("swiftSchema");
            });

            modelBuilder.Entity<FineSwiftServiceInfo>(entity =>
            {
                entity.ToTable("fine_swift_service_info", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.ClusterId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cluster_id");

                entity.Property(e => e.IsSingleton).HasColumnName("is_singleton");

                entity.Property(e => e.Service)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("service");

                entity.Property(e => e.ServiceInfo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("service_info");
            });

            modelBuilder.Entity<FineSwiftTabIdxConf>(entity =>
            {
                entity.HasKey(e => e.TableKey)
                    .HasName("PK__fine_swi__8900162C27511BD9");

                entity.ToTable("fine_swift_tab_idx_conf", "db_owner");

                entity.Property(e => e.TableKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tableKey");

                entity.Property(e => e.AllotRule)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("allotRule");
            });

            modelBuilder.Entity<FineSwiftTablePath>(entity =>
            {
                entity.HasKey(e => new { e.ClusterId, e.TableKey })
                    .HasName("PK__fine_swi__DF4782DF69E9E1FC");

                entity.ToTable("fine_swift_table_path", "db_owner");

                entity.Property(e => e.ClusterId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("clusterId");

                entity.Property(e => e.TableKey)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tableKey");

                entity.Property(e => e.LastPath).HasColumnName("lastPath");

                entity.Property(e => e.TablePath).HasColumnName("tablePath");

                entity.Property(e => e.TmpDir).HasColumnName("tmpDir");
            });

            modelBuilder.Entity<FineSystemMessage>(entity =>
            {
                entity.ToTable("fine_system_message", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Terminal).HasColumnName("terminal");

                entity.Property(e => e.Title)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<FineTenant>(entity =>
            {
                entity.ToTable("fine_tenant", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<FineUsageDatum>(entity =>
            {
                entity.ToTable("fine_usage_data", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("data");

                entity.Property(e => e.DataType).HasColumnName("dataType");

                entity.Property(e => e.SubType).HasColumnName("subType");

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tag");
            });

            modelBuilder.Entity<FineUser>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TenantId })
                    .HasName("PK__fine_use__69528B0D1D5A41C6");

                entity.ToTable("fine_user", "db_owner");

                entity.HasIndex(e => e.RealAlias, "IDX3tp6gfib0xg57j3gvw9el58mo");

                entity.HasIndex(e => e.UserAlias, "IDX4vir3vqq6fthqbfdtw8em2m0l");

                entity.HasIndex(e => new { e.UserName, e.TenantId }, "UK548m71akign8vsov5smlm4uwx")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.TenantId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tenantId")
                    .HasDefaultValueSql("('default')");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.CreationType).HasColumnName("creationType");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Enable).HasColumnName("enable");

                entity.Property(e => e.Language)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("language");

                entity.Property(e => e.LastOperationType).HasColumnName("lastOperationType");

                entity.Property(e => e.Male).HasColumnName("male");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("mobile");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RealAlias)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("realAlias");

                entity.Property(e => e.RealName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("realName");

                entity.Property(e => e.Salt)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("salt");

                entity.Property(e => e.UserAlias)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userAlias");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.Property(e => e.WorkPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("workPhone");
            });

            modelBuilder.Entity<FineUserRoleMiddle>(entity =>
            {
                entity.ToTable("fine_user_role_middle", "db_owner");

                entity.HasIndex(e => e.RoleId, "IDX8sr9fqbahqne0k3utap5kfx24");

                entity.HasIndex(e => e.UserId, "IDX8yknqk4qau61k1h614i87emdn");

                entity.HasIndex(e => e.RoleType, "IDXtmv6e3k2fr907sfbhuu1blkd6");

                entity.HasIndex(e => new { e.UserId, e.RoleId, e.RoleType, e.TenantId }, "UKrjlfin3kwa87iecxe4mqvr7if")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("roleId");

                entity.Property(e => e.RoleType).HasColumnName("roleType");

                entity.Property(e => e.TenantId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tenantId")
                    .HasDefaultValueSql("('default')");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userId");
            });

            modelBuilder.Entity<FineVc>(entity =>
            {
                entity.ToTable("fine_vcs", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CommitCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("commitCode");

                entity.Property(e => e.CommitMsg)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("commitMsg");

                entity.Property(e => e.Filename)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("filename");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<FineWorkflow>(entity =>
            {
                entity.ToTable("fine_workflow", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateTime).HasColumnName("createTime");

                entity.Property(e => e.CreatorId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("creatorId");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.NodesId)
                    .IsUnicode(false)
                    .HasColumnName("nodesId");
            });

            modelBuilder.Entity<FineWorkflowLog>(entity =>
            {
                entity.ToTable("fine_workflow_log", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.DateTime).HasColumnName("dateTime");

                entity.Property(e => e.Message)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("operatorName");

                entity.Property(e => e.ProcessName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("processName");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskName");
            });

            modelBuilder.Entity<FineWorkflowNode>(entity =>
            {
                entity.ToTable("fine_workflow_node", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.AlertControl)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("alertControl");

                entity.Property(e => e.Authority)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("authority");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.NeedAllComplete).HasColumnName("needAllComplete");

                entity.Property(e => e.NeedOfflineReport).HasColumnName("needOfflineReport");

                entity.Property(e => e.ProcessId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("processId");

                entity.Property(e => e.ReportControl)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("reportControl");
            });

            modelBuilder.Entity<FineWorkflowStashDatum>(entity =>
            {
                entity.ToTable("fine_workflow_stash_data", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Data)
                    .IsUnicode(false)
                    .HasColumnName("data");

                entity.Property(e => e.ReportPath)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("reportPath");

                entity.Property(e => e.TaskId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskId");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userId");
            });

            modelBuilder.Entity<FineWorkflowTask>(entity =>
            {
                entity.ToTable("fine_workflow_task", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateTime).HasColumnName("createTime");

                entity.Property(e => e.CreatorId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("creatorId");

                entity.Property(e => e.CreatorName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("creatorName");

                entity.Property(e => e.DeadLineDate).HasColumnName("deadLineDate");

                entity.Property(e => e.DeadLineType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("deadLineType");

                entity.Property(e => e.IssueControl)
                    .IsUnicode(false)
                    .HasColumnName("issueControl");

                entity.Property(e => e.IssueOver).HasColumnName("issueOver");

                entity.Property(e => e.LeapfrogBack).HasColumnName("leapfrogBack");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("parentId");

                entity.Property(e => e.ProcessId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("processId");

                entity.Property(e => e.RemindControl)
                    .IsUnicode(false)
                    .HasColumnName("remindControl");

                entity.Property(e => e.TaskNameCalculateOnce).HasColumnName("taskNameCalculateOnce");
            });

            modelBuilder.Entity<FineWorkflowTaskImpl>(entity =>
            {
                entity.ToTable("fine_workflow_task_impl", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Alerted).HasColumnName("alerted");

                entity.Property(e => e.CompleteState)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("completeState");

                entity.Property(e => e.CreateTime).HasColumnName("createTime");

                entity.Property(e => e.CurrentNodeIdx).HasColumnName("currentNodeIdx");

                entity.Property(e => e.DeadLine).HasColumnName("deadLine");

                entity.Property(e => e.FrTaskId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("frTaskId");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.NeedAllComplete).HasColumnName("needAllComplete");

                entity.Property(e => e.NodeRoute)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("nodeRoute");

                entity.Property(e => e.Note)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.OperatorJson)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("operatorJSON");

                entity.Property(e => e.OperatorOffset)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("operatorOffset");

                entity.Property(e => e.OperatorOffsetName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("operatorOffsetName");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("parentId");

                entity.Property(e => e.ProcessId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("processId");

                entity.Property(e => e.ReportOffset).HasColumnName("reportOffset");

                entity.Property(e => e.SendTime).HasColumnName("sendTime");

                entity.Property(e => e.Sender)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("sender");

                entity.Property(e => e.SenderId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("senderId");

                entity.Property(e => e.SonTaskId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("sonTaskId");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.TaskBackTarget)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskBackTarget");

                entity.Property(e => e.TaskId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskId");
            });

            modelBuilder.Entity<FineWriteStash>(entity =>
            {
                entity.ToTable("fine_write_stash", "db_owner");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Data)
                    .IsUnicode(false)
                    .HasColumnName("data");

                entity.Property(e => e.ReportPath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("reportPath");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Inquiry>(entity =>
            {
                entity.ToTable("Inquiry");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.EscalationRequestedDate).HasMaxLength(50);

                entity.Property(e => e.InquiryCode).HasMaxLength(4000);

                entity.Property(e => e.InquiryComment).HasMaxLength(500);

                entity.Property(e => e.InquiryCommentsAddedOn).HasMaxLength(50);

                entity.Property(e => e.InquiryDescription).HasMaxLength(500);

                entity.Property(e => e.InquiryDueDate).HasMaxLength(50);

                entity.Property(e => e.InquiryEndDate).HasMaxLength(50);

                entity.Property(e => e.InquiryName).HasMaxLength(500);

                entity.Property(e => e.InquiryStartDate).HasMaxLength(50);

                entity.Property(e => e.MeasurementFees).HasMaxLength(50);

                entity.Property(e => e.PromoDiscount).HasMaxLength(50);

                entity.Property(e => e.QuotationAddedOn).HasMaxLength(50);

                entity.Property(e => e.QuotationScheduleDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

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

                entity.HasOne(d => d.EscalationRequestedByNavigation)
                    .WithMany(p => p.InquiryEscalationRequestedByNavigations)
                    .HasForeignKey(d => d.EscalationRequestedBy)
                    .HasConstraintName("FK_Inquiry_User2");

                entity.HasOne(d => d.InquiryStatus)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.InquiryStatusId)
                    .HasConstraintName("FK_Inquiry_InquiryStatus");

                entity.HasOne(d => d.ManagedByNavigation)
                    .WithMany(p => p.InquiryManagedByNavigations)
                    .HasForeignKey(d => d.ManagedBy)
                    .HasConstraintName("FK_Inquiry_User");

                entity.HasOne(d => d.Promo)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.PromoId)
                    .HasConstraintName("FK_Inquiry_Promo");

                entity.HasOne(d => d.QuotationAssignToNavigation)
                    .WithMany(p => p.InquiryQuotationAssignToNavigations)
                    .HasForeignKey(d => d.QuotationAssignTo)
                    .HasConstraintName("FK_Inquiry_User1");
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

                entity.Property(e => e.DesignAddedOn).HasMaxLength(50);

                entity.Property(e => e.DesignScheduleDate).HasMaxLength(50);

                entity.Property(e => e.MeasurementAddedOn).HasMaxLength(50);

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

            modelBuilder.Entity<ItemColor>(entity =>
            {
                entity.ToTable("ItemColor");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.Skucode).HasColumnName("SKUCode");

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ItemColors)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_ItemColor_Color");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemColors)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_ItemColor_ApplianceAccessory");
            });

            modelBuilder.Entity<JobOrder>(entity =>
            {
                entity.ToTable("JobOrder");

                entity.Property(e => e.CommercialCheckListCompletionDate).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.DataSheetApplianceFileUrl).HasColumnName("DataSheetApplianceFileURL");

                entity.Property(e => e.IsSpecialApprovalRequired).HasColumnName("Is SpecialApprovalRequired");

                entity.Property(e => e.JobOrderApprovalRequestDate).HasMaxLength(50);

                entity.Property(e => e.JobOrderChecklistFileUrl).HasColumnName("JobOrderChecklistFileURL");

                entity.Property(e => e.JobOrderCompletionDate).HasMaxLength(50);

                entity.Property(e => e.JobOrderDelayReason).HasMaxLength(500);

                entity.Property(e => e.JobOrderDeliveryDate).HasMaxLength(50);

                entity.Property(e => e.JobOrderDescription).HasMaxLength(500);

                entity.Property(e => e.JobOrderExpectedDeadline).HasMaxLength(500);

                entity.Property(e => e.JobOrderName).HasMaxLength(500);

                entity.Property(e => e.JobOrderRequestedComments).HasMaxLength(500);

                entity.Property(e => e.JobOrderRequestedDeadline).HasMaxLength(500);

                entity.Property(e => e.MaterialSheetFileUrl).HasColumnName("MaterialSheetFileURL");

                entity.Property(e => e.Mepdrawing).HasColumnName("MEPDrawing");

                entity.Property(e => e.MepdrawingFileUrl).HasColumnName("MEPDrawingFileURL");

                entity.Property(e => e.MepdrawingNotes).HasColumnName("MEPDrawingNotes");

                entity.Property(e => e.TechnicalCheckListCompletionDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Factory)
                    .WithMany(p => p.JobOrders)
                    .HasForeignKey(d => d.FactoryId)
                    .HasConstraintName("FK_JobOrder_Branch");

                entity.HasOne(d => d.Inquiry)
                    .WithMany(p => p.JobOrders)
                    .HasForeignKey(d => d.InquiryId)
                    .HasConstraintName("FK_JobOrder_Inquiry");
            });

            modelBuilder.Entity<JobOrderDetail>(entity =>
            {
                entity.ToTable("JobOrderDetail");

                entity.Property(e => e.CountertopFixingDate).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.InstallationEndDate).HasMaxLength(50);

                entity.Property(e => e.InstallationStartDate).HasMaxLength(50);

                entity.Property(e => e.JobOrderDetailDescription).HasMaxLength(500);

                entity.Property(e => e.JobOrderDetailName).HasMaxLength(50);

                entity.Property(e => e.MaterialDeliveryFinalDate).HasMaxLength(50);

                entity.Property(e => e.MaterialRequestDate).HasMaxLength(50);

                entity.Property(e => e.MissingDocuments).HasMaxLength(500);

                entity.Property(e => e.ProductionCompletionDate).HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.ShopDrawingCompletionDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.Property(e => e.WoodenWorkCompletionDate).HasMaxLength(50);

                entity.HasOne(d => d.JobOrder)
                    .WithMany(p => p.JobOrderDetails)
                    .HasForeignKey(d => d.JobOrderId)
                    .HasConstraintName("FK_JobOrderDetail_JobOrder");
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

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.MaterialName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.ToTable("Measurement");

                entity.Property(e => e.AddedDate).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.MeasurementApprovedOn).HasMaxLength(50);

                entity.Property(e => e.MeasurementComment).HasMaxLength(500);

                entity.Property(e => e.MeasurementName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

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

            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.ToTable("Newsletter");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.NewsletterAttachmentUrl).HasColumnName("NewsletterAttachmentURL");

                entity.Property(e => e.NewsletterSendingDate).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.AddedByNavigation)
                    .WithMany(p => p.Newsletters)
                    .HasForeignKey(d => d.AddedBy)
                    .HasConstraintName("FK_NewsLetter_User");

                entity.HasOne(d => d.NewsletterFrequency)
                    .WithMany(p => p.Newsletters)
                    .HasForeignKey(d => d.NewsletterFrequencyId)
                    .HasConstraintName("FK_NewsLetter_NewsletterFrequency");

                entity.HasOne(d => d.NewsletterType)
                    .WithMany(p => p.Newsletters)
                    .HasForeignKey(d => d.NewsletterTypeId)
                    .HasConstraintName("FK_NewsLetter_NewsletterType");
            });

            modelBuilder.Entity<NewsletterFrequency>(entity =>
            {
                entity.ToTable("NewsletterFrequency");

                entity.Property(e => e.NewsletterFrequencyId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.NewsletterFrequencyDescription).HasMaxLength(500);

                entity.Property(e => e.NewsletterFrequencyName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<NewsletterType>(entity =>
            {
                entity.ToTable("NewsletterType");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.NewsletterTypeName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
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

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.AmountRecievedDate).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(38, 0)");

                entity.Property(e => e.PaymentAmountinPercentage).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PaymentCompletionDate).HasMaxLength(50);

                entity.Property(e => e.PaymentDetail).HasMaxLength(500);

                entity.Property(e => e.PaymentExpectedDate).HasMaxLength(50);

                entity.Property(e => e.PaymentName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Fees)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.FeesId)
                    .HasConstraintName("FK_Payment_Fees");

                entity.HasOne(d => d.Inquiry)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.InquiryId)
                    .HasConstraintName("FK_Payment_Inquiry");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_Payment_PaymentStatus");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK_Payment_PaymentType");

                entity.HasOne(d => d.Quotation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.QuotationId)
                    .HasConstraintName("FK_Payment_Quotation");
            });

            modelBuilder.Entity<PaymentMode>(entity =>
            {
                entity.ToTable("PaymentMode");

                entity.Property(e => e.PaymentModeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PaymentModeDescription).HasMaxLength(500);

                entity.Property(e => e.PaymentModeName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.ToTable("PaymentStatus");

                entity.Property(e => e.PaymentStatusId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PaymentStatusDescription).HasMaxLength(500);

                entity.Property(e => e.PaymentStatusName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentType");

                entity.Property(e => e.PaymentTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PaymentTypeDescription).HasMaxLength(500);

                entity.Property(e => e.PaymentTypeName).HasMaxLength(50);

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

            modelBuilder.Entity<ProjectDetail>(entity =>
            {
                entity.ToTable("ProjectDetail");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.CommercialProject)
                    .WithMany(p => p.ProjectDetails)
                    .HasForeignKey(d => d.CommercialProjectId)
                    .HasConstraintName("FK_ProjectDetail_CommercialProject");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.ProjectDetails)
                    .HasForeignKey(d => d.MaterialId)
                    .HasConstraintName("FK_ProjectDetail_Material");

                entity.HasOne(d => d.ProjectStatus)
                    .WithMany(p => p.ProjectDetails)
                    .HasForeignKey(d => d.ProjectStatusId)
                    .HasConstraintName("FK_ProjectDetail_ProjectStatus");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProjectDetails)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK_ProjectDetail_Size");

                entity.HasOne(d => d.WorkScope)
                    .WithMany(p => p.ProjectDetails)
                    .HasForeignKey(d => d.WorkScopeId)
                    .HasConstraintName("FK_ProjectDetail_WorkScope");
            });

            modelBuilder.Entity<ProjectStatus>(entity =>
            {
                entity.ToTable("ProjectStatus");

                entity.Property(e => e.ProjectStatusId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.ProjectStatusName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Promo>(entity =>
            {
                entity.ToTable("Promo");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.IsPercentage).HasColumnName("isPercentage");

                entity.Property(e => e.PromoCode).HasMaxLength(500);

                entity.Property(e => e.PromoDiscount).HasMaxLength(50);

                entity.Property(e => e.PromoExpiryDate).HasMaxLength(50);

                entity.Property(e => e.PromoName).HasMaxLength(500);

                entity.Property(e => e.PromoStartDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("Promotion");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PromotionDescription).HasMaxLength(500);

                entity.Property(e => e.PromotionFile).HasMaxLength(50);

                entity.Property(e => e.PromotionName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.PromotionType)
                    .WithMany(p => p.Promotions)
                    .HasForeignKey(d => d.PromotionTypeId)
                    .HasConstraintName("FK_Promotion_PromotionType");
            });

            modelBuilder.Entity<PromotionType>(entity =>
            {
                entity.ToTable("PromotionType");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PromotionTypeDescription).HasMaxLength(50);

                entity.Property(e => e.PromotionTypeName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("PurchaseOrder");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PurchaseOrderActualDeliveryDate).HasMaxLength(50);

                entity.Property(e => e.PurchaseOrderDate).HasMaxLength(50);

                entity.Property(e => e.PurchaseOrderExpectedDeliveryDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.PurchaseRequest)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.PurchaseRequestId)
                    .HasConstraintName("FK_PurchaseOrder_PurchaseRequest");

                entity.HasOne(d => d.PurchaseStatus)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.PurchaseStatusId)
                    .HasConstraintName("FK_PurchaseOrder_PurchaseStatus");
            });

            modelBuilder.Entity<PurchaseRequest>(entity =>
            {
                entity.ToTable("PurchaseRequest");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PuchaseRequestFinalDeliveryDate).HasMaxLength(50);

                entity.Property(e => e.PurchaseRequestFinalDeliveryRequestedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.JobOrder)
                    .WithMany(p => p.PurchaseRequests)
                    .HasForeignKey(d => d.JobOrderId)
                    .HasConstraintName("FK_PurchaseRequest_JobOrder");

                entity.HasOne(d => d.PurchaseStatus)
                    .WithMany(p => p.PurchaseRequests)
                    .HasForeignKey(d => d.PurchaseStatusId)
                    .HasConstraintName("FK_PurchaseRequest_PurchaseStatus");
            });

            modelBuilder.Entity<PurchaseStatus>(entity =>
            {
                entity.ToTable("PurchaseStatus");

                entity.Property(e => e.PurchaseStatusId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.PurchaseStatusDescription).HasMaxLength(500);

                entity.Property(e => e.PurchaseStatusName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<QrtzBlobTrigger>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerGroup, e.TriggerName })
                    .HasName("PK__QRTZ_BLO__922200A78645588A");

                entity.ToTable("QRTZ_BLOB_TRIGGERS", "db_owner");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.TriggerGroup)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_GROUP");

                entity.Property(e => e.TriggerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_NAME");

                entity.Property(e => e.BlobData).HasColumnName("BLOB_DATA");
            });

            modelBuilder.Entity<QrtzCalendar>(entity =>
            {
                entity.HasKey(e => new { e.CalendarName, e.SchedName })
                    .HasName("PK__QRTZ_CAL__DCFFF5BC3CE5FA0B");

                entity.ToTable("QRTZ_CALENDARS", "db_owner");

                entity.Property(e => e.CalendarName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CALENDAR_NAME");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.Calendar)
                    .HasMaxLength(4000)
                    .HasColumnName("CALENDAR");
            });

            modelBuilder.Entity<QrtzCronTrigger>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerGroup, e.TriggerName })
                    .HasName("PK__QRTZ_CRO__922200A728CFB7E3");

                entity.ToTable("QRTZ_CRON_TRIGGERS", "db_owner");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.TriggerGroup)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_GROUP");

                entity.Property(e => e.TriggerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_NAME");

                entity.Property(e => e.CronExpression)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CRON_EXPRESSION");

                entity.Property(e => e.TimeZoneId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TIME_ZONE_ID");
            });

            modelBuilder.Entity<QrtzFiredTrigger>(entity =>
            {
                entity.HasKey(e => new { e.EntryId, e.SchedName })
                    .HasName("PK__QRTZ_FIR__4E11BD66FA490D7A");

                entity.ToTable("QRTZ_FIRED_TRIGGERS", "db_owner");

                entity.Property(e => e.EntryId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ENTRY_ID");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.FiredTime)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("FIRED_TIME");

                entity.Property(e => e.InstanceName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INSTANCE_NAME");

                entity.Property(e => e.IsNonconcurrent).HasColumnName("IS_NONCONCURRENT");

                entity.Property(e => e.JobGroup)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JOB_GROUP");

                entity.Property(e => e.JobName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JOB_NAME");

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.RequestsRecovery).HasColumnName("REQUESTS_RECOVERY");

                entity.Property(e => e.SchedTime)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("SCHED_TIME");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.TriggerGroup)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_GROUP");

                entity.Property(e => e.TriggerName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_NAME");
            });

            modelBuilder.Entity<QrtzJobDetail>(entity =>
            {
                entity.HasKey(e => new { e.JobGroup, e.JobName, e.SchedName })
                    .HasName("PK__QRTZ_JOB__1A47ADFBD81A9DBF");

                entity.ToTable("QRTZ_JOB_DETAILS", "db_owner");

                entity.Property(e => e.JobGroup)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JOB_GROUP");

                entity.Property(e => e.JobName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JOB_NAME");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.Creator)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CREATOR");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.IsDurable).HasColumnName("IS_DURABLE");

                entity.Property(e => e.IsNonconcurrent).HasColumnName("IS_NONCONCURRENT");

                entity.Property(e => e.IsUpdateData).HasColumnName("IS_UPDATE_DATA");

                entity.Property(e => e.JobClassName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JOB_CLASS_NAME");

                entity.Property(e => e.JobData).HasColumnName("JOB_DATA");

                entity.Property(e => e.RequestsRecovery).HasColumnName("REQUESTS_RECOVERY");
            });

            modelBuilder.Entity<QrtzLock>(entity =>
            {
                entity.HasKey(e => new { e.LockName, e.SchedName })
                    .HasName("PK__QRTZ_LOC__E5C51B86241E10AE");

                entity.ToTable("QRTZ_LOCKS", "db_owner");

                entity.Property(e => e.LockName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("LOCK_NAME");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");
            });

            modelBuilder.Entity<QrtzPausedTriggerGrp>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerGroup })
                    .HasName("PK__QRTZ_PAU__696155E9E416F77A");

                entity.ToTable("QRTZ_PAUSED_TRIGGER_GRPS", "db_owner");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.TriggerGroup)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_GROUP");
            });

            modelBuilder.Entity<QrtzSchedulerState>(entity =>
            {
                entity.HasKey(e => new { e.InstanceName, e.SchedName })
                    .HasName("PK__QRTZ_SCH__BB16A25D12F3B980");

                entity.ToTable("QRTZ_SCHEDULER_STATE", "db_owner");

                entity.Property(e => e.InstanceName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("INSTANCE_NAME");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.CheckinInterval)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("CHECKIN_INTERVAL");

                entity.Property(e => e.LastCheckinTime)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("LAST_CHECKIN_TIME");
            });

            modelBuilder.Entity<QrtzSimpleTrigger>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerGroup, e.TriggerName })
                    .HasName("PK__QRTZ_SIM__922200A7BFD67733");

                entity.ToTable("QRTZ_SIMPLE_TRIGGERS", "db_owner");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.TriggerGroup)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_GROUP");

                entity.Property(e => e.TriggerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_NAME");

                entity.Property(e => e.RepeatCount)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("REPEAT_COUNT");

                entity.Property(e => e.RepeatInterval)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("REPEAT_INTERVAL");

                entity.Property(e => e.TimesTriggered)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("TIMES_TRIGGERED");
            });

            modelBuilder.Entity<QrtzSimpropTrigger>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerGroup, e.TriggerName })
                    .HasName("PK__QRTZ_SIM__922200A7C3E66C3B");

                entity.ToTable("QRTZ_SIMPROP_TRIGGERS", "db_owner");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.TriggerGroup)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_GROUP");

                entity.Property(e => e.TriggerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_NAME");

                entity.Property(e => e.BoolProp1).HasColumnName("BOOL_PROP_1");

                entity.Property(e => e.BoolProp2).HasColumnName("BOOL_PROP_2");

                entity.Property(e => e.DecProp1)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("DEC_PROP_1");

                entity.Property(e => e.DecProp2)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("DEC_PROP_2");

                entity.Property(e => e.IntProp1).HasColumnName("INT_PROP_1");

                entity.Property(e => e.IntProp2).HasColumnName("INT_PROP_2");

                entity.Property(e => e.LongProp1)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("LONG_PROP_1");

                entity.Property(e => e.LongProp2)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("LONG_PROP_2");

                entity.Property(e => e.StrProp1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STR_PROP_1");

                entity.Property(e => e.StrProp2)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STR_PROP_2");

                entity.Property(e => e.StrProp3)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("STR_PROP_3");
            });

            modelBuilder.Entity<QrtzTrigger>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerGroup, e.TriggerName })
                    .HasName("PK__QRTZ_TRI__922200A7C79606C7");

                entity.ToTable("QRTZ_TRIGGERS", "db_owner");

                entity.Property(e => e.SchedName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SCHED_NAME");

                entity.Property(e => e.TriggerGroup)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_GROUP");

                entity.Property(e => e.TriggerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_NAME");

                entity.Property(e => e.AppointId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("APPOINT_ID");

                entity.Property(e => e.CalendarName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CALENDAR_NAME");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.EndTime)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("END_TIME");

                entity.Property(e => e.JobData).HasColumnName("JOB_DATA");

                entity.Property(e => e.JobGroup)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JOB_GROUP");

                entity.Property(e => e.JobName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("JOB_NAME");

                entity.Property(e => e.MisfireInstr).HasColumnName("MISFIRE_INSTR");

                entity.Property(e => e.NextFireTime)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("NEXT_FIRE_TIME");

                entity.Property(e => e.PrevFireTime)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("PREV_FIRE_TIME");

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.StartTime)
                    .HasColumnType("numeric(19, 2)")
                    .HasColumnName("START_TIME");

                entity.Property(e => e.TriggerState)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_STATE");

                entity.Property(e => e.TriggerType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TRIGGER_TYPE");
            });

            modelBuilder.Entity<Quotation>(entity =>
            {
                entity.ToTable("Quotation");

                entity.Property(e => e.AdvancePayment).HasMaxLength(500);

                entity.Property(e => e.Amount).HasMaxLength(50);

                entity.Property(e => e.CalculationSheetFile).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.Discount).HasMaxLength(500);

                entity.Property(e => e.IsInstallment).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProposalReferenceNumber).HasMaxLength(500);

                entity.Property(e => e.QuotationAddedDate).HasMaxLength(50);

                entity.Property(e => e.QuotationCode).HasMaxLength(500);

                entity.Property(e => e.QuotationCustomerReviewDate).HasMaxLength(50);

                entity.Property(e => e.QuotationValidityDate).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.Property(e => e.Vat).HasMaxLength(50);

                entity.HasOne(d => d.Inquiry)
                    .WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.InquiryId)
                    .HasConstraintName("FK_Quotation_Inquiry");

                entity.HasOne(d => d.QuotationStatus)
                    .WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.QuotationStatusId)
                    .HasConstraintName("FK_Quotation_InquiryStatus");
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

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.SettingDescription).HasMaxLength(500);

                entity.Property(e => e.SettingName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.SizeHeight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SizeName).HasMaxLength(50);

                entity.Property(e => e.SizeWidth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Sizes)
                    .HasForeignKey(d => d.MaterialId)
                    .HasConstraintName("FK_Size_Material");
            });

            modelBuilder.Entity<TermsAndCondition>(entity =>
            {
                entity.HasKey(e => e.TermsAndConditionsId);

                entity.Property(e => e.TermsAndConditionsId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<UnitOfMeasurement>(entity =>
            {
                entity.ToTable("UnitOfMeasurement");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.UnitOfMeasurementDescription).HasMaxLength(500);

                entity.Property(e => e.UnitOfMeasurementName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.CreatedDate).HasMaxLength(50);

                entity.Property(e => e.LastSeen).HasMaxLength(500);

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
