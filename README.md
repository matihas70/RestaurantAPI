# RestaurantAPI(.NET, ASP.NET Core, SQL)
The application store informations about restaurants. It allows to create new restaurants, dishes and managing them. There are procedures in database invoked by .NET program. There are screens from database with that procedures in SQL below. Program contains two controllers, for restaurants and for dishes. Every controller has set of endpoints to manage database.

Data base structure:
- Tables:
  - Addresses: ![obraz](https://user-images.githubusercontent.com/104222527/172222645-60878e8b-728d-44e0-b070-a425f68ccc24.png)

  - Restaurants: ![obraz](https://user-images.githubusercontent.com/104222527/172221333-a4428368-a2e7-47e9-9622-c62d2ce0cc87.png)

  - Dishes: ![obraz](https://user-images.githubusercontent.com/104222527/172447742-fbf99bdc-6945-48bb-aa84-8210e7cca43c.png)

Controllers:
1. Restaurant (Route: /restaurant):

  - GET: 
    - default: Download all restaurants from database. SQL Procedure:  
    ![image](https://user-images.githubusercontent.com/104222527/177623076-b493b799-fc3b-4eca-add1-12198f6ac6db.png)

    - /<restaurant_id>: returns the restaurant by id. SQL Procedure:  
    ![image](https://user-images.githubusercontent.com/104222527/177624798-ef2a17f2-7b20-4ecb-9a83-44f6746aa77c.png)


  - POST:
    - default: Creates restaurant by informations from request body. SQL Procedure:  
    ![image](https://user-images.githubusercontent.com/104222527/177625533-9c62516f-8c33-4cf1-90a6-52b04563c579.png)
    
    Request body template:
```
{
    "name": "<name>",
    "category": "<category>",
    "email": "<email>",
    "city":"<city>",
    "street": "<street>",
    "postal_code": "<code>"
    }
}
```
    
  - PUT:
    - /<restaurant_id>: Update restaurant address by restaurant id. SQL Procedure:  
    ![image](https://user-images.githubusercontent.com/104222527/177626092-9603453a-2260-4e68-b7d4-13fa608442d4.png)
    
    Request body tempate:
    ```
    {
        "city": "<city>",
        "street": "<street>",
        "postal_code": "<postal_code>"
    }
    ```
  - DELETE:
    - /<restaurant_id>: Remove restaurant from database by restaurant id. SQL Procedure:  
    ![image](https://user-images.githubusercontent.com/104222527/177628747-1368cd2f-b581-45ed-954a-da1b8725bee1.png)

2. Dish (Route: /<restaurant_id>/dish>):
  
  - GET:
    - default: download all dishes from the restaurant by id from main route. SQL Procedure:  
    ![image](https://user-images.githubusercontent.com/104222527/177629815-3e50bf1b-d189-42d0-bf79-605cf52b78c5.png)
    
  - POST:
    - default: Create dish by informations from body request. SQL Procedure:  
    ![image](https://user-images.githubusercontent.com/104222527/177630361-5e1f8f1f-5ddc-4136-8b1b-7d7c25bcbc0e.png)
    
  - DELETE:
    - /<dish_id>: Remove dish by id. SQL Procedure:  
    ![image](https://user-images.githubusercontent.com/104222527/178098177-15807b58-2380-4035-a955-97a9dc8fe6a1.png)
