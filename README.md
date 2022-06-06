# RestaurantAPI
Data base structure:
- Tables:
  - Addresses: ![obraz](https://user-images.githubusercontent.com/104222527/172221793-25da99df-9b7e-4677-b0e4-a3055d3c44b6.png)

  - Restaurants: ![obraz](https://user-images.githubusercontent.com/104222527/172221333-a4428368-a2e7-47e9-9622-c62d2ce0cc87.png)
  - Dishes:

Endpoints: 

- GET: 
  - default: Say "Hello user"
  - "restaurants": Returns all restaurants from data base as JSON

- POST
  - "addRestaurant": Takes informations about restaurant from request body as JSON. Template:
```
{
    "id": 0,
    "name": "<name>",
    "category": "<category>",
    "email": "<email>",
    "address":{
        "id": 0,
        "city":"<city>",
        "street": "<street>",
        "postal_code": "<code>"
    }
}
```
Id has to be "0", it will get the right id in data base automatically. There is a trigger in data base which returns id given to the address. This id is set as a foreign key for the restaurant.
- DELETE
  - It has to be send id of wanted restaurant to remove. its taken from query "id". There is a trigger in data base which remove removed restaurant address.
