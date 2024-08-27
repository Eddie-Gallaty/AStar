using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
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
    private Enemy _enemy;
    private Vector2 _enemyPosition;
    private Vector2 _position;
    private Map _map;
    private List<Point> path; 
    private MapRenderer _mapRenderer;

    private Vector2 _debugFontPOS;

    private SpriteFont _debugFont;

    //this is used to hold the index of path
    private int currentPathIndex;

    private AStar astar;

    private Vector2 _velocity;
    private Vector2 _targetPos; 
    private Vector2 _velFontPOS;
    private Vector2 _playerFontPOS;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        //testing git
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _enemyPosition = new Vector2(0,0);
        _position = new Vector2(50, 100);
        _map = new Map(25, 25, 16);
        _debugFontPOS = new Vector2(400, 50);
        _velFontPOS = new Vector2(400, 100);
        _playerFontPOS = new Vector2(400, 150);
        _targetPos = new Vector2(0,0);
        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = _spriteBatch;
        _heroSprite = Content.Load<Texture2D>("hero");
        _oneTile = Content.Load<Texture2D>("tile3");
        _zeroTile = Content.Load<Texture2D>("tile1");
        _debugFont = Content.Load<SpriteFont>("debugFont");
        _map.GenerateRandomMap();
        
        _mapRenderer = new MapRenderer(_oneTile, _zeroTile, _map);

        astar  = new AStar(_map);
        FindandUsePath();
        Debug.WriteLine("Test");

  
        //uncomment for keyboard control

        _enemy = new Enemy(_heroSprite, _enemyPosition);

        _player = new Player(_heroSprite, _position);
        _player.Input = new Input();
        _player.Input.Left = Keys.A;
        _player.Input.Right = Keys.D;
        _player.Input.Up = Keys.W;
        _player.Input.Down = Keys.S;
        //testing git


    }
    

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float speed = 2f; //controls the speed of the sprite 
        // Check if there are remaining points in the path
        if (currentPathIndex  < path.Count)
        {
            Vector2 targetPos = new Vector2(path[currentPathIndex].X * _map.GetCellSize(), path[currentPathIndex].Y * _map.GetCellSize()); //have to convert the points from grid (* by 16 in this case)
            Console.WriteLine("target pos" + targetPos);
            Vector2 direction = Vector2.Normalize(targetPos - _enemy.Position);
            Console.WriteLine("in the first if");


            if (_enemy.Position != targetPos)
            {
                Vector2 Direction = targetPos - _enemy.Position;
                Direction.Normalize();
                
                if (Vector2.Distance(_enemy.Position, targetPos) <= speed)
                {
                    _enemy.Position = targetPos;
                    currentPathIndex++;
                }
                else
                {
                    _enemy.Position += Direction * speed;
                }

            }
            else
            {
                _enemy.Position = targetPos;
                currentPathIndex++;
            }
        }
        _player.Update();
        base.Update(gameTime);
    }   

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        string posText = $"X: {_enemy.Position.X}, Y: {_enemy.Position.Y}";
        string velText = $"Velocity {_enemy.Velocity}";
        _spriteBatch.DrawString(_debugFont, posText, _debugFontPOS, Color.Black);
        _spriteBatch.DrawString(_debugFont , velText, _velFontPOS, Color.Black);
        _mapRenderer.Draw(path);
        _enemy.Draw();
        _player.Draw();
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void FindandUsePath()
    {
        Point start = new Point(0, 0); // start position
        Point goal = new Point(20,20); //goal

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
                //Console.WriteLine($"Path Position: {point}");
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