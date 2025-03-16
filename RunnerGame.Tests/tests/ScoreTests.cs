using NUnit.Framework;
using System.IO;

[TestFixture]
public class ScoreTests
{
    [Test]
    public void Test_ScoreSavingAndLoading()
    {
        Score score = new Score();
        score.SetScore(100);
        score.SaveScore();

        string[] lines = File.ReadAllLines("scores.txt");
        Assert.That(File.Exists("scores.txt"), Is.True, "Le fichier des scores devrait exister.");
    }
}
