USE [BEnterprises_Dev]
GO
ALTER TABLE [dbo].[M_Branch] DROP CONSTRAINT [FK_M_Branch___PK_M_Company]
GO
ALTER TABLE [dbo].[M_UOM] DROP CONSTRAINT [DF__M_UOM__CreatedDa__45F365D3]
GO
ALTER TABLE [dbo].[M_Supplier] DROP CONSTRAINT [DF__M_Supplie__Creat__48CFD27E]
GO
ALTER TABLE [dbo].[M_ProductType] DROP CONSTRAINT [DF__M_Product__Creat__3A81B327]
GO
ALTER TABLE [dbo].[M_Product] DROP CONSTRAINT [DF__M_Product__Creat__5165187F]
GO
ALTER TABLE [dbo].[M_Guage] DROP CONSTRAINT [DF__M_Guage__Created__4E88ABD4]
GO
ALTER TABLE [dbo].[M_Company] DROP CONSTRAINT [DF__M_Company__Creat__36B12243]
GO
ALTER TABLE [dbo].[M_Brand] DROP CONSTRAINT [DF__M_Brand__Created__3F466844]
GO
ALTER TABLE [dbo].[M_Branch] DROP CONSTRAINT [DF__M_Branch__Create__38996AB5]
GO
/****** Object:  Table [dbo].[M_UOM]    Script Date: 04-01-2020 17:14:43 ******/
DROP TABLE [dbo].[M_UOM]
GO
/****** Object:  Table [dbo].[M_Supplier]    Script Date: 04-01-2020 17:14:43 ******/
DROP TABLE [dbo].[M_Supplier]
GO
/****** Object:  Table [dbo].[M_ProductType]    Script Date: 04-01-2020 17:14:43 ******/
DROP TABLE [dbo].[M_ProductType]
GO
/****** Object:  Table [dbo].[M_Product]    Script Date: 04-01-2020 17:14:43 ******/
DROP TABLE [dbo].[M_Product]
GO
/****** Object:  Table [dbo].[M_Guage]    Script Date: 04-01-2020 17:14:43 ******/
DROP TABLE [dbo].[M_Guage]
GO
/****** Object:  Table [dbo].[M_Company]    Script Date: 04-01-2020 17:14:43 ******/
DROP TABLE [dbo].[M_Company]
GO
/****** Object:  Table [dbo].[M_Brand]    Script Date: 04-01-2020 17:14:43 ******/
DROP TABLE [dbo].[M_Brand]
GO
/****** Object:  Table [dbo].[M_Branch]    Script Date: 04-01-2020 17:14:43 ******/
DROP TABLE [dbo].[M_Branch]
GO
USE [master]
GO
/****** Object:  Database [BEnterprises_Dev]    Script Date: 04-01-2020 17:14:43 ******/
DROP DATABASE [BEnterprises_Dev]
GO
/****** Object:  Database [BEnterprises_Dev]    Script Date: 04-01-2020 17:14:43 ******/
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
/****** Object:  Table [dbo].[M_Branch]    Script Date: 04-01-2020 17:14:44 ******/
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
/****** Object:  Table [dbo].[M_Brand]    Script Date: 04-01-2020 17:14:44 ******/
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
/****** Object:  Table [dbo].[M_Company]    Script Date: 04-01-2020 17:14:45 ******/
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
/****** Object:  Table [dbo].[M_Guage]    Script Date: 04-01-2020 17:14:45 ******/
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
/****** Object:  Table [dbo].[M_Product]    Script Date: 04-01-2020 17:14:45 ******/
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
/****** Object:  Table [dbo].[M_ProductType]    Script Date: 04-01-2020 17:14:45 ******/
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
/****** Object:  Table [dbo].[M_Supplier]    Script Date: 04-01-2020 17:14:45 ******/
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
/****** Object:  Table [dbo].[M_UOM]    Script Date: 04-01-2020 17:14:45 ******/
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
ALTER TABLE [dbo].[M_Supplier] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_UOM] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[M_Branch]  WITH CHECK ADD  CONSTRAINT [FK_M_Branch___PK_M_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[M_Company] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[M_Branch] CHECK CONSTRAINT [FK_M_Branch___PK_M_Company]
GO
USE [master]
GO
ALTER DATABASE [BEnterprises_Dev] SET  READ_WRITE 
GO
