```graphql

query {
  deleteBLog(
        id:  "2"
  ){
    blog_Id
  }
 updateBlog(
        id:  "0",
    blogDataModel : {
      blog_Title: "test",
      blog_Author: "author",
      blog_Content: "content",
      blog_Id: 12
    }
  ){
    blog_Id
  }
  addBlog(
    blogDataModel : {
      blog_Title: "test",
      blog_Author: "author",
      blog_Content: "content",
      blog_Id: 0
    }
  ){
    blog_Id
  }
  blogs(pageNo: 1, pageSize: 10) {
    blog_Id
    blog_Title
    blog_Author
  }
}


```



Database First
```
Package Manager

Scaffold-DbContext "Server=.;Database=TestDb;User Id=sa;Password=sa@123;Trusted_Connection=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir EFDbContextModels -Context AppDbContext -force

Terminal

dotnet ef dbcontext scaffold "Server=.;Database=TestDb;User Id=sa;Password=sa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o EFDbContextModels -c AppDbContext -f

```

### Latest Transaction
```
Create View Vw_IncomeExpense as 

select top 10 * from 
(
(select 
'Income'  Category,
IncomeName Name,
IncomeAmount Amount, 
CreatedDate 
from Tbl_Income
--order by CreatedDate desc
)
UNION
(select 
'Expense'  category,
ExpenseName ,
ExpenseAmount, 
CreatedDate 
from Tbl_Expense
--order by CreatedDate desc
)
) as IncomeExpense order by CreatedDate desc

```

### Tbl_SignUp
```
CREATE TABLE [dbo].[Tbl_SignUp](
	[RefId] [nvarchar](50) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[IsDelete] [bit] NOT NULL Default 0
) ON [PRIMARY]
GO
```

### Tbl_User
```
CREATE TABLE [dbo].[Tbl_User](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[UserRegistrationStatus] [int] NOT NULL,
	[Password] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[IsDelete] [bit] NOT NULL Default 0
) ON [PRIMARY]
GO
```

### Tbl_UserType
```
CREATE TABLE [dbo].[Tbl_UserType](
	[UserTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserTypeName] [nvarchar](50) NULL,
	[UserTypeOrder] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[IsDelete] [bit] NOT NULL Default 0
) ON [PRIMARY]
GO

```

#### Tbl_Expense
```
CREATE TABLE [dbo].[Tbl_Expense](
	[ExpenseId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[IncomeId] [bigint] NULL,
	[ExpenseName] [nvarchar](50) NULL,
	[ExpenseAmount] [decimal](20, 2) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[IsDelete] [bit] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tbl_Expense] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
```

### Tbl_Income
```
CREATE TABLE [dbo].[Tbl_Income](
	[IncomeId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[IncomeName] [nvarchar](50) NULL,
	[IncomeAmount] [decimal](20, 2) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[IsDelete] [bit] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tbl_Income] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
```

### [Expense Tracker UI](https://preview.keenthemes.com/metronic8/demo33/index.html)

### Tbl_ProductOrder
```
CREATE TABLE [dbo].[Tbl_ProductOrder](
	[ProductOrderId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProductPrice] [decimal](20, 2) NOT NULL,
	[ProductQuantity] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_ProductOrder] PRIMARY KEY CLUSTERED 
(
	[ProductOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

```

### Tbl_Product
```
CREATE TABLE [dbo].[Tbl_Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductPrice] [decimal](20, 2) NOT NULL,
 CONSTRAINT [PK_Tbl_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

### [Sidebar](https://bootsnipp.com/snippets/Q0dAX)

### [Sweet Alert](https://sweetalert2.github.io/#download)

### [Apex Chart](https://apexcharts.com/docs/installation/)

### Tbl_PieChart
```
CREATE TABLE [dbo].[Tbl_PieChart](
	[PieChartId] [int] IDENTITY(1,1) NOT NULL,
	[PieChartLabel] [nvarchar](50) NOT NULL,
	[PieChartData] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_PieChart] PRIMARY KEY CLUSTERED 
