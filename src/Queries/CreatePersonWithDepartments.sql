BEGIN DISTRIBUTED TRANSACTION;

INSERT INTO [Database1].[dbo].Person ([FirstName],[LastName]) VALUES ('New', 'Person'); 

DECLARE @id INT;
SELECT @id = @@IDENTITY;

INSERT INTO [Database2].[dbo].DepartmentAssignments ([PersonID],[DepartmentName]) VALUES (@id, 'Dep1');
INSERT INTO [Database2].[dbo].DepartmentAssignments ([PersonID],[DepartmentName]) VALUES (@id, 'Dep2');

COMMIT TRANSACTION;
GO