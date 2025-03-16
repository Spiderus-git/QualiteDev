using NUnit.Framework;
using SFML.System;

[TestFixture]
public class CollisionTests
{
    [Test]
    public void Test_PlayerCollidesWithObstacle()
    {
        Player player = new Player();
        Obstacle obstacle = new Obstacle(100);

        // Simule une collision
        player.SetPosition(new Vector2f(100, 450)); // Position du joueur alignée avec l'obstacle

        bool collision = player.CheckCollision(obstacle);
        Assert.That(player.CheckCollision(obstacle), Is.True, "Le joueur devrait être en collision avec l'obstacle.");

    }
}
