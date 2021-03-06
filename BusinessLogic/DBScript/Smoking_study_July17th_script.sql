USE [master]
GO
/****** Object:  Database [smoking_study]    Script Date: 7/17/2020 8:05:19 PM ******/
CREATE DATABASE [smoking_study]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'smoking_study', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\smoking_study.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'smoking_study_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\smoking_study_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [smoking_study] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [smoking_study] SET QUERY_STORE = OFF
GO
USE [smoking_study]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 7/17/2020 8:05:20 PM ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/17/2020 8:05:20 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/17/2020 8:05:20 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/17/2020 8:05:20 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/17/2020 8:05:20 PM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/17/2020 8:05:20 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 7/17/2020 8:05:20 PM ******/
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
/****** Object:  Table [dbo].[facilities]    Script Date: 7/17/2020 8:05:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[facilities](
	[Id] [int] NOT NULL,
	[mflcode] [varchar](50) NULL,
	[name] [varchar](50) NULL,
 CONSTRAINT [PK_facilities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Participants]    Script Date: 7/17/2020 8:05:20 PM ******/
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
	[AlternateMobilePhone] [varchar](50) NULL,
	[AlternateContactName] [varchar](50) NULL,
	[Deleted] [int] NULL,
 CONSTRAINT [PK_Participants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Visits]    Script Date: 7/17/2020 8:05:20 PM ******/
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
/****** Object:  Table [dbo].[VisitSettings]    Script Date: 7/17/2020 8:05:20 PM ******/
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
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a98cb536-5ebe-4a4f-a300-fca118a89237', N'admin', N'ADMIN', N'e9d85882-8968-487c-80a1-f7a8d1e14d27')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c0dad53a-7749-4ac6-ad3c-2de881d47df9', N'Super Admin', N'SUPER ADMIN', N'27e73db7-3fc4-4429-a169-26f54ce0c6d5')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1c80ca82-edc6-4868-95d7-78bcb18ec749', N'c0dad53a-7749-4ac6-ad3c-2de881d47df9')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1c80ca82-edc6-4868-95d7-78bcb18ec749', N'admin@admin.com', N'ADMIN@ADMIN.COM', N'admin@admin.com', N'ADMIN@ADMIN.COM', 1, N'AQAAAAEAACcQAAAAENdWHC6IvTdrTPLH6DN6H+r6KNTr4GPp2lL+mgwPXFHEBC7wxWi0tqTV3XpnuXAQpw==', N'ETGQ6IZ4LOUPXDGCYKAGC47CPFV2BQPN', N'84ba1572-e4ff-4149-9780-8d0902f4a3e2', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'600fb973-add4-4723-afde-1a7da5dec5df', N'user_tester@umb.com', N'USER_TESTER@UMB.COM', N'user_tester@umb.com', N'USER_TESTER@UMB.COM', 0, N'AQAAAAEAACcQAAAAECH96meOXPVRWSylS/csPapAlwe4AkdTW+A+tba8qebjm51wbI5P6myD0Clffhvjqg==', N'PO4NZ5HETEGRZI5CX2XEBJJYEPM6HOCW', N'b892d091-864a-4656-a548-d97552cdcc1d', NULL, 0, 0, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Participants] ON 

