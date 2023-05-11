# MeetupAPI

An ASP.NET Core Web-API project for managing meetups.

All users are allowed to get all meetups and get meetups by id, but only authorized users can create and update/delete their own meetups.

## Endpoints

<details><summary>Meetup</summary>

  `GET` **Get all meetups**  - *{host}/api/meetups/*
  
  `GET` **Get meetup by id**  - *{host}/api/meetups/{id}*
  
  `POST` **Create meetup**  - *{host}/api/meetups/*
  
  `PUT` **Update meetup**  - *{host}/api/meetups/*
  
  `DELETE` **Delete meetup** - *{host}/api/meetups/{id}*

</details>

<details><summary>Auth</summary>
  
  `POST` **Register** - *{host}/api/auth/register*
  
  `POST` **Login** - *{host}/api/auth/login*
  
  `GET` **Refresh token** - *{host}/api/auth/refresh-token*

</details>


## API requests examples
    
  <details>
  <summary>Get all meetups</summary>
  
  ### Request

  `GET /api/meetups/`

  ### Response
  ```
  [
    {
      "id": 1,
      "name": ".NET open meetup",
      "description": "...",
      "organizer": {
        "username": "Igor"
      },
      "place": "Gomel, Belarus",
      "time": "2023-06-01T12:00:00"
    },
    {
      "id": 2,
      "name": "JS meetup",
      "description": "We will discuss js for juniors and for non-js developers.",
      "organizer": {
        "username": "Vasya"
      },
      "place": "Minsk, Belarus",
      "time": "2023-06-20T15:00:00"
    }
]
  ```
  </details>

  <details>
    <summary>Get meetup by id</summary>

  ### Request

  `GET /api/meetups/1`

  ### Response
  ```
    {
      "id": 1,
      "name": ".NET open meetup",
      "description": "...",
      "organizer": {
        "username": "Igor"
      },
      "place": "Gomel, Belarus",
      "time": "2023-06-01T12:00:00"
    }
  ```
  </details>

  <details>
    <summary>Get a non-existent meetup</summary>

  ### Request

  `GET /api/meetups/55`

  ### Response
  ```
  {
      "error": "Meetup not found."
  }
  ```
  </details>

  <details>
    <summary>Create meetup</summary>

  ### Request

  `POST /api/meetups/`
  #### Body
  ```
  {
    "name": "new meetup",
    "description": "something",
    "place": "Minsk, Belarus",
    "time": "2023-05-25T16:00:00.000Z"
  }
  ```

  ### Response
  ```
  {
    "id": 3,
    "name": "new meetup",
    "description": "something",
    "organizer": {
      "username": "Vasya"
    },
    "place": "Minsk, Belarus",
    "time": "2023-05-25T16:00:00Z"
}
  }
  ```
  </details>

  <details>
    <summary>Update meetup</summary>

  ### Request

  `PUT /api/meetups/`
  #### Body
  ```
  {
      "id": "3",
      "name": "new meetup",
      "description": "something else",
      "place": "Minsk, Belarus",
      "time": "2023-05-25T16:00:00.000Z"
  }
  ```

  ### Response
  ```
  {
    "id": 1005,
    "name": "new meetup",
    "description": "something else",
    "organizer": {
      "username": "Vasya"
    },
    "place": "Minsk, Belarus",
    "time": "2023-05-25T16:00:00Z"
  }
  
  ```
  </details>

  <details>
    <summary>Delete meetup</summary>

  ### Request

  `DELETE /api/meetups/3`

  ### Response 
  <sup>(204 No Content)</sup>
</details>

## Installation details

- Change the connection string (in appsettings.json)
- Run `dotnet ef database update` command
- Set user secret for JWT:Key (in secrets.json or via CLI)
```
{
  "JWT": {
    "Key": "6gAiq^DrbL6R&cbj"
  }
}
```
- Ignore all exceptions that pauses the application (they all are handled)
