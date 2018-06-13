CREATE TABLE [Security].[Screener] (
    [Id]                  INT    IDENTITY (1, 1) NOT NULL,
    [CharacterId]         BIGINT NOT NULL,
    [CorporationMemberId] INT    NOT NULL,
    CONSTRAINT [PK_Screener] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Screener_CorporationMember] FOREIGN KEY ([CorporationMemberId]) REFERENCES [Security].[CorporationMember] ([Id])
);

