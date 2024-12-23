const { MongoClient } = require('mongodb');

const uri = "mongodb://localhost:27017"; 
const client = new MongoClient(uri);

const connectToDatabase = async (dbName) => {
    try {
        await client.connect();
        console.log("Connected to MongoDB!");
        return client.db(dbName); // Повертає підключення до певної БД
    } catch (err) {
        console.error("Error connecting to MongoDB:", err);
        process.exit(1);
    }
};

module.exports = connectToDatabase;
