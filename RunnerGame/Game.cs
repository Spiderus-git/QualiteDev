using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Collections.Generic;

class Game
{
    private RenderWindow window;
    private Player player;
    private List<Obstacle> obstacles = new List<Obstacle>();
    private Clock clock;
    private Texture backgroundTexture, groundTexture;
    private Sprite background, ground;
    private Score score;

    public Game(RenderWindow win)
    {
        window = win;
        player = new Player();
        score = new Score();

        backgroundTexture = new Texture("assets/background.jpg");
        groundTexture = new Texture("assets/ground.png");

        background = new Sprite(backgroundTexture);
        ground = new Sprite(groundTexture);
        ground.Position = new Vector2f(0, 500);

        clock = new Clock();
    }

    public void Run()
    {
        while (window.IsOpen)
        {
            window.DispatchEvents();
            HandleInput();
            Update(clock.Restart().AsSeconds());
            Render();
        }
    }

    private void HandleInput()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            window.Close();

        player.HandleInput();
    }

    private void Update(float deltaTime)
    {
        player.Update(deltaTime);
        score.Update();

        // Gérer les obstacles
        foreach (Obstacle obs in obstacles)
            obs.Update(deltaTime);

        // Ajouter de nouveaux obstacles
        if (obstacles.Count == 0 || obstacles[^1].Position.X < 500)
        {
            obstacles.Add(new Obstacle(800));
        }
    }

    private void Render()
    {
        window.Clear(Color.Cyan);
        window.Draw(background);
        window.Draw(ground);

        foreach (Obstacle obs in obstacles)
            obs.Draw(window);

        player.Draw(window);
        score.Draw(window);
        window.Display();
    }
}
