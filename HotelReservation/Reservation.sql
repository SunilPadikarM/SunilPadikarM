USE [HotelDB]
GO

/****** Object:  Table [dbo].[Reservation]    Script Date: 2018-12-04 5:18:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Reservation](
	[ReservationNumber] [int] NOT NULL,
	[CheckInDate] [datetime] NULL,
	[CheckOutDate] [datetime] NULL,
	[NoOfGuests] [int] NULL,
	[NoOfRooms] [int] NULL,
	[ID] [int] NOT NULL,
	[RoomNumber] [int] NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[ReservationNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Customer1] FOREIGN KEY([ID])
REFERENCES [dbo].[Customer] ([ID])
GO

ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Customer1]
GO

ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Rooms] FOREIGN KEY([RoomNumber])
REFERENCES [dbo].[Rooms] ([RoomNumber])
GO

ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Rooms]
GO

