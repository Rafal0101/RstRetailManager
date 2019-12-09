CREATE PROCEDURE [dbo].[spProduct_GetById]
@Id int
AS
set nocount on;

	SELECT Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable
	from dbo.Product 
	where Id = @Id;

RETURN 0