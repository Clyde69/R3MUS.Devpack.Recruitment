CREATE TABLE [Content].[ViewArea] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [ViewId]                  INT            NOT NULL,
    [Name]                    VARCHAR (50)   NOT NULL,
    [Content]                 NVARCHAR (MAX) NOT NULL,
    [ClientCorporationTicker] NVARCHAR (10)  CONSTRAINT [DF_ViewArea_ClientCorporationTicket] DEFAULT ('Default') NOT NULL,
    CONSTRAINT [PK_ViewArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ViewArea_ViewArea] FOREIGN KEY ([Id]) REFERENCES [Content].[ViewArea] ([Id])
);

