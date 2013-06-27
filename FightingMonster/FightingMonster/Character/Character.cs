using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSLibrary.GameEntity;
using TSLibrary.Texture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TSLibrary.Map;

namespace FightingMonster.Character
{
    public class Character : TSGameEntity
    {
        protected string _name;
        protected int _hp;
        protected float _speed;
        protected bool _runMode;
        protected Color _color;

        protected CharacterState _state;
        protected Direction _direction;
        protected Vector2 _targetPosition;
        protected TSMap _map;


        protected TSTexture2DList[] _standingTextures;
        protected TSTexture2DList[] _walkingTextures;
        protected TSTexture2DList[] _runningTextures;
        

        #region Property Region

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int HP
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public virtual float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public virtual bool RunMode
        {
            get { return _runMode; }
            set { _runMode = value; }
        }

        public virtual Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public virtual CharacterState State
        {
            get { return _state; }
            set { 
                _state = value;

                if (_state == CharacterState.Standing)
                    _background = _standingTextures[(int)_direction];
                else if (_state == CharacterState.Walking)
                    _background = _walkingTextures[(int)_direction];
                else if (_state == CharacterState.Running)
                    _background = _runningTextures[(int)_direction];
            }
        }

        public virtual Direction Direction
        {
            get { return _direction; }
            set { 
                _direction = value;
                State = State;    // load lại trạng thái và hướng đứng
            }
        }

        public virtual TSMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        #endregion


        protected float stepX;
        protected float stepY;




        public Character()
        {
            int numDirections = 8; //8 hướng được định nghĩa trong tập tin Direction.cs

            _standingTextures = new TSTexture2DList[numDirections];
            _walkingTextures = new TSTexture2DList[numDirections];

            for (int directionState = 0; directionState < numDirections; directionState++)
            {
                _standingTextures[directionState] = new TSTexture2DList();
                _walkingTextures[directionState] = new TSTexture2DList();
            }

            Width = 100;
            Height = 120;
            Position = new Vector2(100, 100);                 // đứng tại điểm có tọa độ (100, 100)

            State = CharacterState.Standing;                  // trạng thái hiện tại là đang đứng
            Direction = Direction.RightBottom;                // quay mặt về hướng RightBottom

            Speed = 120;
            RunMode = false;

            Color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            MoveToTargetPosition();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visibled == false)
                return;

            if (_background != null)
            {
                if (_map == null)
                {
                    if (_background.CurrentTexture != null)
                        spriteBatch.Draw(
                            _background.CurrentTexture,
                            new Rectangle((int)(_position.X - _width / 2), (int)(_position.Y - _height), _width, _height),
                            _color);
                }
                else
                {
                    if (_background.CurrentTexture != null)
                        spriteBatch.Draw(
                            _background.CurrentTexture,
                            new Rectangle((int)(_position.X - _map.P0.X - _width / 2), (int)(_position.Y - _map.P0.Y - _height), _width, _height),
                            _color);
                }
            }
        }

        public virtual void Draw(Vector2 position, GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visibled == false)
                return;

            if (_background != null)
                if (_background.CurrentTexture != null)
                    spriteBatch.Draw(
                        _background.CurrentTexture,
                        new Rectangle((int)(position.X - _width / 2), (int)(position.Y - _height), _width, _height),
                        _color);
        }

        /// <summary>
        /// Di chuyển đến vị trí có tọa độ targetX
        /// </summary>
        public virtual void MoveTo(float targetX, float targetY)
        {
            if ((State != CharacterState.Walking) && (_state != CharacterState.Running))
                if (RunMode == true)
                    State = CharacterState.Running;
                else
                    State = CharacterState.Walking;

            if (_background.IsAnimated == false)
                _background.IsAnimated = true;

            // di chuyển
            _targetPosition = new Vector2(targetX, targetY);
            Direction = GetDirection(_targetPosition.X, _targetPosition.Y);
            stepX = (_targetPosition.X - PositionX) / Speed;
            stepY = (_targetPosition.Y - PositionY) / Speed;
        }

        /// <summary>
        /// Di chuyển đến vị trí this.TargetPosition
        /// </summary>
        protected virtual void MoveToTargetPosition()
        {
            if ((State != CharacterState.Walking) && (State != CharacterState.Running))
                return;

            //Direction = GetDirection(_targetPosition.X, _targetPosition.Y);
            PositionX += stepX;
            PositionY += stepY;

            int delta = 5;
            if (Math.Abs(PositionX - _targetPosition.X) <= delta && 
                    Math.Abs(PositionY - _targetPosition.Y) <= delta)
                State = CharacterState.Standing;
        }


        /// <summary>
        /// Lấy hướng tới vị trí (pX, pY) so với vị trí đang đứng.
        /// Ví dụ đang đứng ở vị trị (0, 0) mà getDirection(1, 1)
        /// sẽ trả về Direction.TopRight
        /// </summary>
        public virtual Direction GetDirection(float pX, float pY)
        {
            Vector2 newPosition = new Vector2(pX, pY);
            Vector2 pos = newPosition - Position;      // dời 2 điểm newPosition và Position về gốc tọa độ để dễ so sánh

            int delta = _height;  // lệch (_height) pixel so với đường thẳng thì coi như vẫn nằm trên đường thẳng


            if ((pos.X < 0) && (Math.Abs(pos.Y) <= delta))
                return Direction.Left;

            if ((Math.Abs(pos.X) <= delta) && (pos.Y < 0))
                return Direction.Top;

            if ((pos.X > 0) && (Math.Abs(pos.Y) <= delta))
                return Direction.Right;

            if ((Math.Abs(pos.X) <= delta) && (pos.Y > 0))
                return Direction.Bottom;


            if ((pos.X < 0) && (pos.Y < 0))
                return Direction.LeftTop;

            if ((pos.X > 0) && (pos.Y < 0))
                return Direction.TopRight;

            if ((pos.X > 0) && (pos.Y > 0))
                return Direction.RightBottom;

            if ((pos.X < 0) && (pos.Y > 0))
                return Direction.BottomLeft;

            return Direction.RightBottom;  // mặc định
        }
    }
}
