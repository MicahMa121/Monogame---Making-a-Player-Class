using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogame___Making_a_Player_Class
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D amoebaTexture, circleTexture, rectangleTexture;
        KeyboardState keyboardState,prevKeyboardState;

        player amoeba;

        List<Rectangle> barriers,food;

        int speedPlayer;
        bool applyGravity;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            barriers = new List<Rectangle>();
            food = new List<Rectangle>();
            food.Add(new Rectangle(50, 50, 10, 10));
            food.Add(new Rectangle(600, 100, 10, 10));
            food.Add(new Rectangle(50, 200, 10, 10));
            barriers.Add(new Rectangle(100, 100, 200, 10));
            barriers.Add(new Rectangle(400, 400, 100, 10));
            barriers.Add(new Rectangle(200, 400, 100, 10));
            speedPlayer = 3;
            base.Initialize();
            amoeba = new player(amoebaTexture, 10, 10);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            amoebaTexture = Content.Load<Texture2D>("amoeba");
            circleTexture = Content.Load<Texture2D>("circle");
            rectangleTexture = Content.Load<Texture2D>("rectangle");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            amoeba.HSpeed = 0;
            
            
            if (keyboardState.IsKeyDown(Keys.D))
                amoeba.HSpeed = speedPlayer/3;
            else if (keyboardState.IsKeyDown(Keys.A))
                amoeba.HSpeed = -speedPlayer/3;
            if (keyboardState.IsKeyDown(Keys.W))
                amoeba.VSpeed = -speedPlayer*2;
            //else if (keyboardState.IsKeyDown(Keys.S))
                //amoeba.VSpeed = speedPlayer;
            amoeba.Update();

            foreach (Rectangle barrier in barriers)
                if (amoeba.CollideH(barrier))
                    amoeba.UndoMoveH();
            applyGravity = true;
            foreach (Rectangle barrier in barriers)
            {
                if (amoeba.CollideV(barrier))
                {
                    applyGravity = false;
                }
            }
            if (applyGravity)
            {
                amoeba.VSpeed = speedPlayer / 3 ;
            }
            else
                amoeba.VSpeed = 0;
                
            amoeba.Update();
            for (int i = 0; i < food.Count; i++)
                if (amoeba.CollideH(food[i]) || amoeba.CollideV(food[i]))
                {
                    food.RemoveAt(i);
                    i--;
                    amoeba.Grow();
                }
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(rectangleTexture, barrier, Color.Brown);
            foreach (Rectangle bit in food)
                _spriteBatch.Draw(circleTexture, bit, Color.Green);
            amoeba.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}