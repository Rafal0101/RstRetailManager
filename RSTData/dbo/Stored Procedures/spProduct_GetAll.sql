CREATE PROCEDURE [dbo].[spProduct_GetAll]
AS
set nocount on;

	SELECT Id, ProductName, [Description], RetailPrice, QuantityInStock
	from dbo.Product 
	order by ProductName
RETURN 0
