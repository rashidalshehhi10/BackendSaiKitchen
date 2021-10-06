/****** Object:  Database [BackendSaiKitchen_db]    Script Date: 06/10/2021 10:57:01 AM ******/
CREATE DATABASE [BackendSaiKitchen_db]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [BackendSaiKitchen_db] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ANSI_NULLS ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ANSI_PADDING ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ARITHABORT ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET  MULTI_USER 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ENCRYPTION ON
GO
ALTER DATABASE [BackendSaiKitchen_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [BackendSaiKitchen_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = ALL, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_diagramobjects]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE FUNCTION [dbo].[fn_diagramobjects]() 
	RETURNS int
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		declare @id_upgraddiagrams		int
		declare @id_sysdiagrams			int
		declare @id_helpdiagrams		int
		declare @id_helpdiagramdefinition	int
		declare @id_creatediagram	int
		declare @id_renamediagram	int
		declare @id_alterdiagram 	int 
		declare @id_dropdiagram		int
		declare @InstalledObjects	int

		select @InstalledObjects = 0

		select 	@id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),
			@id_sysdiagrams = object_id(N'dbo.sysdiagrams'),
			@id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),
			@id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),
			@id_creatediagram = object_id(N'dbo.sp_creatediagram'),
			@id_renamediagram = object_id(N'dbo.sp_renamediagram'),
			@id_alterdiagram = object_id(N'dbo.sp_alterdiagram'), 
			@id_dropdiagram = object_id(N'dbo.sp_dropdiagram')

		if @id_upgraddiagrams is not null
			select @InstalledObjects = @InstalledObjects + 1
		if @id_sysdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 2
		if @id_helpdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 4
		if @id_helpdiagramdefinition is not null
			select @InstalledObjects = @InstalledObjects + 8
		if @id_creatediagram is not null
			select @InstalledObjects = @InstalledObjects + 16
		if @id_renamediagram is not null
			select @InstalledObjects = @InstalledObjects + 32
		if @id_alterdiagram  is not null
			select @InstalledObjects = @InstalledObjects + 64
		if @id_dropdiagram is not null
			select @InstalledObjects = @InstalledObjects + 128
		
		return @InstalledObjects 
	END
	
