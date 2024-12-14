# Password Manager API

## Description
This project is a backend API for managing user passwords. It allows users to store, retrieve, update, and delete encrypted passwords. The passwords are encrypted using Base64 encoding.

## Tech Stack
- C#
- .NET

## Features
1. Add the user's encrypted password to the list of stored passwords
2. Get the list of all passwords for the user
3. Get a single item from the password store
4. Get a single item from the password store with the password decrypted
5. Update a password store item
6. Delete a password store item

## Password Storage Format
Passwords are stored in the following format:
```json
[
    { "id": 1, "category": "work", "app": "outlook", "userName": "testuser@mytest.com", "encryptedPassword": "TXlQYXNzd29yZEAxMjM=" },
    { "id": 2, "category": "school", "app": "messenger", "userName": "testuser@mytest.com", "encryptedPassword": "TmV3UGFzc3dvcmRAMTIz" }
]
```

## Encryption
Passwords are encrypted using Base64 encoding (ASCII <=> Base64).

## Setup
Clone the repository:
```bash
git clone https://github.com/s32mkoup/password-manager-api.git
cd password-manager-api
```

Install dependencies:
```bash
dotnet restore
```

Build the project:
```bash
dotnet build
```

Run the project:
```bash
dotnet run
```

The API will be available at `https://localhost:7113` or `http://localhost:5251`.

## In-Memory Database
This project uses an in-memory database for storing passwords. The in-memory database is set up using the `System.Runtime.Caching` library. 

## Endpoints
- `GET /api/password` - Get all passwords
- `GET /api/password/{id}` - Get a password by ID
- `GET /api/password/{id}/decrypt` - Get a decrypted password by ID
- `POST /api/password` - Create a new password
- `PUT /api/password/{id}` - Update a password by ID
- `DELETE /api/password/{id}` - Delete a password by ID

