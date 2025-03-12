using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;

class Game
{
    private RenderWindow window;
    private Player player;
    private List<Obstacle> obstacles = new List<Obstacle>();
    private Clock clock;
    private Texture backgroundTexture, groundTexture;
    private Sprite background, ground;
    private Score score;
    private bool isPlaying = false;
    private Font font;
    private Text playText, scoreText, quitText;
    private List<string> recentScores = new List<string>();

    public Game(RenderWindow win)
    {
        window = win;
        window.SetFramerateLimit(60);
        player = new Player();
        score = new Score();

        backgroundTexture = new Texture("assets/background.jpg");
        groundTexture = new Texture("assets/ground.png");

        background = new Sprite(backgroundTexture);
        background.Scale = new Vector2f(
            window.Size.X / (float)backgroundTexture.Size.X,
            window.Size.Y / (float)backgroundTexture.Size.Y
        );

        ground = new Sprite(groundTexture);
        ground.Position = new Vector2f(0, 500);
        ground.Scale = new Vector2f(
            window.Size.X / (float)groundTexture.Size.X,
            100f / groundTexture.Size.Y
        );

        font = new Font("assets/font.ttf");
        playText = new Text("Jouer", font, 30) { Position = new Vector2f(350, 200), FillColor = Color.White };
        scoreText = new Text("Score", font, 30) { Position = new Vector2f(350, 300), FillColor = Color.White };
        quitText = new Text("Quitter", font, 30) { Position = new Vector2f(350, 400), FillColor = Color.White };

        clock = new Clock();
        LoadScores();
    }

    public void Run()
    {
        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear(Color.Black);

            if (!isPlaying)
            {
                ShowMenu();
            }
            else
            {
                float deltaTime = clock.Restart().AsSeconds();
                HandleInput();
                Update(deltaTime);
                Render();
            }

            window.Display();
        }
    }

    private void ShowMenu()
    {
        window.Clear(Color.Black);
        window.Draw(playText);
        window.Draw(scoreText);
        window.Draw(quitText);
        window.Display();

        while (!isPlaying && window.IsOpen)
        {
            window.DispatchEvents();
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePos = Mouse.GetPosition(window);
                if (playText.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
                {
                    isPlaying = true;
                    clock.Restart();
                    return;
                }
                else if (scoreText.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
                {
                    ShowScores();
                    return;
                }
                else if (quitText.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
                {
                    window.Close();
                    return;
                }
            }
        }
    }

    private void ShowScores()
    {
        window.Clear(Color.Black);
        int y = 100;
        foreach (var scoreEntry in recentScores)
        {
            Text text = new Text(scoreEntry, font, 20) { Position = new Vector2f(100, y), FillColor = Color.White };
            window.Draw(text);
            y += 30;
        }
        window.Display();
        System.Threading.Thread.Sleep(2000);
    }

    private void HandleInput()
    {
        player.HandleInput();
    }

    private void Update(float deltaTime)
    {
        player.Update(deltaTime);
        score.Update();

        foreach (Obstacle obs in obstacles)
            obs.Update(deltaTime);

        if (obstacles.Count == 0 || obstacles[^1].Position.X < 500)
            obstacles.Add(new Obstacle(800));
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
    }

    private void LoadScores()
    {
        if (File.Exists("scores.txt"))
            recentScores.AddRange(File.ReadAllLines("scores.txt"));
    }

    private void SaveScore(int points)
    {
        string entry = $"{DateTime.Now}: {points} points";
        File.AppendAllLines("scores.txt", new[] { entry });
    }
}

class Program
{
    static void Main()
    {
        VideoMode desktopMode = VideoMode.DesktopMode;
        RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Runner 2D", Styles.Default);
        Game game = new Game(window);
        game.Run();
    }
}



