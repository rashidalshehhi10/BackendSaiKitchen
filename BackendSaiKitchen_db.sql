USE [master]
GO
/****** Object:  Database [BackendSaiKitchen_db]    Script Date: 14/06/2021 9:46:12 AM ******/
CREATE DATABASE [BackendSaiKitchen_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DbSaiKitchen', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DbSaiKitchen.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DbSaiKitchen_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DbSaiKitchen_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BackendSaiKitchen_db] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BackendSaiKitchen_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET RECOVERY FULL 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET  MULTI_USER 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BackendSaiKitchen_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BackendSaiKitchen_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BackendSaiKitchen_db', N'ON'
GO
ALTER DATABASE [BackendSaiKitchen_db] SET QUERY_STORE = OFF
GO
USE [BackendSaiKitchen_db]
GO
/****** Object:  Table [dbo].[Accesories]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appliances]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BranchRole]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BranchType]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Building]    Script Date: 14/06/2021 9:46:12 AM ******/
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
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_bUILDING] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactStatus]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 14/06/2021 9:46:12 AM ******/
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
	[CustomerCity] [nvarchar](50) NULL,
	[CustomerCountry] [nvarchar](50) NULL,
	[CustomerNationality] [nvarchar](50) NULL,
	[CustomerNationalId] [nvarchar](500) NULL,
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
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerBranch]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Design]    Script Date: 14/06/2021 9:46:12 AM ******/
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
 CONSTRAINT [PK_Design] PRIMARY KEY CLUSTERED 
(
	[DesignId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fees]    Script Date: 14/06/2021 9:46:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fees](
	[FeesId] [int] IDENTITY(1,1) NOT NULL,
	[FeesName] [nvarchar](500) NULL,
	[FeesDescription] [nvarchar](500) NULL,
	[FeesAmount] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Fees] PRIMARY KEY CLUSTERED 
(
	[FeesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[File]    Script Date: 14/06/2021 9:46:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](50) NULL,
	[FileImage] [image] NULL,
	[FileURL] [nvarchar](max) NULL,
	[MeasurementId] [int] NULL,
	[DesignId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_MeasurementFile] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inquiry]    Script Date: 14/06/2021 9:46:12 AM ******/
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
	[CustomerId] [int] NULL,
	[BranchId] [int] NULL,
	[BuildingId] [int] NULL,
	[IsEscalationRequested] [bit] NULL,
	[AddedBy] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Inquiry] PRIMARY KEY CLUSTERED 
(
	[InquiryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InquiryStatus]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InquiryWorkscope]    Script Date: 14/06/2021 9:46:12 AM ******/
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
	[DesignScheduleDate] [nvarchar](50) NULL,
	[IsDesignApproved] [bit] NULL,
	[Comments] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_InquiryMeasurement] PRIMARY KEY CLUSTERED 
(
	[InquiryWorkscopeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KitchenDesignInfo]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Measurement]    Script Date: 14/06/2021 9:46:12 AM ******/
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
	[FeesId] [int] NULL,
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
 CONSTRAINT [PK_Measurement] PRIMARY KEY CLUSTERED 
(
	[MeasurementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasurementDetail]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasurementDetailInfo]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationCategory]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionLevel]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionRole]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleHead]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleType]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 14/06/2021 9:46:12 AM ******/
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
	[UpdaredBy] [int] NULL,
	[UpdatedDate] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WardrobeDesignInformation]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WayOfContact]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkScope]    Script Date: 14/06/2021 9:46:12 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Branch] ON 

INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Head Office', N'Arjan-Dubai', N'+97145514553', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Sakamkam', N'Sakamkam Fujairah', N'+971506671659', 2, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Umm Deira', N'Umm Al Quwain', N'+97145514553', 3, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, N'Test', N'Arjan', N'971545552471', 2, 1, 1, 0, N'03/04/2021 13:44:40', 0, N'03/04/2021 13:48:11')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, N'Test', N'Arjan', N'971545552471', 2, 1, 1, 0, N'03/04/2021 19:15:34', 0, N'05/04/2021 20:01:26')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, N'rter', N'Arjan Dubai', N'971545552471', 2, 1, 1, 2, N'11/04/2021 10:56:16', 2, N'11/04/2021 10:56:41')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, N'Test', N'Arjan Dubai', N'971545552476', 2, 1, 1, 2, N'13/04/2021 09:47:39', 2, N'04/14/2021 12:03 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (15, N'Test Branch', N'Dubaiii', N'971545552475', 2, 1, 1, 2, N'04/14/2021 11:53 AM', 2, N'04/14/2021 11:54 AM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (16, N'test', N'Arjan Dubai', N'971545552471', 2, 1, 1, 2, N'04/22/2021 10:59 AM', 2, N'04/22/2021 11:13 AM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (17, N'test', N'Arjan', N'971545552471', 2, 1, 1, 2, N'04/27/2021 10:31 AM', 2, N'04/27/2021 10:31 AM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (18, N'Arjan Branch', N'Business Cnter', N'971545552471', 2, 1, 0, 2, N'05/03/2021 06:36 PM', 2, N'05/03/2021 06:36 PM')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (19, N'Albarsha', N'dubai', N'971545555271', 2, 1, 0, 2, N'05/26/2021 11:06 AM', 2, N'05/26/2021 11:06 AM')
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO
SET IDENTITY_INSERT [dbo].[BranchRole] ON 

INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1, N'Head Manager', N'All access of one branch', 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (14, N'Procurement Manager', N'Procurement access', 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (15, N'Sales Manager', N'Sales related stuff', 3, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (23, N'CEO', N'', 1, 0, N'31/03/2021 15:24:17', 0, N'03/04/2021 11:29:55', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (24, N'Sale Assistant', N'', 2, 0, N'31/03/2021 15:37:04', 0, N'03/04/2021 09:32:20', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (25, N'Sameer', N'test', 3, 0, N'03/04/2021 13:45:04', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (26, N'Interior Designer', N'test', 4, 0, N'03/04/2021 19:16:11', 0, N'05/04/2021 16:37:13', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (27, N'Test', N'test', 5, 0, N'08/04/2021 09:57:15', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (28, N'test 2', N'test', 1, 0, N'08/04/2021 10:32:01', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (29, N'edit', N'test', 2, 0, N'08/04/2021 10:40:16', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (30, N'Sales Assistant', N'da', 5, 0, N'08/04/2021 20:13:22', 2, N'04/25/2021 11:34 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (31, N'Test', N'hi', 3, 0, N'10/04/2021 10:43:51', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (32, N'Sales Assistant', N'test', 3, 0, N'10/04/2021 10:46:14', 2, N'04/25/2021 11:34 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (33, N'Commercial Manager', N'', 1, 0, N'13/04/2021 09:53:06', NULL, NULL, 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (34, N'Hi', N'da', 4, 2, N'04/14/2021 11:54 AM', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (35, N'Designer', N'', 4, 2, N'04/14/2021 01:34 PM', 2, N'04/14/2021 01:34 PM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (36, N'Ceo', N'', 1, 2, N'04/14/2021 01:35 PM', 2, N'04/20/2021 09:54 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1035, N'Test', N'', 4, 2, N'04/20/2021 09:55 AM', 2, N'04/22/2021 11:12 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1036, N'test', N'test', 2, 2, N'04/22/2021 11:03 AM', 2, N'04/22/2021 11:12 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1037, N'Operation Manager', N'', 1, 2, N'04/26/2021 08:44 AM', 2, N'04/26/2021 08:44 AM', 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1038, N'Sameer', N'', 5, 2, N'04/27/2021 10:32 AM', 2, N'04/27/2021 11:10 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1039, N'Sales Assistant', N'', 3, 2, N'05/03/2021 06:39 PM', 2, N'05/04/2021 11:17 AM', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1040, N'Accountant', N'', 2, 2, N'05/26/2021 11:07 AM', 2, N'05/26/2021 11:07 AM', 1, 0)
SET IDENTITY_INSERT [dbo].[BranchRole] OFF
GO
INSERT [dbo].[BranchType] ([BranchTypeId], [BranchTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Head Office', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[BranchType] ([BranchTypeId], [BranchTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Showroom', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[BranchType] ([BranchTypeId], [BranchTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Factory', 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Building] ON 

INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'test', N'Building', N'Old', N'1,2,4', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Dubai', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Dubai Business Center Arjan', N'Building', N'Old', N'2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Dubai Internet City', N'Villa', N'Old', N'1,2', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'', N'Building', N'New', N'1,2', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Dubai', N'Building', N'Old', N'1,2', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'', N'Building', N'Old', N'1,2', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (19, N'', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (20, N'', N'Building', N'Old', N'1,2', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (21, N'', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (22, N'', N'Building', N'Old', N'', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (23, N'', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (24, N'', N'Villa', N'New', N'', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (25, N'', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (26, N'', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (27, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1005, N'JBR', N'Building', N'Old', N'5,6', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1006, N'Dubai Internet City', N'Building', N'Old', N'1,2', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1007, N'Dubai Business Center Arjan', N'Building', N'Old', N'2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1008, N'Dubai Business Center Arjan', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1009, N'Dubai Business Center Arjan', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1010, N'Dubai Business Center Arjan', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1011, N'Dubai Business Center Arjan', N'Building', N'Old', N'1,2,3,4,5', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1012, N'Dubai Business Center Arjan', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1013, N'Dubai Business Center Arjan', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1014, N'Dubai Internet City', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1015, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1016, N'JBR', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1017, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1018, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1019, N'Dubai Internet City', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1020, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1021, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4,5', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1022, N'Dubai Internet City', N'Villa', N'Old', N'1,2,3', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1023, N'Dubai Business Center Arjan', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1024, N'Dubai Internet City', N'Building', N'Old', N'1,2', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1025, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1026, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1027, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1028, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1029, N'Dubai Internet City', N'Building', N'Old', N'1,2,3', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1030, N'202, Diamond Business Center Arjan', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1031, N'212 Building  Dubai Internet City Dubai', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1032, N'JBR', N'Building', N'Old', N'1,2,4', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1033, N'Dubai Internet City', N'Building', N'Old', N'1,2,3', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1034, N'Dubai Internet City', N'Building', N'Old', N'ground', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1035, N'Dubai Internet City', N'Building', N'Old', N'1', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1036, N'Dubai Internet City', N'Building', N'Old', N'1', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1037, N'Dubai Internet City', N'Building', N'Old', N'1', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1038, N'j[r ', N'Villa', N'New', N'', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1039, N'Dubai Internet City', N'Building', N'Old', N'1', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1040, N'Dubai Internet City', N'Building', N'Old', N'1', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1041, N'Dubai Internet City', N'Building', N'Old', N'1', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1042, N'Dubai Internet City', N'Villa', N'New', N'', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1043, N'Dubai Internet City', N'Villa', N'New', N'1,2', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1044, N'Dubai Internet City', N'Building', N'Old', N'1', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2044, N'Dubai Internet City', N'Building', N'Old', N'1,2,3,4,5,6', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2045, N'al baarsha', N'Villa', N'Old', N'1', 0, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Building] ([BuildingId], [BuildingAddress], [BuildingTypeOfUnit], [BuildingCondition], [BuildingFloor], [BuildingReconstruction], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2046, N'al baarsha', N'Building', N'Old', N'1', 0, 1, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Building] OFF
GO
INSERT [dbo].[ContactStatus] ([ContactStatusId], [ContactStatusName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Contacted', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[ContactStatus] ([ContactStatusId], [ContactStatusName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Need to Contact', 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Customer 1', N'sameer71095@gmail.com', N'971545552474', N'Dubai Business Center Arjan', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78419951234567', 1, 1, 1, 2, 1, NULL, 0, NULL, NULL, 2, N'04/26/2021 09:58 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Customer 2', N'sameer71095@gmail.coms', N'971545552473', N'Dubai Business Center Arjan', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78419901234567', 2, 1, 2, 28, 1, 1, 1, NULL, NULL, 2, N'04/25/2021 12:38 PM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Sameer', N'sameer71095@gmail.com', N'971545552471', N'Dubai Internet City', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78416255', 1, 2, 2, 2, 1, 0, 0, NULL, NULL, 2, N'12/04/2021 11:53:37')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Test', N'sameer71095@gmail.com', N'971545552472', N'Dubai Business Center Arjan', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'7841995215448', 2, 2, 2, 2, 1, 0, 0, NULL, NULL, 2, N'04/15/2021 11:41 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Test', N'sameer.masood@micropolis.ae', N'971545552471', N'Mall of emirates', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'7841995123456789', 2, 4, 1, 1, 1, 1, 1, 0, N'07/04/2021 10:26:53', 2, N'12/04/2021 11:54:13')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Test Customer', N'sameer.masood@micropolis.ae', N'971545552472', N'JBR', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78419952310156', 1, 5, 1, 1, 1, 0, 0, 0, N'07/04/2021 10:28:54', NULL, NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Sameer', N'sameer71095@gmail.com', N'971545552471', N'Dubai Internet City', N'Lahore, Punjab, Pakistan', N'Pakistan', N'Pakistan', N'78416255', 1, 2, 2, 2, 1, NULL, 1, NULL, NULL, 2, N'14/04/2021 09:57:28')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'Test', N'sameer71095@gmail.com', N'971545552472', N'Dubai Business Center Arjan', N'Al Ain, Abu Dhabi, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'7841995215448', 2, 5, 2, 2, 1, NULL, 1, 0, N'08/04/2021 14:38:44', 0, N'04/15/2021 10:08 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'Test', N'sameer71095@gmail.com', N'971545552472', N'Dubai Internet City', N'Sharjah, Ash Shāriqah, United Arab Emirates', N'United Arab Emirates', N'Syrian Arab Republic', N'7841995123456', 2, 3, 2, 2, 1, NULL, 1, 0, N'10/04/2021 09:19:00', 2, N'12/04/2021 11:53:47')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, N'Test', N'sameer71095@gmail.com', N'971555524714', N'Sameer', N'Multān, Punjab, Pakistan', N'Pakistan', N'Algeria', N'7841995123456', 1, 4, 2, 2, 1, NULL, 0, 2, N'13/04/2021 09:57:37', NULL, NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, N'Customer 1', N'sameer71095@gmail.com', N'971545552474', N'Dubai Business Center Arjan', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78419951234567', 2, 1, 2, 2, 1, 0, 1, NULL, NULL, 2, N'04/15/2021 10:09 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, N'Customer 1', N'sameer71095@gmail.com', N'971545552474', N'Dubai Business Center Arjan', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78419951234567', 2, 1, 2, 2, 1, 0, 1, NULL, NULL, 0, N'04/15/2021 11:11 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (24, N'Customer 1', N'sameer71095@gmail.com', N'971545552474', N'Dubai Business Center Arjan', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78419951234567', 2, 1, 2, 2, 1, 0, 1, NULL, NULL, 0, N'04/15/2021 11:38 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (25, N'Customer 1', N'sameer71095@gmail.com', N'971545552474', N'Dubai Business Center Arjan', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78419951234567', 2, 1, 2, 2, 1, 0, 1, NULL, NULL, 2, N'04/15/2021 11:40 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (26, N'Customer 1', N'sameer71095@gmail.com', N'971545552474', N'Dubai Business Center Arjan', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78419951234567', 1, 1, 2, 2, 1, 0, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (27, N'Sameer', N'', N'971545552487', N'', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', 2, 1, 1, 2, 1, NULL, 1, NULL, NULL, 2, N'04/22/2021 11:15 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (28, N'Sameer', N'sameer71095@gmail.com', N'971545552471', N'al baarsha', N'Karachi, Sindh, Pakistan', N'Pakistan', N'Pakistan', N'78416255', 1, 2, 1, 2, 1, 0, 0, NULL, NULL, 2, N'04/22/2021 11:15 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (29, N'Test', N'', N'971545552483', N'', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', 1, 7, 1, 30, 1, NULL, 1, 30, N'04/15/2021 12:57 PM', 2, N'04/22/2021 11:13 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (30, N'amer shafee', N'amer@gmail.com', N'971504506500', N'Dubai Internet City', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'Syed Faisal Ahmed', 2, 8, 2, 2, 1, 0, 0, 2, N'04/15/2021 11:26 PM', 2, N'04/15/2021 11:26 PM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1014, N'Test', N'sameer71095@gmail.com', N'971545552473', N'Sharjah', N'Sharjah, Ash Shāriqah, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'7841995123456789', 2, 3, 1, 2, 1, NULL, 0, 2, N'04/22/2021 11:16 AM', 2, N'04/22/2021 11:16 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1015, N'Ali', N'test@ali.com', N'971545555555', N'', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', 2, 1, 1, 2, 1, NULL, 0, 2, N'04/26/2021 08:54 AM', 2, N'04/26/2021 08:54 AM')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsEscalationRequested], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1016, N'Fareed', N'', N'971545552478', N'', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'United Arab Emirates', N'', 1, 6, 1, 2, 1, NULL, 0, NULL, NULL, 2, N'05/03/2021 06:42 PM')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Inquiry] ON 

INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, NULL, N'', N'tes', NULL, NULL, NULL, 3, 2, 3, 0, NULL, 1, 1, 2, N'14/04/2021 09:56:57', NULL, NULL)
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, NULL, N'', N'', NULL, NULL, NULL, 4, 2, 4, 0, NULL, 1, 1, 2, N'14/04/2021 10:01:45', NULL, NULL)
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, NULL, N'', N'', N'04/14/2021 01:31 PM', NULL, NULL, 3, 2, 5, 0, NULL, 1, 0, 2, N'04/14/2021 01:31 PM', 2, N'04/14/2021 01:31 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, NULL, N'', N'', N'04/14/2021 01:41 PM', NULL, NULL, 13, 2, 6, 0, NULL, 1, 0, 2, N'04/14/2021 01:41 PM', 0, N'04/14/2021 01:41 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, NULL, N'', N'', N'04/15/2021 10:09 AM', NULL, NULL, 13, 2, 7, 0, NULL, 1, 0, 2, N'04/15/2021 10:09 AM', 2, N'04/15/2021 10:09 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, NULL, N'', N'', N'04/15/2021 11:03 AM', NULL, NULL, 4, 2, 8, 0, NULL, 1, 0, 2, N'04/15/2021 11:03 AM', 2, N'04/15/2021 11:03 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, NULL, N'', N'', N'04/15/2021 11:10 AM', NULL, NULL, 14, 2, 9, 0, NULL, 1, 0, 2, N'04/15/2021 11:10 AM', 0, N'04/15/2021 11:10 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, NULL, N'', N'', N'04/15/2021 11:38 AM', NULL, NULL, 24, 2, 19, 0, NULL, 1, 0, 2, N'04/15/2021 11:38 AM', 0, N'04/15/2021 11:38 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, NULL, N'', N'', N'04/15/2021 11:39 AM', NULL, NULL, 25, 2, 20, 0, NULL, 1, 0, 2, N'04/15/2021 11:39 AM', 2, N'04/15/2021 11:39 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, NULL, N'', N'', N'04/15/2021 11:40 AM', NULL, NULL, 26, 2, 21, 0, NULL, 1, 0, 2, N'04/15/2021 11:40 AM', 2, N'04/15/2021 11:40 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, NULL, N'', N'', N'04/15/2021 11:51 AM', NULL, NULL, 26, 2, 22, 0, NULL, 1, 0, 2, N'04/15/2021 11:51 AM', 2, N'04/15/2021 11:51 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, NULL, N'', N'', N'04/15/2021 12:05 PM', NULL, NULL, 28, 1, 23, 0, NULL, 1, 0, 30, N'04/15/2021 12:05 PM', 30, N'04/15/2021 12:05 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (15, NULL, N'', N'', N'04/15/2021 11:40 PM', NULL, NULL, 30, 2, 24, 0, NULL, 1, 0, 2, N'04/15/2021 11:40 PM', 2, N'04/15/2021 11:40 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (16, NULL, N'', N'test', N'04/17/2021 01:28 PM', NULL, NULL, 3, 2, 25, 0, NULL, 1, 0, 30, N'04/17/2021 01:28 PM', 30, N'04/17/2021 01:28 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (17, NULL, N'', N'', N'04/17/2021 01:44 PM', NULL, NULL, 3, 2, 26, 0, NULL, 1, 0, 30, N'04/17/2021 01:44 PM', 30, N'04/17/2021 01:44 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (18, NULL, N'', N'', N'04/17/2021 01:46 PM', NULL, NULL, 3, 2, 27, 0, NULL, 1, 0, 30, N'04/17/2021 01:46 PM', 30, N'04/17/2021 01:46 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1016, NULL, N'', N'test', N'04/18/2021 01:19 PM', NULL, NULL, 6, 1, 1005, 0, NULL, 1, 0, 2, N'04/18/2021 01:19 PM', 2, N'04/18/2021 01:19 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1017, NULL, N'', N'', N'04/19/2021 08:59 AM', NULL, NULL, 3, 2, 1006, 0, NULL, 1, 0, 30, N'04/19/2021 08:59 AM', 30, N'04/19/2021 08:59 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1018, NULL, N'', N'', N'04/19/2021 09:00 AM', NULL, NULL, 4, 2, 1007, 0, NULL, 1, 0, 30, N'04/19/2021 09:00 AM', 30, N'04/19/2021 09:00 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1019, NULL, N'', N'', N'04/19/2021 09:02 AM', NULL, NULL, 4, 2, 1008, 0, NULL, 1, 0, 30, N'04/19/2021 09:02 AM', 30, N'04/19/2021 09:02 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1020, NULL, N'', N'', N'04/19/2021 09:04 AM', NULL, NULL, 4, 2, 1009, 0, NULL, 1, 0, 30, N'04/19/2021 09:04 AM', 30, N'04/19/2021 09:04 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1021, NULL, N'', N'', N'04/19/2021 09:21 AM', NULL, NULL, 4, 2, 1010, 0, NULL, 1, 0, 30, N'04/19/2021 09:21 AM', 30, N'04/19/2021 09:21 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1022, NULL, N'', N'', N'04/19/2021 09:22 AM', NULL, NULL, 26, 2, 1011, 0, NULL, 1, 0, 30, N'04/19/2021 09:22 AM', 30, N'04/19/2021 09:22 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1023, NULL, N'', N'', N'04/19/2021 09:46 AM', NULL, NULL, 26, 2, 1012, 0, NULL, 1, 0, 30, N'04/19/2021 09:48 AM', 30, N'04/19/2021 09:48 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1024, NULL, N'', N'', N'04/19/2021 09:48 AM', NULL, NULL, 4, 2, 1013, 0, NULL, 1, 0, 30, N'04/19/2021 09:48 AM', 30, N'04/19/2021 09:48 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1025, NULL, N'', N'', N'04/19/2021 09:51 AM', NULL, NULL, 3, 2, 1014, 0, NULL, 1, 0, 30, N'04/19/2021 09:51 AM', 30, N'04/19/2021 09:51 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1026, NULL, N'', N'', N'04/19/2021 09:57 AM', NULL, NULL, 28, 1, 1015, 0, NULL, 1, 0, 2, N'04/19/2021 09:57 AM', 2, N'04/19/2021 09:57 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1027, NULL, N'', N'', N'04/19/2021 09:59 AM', NULL, NULL, 6, 1, 1016, 0, NULL, 1, 0, 2, N'04/19/2021 09:59 AM', 2, N'05/04/2021 10:59 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1028, NULL, N'', N'', N'04/19/2021 10:00 AM', NULL, NULL, 28, 1, 1017, 0, NULL, 1, 0, 2, N'04/19/2021 10:00 AM', 2, N'04/19/2021 10:00 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1029, NULL, N'', N'', N'04/19/2021 10:01 AM', NULL, NULL, 28, 1, 1018, 0, NULL, 1, 0, 2, N'04/19/2021 10:01 AM', 2, N'04/19/2021 10:01 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1030, NULL, N'', N'', N'04/19/2021 10:04 AM', NULL, NULL, 28, 1, 1019, 0, NULL, 1, 0, 2, N'04/19/2021 10:04 AM', 2, N'04/19/2021 10:04 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1031, NULL, N'', N'', N'04/19/2021 10:05 AM', NULL, NULL, 28, 1, 1020, 0, NULL, 1, 0, 2, N'04/19/2021 10:05 AM', 2, N'04/19/2021 10:05 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1032, NULL, N'', N'', N'04/19/2021 10:23 AM', NULL, NULL, 3, 2, 1021, 0, NULL, 1, 0, 30, N'04/19/2021 10:23 AM', 30, N'04/19/2021 10:23 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1033, NULL, N'', N'', N'04/19/2021 12:25 PM', NULL, NULL, 28, 1, 1022, 0, NULL, 1, 0, 2, N'04/19/2021 12:25 PM', 2, N'04/19/2021 12:25 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1034, NULL, N'', N'', N'04/20/2021 08:47 AM', NULL, NULL, 4, 2, 1023, 0, NULL, 1, 0, 30, N'04/20/2021 08:47 AM', 30, N'04/20/2021 08:47 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1035, NULL, N'', N'', N'04/20/2021 11:18 AM', NULL, NULL, 28, 1, 1024, 0, 2, 1, 0, 2, N'04/20/2021 11:18 AM', 2, N'04/20/2021 11:18 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1036, NULL, N'', N'', N'04/21/2021 09:10 AM', NULL, NULL, 28, 1, 1025, 0, 2, 1, 0, 2, N'04/21/2021 09:10 AM', 2, N'04/21/2021 09:10 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1037, NULL, N'', N'', N'04/21/2021 09:12 AM', NULL, NULL, 28, 1, 1026, 0, 2, 1, 0, 2, N'04/21/2021 09:12 AM', 2, N'04/21/2021 09:12 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1038, NULL, N'', N'', N'04/21/2021 09:14 AM', NULL, NULL, 28, 1, 1027, 0, 2, 1, 0, 33, N'04/21/2021 09:14 AM', 2, N'04/22/2021 02:02 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1039, NULL, N'', N'', N'04/21/2021 10:09 AM', NULL, NULL, 28, 1, 1028, 0, 2, 1, 0, 2, N'04/21/2021 10:09 AM', 2, N'04/22/2021 02:01 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1040, NULL, N'', N'', N'04/21/2021 10:11 AM', NULL, NULL, 28, 1, 1029, 0, 2, 1, 0, 33, N'04/21/2021 10:11 AM', 2, N'04/22/2021 01:50 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1041, NULL, N'', N'', N'04/21/2021 10:13 AM', NULL, NULL, 28, 1, 1030, 0, 2, 1, 0, 33, N'04/21/2021 10:13 AM', 2, N'04/22/2021 01:23 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1042, NULL, N'', N'', N'04/21/2021 10:15 AM', NULL, NULL, 28, 1, 1031, 0, 2, 1, 0, 33, N'04/21/2021 10:15 AM', 2, N'04/22/2021 01:50 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1043, NULL, N'', N'', N'04/22/2021 11:17 AM', NULL, NULL, 6, 1, 1032, 0, 2, 1, 0, 2, N'04/22/2021 11:17 AM', 2, N'04/22/2021 01:49 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1044, NULL, N'', N'', N'04/22/2021 11:24 AM', NULL, NULL, 28, 1, 1033, 0, 2, 1, 0, 2, N'04/22/2021 11:24 AM', 2, N'04/22/2021 01:14 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1045, NULL, N'', N'', N'04/24/2021 04:15 PM', NULL, NULL, 28, 1, 1034, 0, 2, 1, 0, 2, N'04/24/2021 04:15 PM', 2, N'04/24/2021 04:15 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1046, NULL, N'', N'', N'04/26/2021 08:40 AM', NULL, NULL, 28, 1, 1035, 0, 2, 1, 0, 2, N'04/26/2021 08:40 AM', 2, N'04/26/2021 01:58 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1047, NULL, N'', N'', N'04/26/2021 08:47 AM', NULL, NULL, 28, 1, 1036, 0, 2, 1, 0, 36, N'04/26/2021 08:47 AM', 2, N'04/26/2021 09:55 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1048, NULL, N'', N'', N'04/27/2021 10:42 AM', NULL, NULL, 28, 1, 1037, 0, 2, 1, 0, 2, N'04/27/2021 10:42 AM', 2, N'04/27/2021 10:42 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1049, NULL, N'', N'vip ', N'04/27/2021 11:20 AM', NULL, NULL, 28, 1, 1038, 0, 2, 1, 0, 2, N'04/27/2021 11:20 AM', 2, N'04/27/2021 09:12 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1050, NULL, N'', N'', N'04/27/2021 09:17 PM', NULL, NULL, 28, 1, 1039, 0, 2, 1, 0, 2, N'04/27/2021 09:17 PM', 2, N'04/27/2021 09:17 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1051, NULL, N'', N'', N'05/03/2021 12:12 PM', NULL, NULL, 28, 1, 1040, 0, 2, 1, 0, 36, N'05/03/2021 12:12 PM', 36, N'05/03/2021 12:12 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1052, NULL, N'', N'', N'05/03/2021 06:46 PM', NULL, NULL, 28, 1, 1041, 0, 2, 1, 0, 2, N'05/03/2021 06:46 PM', 2, N'05/03/2021 06:46 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1053, NULL, N'', N'', N'05/05/2021 12:06 PM', NULL, NULL, 28, 1, 1042, 0, 2, 1, 0, 2, N'05/05/2021 12:06 PM', 2, N'05/05/2021 09:31 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1054, NULL, N'', N'', N'05/05/2021 09:35 PM', NULL, NULL, 28, 1, 1043, 0, 2, 1, 0, 2, N'05/05/2021 09:35 PM', 2, N'05/05/2021 09:57 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1055, NULL, N'', N'Test', N'05/11/2021 03:29 PM', NULL, NULL, 28, 1, 1044, 0, 2, 1, 0, 2, N'05/11/2021 03:29 PM', 2, N'05/11/2021 03:29 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2055, NULL, N'', N'retret', N'05/24/2021 12:07 PM', NULL, NULL, 28, 1, 2044, 0, 2, 1, 0, 2, N'05/24/2021 12:07 PM', 2, N'05/24/2021 12:07 PM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2056, NULL, N'', N'dont wanna use wood', N'05/26/2021 11:19 AM', NULL, NULL, 28, 1, 2045, 0, 2, 1, 0, 2, N'05/26/2021 11:19 AM', 2, N'05/26/2021 11:19 AM')
INSERT [dbo].[Inquiry] ([InquiryId], [InquiryCode], [InquiryName], [InquiryDescription], [InquiryStartDate], [InquiryDueDate], [InquiryEndDate], [CustomerId], [BranchId], [BuildingId], [IsEscalationRequested], [AddedBy], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2057, NULL, N'', N'', N'05/27/2021 02:53 PM', NULL, NULL, 28, 1, 2046, 0, 2, 1, 0, 2, N'05/27/2021 02:53 PM', 2, N'05/27/2021 02:53 PM')
SET IDENTITY_INSERT [dbo].[Inquiry] OFF
GO
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Measurement Pending', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Measurement Delayed', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Design Pending', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Design Delayed', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Quotation Pending', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Quotation Delayed', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Measurement Accepted', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'Measurement Rejected', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'Measurement Waiting For Approval', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, N'Design Accepted', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, N'Design Rejected', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, N'Design Waiting For Approval', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, N'Quotation Accepted', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, N'Quotation Rejected', NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryStatus] ([InquiryStatusId], [InquiryStatusName], [InquiryStatusDescription], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (15, N'Quotation Waiting For Approval', NULL, 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[InquiryWorkscope] ON 

INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, 4, 1, NULL, NULL, NULL, 1, N'04/27/2021 10:01 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, 4, 2, NULL, NULL, NULL, 2, N'04/27/2021 10:01 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, 5, 1, NULL, 30, 30, 4, N'04/14/2021 1:31 PM', N'04/14/2021 1:31 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, 6, 1, NULL, NULL, NULL, 4, N'04/21/2021 1:40 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, 7, 1, NULL, NULL, NULL, 5, N'05/06/2021 10:08 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, 8, 1, NULL, NULL, NULL, 6, N'05/05/2021 12:02 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, 9, 1, NULL, NULL, NULL, 2, N'04/28/2021 12:07 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, 10, 1, NULL, NULL, NULL, 2, N'04/21/2021 12:35 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, 11, 1, NULL, NULL, NULL, 4, N'04/21/2021 11:38 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, 12, 1, NULL, NULL, NULL, 4, N'04/20/2021 11:40 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, 13, 1, NULL, NULL, NULL, 4, N'04/20/2021 5:55 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, 14, 1, NULL, NULL, NULL, 6, N'04/23/2021 12:04 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, 15, 1, NULL, NULL, NULL, 2, N'04/21/2021 11:37 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, 16, 1, NULL, 1, NULL, 2, N'04/20/2021 1:27 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (15, 16, 2, NULL, 1, NULL, 4, N'04/20/2021 1:27 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (16, 17, 1, NULL, 30, NULL, 2, N'04/29/2021 1:44 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (17, 17, 2, NULL, 30, NULL, 4, N'04/29/2021 1:44 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (18, 18, 1, NULL, 30, NULL, 4, N'04/21/2021 1:46 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (19, 18, 2, NULL, 30, NULL, 2, N'04/21/2021 1:46 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1014, 1016, 1, NULL, 2, NULL, 2, N'04/20/2021 2:19 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1015, 1016, 2, NULL, 2, NULL, 2, N'04/20/2021 2:19 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1016, 1017, 1, NULL, 30, NULL, 4, N'04/21/2021 8:59 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1017, 1018, 1, NULL, 30, NULL, 4, N'04/22/2021 9:00 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1018, 1019, 1, NULL, 30, NULL, 2, N'04/22/2021 9:02 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1019, 1020, 1, NULL, 30, NULL, 2, N'04/21/2021 9:04 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1020, 1020, 2, NULL, 30, NULL, 4, N'04/21/2021 9:04 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1021, 1021, 1, NULL, 30, NULL, 5, N'04/20/2021 9:21 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1022, 1021, 2, NULL, 30, NULL, 2, N'04/20/2021 9:21 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1023, 1022, 1, NULL, 30, NULL, 2, N'04/20/2021 9:22 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1024, 1022, 2, NULL, 30, NULL, 2, N'04/20/2021 9:22 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1025, 1023, 1, NULL, 30, NULL, 4, N'04/22/2021 9:46 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1026, 1023, 2, NULL, 30, NULL, 4, N'04/22/2021 9:46 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1027, 1024, 1, NULL, 30, NULL, 2, N'04/22/2021 9:48 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1028, 1024, 2, NULL, 30, NULL, 2, N'04/22/2021 9:48 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1029, 1025, 1, NULL, 30, NULL, 2, N'04/21/2021 9:51 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1030, 1025, 2, NULL, 30, NULL, 2, N'04/21/2021 9:51 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1031, 1026, 1, NULL, 2, NULL, 2, N'04/21/2021 10:04 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1032, 1026, 2, NULL, 2, NULL, 2, N'04/21/2021 10:04 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1033, 1027, 1, NULL, 2, 2, 3, N'05/06/2021 10:59 AM', N'05/28/2021 10:59 AM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1034, 1027, 2, NULL, 2, 2, 3, N'05/06/2021 10:59 AM', N'05/28/2021 10:59 AM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1035, 1028, 1, NULL, 2, NULL, 2, N'04/20/2021 10:06 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1036, 1028, 2, NULL, 2, NULL, 2, N'04/20/2021 10:06 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1037, 1029, 1, NULL, 2, NULL, 2, N'04/27/2021 10:02 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1038, 1029, 2, NULL, 2, NULL, 2, N'04/27/2021 10:02 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1039, 1030, 1, NULL, 2, NULL, 5, N'04/20/2021 10:09 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1040, 1030, 2, NULL, 2, NULL, 2, N'04/20/2021 10:09 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1041, 1031, 1, NULL, 2, NULL, 2, N'04/29/2021 1:04 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1042, 1031, 2, NULL, 2, NULL, 6, N'04/29/2021 1:04 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1043, 1032, 1, NULL, 30, NULL, 2, N'04/21/2021 1:26 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1044, 1033, 1, NULL, 2, NULL, 1, N'07/22/2021 2:30 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1045, 1034, 1, NULL, 30, NULL, 4, N'04/28/2021 8:48 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1046, 1034, 2, NULL, 30, NULL, 2, N'04/28/2021 8:48 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1047, 1035, 1, NULL, 33, NULL, 5, N'04/23/2021 11:18 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1048, 1035, 2, NULL, 33, NULL, 6, N'04/23/2021 11:18 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1049, 1036, 1, NULL, 33, NULL, 2, N'04/23/2021 9:09 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1050, 1036, 2, NULL, 33, NULL, 2, N'04/23/2021 9:09 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1051, 1037, 1, NULL, 33, NULL, 2, N'04/23/2021 9:09 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1052, 1037, 2, NULL, 33, NULL, 2, N'04/23/2021 9:09 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1053, 1038, 1, NULL, 33, 2, 2, N'04/30/2021 2:01 PM', N'05/08/2021 2:01 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1054, 1038, 2, NULL, 33, 2, 2, N'04/30/2021 2:01 PM', N'05/08/2021 2:01 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1055, 1039, 1, NULL, 33, 2, 1, N'05/07/2021 5:00 PM', N'04/30/2021 2:00 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1056, 1039, 2, NULL, 33, 2, 1, N'05/07/2021 5:00 PM', N'04/30/2021 2:00 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1057, 1040, 1, NULL, 33, 2, 1, N'05/07/2021 1:50 PM', N'05/07/2021 1:50 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1058, 1040, 2, NULL, 33, 2, 1, N'05/07/2021 1:50 PM', N'05/07/2021 1:50 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1059, 1041, 1, NULL, 2, 2, 2, N'04/18/2021 1:23 PM', N'04/18/2021 1:23 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1060, 1041, 2, NULL, 2, 2, 2, N'04/18/2021 1:23 PM', N'04/18/2021 1:23 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1061, 1042, 1, NULL, 33, 2, 1, N'05/07/2021 1:50 PM', N'05/07/2021 1:50 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1062, 1043, 1, NULL, 33, 2, 1, N'05/07/2021 1:49 PM', N'05/07/2021 1:49 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1063, 1044, 1, NULL, 33, 2, 4, N'04/28/2021 1:14 PM', N'04/28/2021 1:14 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1064, 1045, 1, NULL, 2, NULL, 2, N'05/05/2021 6:16 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1065, 1045, 2, NULL, 2, NULL, 2, N'05/05/2021 6:16 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1066, 1046, 1, NULL, 33, 2, 2, N'04/22/2021 12:27 PM', N'04/30/2021 1:58 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1067, 1046, 2, NULL, 33, 2, 2, N'04/22/2021 12:27 PM', N'04/30/2021 1:58 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1068, 1047, 1, NULL, 2, 2, 2, N'04/30/2021 9:54 AM', N'05/07/2021 9:55 AM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1069, 1047, 2, NULL, 2, 2, 2, N'04/30/2021 9:54 AM', N'05/07/2021 9:55 AM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1070, 1048, 1, NULL, 2, NULL, 2, N'05/06/2021 10:41 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1071, 1048, 2, NULL, 2, NULL, 2, N'05/06/2021 10:41 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1072, 1049, 1, NULL, 2, 2, 2, N'04/30/2021 9:11 PM', N'05/06/2021 9:15 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1073, 1049, 2, NULL, 2, 2, 2, N'04/30/2021 9:11 PM', N'05/06/2021 9:15 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1074, 1050, 1, NULL, 36, NULL, 2, N'04/29/2021 10:05 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1075, 1050, 2, NULL, 36, NULL, 2, N'04/29/2021 10:05 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1076, 1051, 3, NULL, 36, NULL, 1, N'06/01/2021 12:12 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1077, 1051, 1, NULL, 36, NULL, 1, N'06/01/2021 12:12 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1078, 1051, 2, NULL, 36, NULL, 1, N'06/01/2021 12:12 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1079, 1051, 4, NULL, 36, NULL, 1, N'06/01/2021 12:12 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1080, 1052, 1, NULL, 36, NULL, 1, N'06/02/2021 7:45 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1081, 1052, 3, NULL, 36, NULL, 1, N'06/02/2021 7:45 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1082, 1052, 2, NULL, 36, NULL, 1, N'06/02/2021 7:45 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1083, 1053, 6, NULL, 36, 2, 1, N'05/13/2021 9:30 PM', N'05/19/2021 9:31 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1084, 1053, 1, NULL, 36, 2, 1, N'05/13/2021 9:30 PM', N'05/19/2021 9:31 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1085, 1054, 7, NULL, 36, 2, 1, N'05/29/2021 9:56 PM', N'06/03/2021 9:57 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1086, 1054, 1, NULL, 36, 2, 1, N'05/29/2021 9:56 PM', N'06/03/2021 9:57 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1087, 1054, 6, NULL, 36, 2, 1, N'05/29/2021 9:56 PM', N'06/03/2021 9:57 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1088, 1054, 2, NULL, 36, 2, 1, N'05/29/2021 9:56 PM', N'06/03/2021 9:57 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1089, 1054, 5, NULL, 36, 2, 1, N'05/29/2021 9:56 PM', N'06/03/2021 9:57 PM', NULL, NULL, 1, 0, NULL, NULL, 2, N'05/06/2021 01:31 PM')
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1090, 1055, 1, NULL, 36, NULL, 1, N'05/19/2021 4:28 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2090, 2055, 1, NULL, 36, NULL, 1, N'10/22/2021 12:06 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2091, 2055, 7, NULL, 36, NULL, 1, N'10/22/2021 12:06 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2092, 2055, 2, NULL, 36, NULL, 1, N'10/22/2021 12:06 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2093, 2055, 4, NULL, 36, NULL, 1, N'10/22/2021 12:06 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2094, 2055, 5, NULL, 36, NULL, 1, N'10/22/2021 12:06 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2095, 2055, 6, NULL, 36, NULL, 1, N'10/22/2021 12:06 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2096, 2055, 3, NULL, 36, NULL, 1, N'10/22/2021 12:06 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2097, 2056, 1, NULL, 36, NULL, 1, N'05/27/2021 12:15 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2098, 2056, 2, NULL, 36, NULL, 1, N'05/27/2021 12:15 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2099, 2056, 3, NULL, 36, NULL, 1, N'05/27/2021 12:15 AM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2100, 2057, 1, NULL, 36, NULL, 1, N'05/28/2021 3:53 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2101, 2057, 3, NULL, 36, NULL, 1, N'05/28/2021 3:53 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[InquiryWorkscope] ([InquiryWorkscopeId], [InquiryId], [WorkscopeId], [IsMeasurementDrawing], [MeasurementAssignedTo], [DesignAssignedTo], [InquiryStatusId], [MeasurementScheduleDate], [DesignScheduleDate], [IsDesignApproved], [Comments], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2102, 2057, 2, NULL, 36, NULL, 1, N'05/28/2021 3:53 PM', NULL, NULL, NULL, 1, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[InquiryWorkscope] OFF
GO
SET IDENTITY_INSERT [dbo].[Log] ON 

INSERT [dbo].[Log] ([Id], [Message], [MessageTemplate], [Level], [TimeStamp], [Exception], [Properties], [LogEvent], [UserName], [IP]) VALUES (4, N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 290', N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 290', N'Error', CAST(N'2021-04-26T09:55:11.1249782+04:00' AS DateTimeOffset), NULL, N'<properties><property key="Username">Guest</property></properties>', NULL, NULL, NULL)
INSERT [dbo].[Log] ([Id], [Message], [MessageTemplate], [Level], [TimeStamp], [Exception], [Properties], [LogEvent], [UserName], [IP]) VALUES (5, N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 290', N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 290', N'Error', CAST(N'2021-04-26T13:58:28.8422088+04:00' AS DateTimeOffset), NULL, N'<properties><property key="Username">Guest</property></properties>', NULL, NULL, NULL)
INSERT [dbo].[Log] ([Id], [Message], [MessageTemplate], [Level], [TimeStamp], [Exception], [Properties], [LogEvent], [UserName], [IP]) VALUES (6, N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 290', N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 290', N'Error', CAST(N'2021-04-27T21:12:10.4287457+04:00' AS DateTimeOffset), NULL, N'<properties><property key="Username">Guest</property></properties>', NULL, NULL, NULL)
INSERT [dbo].[Log] ([Id], [Message], [MessageTemplate], [Level], [TimeStamp], [Exception], [Properties], [LogEvent], [UserName], [IP]) VALUES (7, N'Error: UserId=2 Error=An exception was thrown while attempting to evaluate a LINQ query parameter expression. See the inner exception for more information. To show additional information call ''DbContextOptionsBuilder.EnableSensitiveDataLogging''. System.InvalidOperationException: An exception was thrown while attempting to evaluate a LINQ query parameter expression. See the inner exception for more information. To show additional information call ''DbContextOptionsBuilder.EnableSensitiveDataLogging''.
 ---> System.NullReferenceException: Object reference not set to an instance of an object.
   at lambda_method1041(Closure )
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.GetValue(Expression expression, String& parameterName)
   --- End of inner exception stack trace ---
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.GetValue(Expression expression, String& parameterName)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Evaluate(Expression expression, Boolean generateParameter)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.VisitBinary(BinaryExpression binaryExpression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.VisitBinary(BinaryExpression binaryExpression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Linq.Expressions.ExpressionVisitor.VisitLambda[T](Expression`1 node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Linq.Expressions.ExpressionVisitor.VisitUnary(UnaryExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.ExtractParameters(Expression expression)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error: UserId=2 Error=An exception was thrown while attempting to evaluate a LINQ query parameter expression. See the inner exception for more information. To show additional information call ''DbContextOptionsBuilder.EnableSensitiveDataLogging''. System.InvalidOperationException: An exception was thrown while attempting to evaluate a LINQ query parameter expression. See the inner exception for more information. To show additional information call ''DbContextOptionsBuilder.EnableSensitiveDataLogging''.
 ---> System.NullReferenceException: Object reference not set to an instance of an object.
   at lambda_method1041(Closure )
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.GetValue(Expression expression, String& parameterName)
   --- End of inner exception stack trace ---
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.GetValue(Expression expression, String& parameterName)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Evaluate(Expression expression, Boolean generateParameter)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.VisitBinary(BinaryExpression binaryExpression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.VisitBinary(BinaryExpression binaryExpression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Linq.Expressions.ExpressionVisitor.VisitLambda[T](Expression`1 node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Linq.Expressions.ExpressionVisitor.VisitUnary(UnaryExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at System.Dynamic.Utils.ExpressionVisitorUtils.VisitArguments(ExpressionVisitor visitor, IArgumentProvider nodes)
   at System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(MethodCallExpression node)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.Visit(Expression expression)
   at Microsoft.EntityFrameworkCore.Query.Internal.ParameterExtractingExpressionVisitor.ExtractParameters(Expression expression)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error', CAST(N'2021-05-04T10:59:53.6837854+04:00' AS DateTimeOffset), NULL, N'<properties><property key="Username">Guest</property></properties>', NULL, NULL, NULL)
INSERT [dbo].[Log] ([Id], [Message], [MessageTemplate], [Level], [TimeStamp], [Exception], [Properties], [LogEvent], [UserName], [IP]) VALUES (8, N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error', CAST(N'2021-05-05T12:15:03.7493171+04:00' AS DateTimeOffset), NULL, N'<properties><property key="Username">Guest</property></properties>', NULL, NULL, NULL)
INSERT [dbo].[Log] ([Id], [Message], [MessageTemplate], [Level], [TimeStamp], [Exception], [Properties], [LogEvent], [UserName], [IP]) VALUES (9, N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error', CAST(N'2021-05-05T21:31:13.2482707+04:00' AS DateTimeOffset), NULL, N'<properties><property key="Username">Guest</property></properties>', NULL, NULL, NULL)
INSERT [dbo].[Log] ([Id], [Message], [MessageTemplate], [Level], [TimeStamp], [Exception], [Properties], [LogEvent], [UserName], [IP]) VALUES (10, N'Error: UserId=36 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error: UserId=36 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error', CAST(N'2021-05-05T21:56:18.3404597+04:00' AS DateTimeOffset), NULL, N'<properties><property key="Username">Guest</property></properties>', NULL, NULL, NULL)
INSERT [dbo].[Log] ([Id], [Message], [MessageTemplate], [Level], [TimeStamp], [Exception], [Properties], [LogEvent], [UserName], [IP]) VALUES (11, N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error: UserId=2 Error=The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information. System.InvalidOperationException: The LINQ expression ''DbSet<UserRole>()
    .Where(u => __inquiry_AddedByNavigation_UserRoles_0
        .Where(y => y.UserRoleId == u.UserRoleId)
        .Any() && u.IsActive == (Nullable<bool>)True && u.IsDeleted == (Nullable<bool>)False)'' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to ''AsEnumerable'', ''AsAsyncEnumerable'', ''ToList'', or ''ToListAsync''. See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.<VisitMethodCall>g__CheckTranslated|15_0(ShapedQueryExpression translated, <>c__DisplayClass15_0& )
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryableMethodTranslatingExpressionVisitor.VisitMethodCall(MethodCallExpression methodCallExpression)
   at Microsoft.EntityFrameworkCore.Query.QueryCompilationContext.CreateQueryExecutor[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Storage.Database.CompileQuery[TResult](Expression query, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.CompileQueryCore[TResult](IDatabase database, Expression query, IModel model, Boolean async)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.<>c__DisplayClass9_0`1.<Execute>b__0()
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryCache.GetOrAddQuery[TResult](Object cacheKey, Func`1 compiler)
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute[TResult](Expression query)
   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetEnumerator()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.IncludableQueryable`2.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SaiKitchenBackend.Controllers.InquiryController.UpdateInquiryScheduleDate(UpdateInquirySchedule updateInquirySchedule) in D:\SAI Kitchen\BackendSaiKitchen\BackendSaiKitchen\Controllers\InquiryController.cs:line 197', N'Error', CAST(N'2021-05-05T21:57:11.3866637+04:00' AS DateTimeOffset), NULL, N'<properties><property key="Username">Guest</property></properties>', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Log] OFF
GO
SET IDENTITY_INSERT [dbo].[Notification] ON 

INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 35, 0, 1, 0, 0, N'04/15/2021 11:10 AM', 0, N'04/15/2021 11:10 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 26, 0, 1, 0, 0, N'04/15/2021 11:15 AM', 0, N'04/15/2021 11:15 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 26, 0, 1, 0, 0, N'04/15/2021 11:16 AM', 0, N'04/15/2021 11:16 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 26, 0, 1, 0, 0, N'04/15/2021 11:18 AM', 0, N'04/15/2021 11:18 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 26, 0, 1, 0, 0, N'04/15/2021 11:24 AM', 0, N'04/15/2021 11:24 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 26, 0, 1, 0, 0, N'04/15/2021 11:25 AM', 0, N'04/15/2021 11:25 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 26, 0, 1, 0, 0, N'04/15/2021 11:27 AM', 0, N'04/15/2021 11:27 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 26, 0, 1, 0, 2, N'04/15/2021 11:35 AM', 2, N'04/15/2021 11:35 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 35, 0, 1, 0, 0, N'04/15/2021 11:38 AM', 0, N'04/15/2021 11:38 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 35, 0, 1, 0, 2, N'04/15/2021 11:39 AM', 2, N'04/15/2021 11:39 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, N'Customer 1 generated inquiry on another branch', NULL, 0, NULL, NULL, NULL, NULL, 35, 0, 1, 0, 2, N'04/15/2021 11:40 AM', 2, N'04/15/2021 11:40 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, N' You are assigned for the new measurement', 9, 0, NULL, NULL, 30, 2, 33, 0, 1, 0, 30, N'04/19/2021 08:59 AM', 30, N'04/19/2021 08:59 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, N' You are assigned for the new measurement', 9, 0, NULL, NULL, 30, 2, 33, 0, 1, 0, 30, N'04/19/2021 09:00 AM', 30, N'04/19/2021 09:00 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (14, N' You are assigned for the new measurement', 9, 0, NULL, NULL, 30, 2, 33, 0, 1, 0, 30, N'04/19/2021 09:02 AM', 30, N'04/19/2021 09:02 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (15, N' You are assigned for the new measurement', 2, 0, NULL, NULL, 1, 2, NULL, 0, 1, 0, 30, N'04/19/2021 09:04 AM', 30, N'04/19/2021 09:04 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (16, N' You are assigned for the new measurement', 2, 0, NULL, NULL, 30, 2, NULL, 0, 1, 0, 30, N'04/19/2021 09:21 AM', 30, N'04/19/2021 09:21 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (17, N' You are assigned for the new measurement', 2, 0, NULL, NULL, 30, 2, NULL, 0, 1, 0, 30, N'04/19/2021 09:22 AM', 30, N'04/19/2021 09:22 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (18, N' You are assigned for the new measurement', 2, 0, NULL, NULL, 30, 2, NULL, 0, 1, 0, 30, N'04/19/2021 09:46 AM', 30, N'04/19/2021 09:46 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (19, N' You are assigned for the new measurement', 2, 0, NULL, NULL, 30, 2, NULL, 0, 1, 0, 30, N'04/19/2021 09:48 AM', 30, N'04/19/2021 09:48 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (20, N' You are assigned for the new measurement', 2, 0, NULL, NULL, 30, 2, NULL, 0, 1, 0, 30, N'04/19/2021 09:51 AM', 30, N'04/19/2021 09:51 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (21, N' You are assigned for the new measurement at 04/21/2021 10:04 PM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/19/2021 09:57 AM', 2, N'04/19/2021 09:57 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (22, N' You are assigned for the new measurement at 04/20/2021 10:04 AM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/19/2021 09:59 AM', 2, N'04/19/2021 09:59 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (23, N' You are assigned for the new measurement at 04/20/2021 10:06 AM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/19/2021 10:00 AM', 2, N'04/19/2021 10:00 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (24, N' You are assigned for the new measurement at 04/27/2021 10:02 AM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/19/2021 10:01 AM', 2, N'04/19/2021 10:01 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (25, N' You are assigned for the new measurement at 04/20/2021 10:09 AM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/19/2021 10:04 AM', 2, N'04/19/2021 10:04 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (26, N' You are assigned for the new measurement at 04/29/2021 1:04 PM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/19/2021 10:05 AM', 2, N'04/19/2021 10:05 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (27, N' You are assigned for the new measurement at 04/21/2021 1:26 PM', 2, 0, NULL, NULL, 30, 2, NULL, 0, 1, 0, 30, N'04/19/2021 10:23 AM', 30, N'04/19/2021 10:23 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (28, N' You are assigned for the new measurement at 07/22/2021 2:30 PM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/19/2021 12:25 PM', 2, N'04/19/2021 12:25 PM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (29, N' You are assigned for the new measurement at 04/28/2021 8:48 AM', 2, 0, NULL, NULL, 30, 2, NULL, 0, 1, 0, 30, N'04/20/2021 08:47 AM', 30, N'04/20/2021 08:47 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (30, N' You are assigned for the new measurement at 04/23/2021 11:18 AM', 2, 0, NULL, NULL, 33, 1, NULL, 0, 1, 0, 2, N'04/20/2021 11:18 AM', 2, N'04/20/2021 11:18 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (31, N' You are assigned for the new measurement at 04/23/2021 9:09 AM', 2, 0, NULL, NULL, 33, 1, NULL, 0, 1, 0, 2, N'04/21/2021 09:10 AM', 2, N'04/21/2021 09:10 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (32, N' You are assigned for the new measurement at 04/23/2021 9:09 AM', 2, 0, NULL, NULL, 33, 1, NULL, 0, 1, 0, 2, N'04/21/2021 09:12 AM', 2, N'04/21/2021 09:12 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (33, N' You are assigned for the new measurement at 04/29/2021 12:17 PM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 33, N'04/21/2021 09:14 AM', 33, N'04/21/2021 09:14 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (34, N' You are assigned for the new measurement at 04/22/2021 10:08 AM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/21/2021 10:09 AM', 2, N'04/21/2021 10:09 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (35, N' You are assigned for the new measurement at 04/29/2021 10:11 AM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 33, N'04/21/2021 10:11 AM', 33, N'04/21/2021 10:11 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (36, N' You are assigned for the new measurement at 04/29/2021 10:12 AM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 33, N'04/21/2021 10:13 AM', 33, N'04/21/2021 10:13 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (37, N' You are assigned for the new measurement at 04/27/2021 10:14 AM', 2, 0, NULL, NULL, 33, 1, NULL, 0, 1, 0, 2, N'04/21/2021 10:15 AM', 2, N'04/21/2021 10:15 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (38, N' You are assigned for the new measurement at 04/29/2021 11:16 AM', 2, 0, NULL, NULL, 33, 1, NULL, 0, 1, 0, 2, N'04/22/2021 11:17 AM', 2, N'04/22/2021 11:17 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (39, N' You are assigned for the new measurement at 04/29/2021 11:24 AM', 2, 0, NULL, NULL, 33, 1, NULL, 0, 1, 0, 2, N'04/22/2021 11:24 AM', 2, N'04/22/2021 11:24 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (40, N' You are assigned for the new measurement at 05/05/2021 6:16 PM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/24/2021 04:15 PM', 2, N'04/24/2021 04:15 PM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (41, N' You are assigned for the new measurement at 04/28/2021 8:40 AM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/26/2021 08:40 AM', 2, N'04/26/2021 08:40 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (42, N' You are assigned for the new measurement at 04/29/2021 9:47 AM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'04/26/2021 08:47 AM', 2, N'04/26/2021 08:47 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (43, N' You are assigned for the new measurement at 05/06/2021 10:41 AM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'04/27/2021 10:42 AM', 2, N'04/27/2021 10:42 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (44, N' You are assigned for the new measurement at 05/05/2021 11:19 AM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'04/27/2021 11:20 AM', 2, N'04/27/2021 11:20 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (45, N' You are assigned for the new measurement at 04/29/2021 10:05 AM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'04/27/2021 09:17 PM', 2, N'04/27/2021 09:17 PM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (46, N' You are assigned for the new measurement at 06/01/2021 12:12 PM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'05/03/2021 12:12 PM', 2, N'05/03/2021 12:12 PM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (47, N' You are assigned for the new measurement at 06/02/2021 7:45 PM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'05/03/2021 06:46 PM', 2, N'05/03/2021 06:46 PM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (48, N' You are assigned for the new measurement at 06/03/2021 1:05 PM', 2, 0, NULL, NULL, 2, 1, NULL, 0, 1, 0, 2, N'05/05/2021 12:06 PM', 2, N'05/05/2021 12:06 PM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (49, N' You are assigned for the new measurement at 05/28/2021 9:34 PM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'05/05/2021 09:35 PM', 2, N'05/05/2021 09:35 PM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (50, N' You are assigned for the new measurement at 05/19/2021 4:28 AM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'05/11/2021 03:29 PM', 2, N'05/11/2021 03:29 PM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1050, N' You are assigned for the new measurement at 10/22/2021 12:06 PM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'05/24/2021 12:07 PM', 2, N'05/24/2021 12:07 PM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1051, N' You are assigned for the new measurement at 05/27/2021 12:15 AM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'05/26/2021 11:19 AM', 2, N'05/26/2021 11:19 AM')
INSERT [dbo].[Notification] ([NotificationId], [NotificationContent], [NotificationCategoryId], [IsActionable], [NotificationAcceptAction], [NotificationDeclineAction], [UserId], [BranchId], [UserRoleId], [IsRead], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1052, N' You are assigned for the new measurement at 05/28/2021 3:53 PM', 2, 0, NULL, NULL, 36, 1, NULL, 0, 1, 0, 2, N'05/27/2021 02:53 PM', 2, N'05/27/2021 02:53 PM')
SET IDENTITY_INSERT [dbo].[Notification] OFF
GO
INSERT [dbo].[NotificationCategory] ([NotificationCategoryId], [NotificationCategoryName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Inquiry', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[NotificationCategory] ([NotificationCategoryId], [NotificationCategoryName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Measurement', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[NotificationCategory] ([NotificationCategoryId], [NotificationCategoryName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Design', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[NotificationCategory] ([NotificationCategoryId], [NotificationCategoryName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Quotation', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[NotificationCategory] ([NotificationCategoryId], [NotificationCategoryName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'JobOrder', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[NotificationCategory] ([NotificationCategoryId], [NotificationCategoryName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Procurement', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[NotificationCategory] ([NotificationCategoryId], [NotificationCategoryName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Supplier', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[NotificationCategory] ([NotificationCategoryId], [NotificationCategoryName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'Delivery', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[NotificationCategory] ([NotificationCategoryId], [NotificationCategoryName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'Other', 1, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1, N'All', NULL, NULL, NULL, NULL, 0, 1)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2, N'Manage Branch', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (3, N'Manage Branch Role', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (4, N'Mange Users', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (5, N'Manage Customer', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (6, N'Manage Inquiry', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (7, N'Manage Measurement', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (8, N'Manage Design', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (9, N'Manage Quotation', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (10, N'Manage WorkScope', NULL, NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[PermissionLevel] ([PermissionLevelId], [PermissionLevelName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Read', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[PermissionLevel] ([PermissionLevelId], [PermissionLevelName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Create', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[PermissionLevel] ([PermissionLevelId], [PermissionLevelName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Update', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[PermissionLevel] ([PermissionLevelId], [PermissionLevelName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Escalate', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[PermissionLevel] ([PermissionLevelId], [PermissionLevelName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Delete', 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PermissionRole] ON 

INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1, 2, 1, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (11, 4, 14, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (12, 5, 15, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1011, 2, 14, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1012, 3, 14, 3, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1022, 3, 1, 4, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1023, 4, 1, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1024, 5, 1, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1025, 2, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1026, 3, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1027, 4, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1028, 5, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1029, 5, 24, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1030, 3, 25, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1031, 5, 26, 2, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1032, 2, 27, 2, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1033, 3, 27, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1034, 4, 27, 3, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1035, 5, 27, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1036, 2, 28, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1037, 3, 28, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1038, 4, 28, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1039, 5, 28, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1040, 2, 29, 4, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1041, 3, 29, 3, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1042, 4, 29, 2, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1043, 5, 29, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1044, 6, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1045, 5, 30, 4, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1046, 5, 31, 2, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1047, 6, 31, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1048, 2, 31, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1049, 3, 31, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1050, 4, 31, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1051, 5, 32, 2, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1052, 6, 32, 2, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1053, 2, 32, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1054, 3, 32, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1055, 4, 32, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1056, 2, 33, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1057, 3, 33, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1058, 4, 33, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1059, 5, 33, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1060, 6, 33, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1061, 7, 33, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1062, 8, 33, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1063, 9, 33, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1064, 3, 34, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1065, 7, 35, 4, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1066, 8, 35, 4, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1067, 2, 36, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1068, 3, 36, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1069, 4, 36, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1070, 6, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1071, 7, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1072, 8, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1073, 9, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2065, 6, 1035, 4, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2066, 7, 1035, 4, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2067, 2, 1036, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2068, 3, 1036, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2069, 4, 1036, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2070, 5, 1036, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2071, 6, 1036, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2072, 7, 1036, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2073, 8, 1036, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2074, 9, 1036, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2075, 7, 1037, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2076, 2, 1038, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2077, 10, 23, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (3077, 5, 1039, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (3078, 6, 1039, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (3079, 9, 1040, 5, NULL, NULL, NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[PermissionRole] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleHead] ON 

INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, 14, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, 15, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (7, 33, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (8, 34, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (9, 35, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1009, 1035, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1010, 1036, 1035, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1011, 1037, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1012, 1037, 23, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1013, 1038, 23, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1014, 1039, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1015, 1039, 15, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1016, 1039, 23, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1017, 1040, 1, 1, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[RoleHead] OFF
GO
INSERT [dbo].[RoleType] ([RoleTypeId], [RoleTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Manager', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleType] ([RoleTypeId], [RoleTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Accounts', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleType] ([RoleTypeId], [RoleTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Sales', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleType] ([RoleTypeId], [RoleTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Designer', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleType] ([RoleTypeId], [RoleTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Procurement', 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Info', N'info@sai-group.ae', N'Sai123456', N'0545552471', N'Mff2oemjBNlIHCylG0XiOE2yGDh/LiSAuQ==', NULL, N'clnzPiseo8nc01dTxr5dVZ:APA91bE7zCHpFoOCkAf1rg0_PmoSGXBlVvKZyo0_dcWINKseD-qC2X5ekutm9Se1Z2Exvf2s0LdC6tJDc8QTL2p-UizyDppMCuz05Jif6G4xqHu9Rjq-uoA__g4eRmrSoAGbt2XxrUW8', 1, 0, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'CEO', N'ceo@sai-group.ae', N'Sai123456', N'0555598849', N'MjcZ71qRItlIHNDmfCXdhkGGxqQbhgxYgQ==', NULL, N'clnzPiseo8nc01dTxr5dVZ:APA91bE7zCHpFoOCkAf1rg0_PmoSGXBlVvKZyo0_dcWINKseD-qC2X5ekutm9Se1Z2Exvf2s0LdC6tJDc8QTL2p-UizyDppMCuz05Jif6G4xqHu9Rjq-uoA__g4eRmrSoAGbt2XxrUW8', 1, 0, 1, NULL, NULL, 0, N'31/03/2021 09:10:27')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (21, N'Sameer', N'sameer71095@gmail.com', N'Sai123456', N'971545552471', N'MjH8OlEFDvjYSBNPPrsNqvROscfQZP9j3ks=', NULL, NULL, 0, 1, 0, 0, N'31/03/2021 14:39:23', 0, N'08/04/2021 10:32:33')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (22, N'Sale', N'sales@sai-group.ae', N'Sai123456', N'971545552471', N'MjKkMIt3YPrYSFYMtFY+P8ZJgBdz82WX+t4=', NULL, N'', 0, 1, 0, 0, N'31/03/2021 15:37:33', NULL, NULL)
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (23, N'Test', N'test@test.com', NULL, N'971545552471', NULL, NULL, NULL, 0, 1, 0, 0, N'03/04/2021 09:31:57', 0, N'03/04/2021 09:32:06')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (24, N'Sameer Masood', N'sameer.masood@micropolis.ae', N'Sai123456', N'971545552471', N'MjSvYGRUhfbYSLwMGjn4USNBtpIydYyTurM=', NULL, NULL, 0, 1, 0, 0, N'03/04/2021 13:45:41', 0, N'03/04/2021 13:50:42')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (25, N'Sameer', N'sameer.masood@micropolis.ae', N'Sai123456', N'971545552471', N'MjWxiHPAs/bYSFtotTUZTuJLsMDxb/YsUlA=', NULL, NULL, 0, 1, 0, 0, N'03/04/2021 19:17:10', 0, N'05/04/2021 20:01:58')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (26, N'Sameer', N'sameer.lion16@gmail.com', NULL, N'971545552471', NULL, NULL, NULL, 0, 1, 0, 0, N'04/04/2021 10:23:25', 0, N'08/04/2021 10:32:39')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (27, N'Sameer', N'sameer.masood@micropolis.ae', NULL, N'971545552471', NULL, NULL, NULL, 0, 1, 0, 0, N'05/04/2021 20:02:22', 0, N'05/04/2021 20:03:59')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (28, N'Sameer', N'sameer71095@gmail.com', N'Sai123456', N'971545552475', N'MjgiprS+XvrYSLQV58tvuwBPqIodf3J2bO8=', NULL, N'clnzPiseo8nc01dTxr5dVZ:APA91bE7zCHpFoOCkAf1rg0_PmoSGXBlVvKZyo0_dcWINKseD-qC2X5ekutm9Se1Z2Exvf2s0LdC6tJDc8QTL2p-UizyDppMCuz05Jif6G4xqHu9Rjq-uoA__g4eRmrSoAGbt2XxrUW8', 0, 1, 0, 0, N'08/04/2021 10:33:00', 0, N'08/04/2021 11:31:22')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (29, N'Smae', N'sameer71095@gmail.com', N'Sai123456', N'97154455552471', N'', NULL, N'', 0, 1, 0, 0, N'08/04/2021 20:13:51', NULL, NULL)
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (30, N'Ahmed Galal', N'a.galal@sai-group.ae', N'Sai123456', N'971555614339', N'MzBBpYp4QQ7ZSIE3M1W50PVMspt3AB29I9E=', NULL, N'clnzPiseo8nc01dTxr5dVZ:APA91bE7zCHpFoOCkAf1rg0_PmoSGXBlVvKZyo0_dcWINKseD-qC2X5ekutm9Se1Z2Exvf2s0LdC6tJDc8QTL2p-UizyDppMCuz05Jif6G4xqHu9Rjq-uoA__g4eRmrSoAGbt2XxrUW8', 1, 0, 1, 2, N'13/04/2021 09:56:07', NULL, NULL)
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (31, N'test', N'test@test.com', N'Sai123456', N'971545552471', NULL, NULL, NULL, 0, 1, NULL, 2, N'04/14/2021 11:54 AM', 2, N'04/14/2021 11:54 AM')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (32, N'Test', N'test@gmail.com', N'Sai123456', N'971545552478', N'', NULL, N'', 0, 1, 0, 2, N'04/14/2021 01:35 PM', 2, N'04/14/2021 01:35 PM')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (33, N'Sameer', N'sameer71095@gmail.com', N'Sai123456', N'971545552473', N'', NULL, N'', 1, 1, 0, 2, N'04/20/2021 09:56 AM', 2, N'04/20/2021 09:56 AM')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (34, N'test', N'test@test.com', NULL, N'971545552471', NULL, NULL, NULL, 1, 1, NULL, 2, N'04/22/2021 11:07 AM', 2, N'04/22/2021 11:09 AM')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (35, N'test', N'sameer@gmail.com', NULL, N'971545552471', NULL, NULL, NULL, 1, 1, NULL, 2, N'04/22/2021 11:11 AM', 2, N'04/22/2021 11:11 AM')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (36, N'Aala', N'a.fahd@sai-group.ae', N'Sai123456', N'971504383353', N'MzYqsmzO/SDZSCzSOyHeK15MhmwKCROQH8k=', NULL, N'clnzPiseo8nc01dTxr5dVZ:APA91bE7zCHpFoOCkAf1rg0_PmoSGXBlVvKZyo0_dcWINKseD-qC2X5ekutm9Se1Z2Exvf2s0LdC6tJDc8QTL2p-UizyDppMCuz05Jif6G4xqHu9Rjq-uoA__g4eRmrSoAGbt2XxrUW8', 1, 0, 1, 2, N'04/26/2021 08:45 AM', 2, N'04/26/2021 08:45 AM')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (37, N'Sameer', N'sameer71095@gmail.com', NULL, N'971545552471', NULL, NULL, NULL, 1, 1, NULL, 2, N'04/27/2021 10:39 AM', 2, N'04/27/2021 11:10 AM')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (38, N'Fareed', N'fareed@gmail.com', NULL, N'971545552471', NULL, NULL, NULL, 1, 0, NULL, 2, N'05/03/2021 06:40 PM', 2, N'05/03/2021 06:40 PM')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (39, N'Samee', N'sales@sai-group.ae', NULL, N'97154542545', NULL, NULL, NULL, 1, 0, NULL, 2, N'05/26/2021 11:07 AM', 2, N'05/26/2021 11:07 AM')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1, 1, 2, 15, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (5, 2, 1, 23, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (24, 21, 2, 14, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (25, 22, 1, 24, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (26, 23, 1, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (27, 24, 11, 14, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (28, 25, 2, 14, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (29, 26, 2, 15, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (30, 27, 2, 15, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (31, 28, 2, 28, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (32, 29, 2, 30, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (33, 30, 2, 33, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (34, 31, 1, 14, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (35, 32, 1, 36, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (36, 33, 1, 1035, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (37, 34, 16, 1036, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (38, 35, 16, 1036, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (39, 36, 1, 1037, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (40, 37, 1, 1038, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (41, 38, 1, 1039, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (42, 39, 19, 1040, NULL, NULL, NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Direct', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Google', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Facebook', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Linkedin', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Twitter', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Friend', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Website', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'Mobile App', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'Other', 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[WorkScope] ON 

INSERT [dbo].[WorkScope] ([WorkScopeId], [WorkScopeName], [WorkScopeDescription], [QuestionaireType], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Kitchen', N'', 1, 1, 0, NULL, NULL, 2, N'05/03/2021 01:46 PM')
INSERT [dbo].[WorkScope] ([WorkScopeId], [WorkScopeName], [WorkScopeDescription], [QuestionaireType], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Wardrobe', N'', 2, 1, 0, NULL, NULL, 2, N'05/03/2021 12:09 PM')
INSERT [dbo].[WorkScope] ([WorkScopeId], [WorkScopeName], [WorkScopeDescription], [QuestionaireType], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Pantry', N'', 1, 1, 0, 2, N'05/03/2021 11:53 AM', 2, N'05/03/2021 01:01 PM')
INSERT [dbo].[WorkScope] ([WorkScopeId], [WorkScopeName], [WorkScopeDescription], [QuestionaireType], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Dresser', N'', 2, 1, 0, 2, N'05/03/2021 12:11 PM', 2, N'05/03/2021 12:17 PM')
INSERT [dbo].[WorkScope] ([WorkScopeId], [WorkScopeName], [WorkScopeDescription], [QuestionaireType], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Dressing Room', N'', 2, 1, 0, 2, N'05/03/2021 06:55 PM', 2, N'05/03/2021 06:55 PM')
INSERT [dbo].[WorkScope] ([WorkScopeId], [WorkScopeName], [WorkScopeDescription], [QuestionaireType], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'TV Unit', N'', 2, 1, 0, 2, N'05/05/2021 12:03 PM', 2, N'05/05/2021 12:03 PM')
INSERT [dbo].[WorkScope] ([WorkScopeId], [WorkScopeName], [WorkScopeDescription], [QuestionaireType], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Main Kitchen', N'', 1, 1, 0, 2, N'05/05/2021 09:33 PM', 2, N'05/05/2021 09:33 PM')
SET IDENTITY_INSERT [dbo].[WorkScope] OFF
GO
ALTER TABLE [dbo].[PermissionRole] ADD  CONSTRAINT [DF_PermissionRole_IsActive]  DEFAULT ('true') FOR [IsActive]
GO
ALTER TABLE [dbo].[PermissionRole] ADD  CONSTRAINT [DF_PermissionRole_IsDeleted]  DEFAULT ('false') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Accesories]  WITH CHECK ADD  CONSTRAINT [FK_Accesories_WardrobeDesignInformation] FOREIGN KEY([WardrobeDesignInfoId])
REFERENCES [dbo].[WardrobeDesignInformation] ([WDIId])
GO
ALTER TABLE [dbo].[Accesories] CHECK CONSTRAINT [FK_Accesories_WardrobeDesignInformation]
GO
ALTER TABLE [dbo].[Appliances]  WITH CHECK ADD  CONSTRAINT [FK_Appliances_KitchenDesignInfo] FOREIGN KEY([KitchenDesignInfoId])
REFERENCES [dbo].[KitchenDesignInfo] ([KDIId])
GO
ALTER TABLE [dbo].[Appliances] CHECK CONSTRAINT [FK_Appliances_KitchenDesignInfo]
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_Branch_BranchType] FOREIGN KEY([BranchTypeId])
REFERENCES [dbo].[BranchType] ([BranchTypeId])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Branch_BranchType]
GO
ALTER TABLE [dbo].[BranchRole]  WITH CHECK ADD  CONSTRAINT [FK_BranchRole_RoleType] FOREIGN KEY([RoleTypeId])
REFERENCES [dbo].[RoleType] ([RoleTypeId])
GO
ALTER TABLE [dbo].[BranchRole] CHECK CONSTRAINT [FK_BranchRole_RoleType]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Branch]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_ContactStatus] FOREIGN KEY([ContactStatusId])
REFERENCES [dbo].[ContactStatus] ([ContactStatusId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_ContactStatus]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_User]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_WayOfContact] FOREIGN KEY([WayofContactId])
REFERENCES [dbo].[WayOfContact] ([WayOfContactId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_WayOfContact]
GO
ALTER TABLE [dbo].[CustomerBranch]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBranch_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
GO
ALTER TABLE [dbo].[CustomerBranch] CHECK CONSTRAINT [FK_CustomerBranch_Branch]
GO
ALTER TABLE [dbo].[CustomerBranch]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBranch_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerBranch] CHECK CONSTRAINT [FK_CustomerBranch_Customer]
GO
ALTER TABLE [dbo].[Design]  WITH CHECK ADD  CONSTRAINT [FK_Design_InquiryWorkscope] FOREIGN KEY([InquiryWorkscopeId])
REFERENCES [dbo].[InquiryWorkscope] ([InquiryWorkscopeId])
GO
ALTER TABLE [dbo].[Design] CHECK CONSTRAINT [FK_Design_InquiryWorkscope]
GO
ALTER TABLE [dbo].[Design]  WITH CHECK ADD  CONSTRAINT [FK_Design_User] FOREIGN KEY([DesignTakenBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Design] CHECK CONSTRAINT [FK_Design_User]
GO
ALTER TABLE [dbo].[Design]  WITH CHECK ADD  CONSTRAINT [FK_Design_User1] FOREIGN KEY([DesignApprovedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Design] CHECK CONSTRAINT [FK_Design_User1]
GO
ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_Design] FOREIGN KEY([DesignId])
REFERENCES [dbo].[Design] ([DesignId])
GO
ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_Design]
GO
ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_Measurement] FOREIGN KEY([MeasurementId])
REFERENCES [dbo].[Measurement] ([MeasurementId])
GO
ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_Measurement]
GO
ALTER TABLE [dbo].[Inquiry]  WITH CHECK ADD  CONSTRAINT [FK_Inquiry_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
GO
ALTER TABLE [dbo].[Inquiry] CHECK CONSTRAINT [FK_Inquiry_Branch]
GO
ALTER TABLE [dbo].[Inquiry]  WITH CHECK ADD  CONSTRAINT [FK_Inquiry_bUILDING] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Building] ([BuildingId])
GO
ALTER TABLE [dbo].[Inquiry] CHECK CONSTRAINT [FK_Inquiry_bUILDING]
GO
ALTER TABLE [dbo].[Inquiry]  WITH CHECK ADD  CONSTRAINT [FK_Inquiry_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Inquiry] CHECK CONSTRAINT [FK_Inquiry_Customer]
GO
ALTER TABLE [dbo].[Inquiry]  WITH CHECK ADD  CONSTRAINT [FK_Inquiry_User] FOREIGN KEY([AddedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Inquiry] CHECK CONSTRAINT [FK_Inquiry_User]
GO
ALTER TABLE [dbo].[InquiryWorkscope]  WITH CHECK ADD  CONSTRAINT [FK_InquiryMeasurement_Inquiry] FOREIGN KEY([InquiryId])
REFERENCES [dbo].[Inquiry] ([InquiryId])
GO
ALTER TABLE [dbo].[InquiryWorkscope] CHECK CONSTRAINT [FK_InquiryMeasurement_Inquiry]
GO
ALTER TABLE [dbo].[InquiryWorkscope]  WITH CHECK ADD  CONSTRAINT [FK_InquiryMeasurement_WorkScope] FOREIGN KEY([WorkscopeId])
REFERENCES [dbo].[WorkScope] ([WorkScopeId])
GO
ALTER TABLE [dbo].[InquiryWorkscope] CHECK CONSTRAINT [FK_InquiryMeasurement_WorkScope]
GO
ALTER TABLE [dbo].[InquiryWorkscope]  WITH CHECK ADD  CONSTRAINT [FK_InquiryWorkscope_InquiryStatus] FOREIGN KEY([InquiryStatusId])
REFERENCES [dbo].[InquiryStatus] ([InquiryStatusId])
GO
ALTER TABLE [dbo].[InquiryWorkscope] CHECK CONSTRAINT [FK_InquiryWorkscope_InquiryStatus]
GO
ALTER TABLE [dbo].[InquiryWorkscope]  WITH CHECK ADD  CONSTRAINT [FK_InquiryWorkscope_User] FOREIGN KEY([MeasurementAssignedTo])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[InquiryWorkscope] CHECK CONSTRAINT [FK_InquiryWorkscope_User]
GO
ALTER TABLE [dbo].[InquiryWorkscope]  WITH CHECK ADD  CONSTRAINT [FK_InquiryWorkscope_User1] FOREIGN KEY([DesignAssignedTo])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[InquiryWorkscope] CHECK CONSTRAINT [FK_InquiryWorkscope_User1]
GO
ALTER TABLE [dbo].[Measurement]  WITH CHECK ADD  CONSTRAINT [FK_Measurement_Fees] FOREIGN KEY([FeesId])
REFERENCES [dbo].[Fees] ([FeesId])
GO
ALTER TABLE [dbo].[Measurement] CHECK CONSTRAINT [FK_Measurement_Fees]
GO
ALTER TABLE [dbo].[Measurement]  WITH CHECK ADD  CONSTRAINT [FK_Measurement_InquiryWorkscope] FOREIGN KEY([InquiryWorkscopeId])
REFERENCES [dbo].[InquiryWorkscope] ([InquiryWorkscopeId])
GO
ALTER TABLE [dbo].[Measurement] CHECK CONSTRAINT [FK_Measurement_InquiryWorkscope]
GO
ALTER TABLE [dbo].[Measurement]  WITH CHECK ADD  CONSTRAINT [FK_Measurement_KitchenDesignInfo] FOREIGN KEY([KitchenDesignInfoId])
REFERENCES [dbo].[KitchenDesignInfo] ([KDIId])
GO
ALTER TABLE [dbo].[Measurement] CHECK CONSTRAINT [FK_Measurement_KitchenDesignInfo]
GO
ALTER TABLE [dbo].[Measurement]  WITH CHECK ADD  CONSTRAINT [FK_Measurement_MeasurementDetail] FOREIGN KEY([MeasurementDetailId])
REFERENCES [dbo].[MeasurementDetail] ([MeasurementDetailId])
GO
ALTER TABLE [dbo].[Measurement] CHECK CONSTRAINT [FK_Measurement_MeasurementDetail]
GO
ALTER TABLE [dbo].[Measurement]  WITH CHECK ADD  CONSTRAINT [FK_Measurement_User] FOREIGN KEY([MeasurementTakenBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Measurement] CHECK CONSTRAINT [FK_Measurement_User]
GO
ALTER TABLE [dbo].[Measurement]  WITH CHECK ADD  CONSTRAINT [FK_Measurement_User1] FOREIGN KEY([MeasurementApprovedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Measurement] CHECK CONSTRAINT [FK_Measurement_User1]
GO
ALTER TABLE [dbo].[Measurement]  WITH CHECK ADD  CONSTRAINT [FK_Measurement_WardrobeDesignInformation] FOREIGN KEY([WardrobeDesignInfoId])
REFERENCES [dbo].[WardrobeDesignInformation] ([WDIId])
GO
ALTER TABLE [dbo].[Measurement] CHECK CONSTRAINT [FK_Measurement_WardrobeDesignInformation]
GO
ALTER TABLE [dbo].[MeasurementDetailInfo]  WITH CHECK ADD  CONSTRAINT [FK_MeasurementDetailInfo_MeasurementDetail] FOREIGN KEY([MeasurementDetailId])
REFERENCES [dbo].[MeasurementDetail] ([MeasurementDetailId])
GO
ALTER TABLE [dbo].[MeasurementDetailInfo] CHECK CONSTRAINT [FK_MeasurementDetailInfo_MeasurementDetail]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Branch]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_NotificationCategory] FOREIGN KEY([NotificationCategoryId])
REFERENCES [dbo].[NotificationCategory] ([NotificationCategoryId])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_NotificationCategory]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_UserRole] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([UserRoleId])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_UserRole]
GO
ALTER TABLE [dbo].[PermissionRole]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRole_BranchRole] FOREIGN KEY([BranchRoleId])
REFERENCES [dbo].[BranchRole] ([BranchRoleId])
GO
ALTER TABLE [dbo].[PermissionRole] CHECK CONSTRAINT [FK_PermissionRole_BranchRole]
GO
ALTER TABLE [dbo].[PermissionRole]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRole_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permission] ([PermissionId])
GO
ALTER TABLE [dbo].[PermissionRole] CHECK CONSTRAINT [FK_PermissionRole_Permission]
GO
ALTER TABLE [dbo].[PermissionRole]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRole_PermissionLevel] FOREIGN KEY([PermissionLevelId])
REFERENCES [dbo].[PermissionLevel] ([PermissionLevelId])
GO
ALTER TABLE [dbo].[PermissionRole] CHECK CONSTRAINT [FK_PermissionRole_PermissionLevel]
GO
ALTER TABLE [dbo].[RoleHead]  WITH CHECK ADD  CONSTRAINT [FK_RoleHead_BranchRole] FOREIGN KEY([EmployeeRoleId])
REFERENCES [dbo].[BranchRole] ([BranchRoleId])
GO
ALTER TABLE [dbo].[RoleHead] CHECK CONSTRAINT [FK_RoleHead_BranchRole]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_Role_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_Role_Branch]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_Role_BranchRole] FOREIGN KEY([BranchRoleId])
REFERENCES [dbo].[BranchRole] ([BranchRoleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_Role_BranchRole]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
USE [master]
GO
ALTER DATABASE [BackendSaiKitchen_db] SET  READ_WRITE 
GO
