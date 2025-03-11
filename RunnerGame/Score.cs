using SFML.Graphics;
using SFML.System;

class Score
{
    private int points = 0;
    private Font font;
    private Text text;

    public Score()
    {
        font = new Font("assets/font.ttf");
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
}

