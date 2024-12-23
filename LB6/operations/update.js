const connectToDatabase = require('../database/connection');

const updateDocument = async (collectionName, query, updateFields) => {
    const db = await connectToDatabase("ComputerPartsShopDB");
    const result = await db.collection(collectionName).updateOne(query, { $set: updateFields });
    console.log(`Updated documents in ${collectionName}:`, result.modifiedCount);
};

module.exports = { updateDocument };
