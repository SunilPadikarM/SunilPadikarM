USE [HotelDB]
GO

/****** Object:  Table [dbo].[CardDetails]    Script Date: 2018-11-27 7:28:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CardDetails](
	[CardNumber] [varchar](19) NOT NULL,
	[CardType] [varchar](16) NULL,
	[NameOnCard] [varchar](50) NULL,
	[ExpDate] [varchar](8) NULL,
	[ID] [int] NULL,
 CONSTRAINT [PK_CardDetails] PRIMARY KEY CLUSTERED 
(
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CardDetails]  WITH CHECK ADD  CONSTRAINT [FK_CardDetails_Customer] FOREIGN KEY([ID])
REFERENCES [dbo].[Customer] ([ID])
GO

ALTER TABLE [dbo].[CardDetails] CHECK CONSTRAINT [FK_CardDetails_Customer]
GO

