using NUnit.Framework;
using SFML.Graphics;
using System.IO;

[TestFixture]
public class AssetTests
{
    [Test]
    public void Test_AssetsExist()
    {
        Assert.IsTrue(File.Exists("assets/background.jpg"), "L'image de fond est absente.");
        Assert.IsTrue(File.Exists("assets/player.png"), "Le sprite du joueur est absent.");
        Assert.IsTrue(File.Exists("assets/obstacle.jpg"), "Le sprite d'obstacle est absent.");
        Assert.IsTrue(File.Exists("assets/font.ttf"), "La police d'écriture est absente.");
    }

    [Test]
    public void Test_LoadingTextures()
    {
        Texture texture = new Texture("assets/player.png");
        Assert.IsNotNull(texture, "La texture du joueur ne devrait pas être nulle.");
    }
}
