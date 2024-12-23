const connectToDatabase = require('../database/connection');

const seedDatabase = async () => {
    const db = await connectToDatabase("ComputerPartsShopDB");

    const categories = [
        { _id: "category_id_1", name: "Graphics Cards", description: "High-performance GPUs" },
        { _id: "category_id_2", name: "Processors", description: "High-performance CPUs" }
    ];

    const products = [
        {
            _id: "product_id_1",
            name: "RTX 3080",
            price: 799.99,
            category_id: "category_id_1",
            reviews: [
                { review_id: "review_id_1", rating: 5, comment: "Amazing!", customer_id: "customer_id_1" }
            ]
        }
    ];

    const customers = [
        { _id: "customer_id_1", name: "John Doe", email: "john@example.com", address: "123 Main St", phone: "1234567890" }
    ];

    await db.collection("Categories").insertMany(categories);
    await db.collection("Products").insertMany(products);
    await db.collection("Customers").insertMany(customers);

    console.log("Database seeded!");
    process.exit();
};

seedDatabase();
