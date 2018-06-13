USE [Recruitment]
GO
SET IDENTITY_INSERT [Application].[Recruit] ON 
GO
INSERT [Application].[Recruit] ([Id], [CharacterId]) VALUES (4, 93902200)
GO
INSERT [Application].[Recruit] ([Id], [CharacterId]) VALUES (5, 94277907)
GO
INSERT [Application].[Recruit] ([Id], [CharacterId]) VALUES (6, 2113330636)
GO
SET IDENTITY_INSERT [Application].[Recruit] OFF
GO
SET IDENTITY_INSERT [Application].[TokenData] ON 
GO
INSERT [Application].[TokenData] ([Id], [RecruitId], [RefreshToken]) VALUES (1, 4, N'l3rBvuUPYfxOQgsOD6rzTRTD-JFssmPSjZmk6NfwwnA5zzbKqyKpvx3pdN9UjIitDoOoaHcsHx-M25-6_1xb5w2')
GO
INSERT [Application].[TokenData] ([Id], [RecruitId], [RefreshToken]) VALUES (2, 5, N'ld_5rPKvZA4XfHsM6WiYg4dGrl_9UGb1bH8Va63ZMBfQMToCkJlnzyjl8LmdQTGdrmkd78K0pqV0EJgT91IDFA2')
GO
INSERT [Application].[TokenData] ([Id], [RecruitId], [RefreshToken]) VALUES (3, 6, N'aIIa40AxJG7GuYk-TqMoQBiCrIbXLV_aR7UzZCkDwJHK9pYaVQxP-f9u2kOXaVObNaJWKWi1jKHF8LA5zrm7AA2')
GO
SET IDENTITY_INSERT [Application].[TokenData] OFF
GO
SET IDENTITY_INSERT [Security].[CorporationMember] ON 
GO
INSERT [Security].[CorporationMember] ([Id], [CorporationId], [Name], [Ticker]) VALUES (1, 98518643, N'White Fang Militia', N'F3R4L')
GO
SET IDENTITY_INSERT [Security].[CorporationMember] OFF
GO
SET IDENTITY_INSERT [Content].[View] ON 
GO
INSERT [Content].[View] ([Id], [Controller], [View]) VALUES (1, N'Home', N'Index')
GO
SET IDENTITY_INSERT [Content].[View] OFF
GO
SET IDENTITY_INSERT [Content].[ViewArea] ON 
GO
INSERT [Content].[ViewArea] ([Id], [ViewId], [Name], [Content], [ClientCorporationTicker]) VALUES (1, 1, N'area-a', N'<h2>Test</h2>', N'0')
GO
SET IDENTITY_INSERT [Content].[ViewArea] OFF
GO
INSERT [Security].[ClientCorporation] ([Id], [CorporationId], [Name], [Ticker]) VALUES (N'0', 0, N'Default', N'Default')
GO
SET IDENTITY_INSERT [Security].[ESIEndpoint] ON 
GO
INSERT [Security].[ESIEndpoint] ([Id], [ClientId], [SecretKey], [CallbackUrl], [Name]) VALUES (1, N'885fd0f759684699be3e7e4a4df5b985', N'K4XDndpGfX9knOZMeMvX9fFbGDtDxN6E4ThXkN99', N'http://localhost:34503/Applicant/SSORedirect', N'Applicant')
GO
INSERT [Security].[ESIEndpoint] ([Id], [ClientId], [SecretKey], [CallbackUrl], [Name]) VALUES (3, N'dc8f3482626340498d0c8eee5081100c', N'peaDNfIzp1XKbz0dxPGgoXexARum669OXSKbpohF', N'http://localhost:34503/Screener/SSORedirect', N'Screener')
GO
SET IDENTITY_INSERT [Security].[ESIEndpoint] OFF
GO
