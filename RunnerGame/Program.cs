using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Program
{
    static void Main()
    {
        RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Runner 2D");
        window.SetFramerateLimit(60);
        Game game = new Game(window);
        game.Run();
    }
}

