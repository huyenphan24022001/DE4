USE [DE4]
GO
/****** Object:  Table [dbo].[LichSu]    Script Date: 7/29/2024 2:17:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDTT] [int] NULL,
	[NgayChinhSua] [datetime] NULL,
	[ChinhSuaBy] [nvarchar](200) NULL,
	[NoiDungChinhSua] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 7/29/2024 2:17:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Users_ID] [int] NULL,
	[access_token] [nvarchar](255) NULL,
	[refresh_token] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TuyenTruyen]    Script Date: 7/29/2024 2:17:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TuyenTruyen](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TieuDe] [nvarchar](255) NOT NULL,
	[MoTa] [nvarchar](500) NULL,
	[NgayBD] [datetime] NULL,
	[NgayKT] [datetime] NULL,
	[TenDonVi] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
	[DaDuyet] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/29/2024 2:17:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Pass] [nvarchar](255) NULL,
	[Role] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LichSu] ON 

INSERT [dbo].[LichSu] ([ID], [IDTT], [NgayChinhSua], [ChinhSuaBy], [NoiDungChinhSua]) VALUES (1, 1, CAST(N'2024-06-16T13:10:19.723' AS DateTime), N'Admin', N'{"LichSus":[],"ID":1,"TieuDe":"Red Carpet Ready testttt hihi","MoTa":"hdjkfjfjsjlkjgfjkgfkjffjdjfdkjdffskjfjfddshjhdffdsdkj","NgayBD":null,"NgayKT":null,"TenDonVi":"STT&TT","IsDelete":false,"DaDuyet":true}')
INSERT [dbo].[LichSu] ([ID], [IDTT], [NgayChinhSua], [ChinhSuaBy], [NoiDungChinhSua]) VALUES (2, 2, CAST(N'2024-06-16T13:11:29.653' AS DateTime), N'Admin', N'{"LichSus":[],"ID":2,"TieuDe":"fffdsfs","MoTa":"dkjdfhjfkhdfsjddffnjfj","NgayBD":"2024-06-15T00:00:00","NgayKT":"2024-06-22T00:00:00","TenDonVi":"STT&TT","IsDelete":false,"DaDuyet":true}')
INSERT [dbo].[LichSu] ([ID], [IDTT], [NgayChinhSua], [ChinhSuaBy], [NoiDungChinhSua]) VALUES (3, 2, CAST(N'2024-06-16T13:29:27.390' AS DateTime), N'Admin', N'{"ID":2,"TieuDe":"fffdsfs","MoTa":"dkjdfhjfkhdfsjddffnjfj","NgayBD":"2024-06-16T00:00:00","NgayKT":"2024-06-19T00:00:00","TenDonVi":"STT&TT","IsDelete":false,"DaDuyet":true}')
INSERT [dbo].[LichSu] ([ID], [IDTT], [NgayChinhSua], [ChinhSuaBy], [NoiDungChinhSua]) VALUES (4, 2, CAST(N'2024-06-16T13:30:00.097' AS DateTime), N'Admin', N'{"ID":2,"TieuDe":"fffdsfs","MoTa":"dkjdfhjfkhdfsjddffnjfj","NgayBD":"2024-06-14T00:00:00","NgayKT":"2024-06-19T00:00:00","TenDonVi":"STT&TT","IsDelete":false,"DaDuyet":true}')
INSERT [dbo].[LichSu] ([ID], [IDTT], [NgayChinhSua], [ChinhSuaBy], [NoiDungChinhSua]) VALUES (5, 2, CAST(N'2024-06-16T13:32:17.760' AS DateTime), N'Admin', N'{"ID":2,"TieuDe":"fffdsfs","MoTa":"dkjdfhjfkhdfsjddffnjfj","NgayBD":"2024-06-14T00:00:00","NgayKT":"2024-06-16T00:00:00","TenDonVi":"STT&TT","IsDelete":false,"DaDuyet":true}')
INSERT [dbo].[LichSu] ([ID], [IDTT], [NgayChinhSua], [ChinhSuaBy], [NoiDungChinhSua]) VALUES (6, 2, CAST(N'2024-06-16T13:32:35.757' AS DateTime), N'Admin', N'{"ID":2,"TieuDe":"fffdsfs","MoTa":"dkjdfhjfkhdfsjddffnjfj","NgayBD":"2024-06-14T00:00:00","NgayKT":"2024-06-17T00:00:00","TenDonVi":"STT&TT","IsDelete":false,"DaDuyet":true}')
SET IDENTITY_INSERT [dbo].[LichSu] OFF
GO
SET IDENTITY_INSERT [dbo].[Token] ON 

INSERT [dbo].[Token] ([ID], [Users_ID], [access_token], [refresh_token]) VALUES (1, 1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjEiLCJVc2VyTmFtZSI6ImFkbWluIiwiUm9sZSI6IjEiLCJleHAiOjE3MjIyNDA5Njl9.nbD1GejbhiLqUOqlfOas6AainB_YtM2CZ9-SE32PG7o', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjEiLCJVc2VyTmFtZSI6ImFkbWluIiwiUm9sZSI6IjEiLCJleHAiOjE3MjI4NDIxNjl9.C2LBp2M8WYspdeXxfeIkl8VXkgE3ERosNv7_H9vewjs')
SET IDENTITY_INSERT [dbo].[Token] OFF
GO
SET IDENTITY_INSERT [dbo].[TuyenTruyen] ON 

INSERT [dbo].[TuyenTruyen] ([ID], [TieuDe], [MoTa], [NgayBD], [NgayKT], [TenDonVi], [IsDelete], [DaDuyet]) VALUES (1, N'Red Carpet Ready testttt hihi', N'hdjkfjfjsjlkjgfjkgfkjffjdjfdkjdffskjfjfddshjhdffdsdkj', NULL, NULL, N'STT&TT', 0, 1)
INSERT [dbo].[TuyenTruyen] ([ID], [TieuDe], [MoTa], [NgayBD], [NgayKT], [TenDonVi], [IsDelete], [DaDuyet]) VALUES (2, N'fffdsfs', N'dkjdfhjfkhdfsjddffnjfj', CAST(N'2024-06-14T00:00:00.000' AS DateTime), CAST(N'2024-06-17T00:00:00.000' AS DateTime), N'STT&TT', 0, 1)
INSERT [dbo].[TuyenTruyen] ([ID], [TieuDe], [MoTa], [NgayBD], [NgayKT], [TenDonVi], [IsDelete], [DaDuyet]) VALUES (3, N'fffdsfs', N'ijfdjkjgkjdshfjfhfhshfhhh', CAST(N'2024-06-15T00:00:00.000' AS DateTime), CAST(N'2024-06-22T00:00:00.000' AS DateTime), N'STT&TT', 0, 0)
SET IDENTITY_INSERT [dbo].[TuyenTruyen] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [UserName], [Pass], [Role]) VALUES (1, N'admin', N'f52EmOY2EqOlO+TvezMgDgWOo+sI249P1hzRKVcu1gE=', 1)
INSERT [dbo].[Users] ([ID], [UserName], [Pass], [Role]) VALUES (2, N'huyen', N'f52EmOY2EqOlO+TvezMgDgWOo+sI249P1hzRKVcu1gE=', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[LichSu]  WITH CHECK ADD FOREIGN KEY([IDTT])
REFERENCES [dbo].[TuyenTruyen] ([ID])
GO
ALTER TABLE [dbo].[Token]  WITH CHECK ADD FOREIGN KEY([Users_ID])
REFERENCES [dbo].[Users] ([ID])
GO
