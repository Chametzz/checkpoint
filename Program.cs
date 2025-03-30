DB.SETDATABASE(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "database.db"));

int screenWidth = Console.WindowWidth;
int screenHeight = Console.WindowHeight;

Thread resize = new Thread(() => {
    while(true) {
        if(Console.WindowWidth != screenWidth || Console.WindowHeight != screenHeight) {
            screenWidth = Console.WindowWidth;
            screenHeight = Console.WindowHeight;
            Controller.layout.wind.RefreshPage();
        }
        Thread.Sleep(1000/60);
    }
});
resize.Start();

C_Employees employees = new C_Employees();
C_Games games = new C_Games();
C_Prizes prizes = new C_Prizes();
C_Playcards playcards = new C_Playcards();
Controller.layout.wind.Execute();