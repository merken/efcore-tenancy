USE [DellDb]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 24/09/2019 13:45:15 ******/
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
INSERT [dbo].[Products] ([ID], [Name], [Price]) VALUES (1, N'Latitude 2 in 1', CAST(1200.40 AS Decimal(18, 2)))
GO
INSERT [dbo].[Products] ([ID], [Name], [Price]) VALUES (2, N'XPS 15', CAST(2200.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Products] ([ID], [Name], [Price]) VALUES (3, N'XPS 13', CAST(1900.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
