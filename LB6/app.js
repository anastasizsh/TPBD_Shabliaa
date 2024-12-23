const { createDocument } = require('./operations/create');
const { readDocuments } = require('./operations/read');
const { updateDocument } = require('./operations/update');
const { deleteDocument } = require('./operations/delete');

(async () => {
    console.log("Select an operation:");
    console.log("1. Create a document");
    console.log("2. Read all documents");
    console.log("3. Update a document");
    console.log("4. Delete a document");

    const choice = await new Promise((resolve) => {
        process.stdin.once('data', (data) => resolve(data.toString().trim()));
    });

    switch (choice) {
        case "1":
            await createDocument("Products", {
                _id: "product_1",
                name: "RTX 3080",
                price: 799.99,
                category_id: "category_1"
            });
            break;

        case "2":
            await readDocuments("Products");
            break;

        case "3":
            await updateDocument("Products", { _id: "product_1" }, { price: 699.99 });
            break;

        case "4":
            await deleteDocument("Products", { _id: "product_1" });
            break;

        default:
            console.log("Invalid choice.");
    }

    process.exit();
})();
