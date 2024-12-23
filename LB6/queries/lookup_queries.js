const connectToDatabase = require('../database/connection');

const fetchProductsWithCategories = async () => {
    const db = await connectToDatabase("ComputerPartsShopDB");

    const result = await db.collection("Products").aggregate([
        {
            $lookup: {
                from: "Categories",
                localField: "category_id",
                foreignField: "_id",
                as: "category_info"
            }
        }
    ]).toArray();

    console.log(result);
};

fetchProductsWithCategories();
