CREATE OR ALTER PROCEDURE SP_ADD_ORDER
    @EmpId INT,
    @CustId INT,
    @OrderDate DATE,
    @RequiredDate DATE,
    @ShippedDate DATE,
    @ShipperId INT,
    @Freight DECIMAL(10,2),
    @ShipName NVARCHAR(40),
    @ShipAddress NVARCHAR(60),
    @ShipCity NVARCHAR(15),
    @ShipCountry NVARCHAR(15),
    @ProductId INT,
    @UnitPrice DECIMAL(10,2),
    @Quantity INT,
    @Discount NUMERIC(4,2)
AS
BEGIN
    SET NOCOUNT ON
    DECLARE @Result INT = 0; 

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Sales.Orders
            (custid, empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
        VALUES
            (@CustId ,@EmpId, @ShipperId, @ShipName, @ShipAddress, @ShipCity, @OrderDate, @RequiredDate, @ShippedDate, @Freight, @ShipCountry);

        DECLARE @NewOrderId INT = SCOPE_IDENTITY();

        INSERT INTO Sales.OrderDetails (orderid, productid, unitprice, qty, discount)
        VALUES (@NewOrderId, @ProductId, @UnitPrice, @Quantity, @Discount);

        COMMIT TRANSACTION;
        SET @Result = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @Result = 0; 
    END CATCH

    SELECT @Result AS Result;
END
GO

CREATE OR ALTER PROCEDURE SP_GET_CLIENT_ORDERS (
    @CustomerId INT = 0,
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @SortColumn NVARCHAR(50) = 'orderid',
    @SortDirection NVARCHAR(4) = 'ASC'
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    
    IF @SortDirection NOT IN ('ASC', 'DESC')
        SET @SortDirection = 'ASC';
    
    IF @SortColumn NOT IN ('orderid', 'requireddate', 'shippeddate', 'shipname', 'shipaddress', 'shipcity')
        SET @SortColumn = 'orderid';

    DECLARE @Sql NVARCHAR(MAX) = '
        SELECT 
            ord.orderid, 
            FORMAT(ord.requireddate, ''yyyy-MM-dd'') + '', '' + FORMAT(ord.requireddate, ''hh:mm:ss tt'') AS requireddate,
            FORMAT(ord.shippeddate, ''yyyy-MM-dd'') + '', '' + FORMAT(ord.shippeddate, ''hh:mm:ss tt'') AS shippeddate,
            ord.shipname,  
            ord.shipaddress, 
            ord.shipcity
        FROM Sales.Customers cus
        INNER JOIN Sales.Orders ord ON cus.custid = ord.custid
        WHERE cus.custid = @CustomerId';

    DECLARE @SqlCount NVARCHAR(MAX) = '
        SELECT COUNT(*) AS TotalCount FROM (' + @Sql + ') AS CountQuery;';

    DECLARE @SqlPaged NVARCHAR(MAX) = @Sql + '
        ORDER BY ' + QUOTENAME(@SortColumn) + ' ' + @SortDirection + '
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY;';

    EXEC sp_executesql @SqlCount, N'@CustomerId INT', @CustomerId;
    EXEC sp_executesql @SqlPaged, N'@CustomerId INT, @Offset INT, @PageSize INT', @CustomerId, @Offset, @PageSize;
END
GO

CREATE OR ALTER PROCEDURE SP_GET_EMPLOYEES
AS
BEGIN
    SELECT empid, CONCAT(firstname, ' ', lastname) AS 'FullName' FROM HR.Employees
END
GO

CREATE OR ALTER PROCEDURE SP_GET_PRODUCTS
AS
BEGIN
    SELECT productid, productname FROM Production.Products ORDER BY productid
END
GO

CREATE OR ALTER PROCEDURE SP_GET_SALES_PREDICTION (
    @PageNumber INT = 1,
    @PageSize INT = 10,
    @SortColumn NVARCHAR(50) = 'LastOrderDate',
    @SortDirection NVARCHAR(4) = 'ASC'
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    
    IF @SortDirection NOT IN ('ASC', 'DESC')
        SET @SortDirection = 'ASC';
    
    IF @SortColumn NOT IN ('CustomerName', 'LastOrderDate', 'NextPredictedOrder')
        SET @SortColumn = 'LastOrderDate';

    -- Query base (sin paginar)
    DECLARE @Sql NVARCHAR(MAX) = '
        SELECT 
            cus.contactname AS CustomerName, 
            CONVERT(VARCHAR(23), MAX(ord.orderdate), 121) AS LastOrderDate,
            CONVERT(VARCHAR(23),
                DATEADD(
                    DAY, 
                    DATEDIFF(DAY, MIN(ord.orderdate), MAX(ord.orderdate)) / COUNT(*), 
                    MAX(ord.orderdate)
                ), 121
            ) AS NextPredictedOrder
        FROM Sales.Customers cus
        INNER JOIN Sales.Orders ord ON cus.custid = ord.custid
        GROUP BY cus.contactname, cus.custid';

    -- Total sin paginar
    DECLARE @SqlCount NVARCHAR(MAX) = '
        SELECT COUNT(*) AS TotalCount FROM (' + @Sql + ') AS CountQuery;';

    -- Paginado
    DECLARE @SqlPaged NVARCHAR(MAX) = @Sql + '
        ORDER BY ' + QUOTENAME(@SortColumn) + ' ' + @SortDirection + '
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY;';

    -- Ejecutar: primero total, luego página
    EXEC sp_executesql @SqlCount;
    EXEC sp_executesql @SqlPaged, N'@Offset INT, @PageSize INT', @Offset, @PageSize;
END
GO

CREATE OR ALTER PROCEDURE SP_GET_SHIPPERS
AS
BEGIN
    SELECT shipperid, companyname FROM Sales.Shippers
END
