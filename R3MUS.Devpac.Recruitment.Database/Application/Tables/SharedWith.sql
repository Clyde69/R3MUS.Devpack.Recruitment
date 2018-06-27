CREATE TABLE [Application].[SharedWith] (
    [Id]            INT    IDENTITY (1, 1) NOT NULL,
    [CorporationId] BIGINT NOT NULL,
    [RecruitId]     INT    NOT NULL,
    [AllianceId]    BIGINT NULL,
    [Status]        INT    CONSTRAINT [DF_SharedWith_Status] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SharedWith] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SharedWith_Recruit] FOREIGN KEY ([RecruitId]) REFERENCES [Application].[Recruit] ([Id])
);





