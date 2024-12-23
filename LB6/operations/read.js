const connectToDatabase = require('../database/connection');

const readDocuments = async (collectionName) => {
    const db = await connectToDatabase("ComputerPartsShopDB");
    const documents = await db.collection(collectionName).find().toArray();
    console.log(`Documents in ${collectionName}:`, documents);
};

module.exports = { readDocuments };
