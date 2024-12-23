const { MongoClient } = require('mongodb');

const uri = "mongodb://localhost:27017";
const client = new MongoClient(uri);

const connectToDatabase = async (dbName) => {
    try {
        await client.connect();
        console.log("Connected to MongoDB!");
        return client.db(dbName);
    } catch (err) {
        console.error("Error connecting to MongoDB:", err);
    }
};

module.exports = connectToDatabase;
