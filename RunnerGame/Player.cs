using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Player
{
    // Texture et sprite du joueur
    private Texture texture;
    private Sprite sprite;

    // Gestion des animations (spritesheet)
    private IntRect frameRect;
    private int frameWidth = 32;  // Largeur d'une frame d'animation
    private int frameHeight = 32; // Hauteur d'une frame d'animation
    private int currentFrame = 0; // Indice de la frame actuelle
    private float animationTimer = 0f; // Timer pour gérer la vitesse de l'animation

    // Propriétés physiques du joueur
    private Vector2f velocity; // Vitesse et direction du mouvement
    private bool isJumping = false; // Indique si le joueur est en train de sauter
    private float gravity = 1000f; // Force de gravité appliquée au joueur
    private float jumpStrength = -400f; // Force du saut (valeur négative pour monter)
    private float speed = 200f; // Vitesse horizontale du joueur

    // Contraintes du jeu
    private const float GroundY = 450; // Position du sol (limite en Y)
    private const float WindowWidth = 800; // Largeur de la fenêtre (limite en X)

    // Propriété pour obtenir la position actuelle du joueur
    public Vector2f Position { get; private set; }

    // Constructeur du joueur
    public Player()
    {
        // Chargement de la texture du joueur
        texture = new Texture(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets/player.png"));
        sprite = new Sprite(texture);

        // Sélectionne la première frame de l'animation "RUN"
        frameRect = new IntRect(0, frameHeight * 1, frameWidth, frameHeight);
        sprite.TextureRect = frameRect;

        // Définit la position initiale du joueur sur le sol
        SetPosition(new Vector2f(100, GroundY));

        // Agrandit le sprite (x2)
        sprite.Scale = new Vector2f(2, 2);
    }

    // Réinitialise la position du joueur après une perte de vie
    public void ResetPosition()
    {
        SetPosition(new Vector2f(100, GroundY));
    }

    // Gestion des entrées clavier pour déplacer le joueur
    public void HandleInput()
    {
        // Déplacement vers la gauche
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            SetPosition(new Vector2f(Position.X - speed * 0.016f, Position.Y));

        // Déplacement vers la droite
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            SetPosition(new Vector2f(Position.X + speed * 0.016f, Position.Y));

        // Saut (si le joueur est au sol)
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !isJumping)
        {
            velocity.Y = jumpStrength; // Applique la force du saut
            isJumping = true; // Le joueur est en l'air
        }
    }

    // Mise à jour du joueur (position, animation, physique)
    public void Update(float deltaTime)
    {
        animationTimer += deltaTime; // Incrémente le timer d'animation

        bool isMoving = false; // Vérifie si le joueur est en mouvement

        // Animation de course (si le joueur appuie sur Gauche ou Droite)
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            isMoving = true;
            if (animationTimer >= 0.1f) // Change de frame toutes les 0.1 secondes
            {
                currentFrame = (currentFrame + 1) % 6; // Il y a 6 frames dans l'animation
                frameRect.Left = currentFrame * frameWidth; // Change la frame affichée
                frameRect.Top = frameHeight * 2; // Ligne de l'animation de course
                sprite.TextureRect = frameRect;
                animationTimer = 0f; // Réinitialise le timer
            }
        }

        // Si le joueur ne bouge pas, revenir à la première frame de "RUN"
        if (!isMoving)
        {
            currentFrame = 0;
            frameRect.Left = 0;
            frameRect.Top = frameHeight * 2; // Position de l'animation au repos
            sprite.TextureRect = frameRect;
        }

        // Applique la gravité au joueur
        velocity.Y += gravity * deltaTime;

        // Met à jour la position du joueur en fonction de sa vitesse
        SetPosition(Position + velocity * deltaTime);

        // Vérifie si le joueur touche le sol
        if (Position.Y >= GroundY)
        {
            SetPosition(new Vector2f(Position.X, GroundY));
            isJumping = false;
            velocity.Y = 0;
        }
    }

    // Empêche le joueur de sortir de l'écran
    public void SetPosition(Vector2f newPosition)
    {
        Position = new Vector2f(
            Math.Clamp(newPosition.X, 0, WindowWidth - frameWidth * sprite.Scale.X), // Empêche la sortie de l'écran en X
            newPosition.Y
        );
        sprite.Position = Position; // Synchronise le sprite avec la nouvelle position
    }

    // Affichage du joueur à l'écran
    public void Draw(RenderWindow window)
    {
        window.Draw(sprite);
    }

    public bool CheckCollision(Obstacle obstacle)
    {
        return Position.X + 40 > obstacle.Position.X &&
               Position.X < obstacle.Position.X + 50 &&
               Position.Y >= 450;
    }
}
