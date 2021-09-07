/*EXPORT_PRODUCT_TO_EXCEL*/
CREATE VIEW EXPORT_PRODUCT_TO_EXCEL AS
SELECT  [ID]
      ,(select name from Categories where ID= ID_CATEGORY) as CATEGORY
      ,(select name from Categories where ID=ID_TRANSPORTER ) as TRANSPORTER
      ,(select name from Categories where ID=ID_MATERIALS ) as MATERIAL
      ,(select name from Companys where ID=ID_COMPANY ) as COMPANY
      ,[COUNTRY]
      ,[NAME]
      ,[PRICE]
      ,[DESCRIPTION]
      ,[QUANTITY]
      ,[STOCK]
      ,[WEIGHT]
      ,[WIDTH]
      ,[LENGHT]
      ,[STATUS]
      ,[HEIGHT]
	  from Products


/*import data to db*/
BULK INSERT Medias
FROM 'E:\WorkSpace\_BackEnd\ProductStore\mock_data\medias.csv'
WITH
(
	FORMAT ='CSV',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)