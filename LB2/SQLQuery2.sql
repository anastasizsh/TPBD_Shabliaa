CREATE DATABASE ComputerPartsShopDB;
GO

USE ComputerPartsShopDB;
GO

-- Create tables

-- Customers table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE
);

-- Categories table
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL
);

-- Products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID)
);

-- Orders table
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY,
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    OrderDate DATETIME DEFAULT GETDATE()
);

-- OrderDetails table
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT NOT NULL,
    TotalPrice AS (Quantity * (SELECT Price FROM Products WHERE Products.ProductID = OrderDetails.ProductID)) PERSISTED
);

-- Insert sample data
INSERT INTO Customers (FullName, Email) VALUES
('John Doe', 'john.doe@example.com'),
('Jane Smith', 'jane.smith@example.com');

INSERT INTO Categories (Name) VALUES
('Processors'),
('Motherboards'),
('RAM');

INSERT INTO Products (Name, Description, Price, CategoryID) VALUES
('Intel Core i5', '10th Gen Intel Processor', 6000.00, 1),
('AMD Ryzen 5', 'Powerful AMD Processor', 5500.00, 1),
('MSI B450 Motherboard', 'Compatible with AMD Ryzen', 3200.00, 2),
('Kingston 16GB RAM', 'High-performance RAM', 2000.00, 3);

INSERT INTO Orders (CustomerID) VALUES
(1), 
(2);

INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES
(1, 1, 2),
(2, 3, 1);
