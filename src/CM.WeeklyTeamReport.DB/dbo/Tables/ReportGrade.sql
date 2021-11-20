CREATE TABLE [dbo].[ReportGrade] (
    [ReportGradeId] INT            IDENTITY (1, 1) NOT NULL,
    [Level]         INT            NOT NULL,
    [Commentary]    NVARCHAR (600) NULL,
    PRIMARY KEY CLUSTERED ([ReportGradeId] ASC)
);

