CREATE TABLE [dbo].[ReportingTeamMemberToTeamMember] (
    [LinkId] INT IDENTITY (1, 1) NOT NULL,
    [ReportingTMId] INT NOT NULL,
    [LeaderTMId]    INT NOT NULL,
    PRIMARY KEY CLUSTERED ([LinkId] ASC),
    CONSTRAINT [FK_LeaderTM] FOREIGN KEY ([LeaderTMId]) REFERENCES [dbo].[TeamMember] ([TeamMemberId]),
    CONSTRAINT [FK_ReportingTM] FOREIGN KEY ([ReportingTMId]) REFERENCES [dbo].[TeamMember] ([TeamMemberId])
);

