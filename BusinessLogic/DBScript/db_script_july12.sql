USE [master]
GO
/****** Object:  Database [smoking_study]    Script Date: 7/12/2020 2:31:20 PM ******/
CREATE DATABASE [smoking_study]
GO
ALTER DATABASE [smoking_study] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [smoking_study].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [smoking_study] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [smoking_study] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [smoking_study] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [smoking_study] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [smoking_study] SET ARITHABORT OFF 
GO
ALTER DATABASE [smoking_study] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [smoking_study] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [smoking_study] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [smoking_study] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [smoking_study] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [smoking_study] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [smoking_study] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [smoking_study] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [smoking_study] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [smoking_study] SET  DISABLE_BROKER 
GO
ALTER DATABASE [smoking_study] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [smoking_study] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [smoking_study] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [smoking_study] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [smoking_study] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [smoking_study] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [smoking_study] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [smoking_study] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [smoking_study] SET  MULTI_USER 
GO
ALTER DATABASE [smoking_study] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [smoking_study] SET DB_CHAINING OFF 
GO
ALTER DATABASE [smoking_study] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [smoking_study] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [smoking_study]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 7/12/2020 2:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/12/2020 2:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/12/2020 2:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/12/2020 2:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/12/2020 2:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/12/2020 2:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 7/12/2020 2:31:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Participants]    Script Date: 7/12/2020 2:31:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Participants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudyID] [varchar](50) NULL,
	[Gender] [int] NOT NULL,
	[EnrollmentDate] [datetime2](7) NULL,
	[FacilityFrom] [varchar](50) NULL,
	[MobilePhone] [varchar](50) NULL,
	[Deleted] [int] NULL,
 CONSTRAINT [PK_Participants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Visits]    Script Date: 7/12/2020 2:31:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisitDate] [datetime] NOT NULL,
	[ParticipantId] [int] NOT NULL,
	[VisitSettingId] [int] NOT NULL,
	[NextAppointment] [datetime] NULL,
	[ParticipantCame] [int] NOT NULL,
	[DateParticipantCame] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[Deleted] [int] NOT NULL,
 CONSTRAINT [PK_Visits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VisitSettings]    Script Date: 7/12/2020 2:31:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WindowPeriod] [int] NULL,
	[VisitStage] [int] NULL,
	[VisitType] [nvarchar](50) NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_VisitSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Participants] ADD  CONSTRAINT [DF_Participants_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Visits] ADD  CONSTRAINT [DF_Visits_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Visits]  WITH CHECK ADD  CONSTRAINT [FK_Visits_Participants] FOREIGN KEY([ParticipantId])
REFERENCES [dbo].[Participants] ([Id])
GO
ALTER TABLE [dbo].[Visits] CHECK CONSTRAINT [FK_Visits_Participants]
GO
ALTER TABLE [dbo].[Visits]  WITH CHECK ADD  CONSTRAINT [FK_Visits_VisitSettings] FOREIGN KEY([VisitSettingId])
REFERENCES [dbo].[VisitSettings] ([Id])
GO
ALTER TABLE [dbo].[Visits] CHECK CONSTRAINT [FK_Visits_VisitSettings]
GO
USE [master]
GO
ALTER DATABASE [smoking_study] SET  READ_WRITE 
GO
