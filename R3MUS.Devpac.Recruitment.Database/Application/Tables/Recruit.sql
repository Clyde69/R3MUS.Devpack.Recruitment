CREATE TABLE [Application].[Recruit] (
    [Id]          INT    IDENTITY (1, 1) NOT NULL,
    [CharacterId] BIGINT NOT NULL,
    CONSTRAINT [PK_Recruit] PRIMARY KEY CLUSTERED ([Id] ASC)
);

