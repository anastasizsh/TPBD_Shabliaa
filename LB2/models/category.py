from sqlalchemy import Column, Integer, String
from sqlalchemy.orm import relationship
from db_setup import Base

class Category(Base):
    __tablename__ = 'Categories'
    CategoryId = Column(Integer, primary_key=True, autoincrement=True)
    Name = Column(String(100), nullable=False)

    products = relationship('Product', back_populates='category')

    def __repr__(self):
        return f"<Category(CategoryId={self.CategoryId}, Name='{self.Name}')>"
