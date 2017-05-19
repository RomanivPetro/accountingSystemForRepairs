USE AccountingSystemDB
GO

INSERT INTO [dbo].[Administrator]([Login], [Password]) 
VALUES ('admin', '19a2854144b63a8f7617a6f225019b12') --hashed 'admin'
GO

INSERT INTO [dbo].[Order]([CustomerName], [PhoneNumber], [Email], [Device], [Problem],
[ReceptionDate], [GivingDate], [Cost], [Income], [Note])
VALUES
	('Bob', '+380913434365', NULL, 'device1', 'problem bla, bla', '2017-05-03', '2017-05-05', 1000, 250, 'some note'),
	('Nick', '+380913564578', NULL, 'device2', 'problem bla, bla', '2017-05-15', '2017-05-16', 800, 200, 'some note'),
	('Jack', '+380939674504', 'jack@mail.com', 'device3', 'problem bla, bla', '2017-05-17', NULL, 500, 100, 'some note'),
	('Ann', '+380679034875', NULL, 'device1', 'problem bla, bla', '2017-05-17', '2017-05-18', 1200, 300, 'some note');
GO

INSERT INTO [dbo].[Worker]([Name])
VALUES ('Alex'),
	('Vasya');
GO

INSERT INTO [dbo].[WorkerOrders]
VALUES (1, 1),
(1, 2),
(2, 3),
(2, 4),
(1, 4);
GO

INSERT INTO [dbo].[Spending]([Description], [Cost], [Date])
VALUES ('for intruments', 100, '2017-05-12'),
	('for food', 50, '2017-05-15'),
	('for beer :)', 100, '2017-05-17');
GO
