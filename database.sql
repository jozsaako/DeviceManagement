USE [master]
GO
/****** Object:  Database [deviceManagement]    Script Date: 6/21/2018 6:37:50 PM ******/
CREATE DATABASE [deviceManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'deviceManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS01\MSSQL\DATA\deviceManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'deviceManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS01\MSSQL\DATA\deviceManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [deviceManagement] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [deviceManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [deviceManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [deviceManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [deviceManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [deviceManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [deviceManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [deviceManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [deviceManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [deviceManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [deviceManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [deviceManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [deviceManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [deviceManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [deviceManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [deviceManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [deviceManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [deviceManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [deviceManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [deviceManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [deviceManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [deviceManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [deviceManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [deviceManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [deviceManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [deviceManagement] SET  MULTI_USER 
GO
ALTER DATABASE [deviceManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [deviceManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [deviceManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [deviceManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [deviceManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [deviceManagement] SET QUERY_STORE = OFF
GO
USE [deviceManagement]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [deviceManagement]
GO
/****** Object:  Table [dbo].[device]    Script Date: 6/21/2018 6:37:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[device](
	[DeviceID] [int] IDENTITY(1,1) NOT NULL,
	[DName] [varchar](50) NULL,
	[DManufacturer] [varchar](50) NULL,
	[DType] [varchar](50) NULL,
	[DOS] [varchar](50) NULL,
	[DOSVersion] [varchar](50) NULL,
	[DProcessor] [varchar](50) NULL,
	[DRAM] [varchar](50) NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_device] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 6/21/2018 6:37:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[isAdmin] [bit] NULL,
	[Location] [varchar](50) NULL,
	[DeviceID] [int] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[device]  WITH CHECK ADD  CONSTRAINT [FK_device_user] FOREIGN KEY([UserID])
REFERENCES [dbo].[user] ([userID])
GO
ALTER TABLE [dbo].[device] CHECK CONSTRAINT [FK_device_user]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_device] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[device] ([DeviceID])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_device]
GO
USE [master]
GO
ALTER DATABASE [deviceManagement] SET  READ_WRITE 
GO

use deviceManagement;

select * from [dbo].[user];

insert into [dbo].[user] values ('admin','Admin123','admin@admin.com',1,'Cluj-Napoca',null);
insert into [dbo].[user] values ('testt','Testing123','testing@gmail.com',0,'Cluj-Napoca',null);
insert into [dbo].[user] values ('jozsaakos','Testing123','jozsaakos@gmail.com',0,'Cluj-Napoca',null);
insert into [dbo].[user] values ('newUser','Testing123','user@gmail.com',0,'Bucuresti',null);

select * from [dbo].[device];

insert into [dbo].[device] values ('Apple iPhone 7','Apple','Phone','iOS','10','Apple A10 Fusion','PowerVR Series 7XT Plus', null);
insert into [dbo].[device] values ('Acer Aspire 5','Acer','Laptop','Windows','Windows 10','AMD Quad Core A12','4 GB', null);
insert into [dbo].[device] values ('Apple iPhone 6','Apple','Phone','iOS','10','Apple A10 Fusion','PowerVR Series7XT Plus', null);
insert into [dbo].[device] values ('Apple iPad','Apple','Tablet','iOS','iOS v10.x','Apple A9','2 GB', null);
insert into [dbo].[device] values ('ASUS FX502VM','Asus','Laptop','Windows','Windows 10','Intel Core i7 7700HQ','8 GB', null);
insert into [dbo].[device] values ('ASUS ROG GL553VE','Asus','Laptop','Windows','Windows 10','Intel Core i7 7700HQ','8 GB', null);
insert into [dbo].[device] values ('ASUS ZenPad','Asus','Tablet','Android','Android v6.0','MediaTek MT8163','2 GB', null);
insert into [dbo].[device] values ('HP 250','HP','Laptop','Windows','Windows 10','Intel Celeron N3350','4 GB', null);
insert into [dbo].[device] values ('HP MT42','HP','Laptop','Windows','Windows 10','Bang Olufsen','4 GB', null);
insert into [dbo].[device] values ('HTC WINDOWS PHONE 8X','HTC','Phone','WP 80','WinPhone 8.0','Qualcomm MSM8960 Snapdragon','Adreno 225', null);
insert into [dbo].[device] values ('HTC ZETA','HTC','Phone','Ice Cream Sandwich','Android OS v4.x','Quad core','Adreno 412', null);
insert into [dbo].[device] values ('Huawei MediaPad','Huawei','Tablet','Android','Android OS v6.x','Cortex A7','1 GB', null);
insert into [dbo].[device] values ('Lenovo Legion','Lenovo','Laptop','Windows','Windows 10','Intel Core i5 7300HQ','4 GB', null);
insert into [dbo].[device] values ('Lenovo Legion','Lenovo','Laptop','Windows','Windows 10','Intel Core i5 7300HQ','8 GB', null);
insert into [dbo].[device] values ('Samsung Tab S2','Samsung','Tablet','Android','Android v6.0','Qualcomm MSM8976','3 GB', null);
