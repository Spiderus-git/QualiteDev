using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Game
{
    private RenderWindow window; // Fenêtre de jeu
    private Player player; // Joueur
    private List<Obstacle> obstacles = new List<Obstacle>(); // Liste des obstacles
    private Clock clock; // Horloge principale du jeu
    private Texture backgroundTexture, groundTexture; // Textures du fond et du sol
    private Sprite background, ground; // Sprites pour afficher le fond et le sol
    private Score score; // Système de score
    private int lives = 3; // Nombre de vies du joueur
    private bool isInvincible = false; // Indique si le joueur est temporairement invincible
    private Clock invincibilityClock; // Timer pour l'invincibilité
    private Font font; // Police utilisée pour l'affichage des textes
    private Text livesText; // Texte des vies affichées à l'écran
    private bool isPlaying = false; // État du jeu (en cours ou menu)
    private bool isShowingScores = false; // Indique si l'écran des scores est affiché
    private Text playText, scoreText, quitText, backText, controlsText, titleText; // Textes du menu principal et des scores
    private List<string> recentScores = new List<string>(); // Liste des scores récents

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
        backText = new Text("Retour", font, 30) { Position = new Vector2f(350, 500), FillColor = Color.White };
        controlsText = new Text("Contrôles:\n- Flèche gauche/droite: Déplacement\n- Espace: Saut\n- Échap: Retour menu", font, 20)
        {
            Position = new Vector2f(50, 500),
            FillColor = Color.White
        };

        livesText = new Text("Vies: " + lives, font, 20) { Position = new Vector2f(700, 10), FillColor = Color.White };
        titleText = new Text("Scores récents", font, 40) { Position = new Vector2f(300, 50), FillColor = Color.White };

        invincibilityClock = new Clock();
        clock = new Clock();

        LoadScores();
    }

    public void Run()
    {
        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear(Color.Black);

            if (isShowingScores)
            {
                ShowScores();
            }
            else if (!isPlaying)
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

    private void LoadScores()
    {
        if (File.Exists("scores.txt"))
        {
            recentScores = File.ReadAllLines("scores.txt")
                            .OrderByDescending(s => int.Parse(s.Split(':')[1].Trim().Split(' ')[0]))
                            .ToList();
        }
    }

    private void ShowScores()
    {
        window.Clear(Color.Black);
        window.Draw(titleText);

        int y = 120;
        foreach (var scoreEntry in recentScores)
        {
            Text text = new Text(scoreEntry, font, 20) { Position = new Vector2f(100, y), FillColor = Color.White };
            window.Draw(text);
            y += 30;
        }

        window.Draw(backText);
        window.Display();

        while (isShowingScores && window.IsOpen)
        {
            window.DispatchEvents();
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePos = Mouse.GetPosition(window);
                if (backText.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
                {
                    isShowingScores = false;
                    return;
                }
            }
        }
    }

    private void ShowMenu()
    {
        isPlaying = false;

        while (!isPlaying && !isShowingScores && window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear(Color.Black);
            window.Draw(playText);
            window.Draw(scoreText);
            window.Draw(quitText);
            window.Draw(controlsText);
            window.Display();

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePos = Mouse.GetPosition(window);
                if (playText.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
                {
                    isPlaying = true;
                    foreach (Obstacle obs in obstacles)
                    {
                        obs.Resume();
                    }
                }
                else if (scoreText.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
                {
                    isShowingScores = true;
                }
                else if (quitText.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
                {
                    window.Close();
                }
            }
        }
    }

    private void HandleInput()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
        {
            isPlaying = false;
            foreach (Obstacle obs in obstacles)
            {
                obs.Stop();
            }
            return;
        }
        player.HandleInput();
    }

    private void Update(float deltaTime)
    {
        if (!isPlaying) return;

        player.Update(deltaTime);
        score.Update();

        if (isInvincible && invincibilityClock.ElapsedTime.AsSeconds() >= 2)
        {
            isInvincible = false;
        }

        if (obstacles.Count == 0 || obstacles[^1].Position.X < 500)
        {
            obstacles.Add(new Obstacle(800));
        }

        List<Obstacle> toRemove = new List<Obstacle>();

        foreach (Obstacle obs in obstacles)
        {
            obs.Update(deltaTime);

            if (!isInvincible && player.Position.X + 40 > obs.Position.X && player.Position.X < obs.Position.X + 50 && player.Position.Y >= 450)
            {
                lives--;
                livesText.DisplayedString = "Vies: " + lives;
                isInvincible = true;
                invincibilityClock.Restart();

                if (lives <= 0)
                {
                    ResetGame();
                    return;
                }
            }

            if (obs.Position.X < -50)
            {
                toRemove.Add(obs);
            }
        }

        foreach (Obstacle obs in toRemove)
        {
            obstacles.Remove(obs);
        }
    }

    private void ResetGame()
    {
        lives = 3;
        score.Reset();
        player.ResetPosition();
        obstacles.Clear();
        isPlaying = false;
    }

    private void Render()
    {
        window.Clear(Color.Cyan);
        window.Draw(background);
        window.Draw(ground);

        foreach (Obstacle obs in obstacles)
        {
            obs.Draw(window);
        }

        player.Draw(window);
        score.Draw(window);
        window.Draw(livesText);
    }
}

class Program
{
    static void Main()
    {
        RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Runner 2D", Styles.Default);
        Game game = new Game(window);
        game.Run();
    }
}
