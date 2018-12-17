USE [HotelDB]
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 2018-11-28 11:49:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](max) NULL,
	[lastName] [varchar](max) NULL,
	[addrses1] [varchar](max) NULL,
	[city] [varchar](max) NULL,
	[province] [varchar](max) NULL,
	[country] [varchar](max) NULL,
	[postal] [varchar](max) NULL,
	[phone] [varchar](max) NULL,
	[email] [varchar](max) NULL,
	[password] [varchar](10) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

