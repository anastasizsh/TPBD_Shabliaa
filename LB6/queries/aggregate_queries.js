const connectToDatabase = require('../database/connection');

const calculateTotalSales = async () => {
    const db = await connectToDatabase("ComputerPartsShopDB");

    const result = await db.collection("Orders").aggregate([
        {
            $group: {
                _id: null,
                totalSales: { $sum: "$total_price" }
            }
        }
    ]).toArray();

    console.log(result);
};

calculateTotalSales();
