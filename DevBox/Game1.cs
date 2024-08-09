using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _position = new Vector2(0, 0);
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

        _player = new Player(_heroSprite, _position);
        _player.Input = new Input();
        _player.Input.Left = Keys.A;
        _player.Input.Right = Keys.D;
        _player.Input.Up = Keys.W;
        _player.Input.Down = Keys.S;

        //*/
        

        // TODO: use this.Content to load your game content here
        //currentPathIndex = 0;
    }
    

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float speed = 1f;
        _player.Update(); // Update player logic (input handling, etc.)
        bool EndOfPath = true;
        //Vector2 targetPos = new Vector2(path[currentPathIndex].X, path[currentPathIndex].Y);  //TOOK OUT OF LOOP
        //Vector2 direction = Vector2.Normalize(_targetPos - _player.Position); // TOOK OUT OF LOOP
        // Check if there are remaining points in the path

        while(EndOfPath)
            {
                //currentPathIndex++;
                if(currentPathIndex  + 1 < path.Count)
                {
                    
                    _targetPos.X = path[5].X;
                    _targetPos.Y = path[5].Y;

                    Vector2 direction = Vector2.Normalize(_targetPos - _player.Position);


                    //_player.Velocity = direction * speed;
                   // _position += _player.Velocity;
                   //_position = _targetPos * 16;

                   
                    //if ( _player.Velocity == Vector2.Zero && _targetPos == _player.Position)
                    if (_player.Position != _targetPos)
                    {
                        Console.WriteLine("here!!!!!!");
                        Console.WriteLine(_player.Position);

                        _player.Velocity = direction * speed;
                        _position += _player.Velocity;
                        _player.Position.X += path[currentPathIndex].X;
                        _player.Position.Y += path[currentPathIndex].Y;


                        currentPathIndex++;
                    }

                    else
                    {
                        _player.Velocity = Vector2.Zero;
                    }

                    /*
                    {
                        Console.WriteLine("Velocity is Zero");
                        Console.WriteLine(_player.Position);
                        Console.WriteLine(_targetPos);
                    }
                    else
                    {
                        Console.WriteLine(_position);
                    }
                    */
                    




                    //Console.WriteLine("From the if loop in update Direction" + direction);

                   /* //_position += direction * speed;
                   // _player.Position += targetPos * 1
                    //Console.WriteLine("From the if loop in update Position" + _position);  
                    //Console.WriteLine("players position: " +_player.Position );
                    Console.WriteLine("Player Velocity: " + _player.Velocity);
                    Console.WriteLine("Direction: "+ direction);
                    Console.WriteLine("Target Position"+_targetPos + " player POS "+_player.Position);
                    Console.WriteLine("Current path index" +currentPathIndex+ " and path count"+path.Count); 
                    Console.WriteLine("Distance to target" + Vector2.Distance(_player.Position, _targetPos));
                    Console.WriteLine(path.Count); */
                    //currentPathIndex++;


                }
                
                else
                {
                    EndOfPath = false;
                    //_player.Velocity = Vector2.Zero;
                    Console.WriteLine("leaving loop");
                    //Console.WriteLine(_player.Position);
                    //return;
                    
                }
            }
        
            
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        // TODO: Add your drawing code here
        _spriteBatch.Begin();
      //  _spriteBatch.Draw(_heroSprite, _position, Color.White);
        //_mapRenderer.Draw(path);
            //Console.WriteLine(point);
        string posText = $"X: {_player.Position.X}, Y: {_player.Position.Y}";
        string velText = $"Velocity {_player.Velocity}";
        //string playText = $"Player pos{_player.Position}";
        _spriteBatch.DrawString(_debugFont, posText, _debugFontPOS, Color.Black);
        _spriteBatch.DrawString(_debugFont , velText, _velFontPOS, Color.Black);
       // _spriteBatch.DrawString(_debugFont, playText, _playerFontPOS, Color.Black);

        _mapRenderer.Draw(path);
        _player.Draw();
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