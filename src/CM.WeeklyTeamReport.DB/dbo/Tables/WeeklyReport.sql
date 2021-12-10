CREATE TABLE [dbo].[WeeklyReport] (
    [ReportId]        INT            IDENTITY (1, 1) NOT NULL,
    [AuthorId]        INT            NOT NULL,
    [MoraleGradeId]   INT            NOT NULL,
    [StressGradeId]   INT            NOT NULL,
    [WorkloadGradeId] INT            NOT NULL,
    [HighThisWeek]    NVARCHAR (600) NOT NULL,
    [LowThisWeek]     NVARCHAR (600) NOT NULL,
    [AnythingElse]    NVARCHAR (600) NULL,
    [Date]            DATE           NOT NULL,
    PRIMARY KEY CLUSTERED ([ReportId] ASC),
    CONSTRAINT [FK_Report_Author] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[TeamMember] ([TeamMemberId]),
    CONSTRAINT [FK_Report_MoraleGrade] FOREIGN KEY ([MoraleGradeId]) REFERENCES [dbo].[ReportGrade] ([ReportGradeId]),
    CONSTRAINT [FK_Report_StressGrade] FOREIGN KEY ([StressGradeId]) REFERENCES [dbo].[ReportGrade] ([ReportGradeId]),
    CONSTRAINT [FK_Report_WorkloadGrade] FOREIGN KEY ([WorkloadGradeId]) REFERENCES [dbo].[ReportGrade] ([ReportGradeId])
);



