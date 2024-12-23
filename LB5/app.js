const seedDatabase = require('./scripts/seed_data');
const fetchProductsWithCategories = require('./queries/lookup_queries');
const fetchProductsWithHighRating = require('./queries/array_queries');
const calculateTotalSales = require('./queries/aggregate_queries');

(async () => {
    console.log("Choose an option:");
    console.log("1. Seed database");
    console.log("2. Fetch products with categories");
    console.log("3. Fetch products with high rating");
    console.log("4. Calculate total sales");

    const choice = parseInt(await new Promise(res => {
        process.stdin.once('data', data => res(data.toString().trim()));
    }));

    switch (choice) {
        case 1:
            await seedDatabase();
            break;
        case 2:
            await fetchProductsWithCategories();
            break;
        case 3:
            await fetchProductsWithHighRating();
            break;
        case 4:
            await calculateTotalSales();
            break;
        default:
            console.log("Invalid choice.");
    }

    process.exit();
})();
