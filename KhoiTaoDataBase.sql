USE [master]
GO
/****** Object:  Database [CMX]    Script Date: 10/27/2023 11:13:55 AM ******/
CREATE DATABASE [CMX]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CMX', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CMX.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CMX_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CMX_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE CMX SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC CMX.[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE CMX SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE CMX SET ANSI_NULLS OFF 
GO
ALTER DATABASE CMX SET ANSI_PADDING OFF 
GO
ALTER DATABASE CMX SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE CMX SET ARITHABORT OFF 
GO
ALTER DATABASE CMX SET AUTO_CLOSE OFF 
GO
ALTER DATABASE CMX SET AUTO_SHRINK OFF 
GO
ALTER DATABASE CMX SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE CMX SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE CMX SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE CMX SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE CMX SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE CMX SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE CMX SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE CMX SET  ENABLE_BROKER 
GO
ALTER DATABASE CMX SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE CMX SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE CMX SET TRUSTWORTHY OFF 
GO
ALTER DATABASE CMX SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE CMX SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE CMX SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE CMX SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE CMX SET RECOVERY FULL 
GO
ALTER DATABASE CMX SET  MULTI_USER 
GO
ALTER DATABASE CMX SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE CMX SET DB_CHAINING OFF 
GO
ALTER DATABASE CMX SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE CMX SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE CMX SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE CMX SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CMX', N'ON'
GO
ALTER DATABASE CMX SET QUERY_STORE = OFF
GO
USE CMX
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 10/27/2023 11:13:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[AssignmentId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[Display] [bit] NOT NULL,
	[Deadline] [datetime] NULL,
	[File] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 10/27/2023 11:13:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[LecturerId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseEnrollment]    Script Date: 10/27/2023 11:13:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseEnrollment](
	[EnrollmentId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EnrollmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 10/27/2023 11:13:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[QuizId] [int] NOT NULL,
	[Question] [nvarchar](max) NULL,
	[OptionA] [nvarchar](max) NULL,
	[OptionB] [nvarchar](max) NULL,
	[OptionC] [nvarchar](max) NULL,
	[OptionD] [nvarchar](max) NULL,
	[CorrectOption] [nvarchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 10/27/2023 11:13:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quiz](
	[QuizId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[Title] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[QuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizAttendance]    Script Date: 10/27/2023 11:13:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizAttendance](
	[AttendanceId] [int] IDENTITY(1,1) NOT NULL,
	[QuizId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[Score] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentAssignment]    Script Date: 10/27/2023 11:13:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAssignment](
	[SubmissionId] [int] IDENTITY(1,1) NOT NULL,
	[AssignmentId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[File] [nvarchar](255) NULL,
	[SubmissionDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SubmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/27/2023 11:13:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[Username] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[Role] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Assignment] ON 

INSERT [dbo].[Assignment] ([AssignmentId], [CourseId], [Title], [Description], [Display], [Deadline], [File]) VALUES (1, 1, N'DBI Assignment', N'30%', 1, CAST(N'2023-10-27T14:47:00.000' AS DateTime), N'AssignmentDBI.docx')
INSERT [dbo].[Assignment] ([AssignmentId], [CourseId], [Title], [Description], [Display], [Deadline], [File]) VALUES (2, 1, N'DBI PEtrial', N'thi thu', 1, CAST(N'2023-10-27T15:05:00.000' AS DateTime), N'DBIscript.sql')
INSERT [dbo].[Assignment] ([AssignmentId], [CourseId], [Title], [Description], [Display], [Deadline], [File]) VALUES (3, 2, N'Library report', N'30%', 1, CAST(N'2023-10-27T14:47:00.000' AS DateTime), N'SD_Library.docx')
INSERT [dbo].[Assignment] ([AssignmentId], [CourseId], [Title], [Description], [Display], [Deadline], [File]) VALUES (4, 3, N'XML and JSON Serializing', N'20%', 1, CAST(N'2023-10-27T15:05:00.000' AS DateTime), N'Slot 05_06-Working with XML and JSON Serializing.pptx')
INSERT [dbo].[Assignment] ([AssignmentId], [CourseId], [Title], [Description], [Display], [Deadline], [File]) VALUES (5, 3, N'ASP.NET Core Razor Pages', N'20%', 1, CAST(N'2023-10-27T14:47:00.000' AS DateTime), N'Slot 17_18_19-Building Websites Using ASP.NET Core Razor Pages.pptx')
INSERT [dbo].[Assignment] ([AssignmentId], [CourseId], [Title], [Description], [Display], [Deadline], [File]) VALUES (6, 4, N'PE Note', N' ', 1, CAST(N'2023-10-27T14:47:00.000' AS DateTime), N'PRN3 PE Note.docx')
SET IDENTITY_INSERT [dbo].[Assignment] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([CourseId], [Title], [Description], [LecturerId]) VALUES (1, N'DBI', N'Database', 2)
INSERT [dbo].[Course] ([CourseId], [Title], [Description], [LecturerId]) VALUES (2, N'PRN 1', N'Winform + MVC', 2)
INSERT [dbo].[Course] ([CourseId], [Title], [Description], [LecturerId]) VALUES (3, N'PRN 2', N'WPF + Razor page', 3)
INSERT [dbo].[Course] ([CourseId], [Title], [Description], [LecturerId]) VALUES (4, N'PRN 3', N'API + gRPC', 2)
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseEnrollment] ON 

INSERT [dbo].[CourseEnrollment] ([EnrollmentId], [CourseId], [StudentId]) VALUES (1, 1, 4)
INSERT [dbo].[CourseEnrollment] ([EnrollmentId], [CourseId], [StudentId]) VALUES (2, 1, 7)
INSERT [dbo].[CourseEnrollment] ([EnrollmentId], [CourseId], [StudentId]) VALUES (3, 1, 6)
INSERT [dbo].[CourseEnrollment] ([EnrollmentId], [CourseId], [StudentId]) VALUES (4, 2, 4)
INSERT [dbo].[CourseEnrollment] ([EnrollmentId], [CourseId], [StudentId]) VALUES (5, 2, 5)
INSERT [dbo].[CourseEnrollment] ([EnrollmentId], [CourseId], [StudentId]) VALUES (6, 2, 6)
INSERT [dbo].[CourseEnrollment] ([EnrollmentId], [CourseId], [StudentId]) VALUES (7, 2, 7)
INSERT [dbo].[CourseEnrollment] ([EnrollmentId], [CourseId], [StudentId]) VALUES (8, 2, 8)
INSERT [dbo].[CourseEnrollment] ([EnrollmentId], [CourseId], [StudentId]) VALUES (9, 3, 4)
SET IDENTITY_INSERT [dbo].[CourseEnrollment] OFF
GO
SET IDENTITY_INSERT [dbo].[Question] ON 
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (1, 1 ,N'What is the benefit of "de-normalization"?','"de-normalization" has no benefit','The main benefit of de-normalization is improved performance with simplified data retrieval (this is done by reduction in the number of joins needed for data processing','The main benefit of de-normalization is eliminating redundant information from a table and organizing the data so that future changes to the table are easier','','B')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (2, 1 ,N'A class in UML is similar to...........','An entity set in E/R model','An attribute in E/R model','A Relationship in E/R model','None of the others','A')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (3, 1 ,N'An association class in UML is similar to ______ in the ER model','attributes on a relationship','Attributes','Entities','entity sets','A')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (4, 1 ,N'Which of the following is NOT a standard aggregation operator?','GROUP','SUM','COUNT','AVG','A')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (5, 1 ,N'In SQL , the command/statement that let you add an attribute to a relation schema is .......','Insert','Update','Alter','None of the others','C')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (6, 1 ,N'To update a relation`s schema, which one of the following statements can be used?','UPDATE','SELECT','INSERT','ALTER TABLE','D')

INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (7, 2 ,N'(TRUE OR NULL) return:','TRUE','FALSE','NULL','None of the others','A')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (8, 2 ,N'Choose one correct statement:','Two null values are equal','Comparisons between two null values, or between a NULL and any other value, return unknown','Comparisons between two null values, or between a NULL and any other value, return FALSE','','B')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (9, 2 ,N'Foreign key constraints are created by using "_____" keyword to refer to the primary key of another table','POINT TO','REFERENCES','REFER','None of the others','B')

INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (10, 3 ,N'Which of the following keywords meaning access is limited in the same assembly but not outside the assembly?','private','protected internal','internal','public','C')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (11, 3 ,N'Which of the following web servers support for ASP.NET Core application?','Local computer','Tomcat','ReactJS','Internet Information Services (IIS)','D')

INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (12, 4 ,N'Which property of ComboBox is used to specify the string displayed in the editing field?','SelectedValue','SelectedItem','Text','SelectedIndex','C')

INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (13, 6 ,N'TEST 1','1','2','3','4','A')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (14, 6 ,N'TEST 6','7','6','9','2','B')
INSERT [dbo].[Question] ([QuestionId],[QuizId],Question, OptionA,OptionB,OptionC,OptionD,CorrectOption) VALUES (15, 6 ,N'TEST 4','1','2','3','4','D')
SET IDENTITY_INSERT [dbo].[Question] OFF
GO
SET IDENTITY_INSERT [dbo].[Quiz] ON 
INSERT [dbo].[Quiz] ([QuizId],CourseId, Title) VALUES (1, 1 ,N'DBI TEST 1')
INSERT [dbo].[Quiz] ([QuizId],CourseId, Title) VALUES (2, 1 ,N'DBI TEST 2')
INSERT [dbo].[Quiz] ([QuizId],CourseId, Title) VALUES (3, 2 ,N'PRN 1 PT1')
INSERT [dbo].[Quiz] ([QuizId],CourseId, Title) VALUES (4, 2 ,N'PRN 1 PT2')
INSERT [dbo].[Quiz] ([QuizId],CourseId, Title) VALUES (5, 2 ,N'PRN 1 PT3')
INSERT [dbo].[Quiz] ([QuizId],CourseId, Title) VALUES (6, 4 ,N'Pe quiz')
SET IDENTITY_INSERT [dbo].[Quiz] OFF

GO
SET IDENTITY_INSERT [dbo].[QuizAttendance] ON 
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (1,1, 4 ,6)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (2,2, 4 ,3)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (3,3, 4 ,2)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (4,4, 4 ,1)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (5,5, 4 ,2)

INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (6,3, 5 ,1)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (7,4, 5 ,1)

INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (8,1, 6 ,4)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (9,3, 6 ,1)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (10,4, 6 ,1)

INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (11,1, 7 ,4)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (12,2, 7 ,2)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (13,3, 7 ,1)
INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (14,4, 7 ,1)

INSERT [dbo].[QuizAttendance] ([AttendanceId],QuizId,[StudentId], Score) VALUES (15,1, 8 ,3)

SET IDENTITY_INSERT [dbo].[QuizAttendance] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[StudentAssignment] ON 

INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (1, 1, 4, N'NhatDBI.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (2, 1, 5, N'MinhDBI.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (3, 2, 6, N'VietDBIpe.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (4, 2, 7, N'ThuanDBIpe.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (5, 3, 4, N'NhatLibraryReport.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (6, 3, 5, N'MinhLibraryReport.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (7, 3, 6, N'VietLibraryReport.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (8, 3, 7, N'ThuanLibraryReport.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (9, 3, 8, N'KhanhLibraryReport.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (10, 4, 8, N'KhanhSerialize.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (11, 5, 5, N'MinhRazorPage.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))
INSERT [dbo].[StudentAssignment] ([SubmissionId], [AssignmentId], [StudentId], [File], [SubmissionDate]) VALUES (12, 5, 6, N'VietRazorPage.docx', CAST(N'2023-10-16T14:52:33.000' AS DateTime))

SET IDENTITY_INSERT [dbo].[StudentAssignment] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Email], [Username], [Password], [Role]) VALUES (1, N'admin@fe.edu.vn', N'Admin', N'123', N'Admin')
INSERT [dbo].[User] ([UserId], [Email], [Username], [Password], [Role]) VALUES (2, N'huong@fpt', N'HuongNT', N'12345', N'Teacher')
INSERT [dbo].[User] ([UserId], [Email], [Username], [Password], [Role]) VALUES (3, N'lan@fpt', N'LanNT', N'12345', N'Teacher')
INSERT [dbo].[User] ([UserId], [Email], [Username], [Password], [Role]) VALUES (4, N'nhattshe150652@fpt.edu.vn', N'Tống Sỹ Nhật', N'123', N'Student')
INSERT [dbo].[User] ([UserId], [Email], [Username], [Password], [Role]) VALUES (5, N'minhdnhe151130@fpt.edu.vn', N'Đỗ Nhật Minh', N'123', N'Student')
INSERT [dbo].[User] ([UserId], [Email], [Username], [Password], [Role]) VALUES (6, N'vietnqhe151190@fpt.edu.vn', N'Nguyễn Quang Việt', N'123', N'Student')
INSERT [dbo].[User] ([UserId], [Email], [Username], [Password], [Role]) VALUES (7, N'thuantvhe153023@fpt.edu.vn', N'Trần Văn Thuận', N'123', N'Student')
INSERT [dbo].[User] ([UserId], [Email], [Username], [Password], [Role]) VALUES (8, N'khanhnche153409@fpt.edu.vn', N'Nguyễn Công Khanh', N'123', N'Student')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
/****** Object:  Index [UC_QuizAttendance]    Script Date: 10/27/2023 11:13:56 AM ******/
ALTER TABLE [dbo].[QuizAttendance] ADD  CONSTRAINT [UC_QuizAttendance] UNIQUE NONCLUSTERED 
(
	[QuizId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UC_StudentAssignment]    Script Date: 10/27/2023 11:13:56 AM ******/
ALTER TABLE [dbo].[StudentAssignment] ADD  CONSTRAINT [UC_StudentAssignment] UNIQUE NONCLUSTERED 
(
	[AssignmentId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD FOREIGN KEY([LecturerId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[CourseEnrollment]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[CourseEnrollment]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quiz] ([QuizId])
GO
ALTER TABLE [dbo].[Quiz]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[QuizAttendance]  WITH CHECK ADD FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quiz] ([QuizId])
GO
ALTER TABLE [dbo].[QuizAttendance]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[StudentAssignment]  WITH CHECK ADD FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([AssignmentId])
GO
ALTER TABLE [dbo].[StudentAssignment]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[User] ([UserId])
GO
USE [master]
GO
ALTER DATABASE CMX SET  READ_WRITE 
GO
