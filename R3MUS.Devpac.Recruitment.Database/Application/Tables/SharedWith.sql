CREATE TABLE [Application].[SharedWith] (
    [Id]            INT    IDENTITY (1, 1) NOT NULL,
    [CorporationId] BIGINT NOT NULL,
    [RecruitId]     INT    NOT NULL,
    [AllianceId]    BIGINT NULL,
    CONSTRAINT [PK_SharedWith] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SharedWith_Recruit] FOREIGN KEY ([RecruitId]) REFERENCES [Application].[Recruit] ([Id])
);



