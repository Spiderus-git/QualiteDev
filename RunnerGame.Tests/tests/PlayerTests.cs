using NUnit.Framework;
using SFML.System;

[TestFixture]
public class PlayerTests
{
    [Test]
    public void Test_PlayerCannotMoveOutOfBounds()
    {
        Player player = new Player();

        // Essai de déplacement à gauche hors de l'écran
        player.SetPosition(new Vector2f(-10, 450));
        player.Update(0.1f);
        Assert.That(player.Position.X, Is.GreaterThanOrEqualTo(0));

        // Essai de déplacement à droite hors de l'écran (supposons une largeur de 800px)
        player.SetPosition(new Vector2f(810, 450));
        player.Update(0.1f);
        Assert.That(player.Position.X, Is.LessThanOrEqualTo(800));
    }
}
