use master
go

create database WeeklyReport
go

use WeeklyReport;

CREATE TABLE Company (
    [CompanyId]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [CreationDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([CompanyId] ASC)
);

CREATE TABLE TeamMember (
    [TeamMemberId] INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (20) NOT NULL,
    [LastName]     NVARCHAR (20) NOT NULL,
    [Title]        NVARCHAR (20) NOT NULL,
    [Email]        NVARCHAR (50) NOT NULL,
    [CompanyId]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([TeamMemberId] ASC),
    CONSTRAINT [FK_History_TeamMember] FOREIGN KEY ([CompanyId]) REFERENCES Company ([CompanyId])
);

CREATE TABLE ReportGrade (
    [ReportGradeId] INT            IDENTITY (1, 1) NOT NULL,
    [Level]         INT            NOT NULL,
    [Commentary]    NVARCHAR (600) NULL,
    PRIMARY KEY CLUSTERED ([ReportGradeId] ASC)
);

CREATE TABLE ReportingTeamMemberToTeamMember (
    [ReportingTMId] INT NOT NULL,
    [LeaderTMId]    INT NOT NULL,
    CONSTRAINT [FK_LeaderTM] FOREIGN KEY ([LeaderTMId]) REFERENCES TeamMember ([TeamMemberId]),
    CONSTRAINT [FK_ReportingTM] FOREIGN KEY ([ReportingTMId]) REFERENCES TeamMember ([TeamMemberId])
);

CREATE TABLE WeeklyReport (
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
    CONSTRAINT [FK_Report_Author] FOREIGN KEY ([AuthorId]) REFERENCES TeamMember ([TeamMemberId]),
    CONSTRAINT [FK_Report_MoraleGrade] FOREIGN KEY ([MoraleGradeId]) REFERENCES ReportGrade ([ReportGradeId]),
    CONSTRAINT [FK_Report_StressGrade] FOREIGN KEY ([StressGradeId]) REFERENCES ReportGrade ([ReportGradeId]),
    CONSTRAINT [FK_Report_WorkloadGrade] FOREIGN KEY ([WorkloadGradeId]) REFERENCES ReportGrade ([ReportGradeId])
);