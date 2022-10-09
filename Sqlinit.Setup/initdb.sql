USE master;
GO
  IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DataBase')
  BEGIN
    CREATE DATABASE [Checkout]
    END
    GO
       USE [Checkout]
    GO
--You need to check if the table exists
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Basket' and xtype='U')
BEGIN
    CREATE TABLE [dbo].[Basket](
        [Id] [uniqueidentifier] NOT NULL,
        [Customer] [varchar](max) NULL,
        [Status] [bit] NOT NULL,
        [Vat] [bit] NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Article' and xtype='U')
BEGIN
    CREATE TABLE [dbo].[Article](
        [Id] [uniqueidentifier] NOT NULL,
        [Article] [varchar](max) NULL,
        [Price] [int] NULL,
        [BasketId] [uniqueidentifier] NULL,
    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

    ALTER TABLE [dbo].[Article]  WITH CHECK ADD FOREIGN KEY([BasketId])
    REFERENCES [dbo].[Basket] ([Id])
END