(
	[PieChartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

### [SignalR](https://learn.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-3.1&tabs=visual-studio)

### [Jquery DataTable in Asp.Net Core](https://codewithmukesh.com/blog/jquery-datatable-in-aspnet-core/)

### Loging
1. [Serilog](https://github.com/serilog/serilog/wiki/Getting-Started)
2. [Serilog Database Logging](https://www.thecodebuzz.com/serilog-database-sql-logging-in-asp-net-core/)

### [Refit Client](https://code-maze.com/using-refit-to-consume-apis-in-csharp/)

#### [HTTP Status](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status)
1. [Informational responses (100 – 199)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status#information_responses)
2. [Successful responses (200 – 299)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status#successful_responses)
3. [Redirection messages (300 – 399)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status#redirection_messages)
4. [Client error responses (400 – 499)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status#client_error_responses)
5. [Server error responses (500 – 599)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status#server_error_responses)

#### [API Architecuture](https://www.linkedin.com/posts/alexxubyte_systemdesign-coding-interviewtips-activity-7054117638678945792-tfSG?utm_source=share&utm_medium=member_android)
1. ***SOAP***:
Mature, comprehensive, XML-based 
Best for enterprise applications 

2. ***RESTful:*** 
Popular, easy-to-implement, HTTP methods 
Ideal for web services 

3. ***GraphQL***: 
Query language, request specific data 
Reduces network overhead, faster responses 

4.  ***gRPC:*** 
Modern, high-performance, Protocol Buffers 
Suitable for microservices architectures 

5. ***WebSocket:*** 
Real-time, bidirectional, persistent connections 
Perfect for low-latency data exchange 

6. ***Webhook:*** 
Event-driven, HTTP callbacks, asynchronous 
Notifies systems when events occur 


#### Expense Tracker
```sql
CREATE TABLE [dbo].[Tbl_ExpenseTracker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Description] [nvarchar](250) NULL,
	[TransactionType] [nvarchar](50) NULL,
	[Amount] [decimal](20, 2) NULL,
 CONSTRAINT [PK_Tbl_ExpenseTracker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

#### Blog
```sql
Create proc [dbo].[Sp_BlogPagination]
@PageNo int,
@PageSize int
as
begin


SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]
ORDER BY [Blog_Id] desc
OFFSET ((@PageNo - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY

end

Create Proc [dbo].[Sp_BlogCreate]
@BlogTitle nvarchar(50),
@BlogAuthor nvarchar(50),
@BlogContent nvarchar(600)
as
begin
INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)
end

Create proc [dbo].[Sp_BlogUpdate]
@BlogId int,
@BlogTitle nvarchar(50),
@BlogAuthor nvarchar(50),
@BlogContent nvarchar(600)
as
begin

UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @BlogTitle
      ,[Blog_Author] = @BlogAuthor
      ,[Blog_Content] = @BlogContent
 WHERE Blog_Id = @BlogId

end
```

## Contents
Console App

Ado.Net
EF
Dapper 
RepoDB

Asp.Net Core MVC (OMDB Api)
Asp.Net Core Web Api (Rest Api)
Api Call [MVC, Console]
HttpClient
RestClient
Refit
Minimal Api
GraphQL
gRPC
Text Logging
Db Logging

Chart [ApexChart, ChartJs, HighCharts, CanvasJS]
https://naver.github.io/billboard.js/
https://developers.google.com/chart
https://www.highcharts.com/
https://canvasjs.com/
https://www.amcharts.com/
https://www.fusioncharts.com/
https://observablehq.com/@d3/gallery

SignalR - (Insert Data => UpdateChart, Chat Message)
UI Design
Blazor CRUD [Server, WASM]
Deploy WASM
Deploy on IIS

Weather App
Middleware For MVC