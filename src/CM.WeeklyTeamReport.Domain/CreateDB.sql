create database WeeklyReport
go

use WeeklyReport;

create table TeamMember(
	TeamMemberId int identity(1,1) primary key,
	FirstName nvarchar(20) not null,
	LastName nvarchar(20) not null,
	Title nvarchar(20) not null,
	Email nvarchar(50) not null
);

create table ReportingTeamMembersToTeamMembers(
	ReportingTMId int not null,
	LeaderTMId int not null
);

create table ReportGrade(
	ReportGradeId int identity(1,1) primary key,
	[Level] int not null,
	Commentary nvarchar(600)
);

create table WeeklyReport(
	ReportId int identity(1,1) primary key,
	AuthorId int not null,
	MoraleGradeId int not null,
	StressGradeId int not null,
	WorkloadGradeId int not null,
	HighThisWeek nvarchar(600) not null,
	LowThisWeek nvarchar(600) not null,
	AnythingElse nvarchar(600),
	constraint FK_Report_Author foreign key (AuthorId) references TeamMember(TeamMemberId),
	constraint FK_Report_MoraleGrade foreign key (MoraleGradeId) references ReportGrade(ReportGradeId),
	constraint FK_Report_StressGrade foreign key (StressGradeId) references ReportGrade(ReportGradeId),
	constraint FK_Report_WorkloadGrade foreign key (WorkloadGradeId) references ReportGrade(ReportGradeId)
);