GO
/****** Object:  Table [dbo].[Accesories]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accesories](
	[AccesoriesId] [int] IDENTITY(1,1) NOT NULL,
	[WardrobeDesignInfoId] [int] NULL,
	[AccesoriesName] [nvarchar](500) NULL,
	[AccesoriesValue] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Accesories] PRIMARY KEY CLUSTERED 
(
	[AccesoriesId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appliances]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appliances](
	[AppliancesId] [int] IDENTITY(1,1) NOT NULL,
	[KitchenDesignInfoId] [int] NULL,
	[AppliancesName] [nvarchar](50) NULL,
	[AppliancesValue] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Appliances] PRIMARY KEY CLUSTERED 
(
	[AppliancesId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [nvarchar](50) NULL,
	[BranchAddress] [nvarchar](500) NULL,
	[BranchContact] [nvarchar](50) NULL,
	[BranchTypeId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_branch] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BranchRole]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchRole](
	[BranchRoleId] [int] IDENTITY(1,1) NOT NULL,
	[BranchRoleName] [nvarchar](50) NULL,
	[BranchRoleDescription] [nvarchar](500) NULL,
	[RoleTypeId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_BranchRole] PRIMARY KEY CLUSTERED 
(
	[BranchRoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BranchType]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchType](
	[BranchTypeId] [int] NOT NULL,
	[BranchTypeName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_BranchType] PRIMARY KEY CLUSTERED 
(
	[BranchTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Building]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Building](
	[BuildingId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingAddress] [nvarchar](500) NULL,
	[BuildingTypeOfUnit] [nvarchar](500) NULL,
	[BuildingCondition] [nvarchar](500) NULL,
	[BuildingFloor] [nvarchar](500) NULL,
	[BuildingReconstruction] [bit] NULL,
	[IsOccupied] [bit] NULL,
	[BuildingMakaniMap] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_bUILDING] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalendarEvent]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalendarEvent](
	[CalendarEventId] [int] IDENTITY(1,1) NOT NULL,
	[CalendarEventName] [nvarchar](500) NULL,
	[CalendarEventDescription] [nvarchar](max) NULL,
	[CalendarEventOnClickURL] [nvarchar](500) NULL,
	[CalendarEventDate] [nvarchar](500) NULL,
	[EventTypeId] [int] NULL,
	[UserId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_CalendarEvent] PRIMARY KEY CLUSTERED 
(
	[CalendarEventId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactStatus]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactStatus](
	[ContactStatusId] [int] NOT NULL,
	[ContactStatusName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_ContactStatus] PRIMARY KEY CLUSTERED 
(
	[ContactStatusId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](50) NULL,
	[CustomerEmail] [nvarchar](50) NULL,
	[CustomerContact] [nvarchar](50) NULL,
	[CustomerAddress] [nvarchar](max) NULL,
	[CustomerCity] [nvarchar](max) NULL,
	[CustomerCountry] [nvarchar](max) NULL,
	[CustomerNationality] [nvarchar](max) NULL,
	[CustomerNationalId] [nvarchar](max) NULL,
	[CustomerNotes] [nvarchar](max) NULL,
	[CustomerNextMeetingDate] [nvarchar](50) NULL,
	[ContactStatusId] [int] NULL,
	[WayofContactId] [int] NULL,
	[BranchId] [int] NULL,
	[UserId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsEscalationRequested] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[CustomerWhatsapp] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerBranch]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerBranch](
	[CustomerBranchId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[BranchId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_CustomerBranch] PRIMARY KEY CLUSTERED 
(
	[CustomerBranchId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Design]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Design](
	[DesignId] [int] IDENTITY(1,1) NOT NULL,
	[DesignName] [nvarchar](50) NULL,
	[DesignDescription] [nvarchar](500) NULL,
	[DesignComment] [nvarchar](max) NULL,
	[StatusId] [int] NULL,
	[InquiryWorkscopeId] [int] NULL,
	[DesignTakenBy] [int] NULL,
	[IsDesignApproved] [bit] NULL,
	[DesignApprovedBy] [int] NULL,
	[DesignApprovedon] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[DesignAddedDate] [nvarchar](50) NULL,
	[DesignAddedBy] [int] NULL,
	[DesignCustomerReviewDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Design] PRIMARY KEY CLUSTERED 
(
	[DesignId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventType](
	[EventTypeId] [int] NOT NULL,
	[EventTypeName] [nvarchar](500) NULL,
	[EventTypeDescription] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED 
(
	[EventTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fees]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fees](
	[FeesId] [int] NOT NULL,
	[FeesName] [nvarchar](500) NULL,
	[FeesDescription] [nvarchar](500) NULL,
	[FeesAmount] [nvarchar](500) NULL,
	[IsPercentage] [bit] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Fees] PRIMARY KEY CLUSTERED 
(
	[FeesId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[File]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](50) NULL,
	[FileImage] [image] NULL,
	[FileURL] [nvarchar](max) NULL,
	[FileContentType] [nvarchar](max) NULL,
	[IsImage] [bit] NULL,
	[MeasurementId] [int] NULL,
	[DesignId] [int] NULL,
	[QuotationId] [int] NULL,
	[Paymentid] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_MeasurementFile] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inquiry]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inquiry](
	[InquiryId] [int] IDENTITY(1,1) NOT NULL,
	[InquiryCode] [nvarchar](4000) NULL,
	[InquiryName] [nvarchar](500) NULL,
	[InquiryDescription] [nvarchar](500) NULL,
	[InquiryStartDate] [nvarchar](50) NULL,
	[InquiryDueDate] [nvarchar](50) NULL,
	[InquiryEndDate] [nvarchar](50) NULL,
	[IsMeasurementProvidedByCustomer] [bit] NULL,
	[IsDesignProvidedByCustomer] [bit] NULL,
	[MeasurementFees] [nvarchar](50) NULL,
	[InquiryComment] [nvarchar](500) NULL,
	[InquiryCommentsAddedOn] [nvarchar](50) NULL,
	[QuotationAssignTo] [int] NULL,
	[QuotationScheduleDate] [nvarchar](50) NULL,
	[QuotationAddedOn] [nvarchar](50) NULL,
	[CustomerId] [int] NULL,
	[BranchId] [int] NULL,
	[BuildingId] [int] NULL,
	[PromoId] [int] NULL,
	[PromoDiscount] [nvarchar](50) NULL,
	[IsMeasurementPromo] [bit] NULL,
	[IsEscalationRequested] [bit] NULL,
	[InquiryStatusId] [int] NULL,
	[ManagedBy] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Inquiry] PRIMARY KEY CLUSTERED 
(
	[InquiryId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InquiryStatus]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InquiryStatus](
	[InquiryStatusId] [int] NOT NULL,
	[InquiryStatusName] [nvarchar](500) NULL,
	[InquiryStatusDescription] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_InquiryStatus] PRIMARY KEY CLUSTERED 
(
	[InquiryStatusId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InquiryWorkscope]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InquiryWorkscope](
	[InquiryWorkscopeId] [int] IDENTITY(1,1) NOT NULL,
	[InquiryId] [int] NULL,
	[WorkscopeId] [int] NULL,
	[IsMeasurementDrawing] [bit] NULL,
	[MeasurementAssignedTo] [int] NULL,
	[DesignAssignedTo] [int] NULL,
	[InquiryStatusId] [int] NULL,
	[MeasurementScheduleDate] [nvarchar](50) NULL,
	[MeasurementAddedOn] [nvarchar](50) NULL,
	[DesignScheduleDate] [nvarchar](50) NULL,
	[IsDesignApproved] [bit] NULL,
	[DesignAddedOn] [nvarchar](50) NULL,
	[Comments] [nvarchar](max) NULL,
	[FeedbackReaction] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_InquiryMeasurement] PRIMARY KEY CLUSTERED 
(
	[InquiryWorkscopeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobOrder]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobOrder](
	[JobOrderId] [int] IDENTITY(1,1) NOT NULL,
	[JobOrderCode] [nvarchar](max) NULL,
	[JobOrderName] [nvarchar](500) NULL,
	[JobOrderDescription] [nvarchar](500) NULL,
	[JobOrderRequestedDeadline] [nvarchar](500) NULL,
	[JobOrderExpectedDeadline] [nvarchar](500) NULL,
	[JobOrderRequestedComments] [nvarchar](500) NULL,
	[JobOrderDelayReason] [nvarchar](500) NULL,
	[JobOrderDeliveryDate] [nvarchar](50) NULL,
	[InquiryId] [int] NULL,
	[MaterialSheetFileURL] [nvarchar](max) NULL,
	[MEPDrawingFileURL] [nvarchar](max) NULL,
	[JobOrderChecklistFileURL] [nvarchar](max) NULL,
	[DataSheetApplianceFileURL] [nvarchar](max) NULL,
	[IsAppliancesProvidedByClient] [bit] NULL,
	[FeedbackReaction] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[FactoryId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[SiteMeasurementMatchingWithDesign] [bit] NULL,
	[MaterialConfirmation] [bit] NULL,
	[MEPDrawing] [bit] NULL,
	[QuotationAndCalculationSheetMatchingProposal] [bit] NULL,
	[ApprovedDrawingsAndAvailabilityOfClientSignature] [bit] NULL,
	[AppliancesDataSheet] [bit] NULL,
	[SiteMeasurementMatchingWithDesignNotes] [nvarchar](max) NULL,
	[MaterialConfirmationNotes] [nvarchar](max) NULL,
	[MEPDrawingNotes] [nvarchar](max) NULL,
	[QuotationAndCalculationSheetMatchingProposalNotes] [nvarchar](max) NULL,
	[ApprovedDrawingsAndAvailabilityOfClientSignatureNotes] [nvarchar](max) NULL,
	[AppliancesDataSheetNotes] [nvarchar](max) NULL,
 CONSTRAINT [PK_JobOrder] PRIMARY KEY CLUSTERED 
(
	[JobOrderId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobOrderDetail]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobOrderDetail](
	[JobOrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[JobOrderId] [int] NULL,
	[JobOrderDetailName] [nvarchar](50) NULL,
	[JobOrderDetailDescription] [nvarchar](500) NULL,
	[MaterialAvailabilityDate] [nvarchar](50) NULL,
	[ShopDrawingCompletionDate] [nvarchar](50) NULL,
	[MissingDocuments] [nvarchar](500) NULL,
	[ProductionCompletionDate] [nvarchar](50) NULL,
	[WoodenWorkCompletionDate] [nvarchar](50) NULL,
	[MaterialDeliveryFinalDate] [nvarchar](50) NULL,
	[CountertopFixingDate] [nvarchar](50) NULL,
	[InstallationStartDate] [nvarchar](50) NULL,
	[InstallationEndDate] [nvarchar](50) NULL,
	[IsNewlyRequested] [bit] NULL,
	[IsFromFactory] [bit] NULL,
	[Remarks] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_JobOrderDetail] PRIMARY KEY CLUSTERED 
(
	[JobOrderDetailId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KitchenDesignInfo]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KitchenDesignInfo](
	[KDIId] [int] IDENTITY(1,1) NOT NULL,
	[KDIKitchenType] [nvarchar](50) NULL,
	[KDIBoardModelCarcass] [nvarchar](50) NULL,
	[KDIBoardModelCarcassColor] [nvarchar](50) NULL,
	[KDIBoradModelDoorShutter] [nvarchar](50) NULL,
	[KDIBoardModelDoorShutterColor] [nchar](10) NULL,
	[KDIBaseUnitHeight] [nvarchar](50) NULL,
	[KDIWallUnitHeight] [nvarchar](50) NULL,
	[KDINotes] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_KitchenDesignInfo] PRIMARY KEY CLUSTERED 
(
	[KDIId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](128) NULL,
	[TimeStamp] [datetimeoffset](7) NOT NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [xml] NULL,
	[LogEvent] [nvarchar](max) NULL,
	[UserName] [nvarchar](200) NULL,
	[IP] [varchar](200) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Measurement]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Measurement](
	[MeasurementId] [int] IDENTITY(1,1) NOT NULL,
	[MeasurementName] [nvarchar](500) NULL,
	[MeasurementDescription] [nvarchar](max) NULL,
	[MeasurementDetailId] [int] NULL,
	[MeasurementStatusId] [int] NULL,
	[MeasurementComment] [nvarchar](500) NULL,
	[InquiryWorkscopeId] [int] NULL,
	[MeasurementTakenBy] [int] NULL,
	[KitchenDesignInfoId] [int] NULL,
	[WardrobeDesignInfoId] [int] NULL,
	[IsMeasurementApproved] [bit] NULL,
	[MeasurementApprovedBy] [int] NULL,
	[MeasurementApprovedOn] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[AddedDate] [nvarchar](50) NULL,
	[AddedBy] [int] NULL,
 CONSTRAINT [PK_Measurement] PRIMARY KEY CLUSTERED 
(
	[MeasurementId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasurementDetail]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasurementDetail](
	[MeasurementDetailId] [int] IDENTITY(1,1) NOT NULL,
	[MeasurementDetailName] [nvarchar](500) NULL,
	[MeasurementDetailDescription] [nvarchar](500) NULL,
	[MeasurementDetailCeilingHeight] [nvarchar](50) NULL,
	[MeasurementDetailCielingDiameter] [nvarchar](50) NULL,
	[MeasurementDetailCornishHeight] [nvarchar](50) NULL,
	[MeasurementDetailCornishDiameter] [nvarchar](50) NULL,
	[MeasurementDetailSkirtingHeight] [nvarchar](50) NULL,
	[MeasurementDetailSkirtingDiameter] [nvarchar](50) NULL,
	[MeasurementDetailPlinthHeight] [nvarchar](50) NULL,
	[MeasurementDetailPlinthDiameter] [nvarchar](50) NULL,
	[MeasurementDetailDoorHeight] [nvarchar](50) NULL,
	[MeasurementDetailSpotLightFromWall] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_MeasurementDetail] PRIMARY KEY CLUSTERED 
(
	[MeasurementDetailId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasurementDetailInfo]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasurementDetailInfo](
	[MeasurementDetailInfoId] [int] IDENTITY(1,1) NOT NULL,
	[MeasurementDetailInfoName] [nvarchar](50) NULL,
	[MeasurementDetailInfoDistanceHeight] [nvarchar](50) NULL,
	[MeasurementDetailInfoDistanceLL] [nvarchar](50) NULL,
	[MeasurementDetailInfoDistanceRR] [nvarchar](50) NULL,
	[MeasurementDetailInfoDistanceHFF] [nvarchar](50) NULL,
	[MeasurementDetailId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_MeasurementDetailInfo] PRIMARY KEY CLUSTERED 
(
	[MeasurementDetailInfoId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[NotificationContent] [nvarchar](500) NULL,
	[NotificationCategoryId] [int] NULL,
	[IsActionable] [bit] NULL,
	[NotificationAcceptAction] [nvarchar](500) NULL,
	[NotificationDeclineAction] [nvarchar](500) NULL,
	[UserId] [int] NULL,
	[BranchId] [int] NULL,
	[UserRoleId] [int] NULL,
	[IsRead] [bit] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationCategory]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationCategory](
	[NotificationCategoryId] [int] NOT NULL,
	[NotificationCategoryName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_NotificationCateory] PRIMARY KEY CLUSTERED 
(
	[NotificationCategoryId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentName] [nvarchar](50) NULL,
	[PaymentDetail] [nvarchar](500) NULL,
	[PaymentAmount] [decimal](38, 0) NULL,
	[PaymentTypeId] [int] NULL,
	[PaymentStatusId] [int] NULL,
	[PaymentModeId] [int] NULL,
	[PaymentAmountinPercentage] [decimal](18, 0) NULL,
	[PaymentExpectedDate] [nvarchar](50) NULL,
	[FeesId] [int] NULL,
	[InquiryId] [int] NULL,
	[QuotationId] [int] NULL,
	[PaymentIntentToken] [nvarchar](max) NULL,
	[ClientSecret] [nvarchar](max) NULL,
	[PaymentMethod] [nvarchar](max) NULL,
	[InvoiceCode] [nvarchar](max) NULL,
	[PaymentIntentResponse] [nvarchar](max) NULL,
	[PaymentResponse] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMode]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMode](
	[PaymentModeId] [int] NOT NULL,
	[PaymentModeName] [nvarchar](500) NULL,
	[PaymentModeDescription] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_PaymentMode] PRIMARY KEY CLUSTERED 
(
	[PaymentModeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentStatus]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentStatus](
	[PaymentStatusId] [int] NOT NULL,
	[PaymentStatusName] [nvarchar](50) NULL,
	[PaymentStatusDescription] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_PaymentStatus] PRIMARY KEY CLUSTERED 
(
	[PaymentStatusId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentType]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentType](
	[PaymentTypeId] [int] NOT NULL,
	[PaymentTypeName] [nvarchar](50) NULL,
	[PaymentTypeDescription] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_PaymentType] PRIMARY KEY CLUSTERED 
(
	[PaymentTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[PermissionId] [int] NOT NULL,
	[PermissionName] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_BranchPermission] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionLevel]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionLevel](
	[PermissionLevelId] [int] NOT NULL,
	[PermissionLevelName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_PermissionLevel] PRIMARY KEY CLUSTERED 
(
	[PermissionLevelId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionRole]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionRole](
	[PermissionRoleId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionId] [int] NULL,
	[BranchRoleId] [int] NULL,
	[PermissionLevelId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PermissionRole] PRIMARY KEY CLUSTERED 
(
	[PermissionRoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promo]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promo](
	[PromoId] [int] IDENTITY(1,1) NOT NULL,
	[PromoName] [nvarchar](500) NULL,
	[PromoDescription] [nvarchar](max) NULL,
	[PromoTermsAndCondition] [nvarchar](max) NULL,
	[PromoCode] [nvarchar](500) NULL,
	[isPercentage] [bit] NULL,
	[PromoDiscount] [nvarchar](50) NULL,
	[PromoCurrency] [int] NULL,
	[PromoStartDate] [nvarchar](50) NULL,
	[PromoExpiryDate] [nvarchar](50) NULL,
	[IsMeasurementPromo] [bit] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Promo] PRIMARY KEY CLUSTERED 
(
	[PromoId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quotation]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotation](
	[QuotationId] [int] IDENTITY(1,1) NOT NULL,
	[InquiryId] [int] NULL,
	[QuotationCode] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[QuotationValidityDate] [nvarchar](50) NULL,
	[AdvancePayment] [nvarchar](500) NULL,
	[BeforeInstallation] [nvarchar](max) NULL,
	[AfterDelivery] [nvarchar](max) NULL,
	[Amount] [nvarchar](50) NULL,
	[TotalAmount] [nvarchar](500) NULL,
	[Vat] [nvarchar](50) NULL,
	[Discount] [nvarchar](500) NULL,
	[ProposalReferenceNumber] [nvarchar](500) NULL,
	[IsInstallment] [bit] NULL,
	[NoOfInstallment] [int] NULL,
	[FeedBackReactionId] [int] NULL,
	[QuotationStatusId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[QuotationAddedDate] [nvarchar](50) NULL,
	[QuotationAddedBy] [int] NULL,
	[QuotationCustomerReviewDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Quotation] PRIMARY KEY CLUSTERED 
(
	[QuotationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleHead]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleHead](
	[RoleHeadId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeRoleId] [int] NULL,
	[HeadRoleId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_RoleHead] PRIMARY KEY CLUSTERED 
(
	[RoleHeadId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleType]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleType](
	[RoleTypeId] [int] NOT NULL,
	[RoleTypeName] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_RoleType] PRIMARY KEY CLUSTERED 
(
	[RoleTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Setting]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[SettingId] [int] IDENTITY(1,1) NOT NULL,
	[SettingName] [nvarchar](500) NULL,
	[SettingDescription] [nvarchar](500) NULL,
	[SettingMeasurementDelay] [int] NULL,
	[SettingDesignDelay] [int] NULL,
	[SettingQuotationDelay] [int] NULL,
	[SettingNoActionDelayFromCustomer] [int] NULL,
	[SettingMaintenanceAfterMonth] [int] NULL,
	[SettingAssigneeDelay] [int] NULL,
	[SettingApprovalDelay] [int] NULL,
	[SettingCustomerContactDelay] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED 
(
	[SettingId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [sysname] NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TermsAndConditions]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TermsAndConditions](
	[TermsAndConditionsId] [int] NOT NULL,
	[TermsAndConditionsDetail] [nvarchar](max) NULL,
	[IsInstallmentTerms] [bit] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_TermsAndConditions] PRIMARY KEY CLUSTERED 
(
	[TermsAndConditionsId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserEmail] [nvarchar](50) NULL,
	[UserPassword] [nvarchar](50) NULL,
	[UserMobile] [nvarchar](50) NULL,
	[UserToken] [nvarchar](500) NULL,
	[UserProfileImageURL] [nvarchar](500) NULL,
	[UserFCMToken] [nvarchar](500) NULL,
	[LastSeen] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[IsOnline] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[BranchId] [int] NULL,
	[BranchRoleId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WardrobeDesignInformation]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WardrobeDesignInformation](
	[WDIId] [int] IDENTITY(1,1) NOT NULL,
	[WDIClosetType] [nvarchar](50) NULL,
	[WDIBoardModel] [nvarchar](50) NULL,
	[WDISelectedColor] [nvarchar](50) NULL,
	[WDICeilingHeight] [nvarchar](50) NULL,
	[WDIClosetHeight] [nvarchar](50) NULL,
	[WDIStorageDoor] [bit] NULL,
	[WDIDoorDesign] [nvarchar](50) NULL,
	[WDIHandleDesign] [nvarchar](50) NULL,
	[WDIDoorMaterial] [nvarchar](50) NULL,
	[WDINotes] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_WardrobeDesignInformation] PRIMARY KEY CLUSTERED 
(
	[WDIId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WayOfContact]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WayOfContact](
	[WayOfContactId] [int] NOT NULL,
	[WayOfContactName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_WayOfContact] PRIMARY KEY CLUSTERED 
(
	[WayOfContactId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkScope]    Script Date: 06/10/2021 10:57:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkScope](
	[WorkScopeId] [int] IDENTITY(1,1) NOT NULL,
	[WorkScopeName] [nvarchar](500) NULL,
	[WorkScopeDescription] [nvarchar](500) NULL,
	[QuestionaireType] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_WorkScope] PRIMARY KEY CLUSTERED 
(
	[WorkScopeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Branch] ON 

INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Head Office', N'202B Diamond Business Center 1, Arjan, Dubai', N'+97145514553', 1, 1, 0, NULL, NULL, 2, N'08/07/2021 03:06 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Sakamkam', N'Sakamkam Fujairah', N'+971506671659', 2, 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Umm Deira', N'Umm Al Quwain', N'+97145514553', 3, 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, N'Test', N'Arjan', N'971545552471', 2, 1, 1, 0, N'03/04/2021 13:44:40', 0, N'03/04/2021 13:48:11')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, N'Test', N'Arjan', N'971545552471', 2, 1, 1, 0, N'03/04/2021 19:15:34', 0, N'05/04/2021 20:01:26')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, N'rter', N'Arjan Dubai', N'971545552471', 2, 1, 1, 2, N'11/04/2021 10:56:16', 2, N'11/04/2021 10:56:41')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, N'Test', N'Arjan Dubai', N'971545552476', 2, 1, 1, 2, N'13/04/2021 09:47:39', 2, N'04/14/2021 12:03 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (15, N'Test Branch', N'Dubaiii', N'971545552475', 2, 1, 1, 2, N'04/14/2021 11:53 AM', 2, N'04/14/2021 11:54 AM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (16, N'test', N'Arjan Dubai', N'971545552471', 2, 1, 1, 2, N'04/22/2021 10:59 AM', 2, N'04/22/2021 11:13 AM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (17, N'test', N'Arjan', N'971545552471', 2, 1, 1, 2, N'04/27/2021 10:31 AM', 2, N'04/27/2021 10:31 AM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (18, N'Arjan Branch', N'Business Cnter', N'971545552471', 2, 1, 1, 2, N'05/03/2021 06:36 PM', 2, N'05/03/2021 06:36 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (19, N'Albarsha', N'dubai', N'971545555271', 2, 1, 1, 2, N'05/26/2021 11:06 AM', 2, N'05/26/2021 11:06 AM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (20, N'Jumera Branch', N'Jumera 3 Dubai', N'97154554275', 2, 1, 1, 2, N'06/15/2021 06:31 AM', 2, N'06/15/2021 06:31 AM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (21, N'Jumera', N'Jumera 1', N'971545552478', 2, 1, 1, 2, N'06/16/2021 10:58 AM', 2, N'06/16/2021 10:58 AM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (22, N'Syria', N'Zabadani,Syria', N'97197545216478', 2, 1, 0, 2, N'06/21/2021 12:20 PM', 2, N'06/21/2021 12:20 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (23, N'test', N'Dubai test', N'971324623665', 1, 1, 1, 2, N'07/18/2021 03:33 PM', 2, N'07/18/2021 04:50 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (24, N'testtest', N'Dubai test', N'971324623665', 1, 1, 1, 2, N'07/18/2021 05:21 PM', 2, N'08/01/2021 12:25 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (25, N'Test Factory', N'dubai', N'97154652150', 3, 1, 0, NULL, NULL, 2, N'09/29/2021 04:33 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (26, N'TestBranch1', N'dubi1', N'97154652151', 2, 1, 1, 2, N'08/03/2021 12:17 PM', 2, N'08/03/2021 12:19 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (27, N'TestBranch', N'dubi', N'9715465215', 1, 1, 1, 2, N'08/03/2021 12:18 PM', 2, N'08/03/2021 12:19 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (28, N'TestBranch', N'dubi', N'9715465215', 3, 1, 1, 2, N'08/03/2021 12:20 PM', 2, N'08/03/2021 12:29 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (29, N'TestBranch', N'dubi', N'9715465215', 3, 1, 1, 2, N'08/03/2021 12:24 PM', 2, N'08/03/2021 12:24 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (30, N'TestBranch', N'dubi', N'9715465215', 3, 1, 1, 2, N'08/03/2021 12:24 PM', 2, N'08/03/2021 12:29 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (31, N'TestBranch', N'dubi', N'9715465215', 2, 1, 1, 2, N'08/03/2021 12:30 PM', 2, N'08/03/2021 12:30 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (32, N'Jumeira showroom', N'jumeira3', N'97145514553', 2, 1, 0, 2, N'08/18/2021 10:29 AM', 2, N'09/06/2021 03:10 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (38, N'Umm Al Quwain Factory', N'​148, Al Aqran Street Umm Dera, Umm Al Quwain', N'+971 558439600', 3, 1, 0, 82, N'09/30/2021 09:24 AM', 82, N'09/30/2021 09:24 AM')
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO
SET IDENTITY_INSERT [dbo].[BranchRole] ON 

INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1, N'Head Manager', N'All access of one branch', 1, NULL, NULL, NULL, NULL, 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (14, N'Procurement Manager', N'Procurement access', 5, NULL, NULL, NULL, NULL, 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (15, N'Sales Manager', N'Sales related stuff', 3, NULL, NULL, NULL, NULL, 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (23, N'CEO', N'', 1, 0, N'31/03/2021 15:24:17', 2, N'09/29/2021 12:02 PM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (24, N'Sale Assistant', N'', 2, 0, N'31/03/2021 15:37:04', 0, N'03/04/2021 09:32:20', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (25, N'Sameer', N'test', 3, 0, N'03/04/2021 13:45:04', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (26, N'Interior Designer', N'test', 4, 0, N'03/04/2021 19:16:11', 0, N'05/04/2021 16:37:13', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (27, N'Test', N'test', 5, 0, N'08/04/2021 09:57:15', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (28, N'test 2', N'test', 1, 0, N'08/04/2021 10:32:01', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (29, N'edit', N'test', 2, 0, N'08/04/2021 10:40:16', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (30, N'Sales Assistant', N'da', 5, 0, N'08/04/2021 20:13:22', 2, N'04/25/2021 11:34 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (31, N'Test', N'hi', 3, 0, N'10/04/2021 10:43:51', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (32, N'Sales Assistant', N'test', 3, 0, N'10/04/2021 10:46:14', 2, N'04/25/2021 11:34 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (33, N'Commercial Manager', N'', 1, 0, N'13/04/2021 09:53:06', NULL, NULL, 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (34, N'Hi', N'da', 4, 2, N'04/14/2021 11:54 AM', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (35, N'Designer', N'', 4, 2, N'04/14/2021 01:34 PM', 2, N'04/14/2021 01:34 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (36, N'Ceo', N'', 1, 2, N'04/14/2021 01:35 PM', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1035, N'Test', N'', 4, 2, N'04/20/2021 09:55 AM', 2, N'04/22/2021 11:12 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1036, N'test', N'test', 2, 2, N'04/22/2021 11:03 AM', 2, N'04/22/2021 11:12 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1037, N'Operation Manager', N'', 1, 2, N'04/26/2021 08:44 AM', 2, N'04/26/2021 08:44 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1038, N'Sameer', N'', 5, 2, N'04/27/2021 10:32 AM', 2, N'04/27/2021 11:10 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1039, N'Sales Assistant', N'', 3, 2, N'05/03/2021 06:39 PM', 2, N'05/04/2021 11:17 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1040, N'Accountant', N'', 2, 2, N'05/26/2021 11:07 AM', 2, N'05/26/2021 11:07 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1041, N'Operation Manager', N'', 1, 2, N'06/15/2021 06:33 AM', 2, N'06/15/2021 06:33 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1042, N'Operation Manager', N'', 1, 2, N'06/16/2021 10:59 AM', 2, N'06/16/2021 10:59 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1043, N'Sales-Designer', N'', 3, 41, N'06/16/2021 11:07 AM', 41, N'06/16/2021 11:07 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1044, N'Designer', N'', 4, 2, N'06/21/2021 12:23 PM', 2, N'06/21/2021 12:23 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1045, N'Designer', N'', 4, 2, N'06/21/2021 12:38 PM', 2, N'06/21/2021 12:38 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1046, N'Operation Manager', N'', 1, 2, N'06/21/2021 12:40 PM', 2, N'09/07/2021 12:18 PM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1047, N'Designer', N'test', 1, NULL, NULL, 2, N'07/15/2021 03:09 PM', NULL, NULL)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1048, N'Sales Manager', N'', 1, 2, N'07/12/2021 02:41 PM', 2, N'07/12/2021 02:47 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1049, N'Designer', N'test', 1, 2, N'07/15/2021 02:35 PM', 2, N'07/15/2021 02:36 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1050, N'Designer', N'test', 1, 2, N'07/15/2021 02:36 PM', 2, N'07/15/2021 02:36 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1051, N'Designer', N'test', 1, 2, N'07/15/2021 02:47 PM', 2, N'07/15/2021 02:48 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1052, N'Designer', N'test', 1, 2, N'07/15/2021 03:04 PM', 2, N'07/15/2021 03:04 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1053, N'Designer', N'test', 1, 2, N'07/15/2021 03:07 PM', 2, N'07/15/2021 03:08 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1054, N'Test', N'by mhd', 1, NULL, NULL, 2, N'07/15/2021 03:14 PM', NULL, NULL)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1055, N'Designer', N'', 4, NULL, NULL, 2, N'09/12/2021 02:14 PM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1056, N'Test', N'test', 1, 2, N'07/15/2021 06:28 PM', 2, N'07/17/2021 10:31 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1057, N'Test', N'test', 2, 2, N'07/17/2021 11:13 AM', 2, N'07/24/2021 03:31 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1058, N'NEW TEST', N'test', 3, 2, N'07/17/2021 11:53 AM', 2, N'07/24/2021 03:31 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1059, N'Test Branch Role', N'Desc', 4, 2, N'07/18/2021 08:36 AM', 2, N'07/24/2021 03:31 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1060, N'New Branch', N'New Branch', 1, 2, N'07/18/2021 03:30 PM', 2, N'07/24/2021 03:31 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1061, N'test3', N'', 3, 2, N'07/18/2021 03:34 PM', 2, N'07/18/2021 04:52 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1062, N'Final Branch', N'Final Branch', 4, 2, N'07/18/2021 06:25 PM', 2, N'07/24/2021 03:31 PM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1063, N'Marketing & Sales coordinator', N'this position is only for adding customer and inquiry', 1, 2, N'08/01/2021 09:14 AM', 2, N'08/01/2021 07:05 PM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1064, N'Team Lead', N'', 1, 2, N'08/18/2021 10:31 AM', 2, N'08/23/2021 08:17 AM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1065, N'Office Operation Manager', N'', 1, 2, N'08/18/2021 02:06 PM', 2, N'10/03/2021 09:34 AM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1066, N'Interior Designer', N'', 3, 2, N'08/18/2021 02:10 PM', 2, N'09/16/2021 01:59 PM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1067, N'Head of Design Department', N'', 1, 2, N'08/18/2021 02:30 PM', 82, N'10/03/2021 10:55 AM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1068, N'Test CEO', N'', 1, 2, N'08/21/2021 02:54 PM', 2, N'09/29/2021 12:00 PM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1069, N'Test Factory User', N'', 1, 2, N'09/29/2021 10:47 AM', 2, N'09/29/2021 10:47 AM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1070, N'Production Manager', N'', 1, 82, N'09/30/2021 09:17 AM', 82, N'09/30/2021 09:17 AM', 1, 0)
SET IDENTITY_INSERT [dbo].[BranchRole] OFF
GO
INSERT [dbo].[BranchType] ([BranchTypeId], [BranchTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Head Office', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[BranchType] ([BranchTypeId], [BranchTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Showroom', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[BranchType] ([BranchTypeId], [BranchTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Factory', 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Building] ON 

INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2253, N'Villa 52, Al khamari st, Jumeira 3', N'Building', N'Old', N'3', 0, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2254, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2255, N'RAS ALKHAIMAH', N'Building', N'Old', N'1', 0, 1, N'https://www.google.com/maps/place/KLUDI+RAK+-+Head+Office/@25.669684,55.784915,15z/data=!4m2!3m1!1s0x0:0xcbdb7c512761d466?sa=X&ved=2ahUKEwjdqvjNwsvyAhXJilwKHaOJBfwQ_BIwHnoECEsQBQ', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2256, N'DUBAI', N'Villa', N'Old', N'1', 0, 1, N'https://www.google.com/maps/search/22c+%D8%B4%D8%A7%D8%B1%D8%B9+67/@25.10164451599121,55.21177291870117,17z?hl=en', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2257, N'DUBAI', N'Villa', N'New', N'', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2258, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2259, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2260, N'Dubi', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2261, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2262, N'Dubai', N'Building', N'Old', N'3', 1, 0, N'https://www.google.com/maps', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2263, N'DUBAI', N'Villa', N'New', N'', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2264, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2265, N'Villa 52, Al khamari st, Jumeira 3', N'Building', N'Old', N'3,4,32', 0, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2266, N'Dubi', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2267, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2268, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2269, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2270, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2271, N'Dubi', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2272, N'Zabadani, Syria', N'Villa', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2273, N'Zabadani, Syria', N'Building', N'New', N'1', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2274, N'Villa 52, Al khamari st, Jumeira 3', N'Office', N'New', N'2', 1, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2275, N'Dubai', N'Villa', N'New', N'G+1', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2276, N'Dubai', N'Villa', N'New', N'G+1', 1, 0, N'https://www.google.com/maps?q=25.257305,55.4848765&z=17&hl=en', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2277, N'Dubai,Townsquare Hayat Community,villa 126', N'Villa', N'Old', N'', 0, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2278, N'Abu Dhabi', N'Building', N'Old', N'', 0, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2279, N'Dubai', N'Villa', N'New', N'G+1', 1, 0, N'https://www.google.com/maps?q=25.234220504760742,55.51947784423828&z=17&hl=en', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2280, N'Dubai,sustinable city , block 1 , villa 1', N'Villa', N'Old', N'G', 0, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2281, N'RAS ALKHAIMAH', N'Villa', N'New', N'G+1', 1, 0, N'https://www.google.com/maps/place/25%C2%B047''59.9%22N+56%C2%B000''10.5%22E/@25.7999687,56.0007334,17z/data=!3m1!4b1!4m5!3m4!1s0x0:0x0!8m2!3d25.7999687!4d56.0029221?hl=en', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2282, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2283, N'Zabadani, Syria', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2284, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2285, N'Dubai', N'Villa', N'Old', N'G', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2286, N'202b Diamond Business center', N'Building', N'New', N'2', 1, 1, N'https://maps.google.com/maps?q=25.234220504760742%2C55.51947784423828&z=17&hl=en', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2287, N'Dubai,sustinable city , block 1 , villa 2', N'Villa', N'Old', N'', 0, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2288, N'Dubai', N'Villa', N'New', N'G+1', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2289, N'Villa 52, Al khamari st, Jumeira 3', N'Office', N'New', N'3', 0, 0, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2290, N'AbU Dhabi', N'Villa', N'Old', N'G+1', 0, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2291, N'DUBAI', N'Villa', N'Old', N'G', 0, 1, N'https://www.google.com/maps?q=25.153886795043945,55.38070297241211&z=17&hl=en', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2292, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2293, N'DUBAI - JVC ', N'Building', N'New', N'28', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2294, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2295, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2296, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2297, N'Dubai', N'Villa', N'Old', N'G', 0, 1, N'https://www.google.com/maps?q=24.996379852294922,55.16897964477539&z=17&hl=en', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2298, N'Dubai', N'Office', N'New', N'G', 0, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2299, N'Villa 52, Al khamari st, Jumeira 3', N'Building', N'Old', N'1', 0, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2300, N'alkhwanije', N'Villa', N'New', N'G+1', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2301, N'Dubai', N'Villa', N'New', N'', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2302, N'Zabadani, Syria', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2303, N'Villa 52, Al khamari st, Jumeira 3', N'Villa', N'Old', N'4', 1, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2304, N'Zabadani, Syria', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2305, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2306, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2307, N'Villa 52, Al khamari st, Jumeira 3', N'Office', N'New', N'3', 1, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2308, N'Villa 52, Al khamari st, Jumeira 3', N'Villa', N'New', N'', 1, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2309, N'Villa 52, Al khamari st, Jumeira 3', N'Building', N'New', N'', 1, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2310, N'Villa 52, Al khamari st, Jumeira 3', N'Villa', N'New', N'', 1, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2311, N'Zabadani, Syria', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2312, N'sharjah', N'Villa', N'New', N'G+1', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2313, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2314, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2315, N'Dubi', N'Office', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2316, N'Shikh Zayed Raod, The H hotel, Floor 33, Office 3, Dubai, UAE', N'Villa', N'New', N'1', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2317, N'Villa 52, Al khamari st, Jumeira 3', N'Office', N'New', N'', 1, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2318, N'Villa 322, Dubai Hills, Dubai', N'Villa', N'New', N'1', 0, 1, N'https://www.google.com/maps?q=25.09206199645996,55.254119873046875&z=17&hl=en', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2319, N'DIRC', N'Building', N'New', N'', 1, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2320, N'Dubai Sustainable City', N'Office', N'Old', N'', 1, 0, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2321, N'Villa 52, Al khamari st, Jumeira 3', N'Villa', N'New', N'', 1, 1, N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2322, N'Dubai', N'Villa', N'New', N'', 0, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsOccupied], [BuildingMakaniMap], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2327, N'Dubai', N'Building', N'New', N'4', 0, 1, N'', 1, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Building] OFF
GO
INSERT [dbo].[ContactStatus] ([ContactStatusId], [ContactStatusName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Contacted', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[ContactStatus] ([ContactStatusId], [ContactStatusName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Need to Contact', 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1107, N'Sameer', N'sameer71095@gmail.com', N'971545552471', N'Villa 52, Al khamari st, Jumeira 3', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'https://www.google.com/maps/place/Al+Khamaari+Street+-+JumeirahJumeirah+3+-+Dubai/@25.1830445,55.2338146,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f69fcdb5a36bf:0x3f62ec383c89da3e!8m2!3d25.1830445!4d55.2360033', N'', N'', 1, 1, 1, 2, 1, 0, 0, NULL, NULL, 2, N'09/27/2021 02:21 AM', N'971545552471')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1108, N'Ali', N'Ali_burhan1993@outlook.com', N'0971953144100', N'Zabadani, Syria', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Syrian Arab Republic', N'', N'', N'', 1, 8, 1, 2, 1, 0, 0, NULL, NULL, 2, N'09/12/2021 08:36 AM', N'0971953144100')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1111, N'sAMEER', N'', N'971545552495', N'', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 1, 32, 83, 1, NULL, 1, 83, N'09/11/2021 09:39 AM', 83, N'09/11/2021 09:39 AM', N'971545552495')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1118, N'Sameer', N'sameer71095@gmail.com', N'971545552472', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'https://www.google.com/maps', N'', N'', 1, 3, 1, 2, 1, 0, 0, 2, N'09/11/2021 09:41 AM', 2, N'09/11/2021 09:41 AM', N'971545552472')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1119, N'Sameer', N'sameer71095@gmail.com', N'971545552473', N'Ras Al khaima', N'Khawr Fakkān, Ash Shāriqah, United Arab Emirates', N'United Arab Emirates', N'Jordan', N'https://www.google.com/maps/place/Dubai+KartDrome/@25.0483703,55.2388811,16.38z/data=!4m5!3m4!1s0x3e5f6e6cfe20578d:0x5dd7411b7c193856!8m2!3d25.0448299!4d55.2397379', N'Hi, I want an inquiry ', N'', 1, 3, 1, 2, 1, NULL, 0, 2, N'09/11/2021 09:42 AM', 0, N'09/16/2021 09:27 AM', N'971545552473')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1120, N'Sameer', N'', N'971545552471', N'', N'Ras al-Khaimah, Ras al Khaymah, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 1, 32, 82, 1, NULL, 1, 82, N'09/11/2021 10:09 AM', 82, N'09/11/2021 10:09 AM', N'971545552471')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1121, N'Sameer', N'', N'971545552477', N'', N'Ras al-Khaimah, Raʼs al Khaymah, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 1, 1, 2, 1, NULL, 0, 2, N'09/11/2021 10:10 AM', 2, N'09/11/2021 10:10 AM', N'971545552477')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1122, N'KLUDI RAK (MR.EMAD KOKASH)', N'', N'971561727255', N'RAS ALKHAIMAH', N'Ras al-Khaimah, Raʼs al Khaymah, United Arab Emirates', N'United Arab Emirates', N'Jordan', N'https://www.google.com/maps/place/KLUDI+RAK+-+Head+Office/@25.669684,55.784915,15z/data=!4m2!3m1!1s0x0:0xcbdb7c512761d466?sa=X&ved=2ahUKEwjdqvjNwsvyAhXJilwKHaOJBfwQ_BIwHnoECEsQBQ', NULL, NULL, 1, 9, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'971561727255')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1123, N'KHALED ALSHEHHI', N'', N'971504526077', N'DUBAI', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'https://www.google.com/maps/search/22c+%D8%B4%D8%A7%D8%B1%D8%B9+67/@25.10164451599121,55.21177291870117,17z?hl=en', NULL, NULL, 1, 9, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'971504526077')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1124, N'ENG.DALIA ALMAGRBI', N'', N'971552705715', N'DUBAI', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Jordan', N'', NULL, NULL, 1, 10, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'971552705715')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1125, N'mHD', N'eng.daibus@gmail.com', N'971944848484', N'Dubi', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 1, 1, 2, 1, 0, 0, 2, N'09/11/2021 01:33 PM', 2, N'09/11/2021 01:33 PM', N'971944848484')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1126, N'Test', N'Test@gm.com', N'971944848485', N'Dubi', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 1, 1, 2, 1, NULL, 0, 2, N'09/11/2021 03:20 PM', 2, N'09/11/2021 03:20 PM', N'971944848485')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1127, N'soaud obaidulla', N'', N'9712688383', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 9, 32, 83, 1, 0, 0, NULL, NULL, 83, N'09/13/2021 10:40 AM', N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1128, N'Ahmad', N'ahmad@cm.com', N'971944848486', N'', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 1, 1, 2, 1, NULL, 0, 2, N'09/15/2021 11:48 AM', 2, N'09/15/2021 11:48 AM', N'971944848486')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1137, N'Eng.zinab alkhatat', N'', N'971502526273', N'Dubai,Townsquare Hayat Community,villa 126', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Iraq', N'', N'', N'', 1, 10, 32, 83, 1, NULL, 0, NULL, NULL, 83, N'09/18/2021 10:02 AM', N'971502526273')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1138, N'MS.MARY', N'', N'971555045043', N'Abu Dhabi', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Lebanon', N'', NULL, NULL, 1, 10, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1139, N'hamad obaidulla', N'', N'9712688334', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 9, 32, 83, 1, 0, 0, NULL, NULL, 83, N'09/21/2021 08:31 AM', N'9712688334')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1140, N'obied alsaied ', N'', N'971503444526', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'https://www.google.com/maps?q=25.234220504760742,55.51947784423828&z=17&hl=en', NULL, NULL, 1, 6, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1141, N'Eng.farah abu baker', N'', N'971556641718', N'Dubai,sustinable city , block 1 , villa 2', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Jordan', N'', N'', N'', 1, 9, 32, 83, 1, 0, 0, NULL, NULL, 83, N'09/20/2021 03:15 PM', N'971556641718')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1142, N'ENG.OTHMAN ( DR.MONA VILLA)', N'', N'971506311566', N'RAS ALKHAIMAH', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'https://www.google.com/maps/place/25%C2%B047''59.9%22N+56%C2%B000''10.5%22E/@25.7999687,56.0007334,17z/data=!3m1!4b1!4m5!3m4!1s0x0:0x0!8m2!3d25.7999687!4d56.0029221?hl=en', NULL, NULL, 1, 9, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1143, N'Sameer', N'sameer@gmail.com', N'97154525455', NULL, NULL, NULL, NULL, NULL, N'Hi,

This is test inquiry request

Thanks', NULL, 2, 7, NULL, NULL, 1, NULL, 1, 83, N'09/18/2021 10:51 AM', 2, N'09/18/2021 10:51 AM', NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1144, N'OMRAN', N'o.hawatah@sai-group.ae', N'0505175611', N'', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'HI ', N'', 1, 7, 32, 83, 1, NULL, 1, NULL, NULL, 83, N'09/18/2021 10:55 AM', N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1145, N'Alaa fahed', N'A.fahd@sai-group.ae', N'0504383353', NULL, NULL, NULL, NULL, NULL, N'Contact me now', NULL, 2, 7, NULL, NULL, 1, NULL, 1, 79, N'09/18/2021 11:03 AM', 79, N'09/18/2021 11:04 AM', NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1146, N'Sam', N'sameer.lion16@gmail.com', N'971545544525', N'', NULL, NULL, NULL, N'', N'', N'', 1, 7, 1, 2, 1, NULL, 1, NULL, NULL, 2, N'09/18/2021 01:58 PM', N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1147, N'Abd Allah Naser Almarzoki', N'', N'971507701407', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', NULL, NULL, 1, 6, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1148, N'Ali', N'ali_burhan1993@outlook.com', N'963930104705', NULL, NULL, NULL, NULL, NULL, N'Hello Bro Sameer How R U ?', NULL, 2, 7, NULL, NULL, 1, NULL, 1, 2, N'09/21/2021 02:24 PM', 2, N'09/21/2021 02:27 PM', NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1149, N'Ali', N'ali_burhan1993@outlook.com', N'963930104705', NULL, NULL, NULL, NULL, NULL, N'Hello Bro Sameer How R U ?', NULL, 2, 7, NULL, NULL, 1, NULL, 1, 2, N'09/21/2021 02:24 PM', 2, N'09/21/2021 02:27 PM', NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1150, N'Amer alrashdy', N'', N'971509300103', N'AbU Dhabi', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', NULL, NULL, 1, 10, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1151, N'SAIF ALZAHMI', N'', N'971505798485', N'DUBAI', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'https://www.google.com/maps?q=25.153886795043945,55.38070297241211&z=17&hl=en', NULL, NULL, 1, 10, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1152, N'ALFA DATA TOWER', N'', N'971521021038', N'DUBAI - JVC ', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Jordan', N'', NULL, NULL, 1, 1, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1153, N'samerr', N'95959595', N'9595959559', N'f', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'asdasd', N'', 1, 1, 1, 2, 1, NULL, 0, 2, N'09/26/2021 09:20 AM', 2, N'09/26/2021 09:20 AM', N'9595959559')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1154, N'Rema alkhatieb', N'', N'971507747655', N'DUBAI', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Lebanon', N'', N'the client busy now , she will inform us the time to take measurement ', N'', 1, 10, 32, 83, 1, NULL, 0, 83, N'09/27/2021 01:59 PM', 83, N'09/27/2021 01:59 PM', N'971507747655')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1155, N'Rawan adam', N'', N'971504159291', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Jordan', N'https://www.google.com/maps?q=24.996379852294922,55.16897964477539&z=17&hl=en', NULL, NULL, 1, 10, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1156, N'jeet contracting (eng . leen)', N'', N'971555330092', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Palestine', N'', NULL, NULL, 1, 9, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1157, N'Rashed alsaied ', N'', N'971507951122', N'alkhwanije', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', NULL, NULL, 1, 6, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1158, N'Hani Aabdllah', N'', N'971504552119', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', NULL, NULL, 1, 9, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1159, N'obied salamh (eng.altyeb)', N'', N'971551299776', N'sharjah', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', NULL, NULL, 1, 9, 32, 83, 1, 0, 0, NULL, NULL, NULL, NULL, N'')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1160, N'Lovila', N'procurement@dpiuae.com', N'971507558326', N'Shikh Zayed Raod, The H hotel, Floor 33, Office 3, Dubai, UAE', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'Client wants wardrobes to be quoted', N'', 1, 1, 32, 80, 1, 0, 0, NULL, NULL, 80, N'10/03/2021 11:19 AM', N'971507558326')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1161, N'Laura', N'', N'971527313187', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'Client is busy, will call us back when free.', N'', 1, 1, 32, 80, 1, NULL, 0, NULL, NULL, 80, N'10/05/2021 03:59 PM', N'971527313187')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1162, N'Mona Um Ahmad (Rashed AlShehi)', N'', N'971503655593', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'Client is busy, will call back when free', N'10/05/2021 3:34 PM', 1, 1, 32, 80, 1, 0, 0, NULL, NULL, 80, N'10/05/2021 02:36 PM', N'971503655593')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1163, N'Noor Stars', N'', N'971507230770', N'Villa 322, Dubai Hills, Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'https://www.google.com/maps?q=25.09206199645996,55.254119873046875&z=17&hl=en', N'', N'', 1, 1, 32, 80, 1, NULL, 0, NULL, NULL, 80, N'10/03/2021 03:55 PM', N'971507230770')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1164, N'Alia', N'', N'971522929222', N'', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'client to visit head office next week (Sat-Sun)', N'', 1, 1, 32, 80, 1, NULL, 0, 80, N'10/04/2021 08:43 AM', 80, N'10/04/2021 08:43 AM', N'971522929222')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1165, N'Sultan', N'', N'971588747000', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 1, 1, 80, 0, NULL, 1, 2, N'10/04/2021 03:49 PM', 2, N'10/04/2021 03:49 PM', N'971588747000')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1166, N'Sultan', N'', N'971588747000', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', NULL, NULL, 1, 1, 32, 80, 1, 0, 0, NULL, NULL, NULL, NULL, N'971588747000')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1167, N'diamond developers', N'H.alalami@diamond-developers.ae', N'971507697504', N'Dubai Sustainable City', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'Origin Shop, fitout & joinery', N'', 1, 1, 32, 80, 1, 0, 0, 80, N'10/04/2021 04:07 PM', 80, N'10/04/2021 04:07 PM', N'971507697504')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1168, N'Ali Al Shehi', N'', N'971503485000', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'10/06/2021 2:35 PM', 2, 1, 32, 80, 1, NULL, 0, NULL, NULL, 80, N'10/05/2021 02:35 PM', N'971503485000')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1169, N'Alyazia', N'', N'971569220202', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'Client only needs price for wardrobe 3500mm x 2500mm with reference design from our instagram page', N'', 1, 10, 32, 80, 1, NULL, 0, 80, N'10/05/2021 03:13 PM', 80, N'10/05/2021 03:13 PM', N'971569220202')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1170, N'Sa', N'', N'971543163451', N'', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 1, 1, 2, 1, NULL, 0, 2, N'10/05/2021 04:20 PM', 2, N'10/05/2021 04:20 PM', N'971543163451')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1171, N'john', N'gmail.com', N'887799553322', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'dd', N'', 1, 1, 1, 2, 1, NULL, 0, 2, N'10/06/2021 01:57 AM', 2, N'10/06/2021 01:57 AM', N'887799553322')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [CustomerNotes], [CustomerNextMeetingDate], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [CustomerWhatsapp]) VALUES (1172, N'Yosra Dawood', N'', N'971522501214', N'Dubai', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', N'', N'', 1, 1, 32, 80, 1, 0, 0, 80, N'10/06/2021 09:39 AM', 80, N'10/06/2021 09:39 AM', N'971522501214')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
