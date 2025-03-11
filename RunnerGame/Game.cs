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

        // Charger les textures
        backgroundTexture = new Texture("assets/background.jpg");
        groundTexture = new Texture("assets/ground.png");

        // Configurer le Background
        background = new Sprite(backgroundTexture);
        background.Scale = new Vector2f(
            800f / backgroundTexture.Size.X,
            600f / backgroundTexture.Size.Y
        );

        // Configurer le Sol
        ground = new Sprite(groundTexture);
        ground.Position = new Vector2f(0, 500);
        ground.Scale = new Vector2f(
            800f / groundTexture.Size.X,
            100f / groundTexture.Size.Y
        );

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

        // Afficher le Background
        window.Draw(background);

        // Afficher le Sol
        window.Draw(ground);

        // Afficher les Obstacles
        foreach (Obstacle obs in obstacles)
            obs.Draw(window);

        // Afficher le Joueur
        player.Draw(window);

        // Afficher le Score
        score.Draw(window);

        window.Display();
    }
}

