USE [DiscriminatorDB]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 24/09/2019 13:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Tenant] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([ID], [Name], [Price], [Tenant]) VALUES (3, N'Inspiron 15 2 in 1', CAST(1600.00 AS Decimal(18, 2)), N'Dell')
GO
INSERT [dbo].[Products] ([ID], [Name], [Price], [Tenant]) VALUES (4, N'x360', CAST(1500.00 AS Decimal(18, 2)), N'HP')
GO
INSERT [dbo].[Products] ([ID], [Name], [Price], [Tenant]) VALUES (5, N'XPS 15', CAST(1900.00 AS Decimal(18, 2)), N'Dell')
GO
INSERT [dbo].[Products] ([ID], [Name], [Price], [Tenant]) VALUES (6, N'Workstation G7', CAST(1800.00 AS Decimal(18, 2)), N'HP')
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
