CREATE TABLE [Application].[TokenData] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [RecruitId]    INT            NOT NULL,
    [RefreshToken] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_TokenData] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TokenData_Recruit] FOREIGN KEY ([RecruitId]) REFERENCES [Application].[Recruit] ([Id])
);

