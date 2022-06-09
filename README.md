# RestaurantAPI
The application store informations about restaurants. It allows to create new restaurants, dishes, and download informations from database.

Data base structure:
- Tables:
  - Addresses: ![obraz](https://user-images.githubusercontent.com/104222527/172222645-60878e8b-728d-44e0-b070-a425f68ccc24.png)

  - Restaurants: ![obraz](https://user-images.githubusercontent.com/104222527/172221333-a4428368-a2e7-47e9-9622-c62d2ce0cc87.png)

  - Dishes: ![obraz](https://user-images.githubusercontent.com/104222527/172447742-fbf99bdc-6945-48bb-aa84-8210e7cca43c.png)

Endpoints: 

- GET: 
  - default: Say "Hello user".
  - "restaurants": Returns all restaurants from data base as JSON.
  - "dishes?id=<restaurant_id>": Returns all dishes in restaurant that id is in query.

- POST
  - "addRestaurant": It gets informations about restaurant from request body as JSON. Template:
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
  - "addDish?id=<restaurant_id>": It gets informations about dish from request body as JSON, and id of restaurant where dish is served from query. Request body template:
```
{
    "id": 0,
    "name": "<name>",
    "type": "<type>",
    "price": <price>,
    "restaurant": null
}
```
- DELETE
  - "removeRestaurant?id=<restaurant_id>" It get id from query "id". There is a trigger in data base which remove removed restaurant address.
  - "removeDish?id=<dish_id>" It get id from query "id".
