USE [master]
GO
/****** Object:  Database [DbSaiKitchen]    Script Date: 08/04/2021 9:22:30 AM ******/
CREATE DATABASE [DbSaiKitchen]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DbSaiKitchen', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DbSaiKitchen.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DbSaiKitchen_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DbSaiKitchen_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DbSaiKitchen] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DbSaiKitchen].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DbSaiKitchen] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET ARITHABORT OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DbSaiKitchen] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DbSaiKitchen] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DbSaiKitchen] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DbSaiKitchen] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET RECOVERY FULL 
GO
ALTER DATABASE [DbSaiKitchen] SET  MULTI_USER 
GO
ALTER DATABASE [DbSaiKitchen] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DbSaiKitchen] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DbSaiKitchen] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DbSaiKitchen] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DbSaiKitchen] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DbSaiKitchen] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DbSaiKitchen', N'ON'
GO
ALTER DATABASE [DbSaiKitchen] SET QUERY_STORE = OFF
GO
USE [DbSaiKitchen]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[BranchRole]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[BranchType]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[ContactStatus]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[Permission]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[PermissionLevel]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[PermissionRole]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[RoleHead]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[RoleType]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[UserRole]    Script Date: 08/04/2021 9:22:30 AM ******/
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
/****** Object:  Table [dbo].[WayOfContact]    Script Date: 08/04/2021 9:22:30 AM ******/
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
SET IDENTITY_INSERT [dbo].[Branch] ON 

INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Head Office', N'Arjan-Dubai', N'+97145514553', 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Sakamkam', N'Sakamkam Fujairah', N'+971506671659', 2, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Umm Deira', N'Umm Al Quwain', N'+97145514553', 3, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, N'Test', N'Arjan', N'971545552471', 2, 1, 1, 0, N'03/04/2021 13:44:40', 0, N'03/04/2021 13:48:11')
INSERT [dbo].[Branch] ([BranchId], [BranchName], [BranchAddress], [BranchContact], [BranchTypeId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, N'Test', N'Arjan', N'971545552471', 2, 1, 1, 0, N'03/04/2021 19:15:34', 0, N'05/04/2021 20:01:26')
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO
SET IDENTITY_INSERT [dbo].[BranchRole] ON 

INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1, N'Head Manager', N'All access of one branch', NULL, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (14, N'Procurement Manager', N'Procurement access', NULL, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (15, N'Sales Manager', N'Sales related stuff', NULL, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (23, N'CEO', N'', NULL, 0, N'31/03/2021 15:24:17', 0, N'03/04/2021 11:29:55', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (24, N'Sale Assistant', N'', NULL, 0, N'31/03/2021 15:37:04', 0, N'03/04/2021 09:32:20', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (25, N'Sameer', N'test', NULL, 0, N'03/04/2021 13:45:04', 0, N'03/04/2021 13:47:51', 1, 1)
INSERT [dbo].[BranchRole] ([BranchRoleId], [BranchRoleName], [BranchRoleDescription], [RoleTypeId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (26, N'Interior Designer', N'test', NULL, 0, N'03/04/2021 19:16:11', 0, N'05/04/2021 16:37:13', 1, 1)
SET IDENTITY_INSERT [dbo].[BranchRole] OFF
GO
INSERT [dbo].[BranchType] ([BranchTypeId], [BranchTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Head Office', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[BranchType] ([BranchTypeId], [BranchTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Showroom', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[BranchType] ([BranchTypeId], [BranchTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Factory', 1, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ContactStatus] ([ContactStatusId], [ContactStatusName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Contacted', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[ContactStatus] ([ContactStatusId], [ContactStatusName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Need to Contact', 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Customer 1', N'sameer71095@gmail.com', N'0545552471', N'Dubai Business Center Arjan', N'Dubai', N'United Arab Emirates', N'Pakistan', N'78419951234567', 2, 1, 1, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Customer 2', N'sameer71095@gmail.coms', N'0545552473', N'Dubai Business Center Arjan', N'Dubai', N'United Arab Emirates', N'Pakistan', N'78419901234567', 1, 2, 2, 2, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Sam', N'sameer71095@gmail.com', N'971545552471', N'Dubai Internet City', N'Lahore, Punjab, Pakistan', N'Pakistan', N'Albania', N'78419951234567', 1, 1, 2, 2, 1, 0, NULL, NULL, 0, N'07/04/2021 15:26:02')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'test', N'sameer71095@gmail.com', N'971545552472', N'Dubai Internet City', N'Ajman, Ajman, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'78419951234567', 2, 3, 2, 2, 1, 0, NULL, NULL, 0, N'07/04/2021 14:46:21')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Test', N'sameer.masood@micropolis.ae', N'971545552471', N'Mall of emirates', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Pakistan', N'7841995123456789', 2, 4, 1, 1, 1, 0, 0, N'07/04/2021 10:26:53', NULL, NULL)
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail], [CustomerContact], [CustomerAddress], [CustomerCity], [CustomerCountry], [CustomerNationality], [CustomerNationalId], [ContactStatusId], [WayofContactId], [BranchId], [UserId], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Test Customer', N'sameer.masood@micropolis.ae', N'971545552472', N'JBR', N'Dubai, Dubai, United Arab Emirates', N'United Arab Emirates', N'Palestine', N'78419952310156', 1, 5, 1, 1, 1, 0, 0, N'07/04/2021 10:28:54', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1, N'All', NULL, NULL, NULL, NULL, 0, 1)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (2, N'Manage Branch', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (3, N'Manage Branch Role', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (4, N'Mange Users', NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[Permission] ([PermissionId], [PermissionName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (5, N'Manage Customer', NULL, NULL, NULL, NULL, 1, 0)
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
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (12, 5, 15, 2, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1011, 2, 14, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1012, 3, 14, 3, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1022, 3, 1, 4, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1023, 4, 1, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1024, 5, 1, 3, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1025, 2, 23, 2, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1026, 3, 23, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1027, 4, 23, 2, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1028, 5, 23, 3, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1029, 5, 24, 4, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1030, 3, 25, 5, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[PermissionRole] ([PermissionRoleId], [PermissionId], [BranchRoleId], [PermissionLevelId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1031, 5, 26, 2, NULL, NULL, NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[PermissionRole] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleHead] ON 

INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, 14, 1, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleHead] ([RoleHeadId], [EmployeeRoleId], [HeadRoleId], [IsActive], [IsDeleted], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, 15, 1, 1, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[RoleHead] OFF
GO
INSERT [dbo].[RoleType] ([RoleTypeId], [RoleTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Manager', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleType] ([RoleTypeId], [RoleTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Accountant', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleType] ([RoleTypeId], [RoleTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Sales', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[RoleType] ([RoleTypeId], [RoleTypeName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Designer', 1, 0, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Info', N'info@sai-group.ae', N'Sai123456', N'0545552471', N'MXCXKaKt+dhIfWLJAaJNIkCmZlPc6oMvZA==', NULL, N'clnzPiseo8nc01dTxr5dVZ:APA91bE7zCHpFoOCkAf1rg0_PmoSGXBlVvKZyo0_dcWINKseD-qC2X5ekutm9Se1Z2Exvf2s0LdC6tJDc8QTL2p-UizyDppMCuz05Jif6G4xqHu9Rjq-uoA__g4eRmrSoAGbt2XxrUW8', 1, 0, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'CEO', N'ceo@sai-group.ae', N'Sai123456', N'0555598849', N'Mse0UVdL+thIFR5zBWqdI0uQN29K2O0wDA==', NULL, N'clnzPiseo8nc01dTxr5dVZ:APA91bE7zCHpFoOCkAf1rg0_PmoSGXBlVvKZyo0_dcWINKseD-qC2X5ekutm9Se1Z2Exvf2s0LdC6tJDc8QTL2p-UizyDppMCuz05Jif6G4xqHu9Rjq-uoA__g4eRmrSoAGbt2XxrUW8', 1, 0, 1, NULL, NULL, 0, N'31/03/2021 09:10:27')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (21, N'Sameer', N'sameer71095@gmail.com', N'Sai123456', N'971545552471', N'MjH8OlEFDvjYSBNPPrsNqvROscfQZP9j3ks=', NULL, NULL, 1, 0, 1, 0, N'31/03/2021 14:39:23', NULL, NULL)
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (22, N'Sale', N'sales@sai-group.ae', N'Sai123456', N'971545552471', N'MjLVsvz4h/bYSEn4OS/LlGpPiH4eZiS83kM=', NULL, NULL, 1, 0, 1, 0, N'31/03/2021 15:37:33', NULL, NULL)
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (23, N'Test', N'test@test.com', NULL, N'971545552471', NULL, NULL, NULL, 1, 1, NULL, 0, N'03/04/2021 09:31:57', 0, N'03/04/2021 09:32:06')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (24, N'Sameer Masood', N'sameer.masood@micropolis.ae', N'Sai123456', N'971545552471', N'MjSvYGRUhfbYSLwMGjn4USNBtpIydYyTurM=', NULL, NULL, 1, 1, 1, 0, N'03/04/2021 13:45:41', 0, N'03/04/2021 13:50:42')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (25, N'Sameer', N'sameer.masood@micropolis.ae', N'Sai123456', N'971545552471', N'MjWxiHPAs/bYSFtotTUZTuJLsMDxb/YsUlA=', NULL, NULL, 1, 1, 1, 0, N'03/04/2021 19:17:10', 0, N'05/04/2021 20:01:58')
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (26, N'Sameer', N'sameer.lion16@gmail.com', NULL, N'971545552471', NULL, NULL, NULL, 1, 0, NULL, 0, N'04/04/2021 10:23:25', NULL, NULL)
INSERT [dbo].[User] ([UserId], [UserName], [UserEmail], [UserPassword], [UserMobile], [UserToken], [UserProfileImageURL], [UserFCMToken], [IsActive], [IsDeleted], [IsOnline], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (27, N'Sameer', N'sameer.masood@micropolis.ae', NULL, N'971545552471', NULL, NULL, NULL, 1, 1, NULL, 0, N'05/04/2021 20:02:22', 0, N'05/04/2021 20:03:59')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (1, 1, 1, 15, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (5, 2, 2, 23, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (24, 21, 2, 14, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (25, 22, 1, 24, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (26, 23, 1, 1, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (27, 24, 11, 14, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (28, 25, 2, 14, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (29, 26, 2, 15, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [BranchId], [BranchRoleId], [CreatedBy], [CreatedDate], [UpdaredBy], [UpdatedDate], [IsActive], [IsDeleted]) VALUES (30, 27, 2, 15, NULL, NULL, NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Direct', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Google', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Facebook', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Linkedin', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Twitter', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[WayOfContact] ([WayOfContactId], [WayOfContactName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Other', 1, 0, NULL, NULL, NULL, NULL)
GO
ALTER TABLE [dbo].[PermissionRole] ADD  CONSTRAINT [DF_PermissionRole_IsActive]  DEFAULT ('true') FOR [IsActive]
GO
ALTER TABLE [dbo].[PermissionRole] ADD  CONSTRAINT [DF_PermissionRole_IsDeleted]  DEFAULT ('false') FOR [IsDeleted]
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
ALTER TABLE [dbo].[RoleHead]  WITH CHECK ADD  CONSTRAINT [FK_RoleHead_BranchRole1] FOREIGN KEY([HeadRoleId])
REFERENCES [dbo].[BranchRole] ([BranchRoleId])
GO
ALTER TABLE [dbo].[RoleHead] CHECK CONSTRAINT [FK_RoleHead_BranchRole1]
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
ALTER DATABASE [DbSaiKitchen] SET  READ_WRITE 
GO
