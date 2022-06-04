# RestaurantAPI
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
Id has to be "0", it will get the right id in data base.
