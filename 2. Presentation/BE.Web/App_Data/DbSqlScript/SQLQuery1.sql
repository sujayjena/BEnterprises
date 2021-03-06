USE [master]
GO
/****** Object:  Database [BEnterprises_Dev]    Script Date: 05-01-2020 19:21:46 ******/
CREATE DATABASE [BEnterprises_Dev]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BEnterprises', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\BEnterprises.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BEnterprises_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\BEnterprises_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BEnterprises_Dev] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BEnterprises_Dev].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BEnterprises_Dev] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET ARITHABORT OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BEnterprises_Dev] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BEnterprises_Dev] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BEnterprises_Dev] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BEnterprises_Dev] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BEnterprises_Dev] SET  MULTI_USER 
GO
ALTER DATABASE [BEnterprises_Dev] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BEnterprises_Dev] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BEnterprises_Dev] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BEnterprises_Dev] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BEnterprises_Dev] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BEnterprises_Dev] SET QUERY_STORE = OFF
GO
USE [BEnterprises_Dev]
GO
/****** Object:  Table [dbo].[M_Branch]    Script Date: 05-01-2020 19:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_Branch](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Phone] [nvarchar](10) NULL,
	[Email] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_M_Branch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_Brand]    Script Date: 05-01-2020 19:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_Brand](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_Company]    Script Date: 05-01-2020 19:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_Company](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Phone] [nvarchar](10) NULL,
	[Email] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_M_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_Guage]    Script Date: 05-01-2020 19:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_Guage](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_Product]    Script Date: 05-01-2020 19:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_Product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_ProductType]    Script Date: 05-01-2020 19:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_ProductType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_M_ProductType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_Roles]    Script Date: 05-01-2020 19:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_Roles](
	[Id] [nvarchar](50) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK__M_Roles__3214EC07DA276693] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_Supplier]    Script Date: 05-01-2020 19:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_Supplier](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_UOM]    Script Date: 05-01-2020 19:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_UOM](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_M_ProductUOM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_User]    Script Date: 05-01-2020 19:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_User](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](10) NULL,
	[Email] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserPassword] [nvarchar](50) NOT NULL,
	[RoleId] [nvarchar](50) NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[LastLoginIP] [nvarchar](50) NULL,
	[CurrentLoginTime] [datetime] NULL,
	[CurrentLoginIP] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_M_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[M_Roles] ([Id], [RoleName], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (N'D3A260E8-1200-420B-B8D2-998DF76285D9', N'Admin', N'Super Admin', CAST(N'2020-01-05T13:04:05.257' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[M_Roles] ([Id], [RoleName], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (N'E94F1D04-A117-40EC-991D-87EF6C93D5FE', N'Super Admin', N'Super Admin', CAST(N'2020-01-05T13:03:37.523' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[M_User] ([Id], [Name], [Phone], [Email], [UserName], [UserPassword], [RoleId], [LastLoginTime], [LastLoginIP], [CurrentLoginTime], [CurrentLoginIP], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (N'1fd9782c-050a-49cb-a291-037b7315f488', N'Sujay Jena', N'9861463465', N'sujay930@gmail.com', N'01', N'01', N'E94F1D04-A117-40EC-991D-87EF6C93D5FE', NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-05T13:11:05.077' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[M_User] ([Id], [Name], [Phone], [Email], [UserName], [UserPassword], [RoleId], [LastLoginTime], [LastLoginIP], [CurrentLoginTime], [CurrentLoginIP], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (N'a6e78725-ea84-4b3c-bbf0-453bfa0c4e7b', N'Bijay', N'7205256400', N'bijay3616@gmail.com', N'00', N'00', N'D3A260E8-1200-420B-B8D2-998DF76285D9', NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-05T19:12:31.393' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[M_User] ([Id], [Name], [Phone], [Email], [UserName], [UserPassword], [RoleId], [LastLoginTime], [LastLoginIP], [CurrentLoginTime], [CurrentLoginIP], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (N'ba0f19e6-d4e8-45ca-b275-80acebaa97a8', N'Bijay', N'7205256400', N'bijay3616@gmail.com', N'00', N'00', N'D3A260E8-1200-420B-B8D2-998DF76285D9', NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-05T19:10:14.380' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[M_User] ([Id], [Name], [Phone], [Email], [UserName], [UserPassword], [RoleId], [LastLoginTime], [LastLoginIP], [CurrentLoginTime], [CurrentLoginIP], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (N'3f3df1d2-1527-47d6-aea0-c5925c027cbe', N'Bijay', N'7205256400', N'bijay3616@gmail.com', N'00', N'00', N'D3A260E8-1200-420B-B8D2-998DF76285D9', NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-05T19:09:26.187' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[M_User] ([Id], [Name], [Phone], [Email], [UserName], [UserPassword], [RoleId], [LastLoginTime], [LastLoginIP], [CurrentLoginTime], [CurrentLoginIP], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (N'9c7b2e7c-1c15-4d75-ab1a-d08e533f42f7', N'Bijay', N'7205256400', N'bijay3616@gmail.com', N'00', N'00', N'D3A260E8-1200-420B-B8D2-998DF76285D9', NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-05T19:10:22.503' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[M_User] ([Id], [Name], [Phone], [Email], [UserName], [UserPassword], [RoleId], [LastLoginTime], [LastLoginIP], [CurrentLoginTime], [CurrentLoginIP], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate]) VALUES (N'4c093d5a-5413-4330-bee2-f9d1a096d646', N'Bijay', N'7205256400', N'bijay3616@gmail.com', N'00', N'00', N'D3A260E8-1200-420B-B8D2-998DF76285D9', NULL, NULL, NULL, NULL, NULL, CAST(N'2020-01-05T19:06:39.123' AS DateTime), NULL, NULL)
GO
ALTER TABLE [dbo].[M_Branch] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_Brand] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_Company] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_Guage] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_Product] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_ProductType] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_Roles] ADD  CONSTRAINT [DF__M_Roles__Created__6D0D32F4]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_Supplier] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_UOM] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_User] ADD  CONSTRAINT [DF__M_User__CreatedD__6FE99F9F]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_Branch]  WITH CHECK ADD  CONSTRAINT [FK_M_Branch___PK_M_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[M_Company] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[M_Branch] CHECK CONSTRAINT [FK_M_Branch___PK_M_Company]
GO
ALTER TABLE [dbo].[M_User]  WITH CHECK ADD  CONSTRAINT [FK_M_User_RoleId__PK_M_Roles_Id] FOREIGN KEY([RoleId])
REFERENCES [dbo].[M_Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[M_User] CHECK CONSTRAINT [FK_M_User_RoleId__PK_M_Roles_Id]
GO
USE [master]
GO
ALTER DATABASE [BEnterprises_Dev] SET  READ_WRITE 
GO
