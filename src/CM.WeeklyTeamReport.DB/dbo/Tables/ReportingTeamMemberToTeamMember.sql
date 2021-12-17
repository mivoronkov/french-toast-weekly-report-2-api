CREATE TABLE [dbo].[ReportingTeamMemberToTeamMember] (
    [ReportingTMId] INT NOT NULL,
    [LeaderTMId]    INT NOT NULL,
    CONSTRAINT [PK_TableName] PRIMARY KEY CLUSTERED ([ReportingTMId], [LeaderTMId]),
    CONSTRAINT [FK_LeaderTM] FOREIGN KEY ([LeaderTMId]) REFERENCES [dbo].[TeamMember] ([TeamMemberId]),
    CONSTRAINT [FK_ReportingTM] FOREIGN KEY ([ReportingTMId]) REFERENCES [dbo].[TeamMember] ([TeamMemberId])
);

