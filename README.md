#QuizApp
A simple ASP.NET Core web application for managing and taking quizzes. QuizApp provides functionality to create, read, update, and delete questions, as well as to fetch and display randomized questions for users.

#Features
- Manage Questions
- Add, edit, delete, and list quiz questions.
- Take quiz with randomized questions and a timer, score after submit.
- View submit attempts by time completed.
- Restful APIs to manage and retrieve questions.
- Responsive Frontend

#Installation
###1. Clone the Repository
```bash
Copy code
git clone https://github.com/username/QuizApp.git
cd QuizApp
```

###2. Restore Dependencies
```bash
Copy code
dotnet restore
Set Up the Database
```

###3. Open the appsettings.json file and configure the connection string under "ConnectionStrings":
```json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=QuizApp;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

###4. Apply migrations:
```bash
Copy code
dotnet ef database update
Run the Application
```
```bash
Copy code
dotnet run
```

#Technologies Used
###Backend:
- ASP.NET Core
- Entity Framework Core
###Frontend:
React NextJs
TailwindCSS
###Database:
SQL Server (LocalDB)
