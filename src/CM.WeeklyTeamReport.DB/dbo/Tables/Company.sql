CREATE TABLE [dbo].[Company] (
    [CompanyId]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [CreationDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([CompanyId] ASC)
);

