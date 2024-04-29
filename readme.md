# Insurance Application Backend Documentation

## Database (iInsurance)

The database named iInsurance consists of 4 tables:

1. **user_info**: 
   - Columns: username, email, password
   - Purpose: Stores user login information and other user-related data.
   
2. **card_info**: 
   - Columns: [all card details]
   - Purpose: Stores confidential card information. Operations on cards are performed using procedures to ensure transaction security.
   
3. **policies**: 
   - Columns: [policy information], user_id (Foreign Key)
   - Purpose: Stores policy information associated with users. Uses a Foreign Key constraint to map policies to users in a many-to-one relationship.
   
4. **login_log**: 
   - Columns: loginid, user_id (Foreign Key), time, login_success
   - Purpose: Records user login activities. 

Constraints are added to each table column to ensure data integrity.

## Backend

The Database Context is developed using Entity Framework (EF) to connect to the database.

### Controllers

1. **LoginController**:
   - Endpoint: `/api/login`
   - Functionality: Calls the `user_login` procedure from the database. Both GET and POST methods are implemented, with POST being the primary method. Parameters are passed in the request body.
   
2. **PoliciesController**:
   - Endpoint: `/api/policies`
   - Functionality: 
     - `GetPoliciesByUserId`: Returns all policies associated with the user.
     - `getAll`: Returns all policies without specifying the associated user.
   
3. **CardController**:
   - Endpoint: `/api/card`
   - Functionality: Implements CRUD (Create, Read, Update, Delete) operations for card information.
     - Each operation calls a corresponding stored procedure using EF's `ExecuteSqlRawAsync()` method, passing parameters.
   
### Error Handling
Proper return types are defined to ensure meaningful error messages are provided in case of errors.

## Frontend
No views have been created for the backend. Frontend is handled through Angular.
