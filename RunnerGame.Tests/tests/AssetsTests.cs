using NUnit.Framework;
using SFML.Graphics;
using System.IO;

[TestFixture]
public class AssetTests
{
    [Test]
    public void Test_AssetsExist()
    {
        Assert.That(File.Exists("assets/background.jpg"), Is.True, "L'image de fond est absente.");
        Assert.That(File.Exists("assets/player.png"), Is.True, "Le sprite du joueur est absent.");
        Assert.That(File.Exists("assets/obstacle.jpg"), Is.True, "Le sprite d'obstacle est absent.");
        Assert.That(File.Exists("assets/font.ttf"), Is.True, "La police d'écriture est absente.");
    }

    [Test]
    public void Test_LoadingTextures()
    {
        Texture texture = new Texture("assets/player.png");
        Assert.That(texture, Is.Not.Null, "La texture ne doit pas être nulle.");

    }
}
