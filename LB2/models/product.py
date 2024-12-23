from sqlalchemy import Column, Integer, String, DECIMAL, ForeignKey
from sqlalchemy.orm import relationship
from db_setup import Base

class Product(Base):
    __tablename__ = 'Products'
    ProductId = Column(Integer, primary_key=True, autoincrement=True)
    Name = Column(String(100), nullable=False)
    Price = Column(DECIMAL(10, 2), nullable=False)
    CategoryId = Column(Integer, ForeignKey('Categories.CategoryId'))

    category = relationship('Category', back_populates='products')
    order_details = relationship('OrderDetail', back_populates='product')

    def __repr__(self):
        return f"<Product(ProductId={self.ProductId}, Name='{self.Name}', Price={self.Price})>"
