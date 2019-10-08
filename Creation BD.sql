USE [master]
GO
/****** Object:  Database [CMIM]    Script Date: 08/10/2019 22:30:26 ******/
CREATE DATABASE [CMIM]
Go
ALTER DATABASE [CMIM] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CMIM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CMIM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CMIM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CMIM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CMIM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CMIM] SET ARITHABORT OFF 
GO
ALTER DATABASE [CMIM] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CMIM] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [CMIM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CMIM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CMIM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CMIM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CMIM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CMIM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CMIM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CMIM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CMIM] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CMIM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CMIM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CMIM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CMIM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CMIM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CMIM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CMIM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CMIM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CMIM] SET  MULTI_USER 
GO
ALTER DATABASE [CMIM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CMIM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CMIM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CMIM] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [CMIM]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ActivityPlace]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityPlace](
	[placdeId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
 CONSTRAINT [PK_ActivityPlace] PRIMARY KEY CLUSTERED 
(
	[placdeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bordereau]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bordereau](
	[BordereauId] [bigint] IDENTITY(1,1) NOT NULL,
	[Company] [nvarchar](max) NULL,
	[DateCreation] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Bordereau] PRIMARY KEY CLUSTERED 
(
	[BordereauId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bu]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bu](
	[BuId] [nvarchar](450) NOT NULL,
	[name] [nvarchar](max) NULL,
 CONSTRAINT [PK_bu] PRIMARY KEY CLUSTERED 
(
	[BuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Conjoint]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conjoint](
	[ConjointId] [bigint] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](max) NULL,
	[Lastname] [nvarchar](max) NULL,
	[Employeematricule] [nvarchar](450) NULL,
	[matriculecmim] [nvarchar](max) NULL,
	[DateNs] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Conjoint] PRIMARY KEY CLUSTERED 
(
	[ConjointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dossiers]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dossiers](
	[referance] [nvarchar](450) NOT NULL,
	[employeematricule] [nvarchar](450) NULL,
	[ConjointId] [bigint] NULL,
	[date] [datetime2](7) NOT NULL,
	[dateDossier] [datetime2](7) NOT NULL,
	[dateLunette] [datetime2](7) NULL,
	[montant] [float] NOT NULL,
	[Mt_radio] [float] NOT NULL,
	[Mt_analyse] [float] NOT NULL,
	[MtSoins_D] [float] NOT NULL,
	[MtProthese_D] [float] NOT NULL,
	[Devis_D] [float] NOT NULL,
	[MtVisite_M] [float] NOT NULL,
	[MtPharmacie_M] [float] NOT NULL,
	[MtVisite_L] [float] NOT NULL,
	[MtCadre_L] [float] NOT NULL,
	[MtGi_L] [float] NOT NULL,
	[avance] [float] NOT NULL,
	[rembourse] [float] NOT NULL,
	[diff] [float] NOT NULL,
	[rembourssementId] [bigint] NULL,
	[BordereauId] [bigint] NULL,
	[etat] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[EnfantId] [bigint] NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_Dossiers] PRIMARY KEY CLUSTERED 
(
	[referance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[employees]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employees](
	[matricule] [nvarchar](450) NOT NULL,
	[first_name] [nvarchar](max) NULL,
	[last_name] [nvarchar](max) NULL,
	[adresse] [nvarchar](max) NULL,
	[matriculecmim] [nvarchar](max) NULL,
	[PlaceplacdeId] [bigint] NOT NULL,
	[buId] [nvarchar](450) NULL,
	[company] [nvarchar](max) NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_employees] PRIMARY KEY CLUSTERED 
(
	[matricule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Enfant]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enfant](
	[EnfantId] [bigint] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](max) NULL,
	[Lastname] [nvarchar](max) NULL,
	[DateNs] [datetime2](7) NOT NULL,
	[DateVs] [datetime2](7) NOT NULL,
	[ConjointId] [bigint] NOT NULL,
	[employeematricule] [nvarchar](450) NULL,
	[MatriculeCmim] [nvarchar](max) NULL,
 CONSTRAINT [PK_Enfant] PRIMARY KEY CLUSTERED 
(
	[EnfantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[list]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[list](
	[listId] [bigint] IDENTITY(1,1) NOT NULL,
	[RembouserembourssementId] [bigint] NOT NULL,
	[Dossierreferance] [nvarchar](450) NULL,
	[montant] [float] NOT NULL,
 CONSTRAINT [PK_list] PRIMARY KEY CLUSTERED 
(
	[listId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Regul_Emp]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regul_Emp](
	[regul_empID] [bigint] IDENTITY(1,1) NOT NULL,
	[RégularisationRegularisationID] [bigint] NULL,
	[EmployeeMatricule] [nvarchar](450) NULL,
	[Total] [float] NOT NULL,
 CONSTRAINT [PK_Regul_Emp] PRIMARY KEY CLUSTERED 
(
	[regul_empID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Regularisation]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regularisation](
	[RegularisationID] [bigint] IDENTITY(1,1) NOT NULL,
	[dateRegularisation] [datetime2](7) NOT NULL,
	[Mois] [nvarchar](max) NULL,
	[Total] [float] NOT NULL,
 CONSTRAINT [PK_Regularisation] PRIMARY KEY CLUSTERED 
(
	[RegularisationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Remboursser]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Remboursser](
	[rembourssementId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateRembourssement] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Remboursser] PRIMARY KEY CLUSTERED 
(
	[rembourssementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sites]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sites](
	[SiteId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
 CONSTRAINT [PK_sites] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 08/10/2019 22:30:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Etat] [bit] NOT NULL,
	[Token] [nvarchar](max) NULL,
	[siteId] [bigint] NOT NULL,
	[Role] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190724111021_ldldld', N'2.1.11-servicing-32099')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190724134442_ld', N'2.1.11-servicing-32099')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190806105550_myFirstMigration', N'2.1.11-servicing-32099')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190815104329_migrate_user_employee', N'2.1.11-servicing-32099')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190820212537_delete_site_from_emp_migration', N'2.1.11-servicing-32099')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190820212705_delete_type_user_migration', N'2.1.11-servicing-32099')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190820213340_add_User_To_Dossier_migration', N'2.1.11-servicing-32099')
GO
SET IDENTITY_INSERT [dbo].[ActivityPlace] ON 

GO
INSERT [dbo].[ActivityPlace] ([placdeId], [name]) VALUES (1, N'Avanade')
GO
SET IDENTITY_INSERT [dbo].[ActivityPlace] OFF
GO
INSERT [dbo].[bu] ([BuId], [name]) VALUES (N'BU1', N'BU 1')
GO
INSERT [dbo].[bu] ([BuId], [name]) VALUES (N'BU2', N'BU 2')
GO
SET IDENTITY_INSERT [dbo].[sites] ON 

GO
INSERT [dbo].[sites] ([SiteId], [name]) VALUES (1, N'Casablanca')
GO
INSERT [dbo].[sites] ([SiteId], [name]) VALUES (2, N'CEC Mohammedia')
GO
SET IDENTITY_INSERT [dbo].[sites] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [Email], [Password], [Etat], [Token], [siteId], [Role]) VALUES (1, N'Administrateur', N'Administrateur', N'admin', N'Ya.maghfour@gmail.com', N'admin', 1, NULL, 2, N'Admin')
GO
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [Email], [Password], [Etat], [Token], [siteId], [Role]) VALUES (11, N'Yassinos', N'MAGHFOUR', N'Ya.mag4', N'Ya.mag4@gmail.com', N'10012016', 1, NULL, 1, N'User')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Conjoint_Employeematricule]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Conjoint_Employeematricule] ON [dbo].[Conjoint]
(
	[Employeematricule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Dossiers_BordereauId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Dossiers_BordereauId] ON [dbo].[Dossiers]
(
	[BordereauId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Dossiers_ConjointId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Dossiers_ConjointId] ON [dbo].[Dossiers]
(
	[ConjointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Dossiers_employeematricule]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Dossiers_employeematricule] ON [dbo].[Dossiers]
(
	[employeematricule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Dossiers_EnfantId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Dossiers_EnfantId] ON [dbo].[Dossiers]
(
	[EnfantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Dossiers_rembourssementId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Dossiers_rembourssementId] ON [dbo].[Dossiers]
(
	[rembourssementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Dossiers_UserId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Dossiers_UserId] ON [dbo].[Dossiers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_employees_buId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_employees_buId] ON [dbo].[employees]
(
	[buId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_employees_PlaceplacdeId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_employees_PlaceplacdeId] ON [dbo].[employees]
(
	[PlaceplacdeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_employees_UserId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_employees_UserId] ON [dbo].[employees]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enfant_ConjointId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Enfant_ConjointId] ON [dbo].[Enfant]
(
	[ConjointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Enfant_employeematricule]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Enfant_employeematricule] ON [dbo].[Enfant]
(
	[employeematricule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_list_Dossierreferance]    Script Date: 08/10/2019 22:30:27 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_list_Dossierreferance] ON [dbo].[list]
(
	[Dossierreferance] ASC
)
WHERE ([Dossierreferance] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_list_RembouserembourssementId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_list_RembouserembourssementId] ON [dbo].[list]
(
	[RembouserembourssementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Regul_Emp_EmployeeMatricule]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Regul_Emp_EmployeeMatricule] ON [dbo].[Regul_Emp]
(
	[EmployeeMatricule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Regul_Emp_RégularisationRegularisationID]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Regul_Emp_RégularisationRegularisationID] ON [dbo].[Regul_Emp]
(
	[RégularisationRegularisationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_siteId]    Script Date: 08/10/2019 22:30:27 ******/
CREATE NONCLUSTERED INDEX [IX_Users_siteId] ON [dbo].[Users]
(
	[siteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Dossiers] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UserId]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UserId]
GO
ALTER TABLE [dbo].[Conjoint]  WITH CHECK ADD  CONSTRAINT [FK_Conjoint_employees_Employeematricule] FOREIGN KEY([Employeematricule])
REFERENCES [dbo].[employees] ([matricule])
GO
ALTER TABLE [dbo].[Conjoint] CHECK CONSTRAINT [FK_Conjoint_employees_Employeematricule]
GO
ALTER TABLE [dbo].[Dossiers]  WITH CHECK ADD  CONSTRAINT [FK_Dossiers_Bordereau_BordereauId] FOREIGN KEY([BordereauId])
REFERENCES [dbo].[Bordereau] ([BordereauId])
GO
ALTER TABLE [dbo].[Dossiers] CHECK CONSTRAINT [FK_Dossiers_Bordereau_BordereauId]
GO
ALTER TABLE [dbo].[Dossiers]  WITH CHECK ADD  CONSTRAINT [FK_Dossiers_Conjoint_ConjointId] FOREIGN KEY([ConjointId])
REFERENCES [dbo].[Conjoint] ([ConjointId])
GO
ALTER TABLE [dbo].[Dossiers] CHECK CONSTRAINT [FK_Dossiers_Conjoint_ConjointId]
GO
ALTER TABLE [dbo].[Dossiers]  WITH CHECK ADD  CONSTRAINT [FK_Dossiers_employees_employeematricule] FOREIGN KEY([employeematricule])
REFERENCES [dbo].[employees] ([matricule])
GO
ALTER TABLE [dbo].[Dossiers] CHECK CONSTRAINT [FK_Dossiers_employees_employeematricule]
GO
ALTER TABLE [dbo].[Dossiers]  WITH CHECK ADD  CONSTRAINT [FK_Dossiers_Enfant_EnfantId] FOREIGN KEY([EnfantId])
REFERENCES [dbo].[Enfant] ([EnfantId])
GO
ALTER TABLE [dbo].[Dossiers] CHECK CONSTRAINT [FK_Dossiers_Enfant_EnfantId]
GO
ALTER TABLE [dbo].[Dossiers]  WITH CHECK ADD  CONSTRAINT [FK_Dossiers_Remboursser_rembourssementId] FOREIGN KEY([rembourssementId])
REFERENCES [dbo].[Remboursser] ([rembourssementId])
GO
ALTER TABLE [dbo].[Dossiers] CHECK CONSTRAINT [FK_Dossiers_Remboursser_rembourssementId]
GO
ALTER TABLE [dbo].[Dossiers]  WITH CHECK ADD  CONSTRAINT [FK_Dossiers_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Dossiers] CHECK CONSTRAINT [FK_Dossiers_Users_UserId]
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_ActivityPlace_PlaceplacdeId] FOREIGN KEY([PlaceplacdeId])
REFERENCES [dbo].[ActivityPlace] ([placdeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_ActivityPlace_PlaceplacdeId]
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_bu_buId] FOREIGN KEY([buId])
REFERENCES [dbo].[bu] ([BuId])
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_bu_buId]
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_Users_UserId]
GO
ALTER TABLE [dbo].[Enfant]  WITH CHECK ADD  CONSTRAINT [FK_Enfant_Conjoint_ConjointId] FOREIGN KEY([ConjointId])
REFERENCES [dbo].[Conjoint] ([ConjointId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Enfant] CHECK CONSTRAINT [FK_Enfant_Conjoint_ConjointId]
GO
ALTER TABLE [dbo].[Enfant]  WITH CHECK ADD  CONSTRAINT [FK_Enfant_employees_employeematricule] FOREIGN KEY([employeematricule])
REFERENCES [dbo].[employees] ([matricule])
GO
ALTER TABLE [dbo].[Enfant] CHECK CONSTRAINT [FK_Enfant_employees_employeematricule]
GO
ALTER TABLE [dbo].[list]  WITH CHECK ADD  CONSTRAINT [FK_list_Dossiers_Dossierreferance] FOREIGN KEY([Dossierreferance])
REFERENCES [dbo].[Dossiers] ([referance])
GO
ALTER TABLE [dbo].[list] CHECK CONSTRAINT [FK_list_Dossiers_Dossierreferance]
GO
ALTER TABLE [dbo].[list]  WITH CHECK ADD  CONSTRAINT [FK_list_Remboursser_RembouserembourssementId] FOREIGN KEY([RembouserembourssementId])
REFERENCES [dbo].[Remboursser] ([rembourssementId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[list] CHECK CONSTRAINT [FK_list_Remboursser_RembouserembourssementId]
GO
ALTER TABLE [dbo].[Regul_Emp]  WITH CHECK ADD  CONSTRAINT [FK_Regul_Emp_employees_EmployeeMatricule] FOREIGN KEY([EmployeeMatricule])
REFERENCES [dbo].[employees] ([matricule])
GO
ALTER TABLE [dbo].[Regul_Emp] CHECK CONSTRAINT [FK_Regul_Emp_employees_EmployeeMatricule]
GO
ALTER TABLE [dbo].[Regul_Emp]  WITH CHECK ADD  CONSTRAINT [FK_Regul_Emp_Regularisation_RégularisationRegularisationID] FOREIGN KEY([RégularisationRegularisationID])
REFERENCES [dbo].[Regularisation] ([RegularisationID])
GO
ALTER TABLE [dbo].[Regul_Emp] CHECK CONSTRAINT [FK_Regul_Emp_Regularisation_RégularisationRegularisationID]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_sites_siteId] FOREIGN KEY([siteId])
REFERENCES [dbo].[sites] ([SiteId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_sites_siteId]
GO
USE [master]
GO
ALTER DATABASE [CMIM] SET  READ_WRITE 
GO
