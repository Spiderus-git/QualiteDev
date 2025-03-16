using SFML.Graphics;
using SFML.System;

public class Score
{
    private int currentScore = 0;
    private int points = 0;
    private Font font;
    private Text text;

    public Score()
    {
        font = new Font(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets/font.ttf"));
        text = new Text("Score: 0", font, 20);
        text.Position = new Vector2f(10, 10);
        text.FillColor = Color.Black;
    }

    public void Update()
    {
        points++;
        text.DisplayedString = "Score: " + points;
    }

    public void Draw(RenderWindow window)
    {
        window.Draw(text);
    }

    public int GetPoints()
    {
        return points;
    }

    public void Reset()
    {
        points = 0;
        text.DisplayedString = "Score: 0";
    }

    public void SetScore(int newScore)
    {
        currentScore = newScore;
    }

    public void SaveScore()
    {
        File.AppendAllText("scores.txt", currentScore + Environment.NewLine);
    }


}

