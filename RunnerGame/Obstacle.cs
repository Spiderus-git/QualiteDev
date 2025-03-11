using SFML.Graphics;
using SFML.System;

class Obstacle
{
    private Texture texture;
    private Sprite sprite;
    private float speed = 200;
    private const int size = 50; // Taille carrée de l'obstacle

    public Vector2f Position => sprite.Position;

    public Obstacle(float x)
    {
        texture = new Texture("assets/obstacle.jpg");  // Vérifie le chemin
        sprite = new Sprite(texture);

        // Ajuster la taille pour qu'il soit bien carré
        sprite.Scale = new Vector2f(
            size / (float)texture.Size.X,
            size / (float)texture.Size.Y
        );

        // Ajuster la position pour qu'il touche bien le sol
        sprite.Position = new Vector2f(x, 450);
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
