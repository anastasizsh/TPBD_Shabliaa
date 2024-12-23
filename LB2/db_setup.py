from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, declarative_base
from sqlalchemy import Column, Integer, String, DECIMAL, ForeignKey, DateTime
from sqlalchemy.orm import relationship

Base = declarative_base()

# Налаштування підключення до бази даних
DATABASE_URL = 'mysql+mysqlconnector://avnadmin:AVNS_H9XkQzsdnJUK3P_lk38@netflix-manya4560-9acc.j.aivencloud.com:21425/defaultdb?ssl_disabled=false'
engine = create_engine(DATABASE_URL)

# Конфігурація сесії
Session = sessionmaker(bind=engine)

def create_session():
    """Створення сесії для взаємодії з базою даних."""
    session = Session()
    return session

# Моделі
class Category(Base):
    __tablename__ = 'Categories'
    CategoryId = Column(Integer, primary_key=True, autoincrement=True)
    Name = Column(String(100), nullable=False)

    # Зв'язок з Product
    products = relationship("Product", back_populates="category")

    def __repr__(self):
        return f"Category(ID={self.CategoryId}, Name='{self.Name}')"

class Product(Base):
    __tablename__ = 'Products'
    ProductId = Column(Integer, primary_key=True, autoincrement=True)
    Name = Column(String(100), nullable=False)
    Price = Column(DECIMAL(10, 2), nullable=False)
    CategoryId = Column(Integer, ForeignKey('Categories.CategoryId'))

    # Зв'язок з Category та OrderDetail
    category = relationship("Category", back_populates="products")
    order_details = relationship("OrderDetail", back_populates="product")

    def __repr__(self):
        return f"Product(ID={self.ProductId}, Name='{self.Name}', Price={self.Price})"

class Customer(Base):
    __tablename__ = 'Customers'
    CustomerId = Column(Integer, primary_key=True, autoincrement=True)
    FullName = Column(String(100), nullable=False)
    Email = Column(String(100), unique=True, nullable=False)

    # Зв'язок із замовленнями
    orders = relationship("Order", back_populates="customer")

    def __repr__(self):
        return f"Customer(ID={self.CustomerId}, FullName='{self.FullName}', Email='{self.Email}')"

class Order(Base):
    __tablename__ = 'Orders'
    OrderId = Column(Integer, primary_key=True, autoincrement=True)
    CustomerId = Column(Integer, ForeignKey('Customers.CustomerId'))
    OrderDate = Column(DateTime)

    # Зв'язок із Customer та OrderDetail
    customer = relationship("Customer", back_populates="orders")
    order_details = relationship("OrderDetail", back_populates="order")

    def __repr__(self):
        return f"Order(ID={self.OrderId}, CustomerId={self.CustomerId}, Date='{self.OrderDate}')"

class OrderDetail(Base):
    __tablename__ = 'OrderDetails'
    OrderDetailId = Column(Integer, primary_key=True, autoincrement=True)
    OrderId = Column(Integer, ForeignKey('Orders.OrderId'))
    ProductId = Column(Integer, ForeignKey('Products.ProductId'))
    Quantity = Column(Integer)

    # Зв'язок із Order та Product
    order = relationship("Order", back_populates="order_details")
    product = relationship("Product", back_populates="order_details")

    def __repr__(self):
        return f"OrderDetail(ID={self.OrderDetailId}, OrderId={self.OrderId}, ProductId={self.ProductId}, Quantity={self.Quantity})"

# Ініціалізація таблиць
if __name__ == "__main__":
    Base.metadata.create_all(engine)
    print("Таблиці створено успішно!")
