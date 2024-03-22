using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Monogame___Making_a_Player_Class
{
    class player
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;
        public player(Texture2D texture, int x, int y)
        {
            _texture = texture;
            _location = new Rectangle(x, y, 30, 30);
            _speed = new Vector2();
        }
        public float HSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }
        public float VSpeed
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }
        private void Move()
        {
            _location.X += (int)_speed.X;
            _location.Y += (int)_speed.Y;
        }
        public void Update()
        {
            Move();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
        
        public bool CollideH(Rectangle item)
        {
            return _location.Intersects(item);
        }
        public void UndoMoveH()
        {
            _location.X -= (int)_speed.X;
            
        }
        public bool CollideV(Rectangle item)
        {
            return _location.Intersects(item);
        }
        
        public void Grow()
        {
            _location.Width += 1;
            _location.Height += 1;
        }
    }
}
