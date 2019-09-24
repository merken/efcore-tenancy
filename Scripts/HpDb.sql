USE [HpDb]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 24/09/2019 13:46:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([ID], [Name], [Price]) VALUES (1, N'X360', CAST(1500.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Products] ([ID], [Name], [Price]) VALUES (2, N'Pavillion 14', CAST(1600.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Products] ([ID], [Name], [Price]) VALUES (3, N'Workstation G7', CAST(1801.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
