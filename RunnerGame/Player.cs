using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player
{
    private Texture texture;
    private Sprite sprite;
    private IntRect frameRect;
    private int frameWidth = 32;
    private int frameHeight = 32;
    private int currentFrame = 0;
    private float animationTimer = 0f;
    private Vector2f velocity;
    private bool isJumping = false;

    public Player()
    {
        texture = new Texture("assets/player.jpg");
        sprite = new Sprite(texture);

        frameRect = new IntRect(0, 0, frameWidth, frameHeight);
        sprite.TextureRect = frameRect;
        sprite.Position = new Vector2f(100, 460);
    }

    public void HandleInput()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !isJumping)
        {
            velocity.Y = -400;
            isJumping = true;
        }
    }

    public void Update(float deltaTime)
    {
        animationTimer += deltaTime;

        if (animationTimer >= 0.1f)
        {
            currentFrame = (currentFrame + 1) % 4;
            frameRect.Left = currentFrame * frameWidth;
            sprite.TextureRect = frameRect;
            animationTimer = 0f;
        }

        velocity.Y += 1000 * deltaTime;
        sprite.Position += velocity * deltaTime;

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

