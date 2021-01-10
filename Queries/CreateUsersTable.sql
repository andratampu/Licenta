USE [Licenta]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 09/01/2021 23:03:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(0,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[FirstLoginDone] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


