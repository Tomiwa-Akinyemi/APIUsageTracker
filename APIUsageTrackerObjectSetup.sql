USE [master]
GO
/****** Object:  Database [APIUsageTracker]    Script Date: 10/16/2021 12:23:45 PM ******/
CREATE DATABASE [APIUsageTracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'APIUsageTracker', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\APIUsageTracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'APIUsageTracker_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\APIUsageTracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [APIUsageTracker] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [APIUsageTracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [APIUsageTracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [APIUsageTracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [APIUsageTracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [APIUsageTracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [APIUsageTracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [APIUsageTracker] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [APIUsageTracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [APIUsageTracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [APIUsageTracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [APIUsageTracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [APIUsageTracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [APIUsageTracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [APIUsageTracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [APIUsageTracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [APIUsageTracker] SET  DISABLE_BROKER 
GO
ALTER DATABASE [APIUsageTracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [APIUsageTracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [APIUsageTracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [APIUsageTracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [APIUsageTracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [APIUsageTracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [APIUsageTracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [APIUsageTracker] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [APIUsageTracker] SET  MULTI_USER 
GO
ALTER DATABASE [APIUsageTracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [APIUsageTracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [APIUsageTracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [APIUsageTracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [APIUsageTracker] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [APIUsageTracker] SET QUERY_STORE = OFF
GO
USE [APIUsageTracker]
GO
/****** Object:  Table [dbo].[APIUsageLogs]    Script Date: 10/16/2021 12:23:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APIUsageLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[Token] [nvarchar](100) NULL,
	[IPAddress] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[LogAPICall]    Script Date: 10/16/2021 12:23:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LogAPICall]
	-- Add the parameters for the stored procedure here
	@Token nvarchar(100),
	@IPAddress nvarchar(50),
	@CreatedAt datetime

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	insert into APIUsageLogs 
	(
	CreatedAt,
	Token,
	IPAddress	
	) 
	values
	(
	@CreatedAt,
	@Token,
	@IPAddress
	)

	select 1
    -- Insert statements for procedure here
	
END
GO
USE [master]
GO
ALTER DATABASE [APIUsageTracker] SET  READ_WRITE 
GO
