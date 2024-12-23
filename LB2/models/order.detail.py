from sqlalchemy import Column, Integer, ForeignKey
from sqlalchemy.orm import relationship
from db_setup import Base

class OrderDetail(Base):
    __tablename__ = 'OrderDetails'
    OrderDetailId = Column(Integer, primary_key=True, autoincrement=True)
    OrderId = Column(Integer, ForeignKey('Orders.OrderId'))
    ProductId = Column(Integer, ForeignKey('Products.ProductId'))
    Quantity = Column(Integer)

    order = relationship('Order', back_populates='order_details')
    product = relationship('Product', back_populates='order_details')

    def __repr__(self):
        return f"<OrderDetail(OrderDetailId={self.OrderDetailId}, OrderId={self.OrderId}, ProductId={self.ProductId}, Quantity={self.Quantity})>"
