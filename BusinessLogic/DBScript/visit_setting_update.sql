USE [smoking_study]
GO
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
