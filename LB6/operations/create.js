const connectToDatabase = require('../database/connection');

const createDocument = async (collectionName, document) => {
    const db = await connectToDatabase("ComputerPartsShopDB");
    const result = await db.collection(collectionName).insertOne(document);
    console.log(`Document created in ${collectionName}:`, result.insertedId);
};

module.exports = { createDocument };
