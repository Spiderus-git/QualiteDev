using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player
{
    private Texture texture;
    private Sprite sprite;
    private IntRect frameRect;
    private int frameWidth = 64;  // Ajuste à la taille exacte d'une frame
    private int frameHeight = 64; // Ajuste à la taille exacte d'une frame
    private int currentFrame = 0;
    private float animationTimer = 0f;

    private Vector2f velocity;
    private bool isJumping = false;
    private float gravity = 1000f;
    private float jumpStrength = -400f;
    private float speed = 200f;

    public Player()
    {
        texture = new Texture("assets/player.png");
        sprite = new Sprite(texture);

        // Sélection de la première frame de "RUN"
        frameRect = new IntRect(0, frameHeight * 1, frameWidth, frameHeight);
        sprite.TextureRect = frameRect;
        sprite.Position = new Vector2f(100, 460);
    }

    public void HandleInput()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            sprite.Position -= new Vector2f(speed * 0.016f, 0);

        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            sprite.Position += new Vector2f(speed * 0.016f, 0);

        if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !isJumping)
        {
            velocity.Y = jumpStrength;
            isJumping = true;
        }
    }

    public void Update(float deltaTime)
    {
        animationTimer += deltaTime;

        // Animation du "RUN"
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            if (animationTimer >= 0.1f) // Change de frame toutes les 0.1s
            {
                currentFrame = (currentFrame + 1) % 6; // Nombre total de frames "RUN"
                frameRect.Left = currentFrame * frameWidth; // Sélectionne la bonne frame
                frameRect.Top = frameHeight * 1; // Ligne "RUN"
                sprite.TextureRect = frameRect;
                animationTimer = 0f;
            }
        }
        else
        {
            // Si le joueur ne bouge pas, rester sur la première frame de RUN
            currentFrame = 0;
            frameRect.Left = 0;
            frameRect.Top = frameHeight * 1;
            sprite.TextureRect = frameRect;
        }

        // Appliquer la gravité
        velocity.Y += gravity * deltaTime;
        sprite.Position += velocity * deltaTime;

        // Empêcher le joueur de tomber sous le sol
        if (sprite.Position.Y >= 460)
        {
            sprite.Position = new Vector2f(sprite.Position.X, 460);
            isJumping = false;
            velocity.Y = 0;
        }
    }

    public void Draw(RenderWindow window)
    {
        window.Draw(sprite);
    }
}

