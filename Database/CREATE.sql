IF DB_ID('AccountingSystemDB') IS NULL
	BEGIN
		CREATE DATABASE AccountingSystemDB;
	END
ELSE
	PRINT 'Database already exists';
GO

USE AccountingSystemDB
GO

--Order table
CREATE TABLE [dbo].[Order]
(
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[CustomerName] nvarchar(50) NOT NULL,
	[PhoneNumber] nvarchar(13) NOT NULL,
	[Email] nvarchar(30) NULL,
	[Device] nvarchar(30) NOT NULL,
	[Problem] nvarchar(500) NOT NULL,
	[ReceptionDate] date NOT NULL DEFAULT(SYSDATETIME()),
	[GivingDate] date NULL,
	[Cost] decimal(19,4) NOT NULL DEFAULT(0),
	[Income] decimal(19,4) NOT NULL DEFAULT(0),
	[Note] nvarchar(100) NULL
);
GO

--Worker table
CREATE TABLE [dbo].[Worker]
(
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL
);
GO

--Relations table
CREATE TABLE [dbo].[WorkerOrders]
(
	[WorkerId] int NOT NULL,
	[OrderId] int NOT NULL,
	CONSTRAINT [FK_WorkerID] FOREIGN KEY([WorkerId]) REFERENCES [Worker]([Id]),
	CONSTRAINT [FK_OrderID] FOREIGN KEY([OrderId]) REFERENCES [Order]([Id])
);
GO

--Spendigs
CREATE TABLE [dbo].[Spending]
(
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Description] nvarchar(100) NOT NULL,
	[Cost] decimal(19,4) NOT NULL,
	[Date] date NOT NULL DEFAULT(SYSDATETIME())
);
GO

--for logining
CREATE TABLE [dbo].[Administrator]
(
	[Login] nvarchar(32) PRIMARY KEY,
	[Password] nvarchar(32) NOT NULL
);
GO