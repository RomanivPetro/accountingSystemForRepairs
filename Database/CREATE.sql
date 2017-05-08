

USE AccountingSystemDB
GO

--Order table
CREATE TABLE [Order]
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

--Worker table
CREATE TABLE [Worker]
(
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL
);

--Relations table
CREATE TABLE [WorkerOrders]
(
	[WorkerId] int NOT NULL,
	[OrderId] int NOT NULL,
	CONSTRAINT [FK_WorkerID] FOREIGN KEY([WorkerId]) REFERENCES [Worker]([Id]),
	CONSTRAINT [FK_OrderID] FOREIGN KEY([OrderId]) REFERENCES [Order]([Id])
);

--Spendigs
CREATE TABLE [Spending]
(
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Description] nvarchar(100) NOT NULL,
	[Cost] decimal(19,4) NOT NULL,
	[Date] date NOT NULL DEFAULT(SYSDATETIME())
);

--for logining
CREATE TABLE [Administrator]
(
	[Login] nvarchar(32) PRIMARY KEY,
	[Password] nvarchar(32) NOT NULL
);