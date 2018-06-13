CREATE TABLE [Content].[View] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [Controller] VARCHAR (50) NOT NULL,
    [View]       VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Views] PRIMARY KEY CLUSTERED ([Id] ASC)
);

