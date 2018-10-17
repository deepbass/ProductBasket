This project can either use cosmos db or fall back on in memory data storage.

Download Cosmos DB emulator, or use a real cosmos db instance.

https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator

Insert this document into a collection: 

{
    "id": "1",
    "Products": [
        {
            "Id": "bread",
            "DisplayName": "Bread",
            "PriceInPounds": 1,
            "Quantity": 0
        },
        {
            "Id": "butter",
            "DisplayName": "Butter",
            "PriceInPounds": 0.8,
            "Quantity": 0
        },
        {
            "Id": "milk",
            "DisplayName": "Milk",
            "PriceInPounds": 1.1,
            "Quantity": 0
        }
    ]
}


update App settings in web.config:
<add key="CosmosDBEndpoint" value="https://localhost:8081" />
<add key="PrimaryKey" value="yourkeyhere" />
<add key="DatabaseName" value="decisiontechtest" />
<add key="CollectionName" value="products" />

