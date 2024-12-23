from db_setup import create_session
from models import Category, Product, Customer, Order, OrderDetail

def show_menu():
    print("\n--- Меню ---")
    print("1. Показати всі категорії")
    print("2. Показати всі товари")
    print("3. Показати всіх клієнтів")
    print("4. Показати всі замовлення")
    print("5. Показати деталі замовлень")
    print("6. Вийти")

def display_data(data):
    for item in data:
        print(item)

def main():
    session = create_session()
    while True:
        show_menu()
        choice = input("Оберіть опцію: ")

        if choice == '1':
            categories = session.query(Category).all()
            display_data(categories)
        elif choice == '2':
            products = session.query(Product).all()
            display_data(products)
        elif choice == '3':
            customers = session.query(Customer).all()
            display_data(customers)
        elif choice == '4':
            orders = session.query(Order).all()
            display_data(orders)
        elif choice == '5':
            order_details = session.query(OrderDetail).all()
            display_data(order_details)
        elif choice == '6':
            break
        else:
            print("Невірний вибір. Спробуйте ще раз.")

if __name__ == "__main__":
    main()
