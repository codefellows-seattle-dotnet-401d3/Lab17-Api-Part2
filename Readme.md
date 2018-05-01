# Honeydew API
This is a simple, locally hosted API. When running, it will allow the user to request Todo items or a Todo list. Currently, a todo item can only be associated in one todo list. There are a few prebuilt silly examples.

## Use
To use this API, you will need to download the code and set up a local sql server. In visual studio 2017, with the sql server workload included, this is as simple as opening the nu-get package console and inputing:
```
Add-Migration Initial
Update-Database
```
At this point, you should be able to check if the database exists using the sql server explorer. If it does, your API is now ready to run!


Run the code. A browser page will pop up with a default 404 not found. Because this API has no front end, that is the expected, successful result. Look in the URL at the top of that page. It should read something like ``` localhost:1234/ ``` Copy and paste that whole thing into the tool you are using to send requests to the API (tested with postman, but anything should work). At that endpoint, firing a get request, the response body should look like:
```
//request was get method at http://localhost:56645/api/todo/
[
    {
        "name": "Survive",
        "complete": false,
        "id": 5,
        "belongs": {
            "id": 5,
            "name": "I am a potato"
        }
    },
    {
        "name": "Feed the Dog",
        "complete": true,
        "id": 6,
        "belongs": {
            "id": 6,
            "name": "Daily"
        }
    },
    {
        "name": "Know Thyself",
        "complete": false,
        "id": 7,
        "belongs": {
            "id": 6,
            "name": "Daily"
        }
    }
]

```

This is built based on the default data. It will change to match your own interactions with the API. If everything is set up, it should persist.

### endpoints:
note: 2018-0412, Posts are returning "bad request" status codes but still posting successfully.
#### Todo
- GET ```todo``` gets all the Todo items
- GET ```todo/{id}``` gets the Todo item associated with that id number
- POST ```todo``` A valid Todo item with all fields complete (except id, which will be auto generated if not provided) must be included in the body of the request. Otherwise, your will get a bad request error. This will add a Todo. NOTE: the Todolist associated must already exist.
- DELETE ```todo/{id}``` deletes the item with a matching id
#### Todolist
- GET ```todolist``` gets all the Todolist items
- GET ```todolist/{id}``` gets the Todolist associated with that id number
- POST ```todolist``` A valid Todolist item with all fields complete (except id, which will be auto generated if not provided) must be included in the body of the request. Otherwise, your will get a bad request error.
- DELETE ```todolist/{id}``` deletes the item with a matching id. If a single todo item continues to reference the ID, it will throw an exception and break instead of deleting.

## Architecture
This uses:
- .net core entity framework
- .net core mvc
- sql server

The data is stored in a database. It will be seeded upon first initialization.

## version
2018-04-12 v1.0 App works on my machine. Seeing if works elsewhere. Coming next: update.