USE [master]
GO
/****** Object:  Database [Projekti]    Script Date: 27.11.2020 12.46.10 ******/
CREATE DATABASE [Projekti]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Projekti', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Projekti.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Projekti_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Projekti_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Projekti] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Projekti].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Projekti] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Projekti] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Projekti] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Projekti] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Projekti] SET ARITHABORT OFF 
GO
ALTER DATABASE [Projekti] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Projekti] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Projekti] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Projekti] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Projekti] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Projekti] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Projekti] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Projekti] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Projekti] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Projekti] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Projekti] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Projekti] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Projekti] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Projekti] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Projekti] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Projekti] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Projekti] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Projekti] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Projekti] SET  MULTI_USER 
GO
ALTER DATABASE [Projekti] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Projekti] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Projekti] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Projekti] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Projekti] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Projekti] SET QUERY_STORE = OFF
GO
USE [Projekti]
GO
/****** Object:  Table [dbo].[Tehtavat]    Script Date: 27.11.2020 12.46.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tehtavat](
	[TehtavaID] [int] IDENTITY(1,1) NOT NULL,
	[Tehtava] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tehtavat] PRIMARY KEY CLUSTERED 
(
	[TehtavaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kohteet]    Script Date: 27.11.2020 12.46.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kohteet](
	[KohdeID] [int] IDENTITY(1,1) NOT NULL,
	[Kohde] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Kohde] PRIMARY KEY CLUSTERED 
(
	[KohdeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tyontekija]    Script Date: 27.11.2020 12.46.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tyontekija](
	[TyontekijaID] [int] NOT NULL,
	[Etunimi] [nvarchar](30) NOT NULL,
	[Sukunimi] [nvarchar](30) NOT NULL,
	[Käyttäjätunnus] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tyontekija] PRIMARY KEY CLUSTERED 
(
	[TyontekijaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kirjaus]    Script Date: 27.11.2020 12.46.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kirjaus](
	[KirjausID] [int] IDENTITY(1,1) NOT NULL,
	[TyontekijaID] [int] NULL,
	[TehtavaID] [int] NOT NULL,
	[KohdeID] [int] NOT NULL,
	[Tunnit] [int] NOT NULL,
	[Pvm] [date] NOT NULL,
	[Vapaateksti] [nvarchar](50) NULL,
 CONSTRAINT [PK_Kirjaus] PRIMARY KEY CLUSTERED 
(
	[KirjausID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[omatKirjaukset]    Script Date: 27.11.2020 12.46.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[omatKirjaukset]
AS
SELECT dbo.Tyontekija.Etunimi, dbo.Tyontekija.Sukunimi, dbo.Kirjaus.Pvm, dbo.Tehtavat.Tehtava, dbo.Kohteet.Kohde, dbo.Kirjaus.Tunnit, dbo.Kirjaus.Vapaateksti
FROM   dbo.Kirjaus INNER JOIN
             dbo.Tyontekija ON dbo.Kirjaus.TyontekijaID = dbo.Tyontekija.TyontekijaID INNER JOIN
             dbo.Kohteet ON dbo.Kirjaus.KohdeID = dbo.Kohteet.KohdeID INNER JOIN
             dbo.Tehtavat ON dbo.Kirjaus.TehtavaID = dbo.Tehtavat.TehtavaID
WHERE (dbo.Tyontekija.Käyttäjätunnus = SUSER_NAME())
GO
/****** Object:  Table [dbo].[RELAATIOT]    Script Date: 27.11.2020 12.46.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RELAATIOT](
	[Tyontekija* / TyontekijaID -> Kirjaus* / TyontekijaID] [nchar](10) NULL,
	[Tehtava* / TehtavaID -> Kirjaus* / TehtavaID] [nchar](10) NULL,
	[Kohde* / KohdeID -> Kirjaus* / KohdeID] [nchar](10) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Kirjaus]  WITH CHECK ADD  CONSTRAINT [FK_Kirjaus_Kohde] FOREIGN KEY([KohdeID])
REFERENCES [dbo].[Kohteet] ([KohdeID])
GO
ALTER TABLE [dbo].[Kirjaus] CHECK CONSTRAINT [FK_Kirjaus_Kohde]
GO
ALTER TABLE [dbo].[Kirjaus]  WITH CHECK ADD  CONSTRAINT [FK_Kirjaus_Tehtavat] FOREIGN KEY([TehtavaID])
REFERENCES [dbo].[Tehtavat] ([TehtavaID])
GO
ALTER TABLE [dbo].[Kirjaus] CHECK CONSTRAINT [FK_Kirjaus_Tehtavat]
GO
ALTER TABLE [dbo].[Kirjaus]  WITH CHECK ADD  CONSTRAINT [FK_Kirjaus_Tyontekija] FOREIGN KEY([TyontekijaID])
REFERENCES [dbo].[Tyontekija] ([TyontekijaID])
GO
ALTER TABLE [dbo].[Kirjaus] CHECK CONSTRAINT [FK_Kirjaus_Tyontekija]
GO
/****** Object:  StoredProcedure [dbo].[haeTapahtumat]    Script Date: 27.11.2020 12.46.11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[haeTapahtumat] @KirjausID INT

AS
BEGIN

SET NOCOUNT ON;
DECLARE @alkoipvm date;
DECLARE @loppuipvm date;
SELECT *
FROM dbo.Kirjaus
WHERE KirjausID = SUSER_NAME() AND Pvm BETWEEN @alkoipvm AND @loppuipvm
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[17] 4[41] 2[23] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Kirjaus"
            Begin Extent = 
               Top = 5
               Left = 352
               Bottom = 290
               Right = 574
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tyontekija"
            Begin Extent = 
               Top = 35
               Left = 53
               Bottom = 232
               Right = 275
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Kohteet"
            Begin Extent = 
               Top = 151
               Left = 683
               Bottom = 294
               Right = 905
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tehtavat"
            Begin Extent = 
               Top = 0
               Left = 685
               Bottom = 151
               Right = 907
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1510
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1770
         Or = 1350
         Or = 1350
         Or = 1350
      End' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'omatKirjaukset'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'omatKirjaukset'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'omatKirjaukset'
GO
USE [master]
GO
ALTER DATABASE [Projekti] SET  READ_WRITE 
GO

