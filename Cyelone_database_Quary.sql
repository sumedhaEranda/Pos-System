---frist step:---- create database name and run all Executed quary or one by one-/
create database CyeloneCompany_db;
use CyeloneCompany_db;

/****** Object:  Table [dbo].[Registration]    Script Date: 10/6/2021 12:48:02 PM ******/

CREATE TABLE [dbo].[User_info](
	[UserName] [nchar](30) NOT NULL,
	[UserType] [nchar](20) NOT NULL,
	[Password] [nchar](30) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[ContactNo] [nchar](15) NOT NULL,
	[Email] [varchar](250) NULL,
	[JoiningDate] [nchar](50) NULL,
    CONSTRAINT PK_Registration PRIMARY KEY (UserName));

/****** Object:  Table [dbo].[Category]    Script Date: 10/6/2021 12:44:20 PM ******/


CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](250) NOT NULL,
    CONSTRAINT PK_Category PRIMARY KEY (ID));



/****** Object:  Table [dbo].[Customer]    Script Date: 10/6/2021 12:46:39 PM ******/


CREATE TABLE [dbo].[Customer](
	[CustomerId] [nchar](10) NOT NULL,
	[CustomerName] [nchar](100) NOT NULL,
	[Address] [varchar](max) NOT NULL,
	[City] [varchar](250) NOT NULL,
	[ContactNo] [nchar](15) NOT NULL,
	[ContactNo1] [nchar](15) NULL,
	[Email] [varchar](250) NULL,
	[Notes] [varchar](max) NULL,
    CONSTRAINT PK_Customer PRIMARY KEY (CustomerId));

/****** Object:  Table [dbo].[Invoice_Info]    Script Date: 10/6/2021 12:47:00 PM ******/


CREATE TABLE [dbo].[Invoice_Info](
	[InvoiceNo] [nchar](15) NOT NULL,
	[UserName] [nchar](30) NOT NULL,
	[CustomerID] [nchar](10) NOT NULL ,
	[InvoiceDate] [nchar](30) NOT NULL,
	[SubTotal] [float] NOT NULL,
	[VATPer] [float] NOT NULL,
	[VATAmount] [float] NOT NULL,
	[DiscountPer] [float] NOT NULL,
	[DiscountAmount] [float] NOT NULL,
	[GrandTotal] [float] NOT NULL,
	[TotalPayment] [float] NOT NULL,
	[PaymentDue] [float] NOT NULL,
	[PaymentType] [nchar](100) NOT NULL,
	[Status] [nchar](100) NOT NULL,
	[Remarks] [varchar](250) NULL,
	PRIMARY KEY (InvoiceNo),
	CONSTRAINT FK_Customer FOREIGN KEY (CustomerID)
    REFERENCES Customer(CustomerID),
	CONSTRAINT PK_Invoice_Registration FOREIGN KEY (UserName)
    REFERENCES User_info(UserName));

/****** Object:  Table [dbo].[SubCategory]    Script Date: 10/6/2021 12:48:42 PM ******/


CREATE TABLE [dbo].[SubCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubCategoryName] [varchar](250) NOT NULL,
	[CategoryID] [int] NOT NULL,
    PRIMARY KEY (ID),
	CONSTRAINT FK_Category FOREIGN KEY (CategoryID)
    REFERENCES Category(ID));


/****** Object:  Table [dbo].[Product]    Script Date: 10/6/2021 12:47:21 PM ******/


CREATE TABLE [dbo].[Product](
	[ProductID] [nchar](10) NOT NULL,
	[ProductName] [varchar](max) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[SubCategoryID] [int] NOT NULL,
	[Features] [varchar](max) NULL,
	[Price] [float] NOT NULL,
	[Image] [image]  NULL,
    [Reoderlevel] [int] NOT NULL,
    PRIMARY KEY (ProductID),
	CONSTRAINT FK_PRODUCT_Category FOREIGN KEY (CategoryID)
    REFERENCES Category(ID),
	CONSTRAINT FK_SubCategory FOREIGN KEY (SubCategoryID)
    REFERENCES SubCategory(ID));





/****** Object:  Table [dbo].[ProductSold]    Script Date: 10/6/2021 12:47:42 PM ******/

CREATE TABLE [dbo].[ProductSold](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [nchar](15) NOT NULL,
	[ProductID] [nchar](10) NOT NULL,
	[ProductName] [varchar](250) NOT NULL,
	[Price] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
	[TotalAmount] [float] NOT NULL,
    PRIMARY KEY (ID),
	CONSTRAINT FK_PRODUCT_Invoice_Info FOREIGN KEY (InvoiceNo)
	REFERENCES Invoice_Info(InvoiceNo),
	CONSTRAINT FK_Product FOREIGN KEY (ProductID)
	REFERENCES Product(ProductID));


/****** Object:  Table [dbo].[Supplier]    Script Date: 10/6/2021 12:48:56 PM ******/


CREATE TABLE [dbo].[Supplier](
	[SupplierId] [nchar](10) NOT NULL,
	[SupplierName] [varchar](250) NOT NULL,
	[Address] [varchar](250) NOT NULL,
	[City] [varchar](250) NOT NULL,
	[ContactNo] [nchar](15) NOT NULL,
	[ContactNo1] [nchar](15) NULL,
	[Email] [varchar](250) NULL,
	[Notes] [varchar](max) NULL,
	CONSTRAINT PK_Supplier PRIMARY KEY (SupplierId));



/****** Object:  Table [dbo].[Stock]    Script Date: 10/6/2021 12:48:20 PM ******/


CREATE TABLE [dbo].[Stock](
	[StockID] [nchar](10) NOT NULL,
	[StockDate] [nchar](30) NOT NULL,
	[ProductID] [nchar](10) NOT NULL,
	[SupplierID] [nchar](10) NOT NULL,
	[Quantity] [int] NOT NULL,
	PRIMARY KEY (StockID),
	CONSTRAINT FK_Stock_PRODUCT FOREIGN KEY (ProductID)
	REFERENCES Product(ProductID),
	CONSTRAINT PK_Stock_Supplier FOREIGN KEY (SupplierId)
	REFERENCES Supplier(SupplierId));



/****** Object:  Table [dbo].[Temp_Stock]    Script Date: 10/6/2021 12:48:20 PM ****/

CREATE TABLE [dbo].[Temp_Stock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [nchar](10) NOT NULL,
	[Quantity] [int] NOT NULL,
	PRIMARY KEY (ID),
	CONSTRAINT FK_Temp_Stock_PRODUCT FOREIGN KEY (ProductID)
	REFERENCES [Product](ProductID));

