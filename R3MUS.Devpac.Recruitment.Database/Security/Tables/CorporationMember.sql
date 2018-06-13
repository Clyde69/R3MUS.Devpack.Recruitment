CREATE TABLE [Security].[CorporationMember] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [CorporationId] BIGINT         NOT NULL,
    [Name]          NVARCHAR (300) NOT NULL,
    [Ticker]        NVARCHAR (20)  NOT NULL,
    CONSTRAINT [PK_CorporationMember] PRIMARY KEY CLUSTERED ([Id] ASC)
);

