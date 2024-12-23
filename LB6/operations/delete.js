const connectToDatabase = require('../database/connection');

const deleteDocument = async (collectionName, query) => {
    const db = await connectToDatabase("ComputerPartsShopDB");
    const result = await db.collection(collectionName).deleteOne(query);
    console.log(`Deleted documents in ${collectionName}:`, result.deletedCount);
};

module.exports = { deleteDocument };
