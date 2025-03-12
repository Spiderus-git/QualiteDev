using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player
{
    private Texture texture;       // Texture du sprite sheet
    private Sprite sprite;        // Sprite du joueur
    private IntRect frameRect;     // Rectangle pour sélectionner la frame dans la texture
    private int frameWidth = 32;  // Largeur d'une frame
    private int frameHeight = 32; // Hauteur d'une frame
    private int currentFrame = 0; // Frame actuelle de l'animation
    private float animationTimer = 0f; // Timer pour l'animation

    private Vector2f velocity;    // Vélocité du joueur
    private bool isJumping = false; // Indique si le joueur est en train de sauter
    private float gravity = 1000f; // Force de gravité
    private float jumpStrength = -400f; // Force du saut
    private float speed = 200f;   // Vitesse de déplacement horizontal

    public Player()
    {
        texture = new Texture("assets/player.png");
        sprite = new Sprite(texture);

        // Sélection de la première frame de "RUN" (ligne 1)
        frameRect = new IntRect(0, frameHeight * 1, frameWidth, frameHeight); // Ligne 1 = RUN
        sprite.TextureRect = frameRect;

        // Position initiale du joueur
        sprite.Position = new Vector2f(100, 450);

        // Agrandir le sprite (par exemple, 2x la taille originale)
        sprite.Scale = new Vector2f(2, 2); // Facteur d'échelle (2x en largeur, 2x en hauteur)
    }

    public void HandleInput()
    {
        // Déplacement horizontal
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            sprite.Position -= new Vector2f(speed * 0.016f, 0); // Déplacement vers la gauche

        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            sprite.Position += new Vector2f(speed * 0.016f, 0); // Déplacement vers la droite

        // Saut
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !isJumping)
        {
            velocity.Y = jumpStrength; // Appliquer la force du saut
            isJumping = true; // Le joueur est en train de sauter
        }
    }

    public void Update(float deltaTime)
    {
        // Mettre à jour le timer d'animation
        animationTimer += deltaTime;

        // Animation de course (RUN)
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            if (animationTimer >= 0.1f) // Changer de frame toutes les 0.1 secondes
            {
                currentFrame = (currentFrame + 1) % 6; // 6 frames pour l'animation RUN
                frameRect.Left = currentFrame * frameWidth; // Sélectionner la frame suivante
                frameRect.Top = frameHeight * 2; // Ligne 1 = RUN
                sprite.TextureRect = frameRect; // Appliquer la nouvelle frame
                animationTimer = 0f; // Réinitialiser le timer
            }
        }
        else
        {
            // Si le joueur ne bouge pas, revenir à la première frame de RUN
            currentFrame = 0;
            frameRect.Left = 0;
            frameRect.Top = frameHeight * 2; // Ligne 1 = RUN
            sprite.TextureRect = frameRect;
        }

        // Appliquer la gravité
        velocity.Y += gravity * deltaTime;
        sprite.Position += velocity * deltaTime;

        // Collision avec le sol
        if (sprite.Position.Y >= 450) // 450 = position du sol
        {
            sprite.Position = new Vector2f(sprite.Position.X, 450); // Bloquer le joueur au sol
            isJumping = false; // Le joueur n'est plus en train de sauter
            velocity.Y = 0; // Réinitialiser la vélocité verticale
        }
    }

    public void Draw(RenderWindow window)
    {
        window.Draw(sprite); // Dessiner le joueur
    }
}