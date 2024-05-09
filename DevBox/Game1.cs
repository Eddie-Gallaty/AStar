using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using DevBox.Global;
using DevBox.Inputs;
using DevBox.Sprites;
using DevBox.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DevBox;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _heroSprite;
    private Texture2D _oneTile;
    private Texture2D _zeroTile;
    private Player _player;
    private Vector2 _position;
    private Map _map;
    private MapRenderer _mapRenderer;

    private AStar astar;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _position = new Vector2(100, 100);
        _map = new Map(20, 20, 32);
        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = _spriteBatch;
        _heroSprite = Content.Load<Texture2D>("hero");
        _oneTile = Content.Load<Texture2D>("tile1");
        _zeroTile = Content.Load<Texture2D>("tile3");
        _map.GenerateRandomMap();
        
        _mapRenderer = new MapRenderer(_oneTile, _zeroTile, _map);

        astar  = new AStar(_map);
        FindandUsePath();
        Debug.WriteLine("Test");

  

        _player = new Player(_heroSprite, _position);
        _player.Input = new Input();
        _player.Input.Left = Keys.A;
        _player.Input.Right = Keys.D;
        _player.Input.Up = Keys.W;
        _player.Input.Down = Keys.S;

        
        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _player.Update();
        //FindandUsePath();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        //_spriteBatch.Draw(_sprite, new Vector2(0,0), Color.White);
        _mapRenderer.Draw();
        _player.Draw();
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void FindandUsePath()
    {
        Point start = new Point(0, 0); // start position
        Point goal = new Point(10, 10); //goal

        //find optimal path

        List<Point> path = astar.FindPath(start, goal);

        //check if path is found
        Debug.WriteLine("Before the if statement");
        if(path != null)
        {
            Debug.WriteLine("inside the if statement");
            //output path coordinates  (DEBUG)
            foreach (Point point in path)
            {
                Console.WriteLine($"Path Position: {point}");
            }

            //todo here such as move sprite etc
            
        }
        else
        {
            //no paths were found
            Console.WriteLine("no path found");
        }
    }
    
}
