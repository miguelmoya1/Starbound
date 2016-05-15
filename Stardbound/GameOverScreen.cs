class GameOverScreen
{
    public void Run()
    {
        Font font18 = new Font("data/Joystix.ttf", 18);
        Image player = new Image("data/GameOverScreen.png");

        do
        {
            Hardware.ClearScreen();
            Hardware.DrawHiddenImage(player, 0, 0);
            Hardware.ShowHiddenScreen();

            Hardware.Pause(50);
        }
        while (!Hardware.KeyPressed(Hardware.KEY_Q));
    }
}