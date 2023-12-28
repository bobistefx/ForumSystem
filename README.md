# AutomotiveForumSystem

To access SwaggerUI documentation, launch the program on your local machine and visit:
http://localhost:5000/swagger/index.html

Swagger raw JSON file: 
http://localhost:5000/swagger/v1/swagger.json

How to use the AutomotiveForumSystemAPI:

## Endpoints

### Auth

- **POST /api/auth/login**
  - Description: Log in and obtain an authentication token.
  - Parameters: 
    - `credentials` (Header, username and password split by ':', no spaces): JWT token for authentication.
  - Responses:
    - `200 OK`: Successful login.
   
### Categories

- **GET /api/categories**
  - Description: Get a list of categories.
  - Responses:
    - `200 OK`: Successful operation.

- **GET /api/categories/{id}**
  - Description: Get a category by its ID.
  - Parameters:
    - `id` (Path): The ID of the category to retrieve.
    - `Authorization` (Header): JWT token for authorization. Admin rights required.
  - Responses:
    - `200 OK`: Successful operation. Returns the requested category.
      - Response Body:
        - `CategoryDTO`: The detailed information about the category.
    - `404 Not Found`: The specified category ID was not found.
      - Response Body:
        - `string`: Error message describing the issue.

- **POST /api/categories**
  - Description: Create a new category.
  - Parameters:
    - `Authorization` (Header): JWT token for authorization. Admin rights required.
  - Request Body: CategoryDTO
    - `Name` - required string in the DTO.
  - Responses:
    - `200 OK`: Successful operation. Returns the newly created category.
      - Response Body:
        - `CategoryDTO`: The detailed information about the created category.
    - `400 Bad Request`: A category with the same name already exists.
      - Response Body:
        - `string`: Error message describing the issue.
       
- **PUT /api/categories/{id}**
  - Description: Update an existing category by ID.
  - Parameters:
    - `id` (Path): The ID of the category to update.
    - `Authorization` (Header): JWT token for authorization. Admin rights required.
  - Request Body: CategoryDTO
    - `Name` - required string in the DTO.
  - Responses:
    - `200 OK`: Successful operation. Returns the updated category.
      - Response Body:
        - `CategoryDTO`: The detailed information about the updated category.
    - `400 Bad Request`: A category with the same name already exists.
      - Response Body:
        - `string`: Error message describing the issue.
    - `404 Not Found`: The specified category ID was not found.
      - Response Body:
        - `string`: Error message describing the issue.
       
- **DELETE /api/categories/{id}**
  - Description: Delete an existing category by ID.
  - Parameters:
    - `id` (Path): The ID of the category to delete.
    - `Authorization` (Header): JWT token for authorization. Admin rights required.
  - Responses:
    - `200 OK`: Successful operation. Returns a confirmation message.
      - Response Body:
        - `string`: Confirmation message.
    - `404 Not Found`: The specified category ID was not found.
      - Response Body:
        - `string`: Error message describing the issue.
