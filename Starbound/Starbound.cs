class Starbound
{
    static void Main()
    {
        bool fullScreen = false;
        Hardware.Init(1024, 576, 24, fullScreen);
        bool finished = false;
        Sound sound = new Sound("data/Starbound.mp3");

        

        while (!finished)
        {
            sound.SetSound("data/Starbound.mp3");
            sound.PlayAll();
            WelcomeScreen welcome = new WelcomeScreen();
            welcome.Run();
            sound.Stop();

            if (welcome.GetOptionChosen() == WelcomeScreen.options.Play)
            {
                sound.SetSound("data/InGame.mp3");
                sound.PlayAll();
                Game myGame = new Game();
                myGame.Run();
            }

            if (welcome.GetOptionChosen() == WelcomeScreen.options.Quit)
                finished = true;
        }
    }
}
