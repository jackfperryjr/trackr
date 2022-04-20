# EXP tracking application

<img src="screenshots/Screenshot 2022-04-20 120620.png" alt="Main Page" title="Main Page">
<img src="screenshots/Screenshot 2022-04-20 120644.png" alt="Character Graphs" title="Character Graphs">
<img src="screenshots/Screenshot 2022-04-20 130054.png" alt="Character Update" title="Character Update">
<img src="screenshots/Screenshot 2022-04-20 130118.png" alt="Character Add" title="Character Add">

If you wanted to use this application to track your own DragonRealms character's experience progressions you'll need to complete a few things first:  

- You'll have to add a default `appsettings.json` to the root of this application. The default `appsettings.json` looks like this:  

 ```json
    {
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*"
    }

```
- You'll have to set up a SQL database, locally or in a cloud environment, and add that connection string to your `appsettings.json`.

```json
    "ConnectionStrings": {
        "Default": "Data Source=cecil\\sqlexpress; Initial Catalog=playgroud; Integrated Security=True"
    }
```

- Once those steps are completed, you can run `dotnet ef database update` to build the tables necessary.

## Bonus

In addition to the forms to add/update characters and their experience, there's an API controller to do the same. The caveat there is that you'll have to run your experience string through a tool to escape special characters. I used this: https://www.freeformatter.com/json-escape.html

- To add a character via Postman you can make a POST request and use their name as an attribute in the route as seen below. This won't add necessary data for the character table, though.
<img src="screenshots/Screenshot 2022-04-20 130946.png" alt="Postman Character" title="Postman Character">
- To update, you make the same request but with PUT request.
<img src="screenshots/Screenshot 2022-04-20 125823.png" alt="Postman Character" title="Postman Character">
- Additionally, if you need to backdate experience you can add a query parameter of `date` to the same PUT request. Otherwise, the current date is used.
<img src="screenshots/Screenshot 2022-04-20 125849.png" alt="Postman Character" title="Postman Character">



