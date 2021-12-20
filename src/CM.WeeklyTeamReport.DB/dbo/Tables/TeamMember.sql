CREATE TABLE [dbo].[TeamMember] (
    [TeamMemberId] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (20)  NOT NULL,
    [LastName]     NVARCHAR (20)  NOT NULL,
    [Title]        NVARCHAR (20)  NOT NULL,
    [Email]        NVARCHAR (50)  NOT NULL,
    [Sub]          NVARCHAR (200) NOT NULL,
    [CompanyId]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([TeamMemberId] ASC),
    CONSTRAINT [FK_History_TeamMember] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

