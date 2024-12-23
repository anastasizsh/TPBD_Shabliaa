module.exports = {
    _id: "string", // ID замовлення
    customer_id: "string",
    products: [
        {
            product_id: "string",
            quantity: "number",
            price: "number"
        }
    ],
    order_date: "date",
    total_price: "number"
};
