class WelcomeScreen
{
    public enum options { Play, Quit };
    private options optionChosen;

    public void Run()
    {
        optionChosen = options.Play;
        Image ws = new Image("data/WelcomeScreen.png");
        Image bg = new Image("data/bg.jpg");
        Image single = new Image("data/SinglePlayer.png");
        Image quit = new Image("data/Quit.png");
        Font font18 = new Font("data/Joystix.ttf", 18);


        bool validOptionChosen = false;

        do
        {
            Hardware.ClearScreen();
            Hardware.DrawHiddenImage(bg, 0, 0);
            Hardware.DrawHiddenImage(ws, 130, 50);
            Hardware.DrawHiddenImage(single, 0, 450);
            Hardware.DrawHiddenImage(quit, 0, 500);
            Hardware.WriteHiddenText("Clic W/A/S/D to move and clic to break the floor",
                    20, 10,
                    0xCC, 0xCC, 0xCC,
                    font18);
            Hardware.WriteHiddenText("Clic ESC to open exit menu when you are at game",
                    20, 30,
                    0xCC, 0xCC, 0xCC,
                    font18);
            Hardware.ShowHiddenScreen();
           

            if (Mouse.ColisionWith(0, 450, 334, 495, true))
            {
                validOptionChosen = true;
                optionChosen = options.Play;
            }
            if (Mouse.ColisionWith(0, 500, 187, 543, true) ||
                Hardware.KeyPressed(Hardware.KEY_Q))
            {
                validOptionChosen = true;
                optionChosen = options.Quit;
            }

            Hardware.Pause(20);
        }
        while (!validOptionChosen);
    }
    public options GetOptionChosen()
    {
        return optionChosen;
    }
}