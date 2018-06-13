CREATE TABLE [Application].[History] (
    [Id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [RecruitId]  INT           NOT NULL,
    [Status]     SMALLINT      NOT NULL,
    [ActionDate] DATETIME2 (7) CONSTRAINT [DF_History_ActionDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_History_Recruit] FOREIGN KEY ([RecruitId]) REFERENCES [Application].[Recruit] ([Id])
);

