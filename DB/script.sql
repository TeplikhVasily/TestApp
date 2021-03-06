/*    ==Параметры сценариев==

    Версия исходного сервера : SQL Server 2008 (10.0.2531)
    Выпуск исходного ядра СУБД : Выпуск Microsoft SQL Server Express Edition
    Тип исходного ядра СУБД : Изолированный SQL Server

    Версия целевого сервера : SQL Server 2017
    Выпуск целевого ядра СУБД : Выпуск Microsoft SQL Server Standard Edition
    Тип целевого ядра СУБД : Изолированный SQL Server
*/
USE [master]
GO
/****** Object:  Database [workersdb]    Script Date: 12.09.2017 22:57:27 ******/
CREATE DATABASE [workersdb] ON  PRIMARY 
( NAME = N'workersdb', FILENAME = N'D:\workersdb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'workersdb_log', FILENAME = N'D:\workersdb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [workersdb] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [workersdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [workersdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [workersdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [workersdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [workersdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [workersdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [workersdb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [workersdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [workersdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [workersdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [workersdb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [workersdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [workersdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [workersdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [workersdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [workersdb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [workersdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [workersdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [workersdb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [workersdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [workersdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [workersdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [workersdb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [workersdb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [workersdb] SET  MULTI_USER 
GO
ALTER DATABASE [workersdb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [workersdb] SET DB_CHAINING OFF 
GO
USE [workersdb]
GO
/****** Object:  Table [dbo].[Workers]    Script Date: 12.09.2017 22:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workers](
	[WorkerID] [int] IDENTITY(1,1) NOT NULL,
	[WorkerName] [nvarchar](50) NULL,
	[WorkerLastName] [nvarchar](50) NULL,
	[PayType] [nvarchar](5) NULL,
	[Sum] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Workers] PRIMARY KEY CLUSTERED 
(
	[WorkerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddWorker]    Script Date: 12.09.2017 22:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddWorker]
	-- Add the parameters for the stored procedure here
	    @name NVARCHAR(50),
		@lastname NVARCHAR(50),
		@paytype NVARCHAR(5),
		@sum DECIMAL(10,2)
AS
BEGIN
	INSERT INTO Workers(WorkerName, WorkerLastName, PayType, Sum) 
	VALUES(@name, @lastname, @paytype, @sum)
END
GO
/****** Object:  StoredProcedure [dbo].[FindeWorkers]    Script Date: 12.09.2017 22:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[FindeWorkers]
	  @name NVARCHAR(50),
	  @lastname NVARCHAR(50)

AS
BEGIN
    -- Insert statements for procedure here
	SELECT * FROM Workers WHERE WorkerName = (@name) AND WorkerLastName = (@lastname)
	RETURN
END
GO
USE [master]
GO
ALTER DATABASE [workersdb] SET  READ_WRITE 
GO
