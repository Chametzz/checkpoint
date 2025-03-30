DB.SETDATABASE(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "database.db"));

C_Employees employees = new C_Employees();
C_Games games = new C_Games();
C_Prizes prizes = new C_Prizes();
C_Playcards playcards = new C_Playcards();
Controller.layout.wind.Execute();