INSERT [dbo].[Participants] ([Id], [StudyID], [Gender], [EnrollmentDate], [FacilityFrom], [MobilePhone], [AlternateMobilePhone], [AlternateContactName], [Deleted]) VALUES (1, N'ST100', 1, CAST(N'2020-06-01T00:00:00.0000000' AS DateTime2), N'Mugembe', NULL, N'0734998787', N'Vincent', 1)
INSERT [dbo].[Participants] ([Id], [StudyID], [Gender], [EnrollmentDate], [FacilityFrom], [MobilePhone], [AlternateMobilePhone], [AlternateContactName], [Deleted]) VALUES (2, N'SC200', 2, CAST(N'2020-07-14T00:00:00.0000000' AS DateTime2), N'Mathari', N'0700112233', N'0711998877', N'Gifton', NULL)
INSERT [dbo].[Participants] ([Id], [StudyID], [Gender], [EnrollmentDate], [FacilityFrom], [MobilePhone], [AlternateMobilePhone], [AlternateContactName], [Deleted]) VALUES (4, N'SC205', 1, CAST(N'2020-07-17T00:00:00.0000000' AS DateTime2), N'Luanda', N'0724896567', N'0724226567', N'Mbaditch', NULL)
SET IDENTITY_INSERT [dbo].[Participants] OFF
SET IDENTITY_INSERT [dbo].[Visits] ON 

INSERT [dbo].[Visits] ([Id], [VisitDate], [ParticipantId], [VisitSettingId], [NextAppointment], [ParticipantCame], [DateParticipantCame], [CreateDate], [Deleted]) VALUES (1, CAST(N'2020-07-14T00:00:00.000' AS DateTime), 2, 1, CAST(N'2020-07-24T00:00:00.000' AS DateTime), 1, CAST(N'2020-07-15T00:00:00.000' AS DateTime), CAST(N'2020-07-14T22:32:26.367' AS DateTime), 1)
INSERT [dbo].[Visits] ([Id], [VisitDate], [ParticipantId], [VisitSettingId], [NextAppointment], [ParticipantCame], [DateParticipantCame], [CreateDate], [Deleted]) VALUES (2, CAST(N'2020-07-17T00:00:00.000' AS DateTime), 2, 2, CAST(N'2020-07-24T00:00:00.000' AS DateTime), 1, CAST(N'2020-07-17T00:00:00.000' AS DateTime), CAST(N'2020-07-17T10:52:38.747' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Visits] OFF
SET IDENTITY_INSERT [dbo].[VisitSettings] ON 

INSERT [dbo].[VisitSettings] ([Id], [WindowPeriod], [VisitStage], [VisitType], [Comments]) VALUES (1, 0, 1, N'Screening', N'Day consented and enrolled
')
INSERT [dbo].[VisitSettings] ([Id], [WindowPeriod], [VisitStage], [VisitType], [Comments]) VALUES (2, 2, 2, N'Evaluation', N'2 days after screening visit, Day 1 of study medication')
INSERT [dbo].[VisitSettings] ([Id], [WindowPeriod], [VisitStage], [VisitType], [Comments]) VALUES (3, 2, 3, N'Baseline', N'2 days after evaluation visit')
INSERT [dbo].[VisitSettings] ([Id], [WindowPeriod], [VisitStage], [VisitType], [Comments]) VALUES (4, 3, 4, N'Week 1', N'1 week after evaluation visit')
INSERT [dbo].[VisitSettings] ([Id], [WindowPeriod], [VisitStage], [VisitType], [Comments]) VALUES (5, 3, 5, N'Week 2', N'2 weeks after evaluation visit')
INSERT [dbo].[VisitSettings] ([Id], [WindowPeriod], [VisitStage], [VisitType], [Comments]) VALUES (6, 3, 6, N'Week 4', N'4 weeks after evaluation visit')
INSERT [dbo].[VisitSettings] ([Id], [WindowPeriod], [VisitStage], [VisitType], [Comments]) VALUES (7, 3, 7, N'Week 8', N'8 weeks after evaluation visit')
INSERT [dbo].[VisitSettings] ([Id], [WindowPeriod], [VisitStage], [VisitType], [Comments]) VALUES (8, 7, 8, N'Week 12', N'12 weeks after evaluation visit')
INSERT [dbo].[VisitSettings] ([Id], [WindowPeriod], [VisitStage], [VisitType], [Comments]) VALUES (9, 7, 9, N'Week 36', N'36 weeks after evaluation visit')
SET IDENTITY_INSERT [dbo].[VisitSettings] OFF
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
