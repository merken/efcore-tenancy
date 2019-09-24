USE [TenantPerSchemaDb]
GO
/****** Object:  Schema [dell]    Script Date: 24/09/2019 13:47:12 ******/
CREATE SCHEMA [dell]
GO
/****** Object:  Schema [hp]    Script Date: 24/09/2019 13:47:12 ******/
CREATE SCHEMA [hp]
GO
/****** Object:  Table [dell].[Products]    Script Date: 24/09/2019 13:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dell].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [hp].[Products]    Script Date: 24/09/2019 13:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [hp].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dell].[Products] ON 
GO
INSERT [dell].[Products] ([ID], [Name], [Price]) VALUES (1, N'Inspiron 15 2 in 1', CAST(1600.00 AS Decimal(18, 2)))
GO
INSERT [dell].[Products] ([ID], [Name], [Price]) VALUES (2, N'XPS 13', CAST(1700.00 AS Decimal(18, 2)))
GO
INSERT [dell].[Products] ([ID], [Name], [Price]) VALUES (3, N'XPS 15', CAST(1901.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dell].[Products] OFF
GO
SET IDENTITY_INSERT [hp].[Products] ON 
GO
INSERT [hp].[Products] ([ID], [Name], [Price]) VALUES (1, N'x360', CAST(1600.00 AS Decimal(18, 2)))
GO
INSERT [hp].[Products] ([ID], [Name], [Price]) VALUES (2, N'Workstation G7', CAST(1902.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [hp].[Products] OFF
GO
