#  Technical Test - User Contact Registration API

This project is a **.NET 8 Web API** that allows you to register and manage users' contact information, including their **name, phone number, country, department, municipality, and address**.  
It uses **PostgreSQL** as the relational database and follows clean architecture practices with separation into **Application**, **Domain**, and **Infrastructure** layers.

---

## Technologies Used

- **.NET 8**
- **C#**
- **Entity Framework Core (for DB connections)**
- **PostgreSQL**
- **Docker**
- **Swagger (OpenAPI)**
- **Dependency Injection**
- **Stored Procedures** for database interactions

---

##  Project Structure


Technical_Test_Coink/
│
├── .dockerignore
├── .gitignore
├── Dockerfile
├── README.md
│
└── code/
├── UserContactRegistration.Application/
│ ├── Controllers/
│ ├── Exceptions/
│ ├── Attributes/
│ ├── appsettings.json
│ ├── Program.cs
│ └── UserContactRegistration.Application.csproj
│
├── UserContactRegistration.Domain/
│ └── (Entities, Models, Interfaces)
│
├── UserContactRegistration.Infrastructure/
│ └── (Repositories, DTOs, PostgreSQL settings)
│
└── UserContactRegistration.sln


---

##  Environment Configuration

The environment variables for the database connection are defined inside the file:


### Example:
```json
{
  "PostgreSettings": {
    "Host": "localhost",
    "Port": "5432",
    "Db": "app_contacts_db",
    "DbUser": "postgres",
    "DbPassword": "12345"
  }
}
---

#### Run the Application with Docker
1⃣ Build the Docker image

docker build -t technical_test_coink .


2️ Run the container

docker run -d -p 9291:9291 --name coink_api technical_test_coink

Check if it is running
docker ps


You should see something like:
CONTAINER ID   IMAGE                  COMMAND                  STATUS         PORTS
abcd1234efgh   technical_test_coink   "dotnet UserContactR…"   Up 5 seconds   0.0.0.0:9291->9291/tcp


Access the API Documentation

Once the container is running, open your browser and go to:

http://localhost:9291/swagger

Here you can test all endpoints directly from the Swagger UI.

API Endpoints

| Method     | Endpoint              | Description             |
| ---------- | --------------------- | ----------------------- |
| **GET**    | `/api/user/all-users` | Retrieve all users      |
| **GET**    | `/api/user/{id}`      | Get user by ID          |
| **POST**   | `/api/user/register`  | Register a new user     |
| **PUT**    | `/api/user/update`    | Update an existing user |
| **DELETE** | `/api/user/{id}`      | Delete a user by ID     |


Database Schema
Main Table

users
| Column          | Type        | Description        |
| --------------- | ----------- | ------------------ |
| user_id         | BIGSERIAL   | Primary key        |
| document_number | VARCHAR(50) | Unique identifier  |
| first_name      | TEXT        | User’s first name  |
| last_name       | TEXT        | User’s last name   |
| email           | TEXT        | Unique email       |
| created_at      | TIMESTAMP   | Creation timestamp |


Catalog Tables

countries

departments

municipalities

These tables are referenced in user registration to maintain referential integrity.

Stored Procedures

All database operations (insert, update, delete, select) are implemented using PostgreSQL Stored Procedures to ensure clean and controlled data access.

Example:

CREATE OR REPLACE PROCEDURE sp_register_user(
    p_first_name TEXT,
    p_last_name TEXT,
    p_phone TEXT,
    p_country_id INT,
    p_department_id INT,
    p_municipality_id INT,
    p_address TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO users(first_name, last_name, phone, country_id, department_id, municipality_id, address)
    VALUES (p_first_name, p_last_name, p_phone, p_country_id, p_department_id, p_municipality_id, p_address);
END;
$$;


Development Setup (Without Docker)

If you want to run it locally:

cd code/UserContactRegistration.Application
dotnet restore
dotnet build
dotnet run


Then open:
 http://localhost:9291/swagger


Author

José Bonilla
Backend Developer (.NET & PostgreSQL)
