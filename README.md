# Genshin API
This is a little API I build to perform CRUD operations on a data store I use to store information about characters and gear for a game I often play called [Genshin Impact](https://genshin.mihoyo.com/).

The Swagger documentation can be found at /swagger/index.html

## Database
The app requires you to provide a connection string to a mongo database (see appsettings.json).  This DB is assumed to have 2 collections (artifacts and characters).  The API is not programmed to build it's own DB, since I really think that's an anti-pattern for most development.  While the collections must exist, they can be empty.