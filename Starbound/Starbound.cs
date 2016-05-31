class Starbound
{
    static void Main()
    {
        bool fullScreen = false;
        Hardware.Init(1024, 576, 24, fullScreen);
        bool finished = false;

        while (!finished)
        {
            WelcomeScreen welcome = new WelcomeScreen();
            welcome.Run();

            if (welcome.GetOptionChosen() == WelcomeScreen.options.Play)
            {
                Game myGame = new Game();
                myGame.Run();
            }

            if (welcome.GetOptionChosen() == WelcomeScreen.options.Quit)
                finished = true;
        }
    }
}
