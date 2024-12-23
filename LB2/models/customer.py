from sqlalchemy import Column, Integer, String
from sqlalchemy.orm import relationship
from db_setup import Base

class Customer(Base):
    __tablename__ = 'Customers'
    CustomerId = Column(Integer, primary_key=True, autoincrement=True)
    FullName = Column(String(100), nullable=False)
    Email = Column(String(100), unique=True, nullable=False)

    orders = relationship('Order', back_populates='customer')

    def __repr__(self):
        return f"<Customer(CustomerId={self.CustomerId}, FullName='{self.FullName}', Email='{self.Email}')>"
