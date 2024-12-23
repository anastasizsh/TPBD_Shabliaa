const connectToDatabase = require('../database/connection');

const fetchProductsWithHighRating = async () => {
    const db = await connectToDatabase("ComputerPartsShopDB");

    const result = await db.collection("Products").find({
        "reviews.rating": 5
    }).toArray();

    console.log(result);
};

fetchProductsWithHighRating();
