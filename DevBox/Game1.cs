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
    private List<Point> path; 
    private MapRenderer _mapRenderer;

    //this is used to hold the index of path
    private int currentPathIndex;

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
        _position = new Vector2(300, 300);
        _map = new Map(20, 20, 16);
        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = _spriteBatch;
        _heroSprite = Content.Load<Texture2D>("hero");
        _oneTile = Content.Load<Texture2D>("tile3");
        _zeroTile = Content.Load<Texture2D>("tile1");
        _map.GenerateRandomMap();
        
        _mapRenderer = new MapRenderer(_oneTile, _zeroTile, _map);

        astar  = new AStar(_map);
        FindandUsePath();
        Debug.WriteLine("Test");

  
        //uncomment for keyboard control

        /*_player = new Player(_heroSprite, _position);
        _player.Input = new Input();
        _player.Input.Left = Keys.A;
        _player.Input.Right = Keys.D;
        _player.Input.Up = Keys.W;
        _player.Input.Down = Keys.S;

        */
        

        // TODO: use this.Content to load your game content here
        currentPathIndex = 0;
    }
    

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        //_player.Update();
        //FindandUsePath();

   for (int i = 0; i < path.Count; i++)
            {
                Vector2 targetPos = new Vector2(path[i].X, path[i].Y);
                Vector2 direction = Vector2.Normalize(targetPos - _position);
                float speed = 1f;

                _position += direction * speed;

            }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_heroSprite, _position, Color.White);
        //_mapRenderer.Draw(path);
        
            //Console.WriteLine(point);
        _mapRenderer.Draw(path);
        //_player.Draw();
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void FindandUsePath()
    {
        Point start = new Point(0, 0); // start position
        Point goal = new Point(19, 19); //goal

        //find optimal path

        path = astar.FindPath(start,goal);

        //check if path is found
       // Debug.WriteLine("Before the if statement");
        if(path != null)
        {
            Console.WriteLine("==============================");
            Console.WriteLine("      PATHFINDING TIME");
            Console.WriteLine("==============================");
            Console.WriteLine("Start Position: "+start);
            Console.WriteLine("Goal Position: "+goal);
           
            //Debug.WriteLine("inside the if statement");
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
            //Console.WriteLine(path.Count);
        }
    }
    
}
