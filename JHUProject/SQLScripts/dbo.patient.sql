CREATE TABLE [dbo].[Patient]
(
	[PatientID] INT NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [DateOfBirth] DATE NULL, 
    [CreatedDate] DATE NULL

)
