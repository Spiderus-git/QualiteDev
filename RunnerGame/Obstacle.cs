using SFML.Graphics;
using SFML.System;

class Obstacle
{
    private Texture texture;
    private Sprite sprite;
    private float speed = 200;

    public Vector2f Position => sprite.Position;

    public Obstacle(float x)
    {
        texture = new Texture("assets/obstacle.jpg");
        sprite = new Sprite(texture);
        sprite.Position = new Vector2f(x, 460);
    }

    public void Update(float deltaTime)
    {
        sprite.Position -= new Vector2f(speed * deltaTime, 0);
    }

    public void Draw(RenderWindow window)
    {
        window.Draw(sprite);
    }
}
