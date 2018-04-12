# Lab17 Todo_Api

## Todo List Api
This is a ToDo list api that contains both tasks and assigned
days. Below you can find examples of API calls for both the
todo items and the weekday.

## Tools Used
Microsoft Visual Studio Community Version 15.5.7

C#

ASP.Net Core

Postman

## Getting Started

Clone this repository to your local machine.
```
$ git clone 
```
Once downloaded, cd into the ```Lab17-Api-Part2``` directory.
```
$ cd Lab17-Api-Part2
```
The cd into ```Lab17-Api-Part2``` directory.
```
$ cd Lab17-Api-Part2
```
The cd into the second ```Todo_Api``` directory.
```
$ cd Todo_Api
```
Then run .NET build.
```
$ dotnet build
```
Once that is complete, run the program.
```
$ dotnet run
```

## API Calls

### Get

To get all todo items:

```http://localhost:XXXXX/api/todo```

To get todo item by ID:

```http://localhost:XXXXX/api/todo/X```

To get all WeekDay items:

```http://localhost:XXXXX/api/weekday```

To get WeekDay item by ID:

```http://localhost:XXXXX/api/weekday/X```

## Add
Create a new todo item:

```http://localhost:XXXXX/api/todo```

JSON example:

```
    {
        "name": "Take down christmas decorations",
        "isComplete": true
    }
```

Create a new WeekDay:

```http://localhost:XXXXX/api/weekday```

JSON example:

```
    {
        "day": "Monday",
    }
```

## Edit
Update existing item:

```http://localhost:XXXXX/api/todo/X```

JSON Example:
```
{
    "id": X,
    "name": "Take down christmas decorations",
    "isComplete": false
}
```

Update existing weekday item:

```http://localhost:XXXXX/api/weekday/X```

JSON Example:
```
{
    "id": X,
    "day": "Monday",
}
```

## Delete
Delete an item:

```http://localhost:XXXXX/api/todo/X```

Delete a weekday:

```http://localhost:XXXXX/api/weekday/X```