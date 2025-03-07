using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;


namespace QualiteDev;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private TiledMap _map;
    private TiledMapRenderer _mapRenderer;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _map = Content.Load<TiledMap>("assets/maps/SandBox");
        _mapRenderer = new TiledMapRenderer(GraphicsDevice, _map);
    }



    protected override void Update(GameTime gameTime)
    {
        _mapRenderer.Update(gameTime);
        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _mapRenderer.Draw();

        base.Draw(gameTime);
    }

}
