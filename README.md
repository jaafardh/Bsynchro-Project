# Bsynchro-Project
Hello 
To run the app well you must make these steps:
1. Create local database and put the connection string in appsetting.json file in both Bysnchro.Web and Bysnchro.Api projects and in OnConfiguring methode in
   Bysnchro.InfraStructure.Data.BsynchroDatabaseContext.cs file
2. Then excute in the nuget console the command: 'update-database'
3. Now the database will created
4. Then, run Bysnchro.Web, a default values will added to the database automatically
5. If you run Bysnchro.Api, you can test the api using swagger
6. Also, you can run the Test project to test add account controller
7. Bysnchro.Web consists two pages 1st for testing Add Account and the second for get user info.

Note: I didn't know Angular,Typescipt and how to launch CI/CD using GitHub before. So, sorry for the simple frontend design. 
Thank you.
Mohammad Jaafar Dhainy
