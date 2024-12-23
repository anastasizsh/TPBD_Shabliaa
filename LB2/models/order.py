from sqlalchemy import Column, Integer, DateTime, ForeignKey
from sqlalchemy.orm import relationship
from db_setup import Base

class Order(Base):
    __tablename__ = 'Orders'
    OrderId = Column(Integer, primary_key=True, autoincrement=True)
    CustomerId = Column(Integer, ForeignKey('Customers.CustomerId'))
    OrderDate = Column(DateTime)

    customer = relationship('Customer', back_populates='orders')
    order_details = relationship('OrderDetail', back_populates='order')

    def __repr__(self):
        return f"<Order(OrderId={self.OrderId}, CustomerId={self.CustomerId}, OrderDate='{self.OrderDate}')>"
