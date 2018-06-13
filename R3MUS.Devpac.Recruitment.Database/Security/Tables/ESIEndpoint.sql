CREATE TABLE [Security].[ESIEndpoint] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [ClientId]    VARCHAR (50)  NOT NULL,
    [SecretKey]   VARCHAR (50)  NOT NULL,
    [CallbackUrl] VARCHAR (100) NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_ESIEndpoint] PRIMARY KEY CLUSTERED ([Id] ASC)
);

