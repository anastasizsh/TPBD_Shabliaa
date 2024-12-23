-- Створення бази даних
CREATE DATABASE ComputerPartsShop;
USE ComputerPartsShop;

-- Створення таблиць
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY AUTO_INCREMENT,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Products (
    ProductId INT PRIMARY KEY AUTO_INCREMENT,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    CategoryId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY AUTO_INCREMENT,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY AUTO_INCREMENT,
    CustomerId INT NOT NULL,
    OrderDate DATE NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE OrderDetails (
    OrderDetailId INT PRIMARY KEY AUTO_INCREMENT,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- Наповнення таблиць даними
INSERT INTO Categories (Name) VALUES 
('Процесори'), 
('Материнські плати'), 
('Оперативна пам\'ять');

INSERT INTO Products (Name, Price, CategoryId) VALUES 
('Intel Core i5', 6000, 1),
('AMD Ryzen 5', 5500, 1),
('MSI B450', 3200, 2),
('Gigabyte B550', 3400, 2),
('Kingston 16GB', 2000, 3),
('Corsair 8GB', 1500, 3);

INSERT INTO Customers (FullName, Email) VALUES 
('Іван Іванов', 'ivanov@gmail.com'),
('Петро Петренко', 'petrenko@gmail.com'),
('Марія Шевченко', 'mariya@gmail.com');

INSERT INTO Orders (CustomerId, OrderDate) VALUES 
(1, '2024-12-20'),
(2, '2024-12-21'),
(3, '2024-12-22');

INSERT INTO OrderDetails (OrderId, ProductId, Quantity) VALUES 
(1, 1, 2),
(1, 3, 1),
(2, 2, 1),
(2, 5, 4),
(3, 4, 3),
(3, 6, 2);

-- Тестові запити для перевірки

-- Виведення всіх таблиць
SELECT * FROM Categories;
SELECT * FROM Products;
SELECT * FROM Customers;
SELECT * FROM Orders;
SELECT * FROM OrderDetails;

-- Запит з об'єднанням таблиць (JOIN)
SELECT 
    Orders.OrderId, 
    Customers.FullName AS CustomerName, 
    Products.Name AS ProductName, 
    OrderDetails.Quantity, 
    Orders.OrderDate
FROM OrderDetails
INNER JOIN Orders ON OrderDetails.OrderId = Orders.OrderId
INNER JOIN Customers ON Orders.CustomerId = Customers.CustomerId
INNER JOIN Products ON OrderDetails.ProductId = Products.ProductId;

-- Запит з фільтрацією
SELECT * FROM Products WHERE Price > 3000;

-- Агрегатний запит
SELECT 
    COUNT(ProductId) AS TotalProducts, 
    AVG(Price) AS AveragePrice, 
    MAX(Price) AS MostExpensiveProduct, 
    MIN(Price) AS CheapestProduct
FROM Products;
