CREATE TABLE [Security].[ClientCorporation] (
    [Id]            NVARCHAR (10)  NOT NULL,
    [CorporationId] BIGINT         NOT NULL,
    [Name]          NVARCHAR (500) NOT NULL,
    [Ticker]        NVARCHAR (10)  NOT NULL,
    CONSTRAINT [PK_ClientCorporation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

