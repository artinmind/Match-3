using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3MG
{
    public enum Type { Yellow, Green, Red, Purple, Blue, Empty }
    public class Field
    {
        public Texture2D _texture;
        public Texture2D selected;
        private Stone[,] _stonesArray;
        private Random _random;
        private const int _speed = 10;
        private Point changePos;
        private bool stoneSelected;
        private bool isEnableToTap;
        private Explotion explotion;
        private Bomb replacedBomb;
        private Point swapFirst;
        private Point swapSecond;
        public Stopwatch time;

        private Texture2D textureExplotion;
        private Liner liner;
        
        public bool isInitField { get; set; }


        public Field(Texture2D _texture, Texture2D textureExplotion,  Texture2D selected)
        {
            this._texture = _texture;
            this.selected = selected;
            _stonesArray = new Stone[8, 8];
            _random = new Random();
            isInitField = false;
            this.textureExplotion = textureExplotion;
        }

        private Rectangle TextureType(Stone stone)
        {
            Point pointSize = new Point(100, 100);
            int step = 0;
            if (stone.hor)
                step = 200;
            if (stone.ver)
                step = 300;
            if (stone.coloredBomb)
                step = 100;
            switch (stone.type1)
            {
                case Type.Yellow:
                    return new Rectangle(new Point(0, 0 + step), pointSize);
                case Type.Blue:
                    return new Rectangle(new Point(400, 0 + step), pointSize);
                case Type.Red:
                    return new Rectangle(new Point(200, 0 + step), pointSize);
                case Type.Green:
                    return new Rectangle(new Point(100, 0 + step), pointSize);
                case Type.Purple:
                    return new Rectangle(new Point(300, 0 + step), pointSize);
                default:
                    return new Rectangle(new Point(500, 400), pointSize);
            }
        }

        private Type TypeChanger(Type type)
        {
            type++;
            return type;
        }

        private Type Updater(Type type)
        {
            type += 5;
            return type;
        }

        private Type RandomType()
        {
            return (Type)_random.Next(0, 5);
        }

        public void InitField()
        {
            changePos = new Point(-1, -1);
            swapFirst = new Point(-1, -1);
            swapSecond = new Point(-1, -1);
            replacedBomb = new Bomb();
            explotion = new Explotion();
            //Play.GameScore = 0;
            stoneSelected = false;
            isInitField = true;
            isEnableToTap = true;
            time = new Stopwatch();
            liner = new Liner();
            //time.Start();


            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    _stonesArray[i, j] = new Stone(RandomType(), new Point((i * 100), 0 - (_random.Next(5, 10) * _speed)), false, false);

                    if (j > 1 && _stonesArray[i, j].type1 == _stonesArray[i, j - 1].type1 && _stonesArray[i, j].type1 == _stonesArray[i, j - 2].type1)
                    {
                        _stonesArray[i, j].type1 = TypeChanger(_stonesArray[i, j].type1);
                    }
                    if (i > 1 && _stonesArray[i, j].type1 == _stonesArray[i - 1, j].type1 && _stonesArray[i, j].type1 == _stonesArray[i - 2, j].type1)
                    {
                        _stonesArray[i - 2, j].type1 = TypeChanger(_stonesArray[i - 2, j].type1);
                    }
                }
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (j > 1 && _stonesArray[i, j].type1 == _stonesArray[i, j - 1].type1 && _stonesArray[i, j].type1 == _stonesArray[i, j - 2].type1)
                    {
                        _stonesArray[i, j].type1 = TypeChanger(_stonesArray[i, j].type1);
                    }
                    if (i > 1 && _stonesArray[i, j].type1 == _stonesArray[i - 1, j].type1 && _stonesArray[i, j].type1 == _stonesArray[i - 2, j].type1)
                    {
                        _stonesArray[i - 2, j].type1 = TypeChanger(_stonesArray[i - 2, j].type1);
                    }

                }
        }

        public void FillInTheField()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (_stonesArray[i, j].type1 == Type.Empty)
                    {
                        int k = j;
                        while (k > 0)
                        {
                            QuickSwap(new Point(i, k), new Point(i, k - 1));
                            k--;
                        }
                    }
                }
            }
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (_stonesArray[i, j].type1 == Type.Empty)
                        _stonesArray[i, j] = new Stone(RandomType(), new Point((i*100), 0 - (_random.Next(5, 10)*_speed)), false, false);
        }

        private bool SwapCheck(Point pos)
        {
            bool result = false;

            if (pos.X > 1)
                result = result || (_stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X - 1, pos.Y].type1 && _stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X - 2, pos.Y].type1);
            if (pos.X > 0 && pos.X < 7)
                result = result || (_stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X - 1, pos.Y].type1 && _stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X + 1, pos.Y].type1);
            if (pos.X < 6)
                result = result || (_stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X + 1, pos.Y].type1 && _stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X + 2, pos.Y].type1);

            if (pos.Y > 1)
                result = result || (_stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X, pos.Y - 1].type1 && _stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X, pos.Y - 2].type1);
            if (pos.Y > 0 && pos.Y < 7)
                result = result || (_stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X, pos.Y - 1].type1 && _stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X, pos.Y + 1].type1);
            if (pos.Y < 6)
                result = result || (_stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X, pos.Y + 1].type1 && _stonesArray[pos.X, pos.Y].type1 == _stonesArray[pos.X, pos.Y + 2].type1);

            return result;
        }

        private void Destroy(Point point)                   
        {
            explotion.IsBoom = true;
            explotion.boomList.Remove(point);
            explotion.boomList.Add(_stonesArray[point.X, point.Y]._point);
            if (_stonesArray[point.X, point.Y].coloredBomb)                             
                BombDestroy(point);
            else if (_stonesArray[point.X, point.Y].ver)
            {
                _stonesArray[point.X, point.Y].ver = false;
                MatchDestroy(new Point(point.X, 7), 8, true, true);
                liner.IsBoom = true;
                liner.propList.Remove(new Prop(point, true));
                liner.propList.Add(new Prop(point, true));
            }
            else if (_stonesArray[point.X, point.Y].hor)
            {
                _stonesArray[point.X, point.Y].hor = false;
                MatchDestroy(new Point(7, point.Y), 8, false, true);
                liner.IsBoom = true;
                liner.propList.Remove(new Prop(point, false));
                liner.propList.Add(new Prop(point, false));
            }
            else
                _stonesArray[point.X, point.Y].type1 = Type.Empty;
        }

        private void MatchDestroy(Point point, int match, bool ver, bool special)                  
        {
            bool xCheck = false;
            Point xPoint = new Point(-1, -1);
            if (ver)
            {
                for (int i = 0; i < match; i++)
                {
                    if (special)
                        Destroy(new Point(point.X, point.Y - i));
                    else
                    {
                        xCheck = FoundAndDestroy(new Point(point.X, point.Y - i), ver);
                        if (xCheck)
                        {
                            xPoint.X = point.X;
                            xPoint.Y = point.Y - i;
                        }
                        else if (match == 5 && (new Point(point.X, point.Y - i) == swapFirst || new Point(point.X, point.Y - i) == swapSecond) && !xCheck)  
                        {
                            swapFirst = new Point(-1, -1);
                            swapSecond = new Point(-1, -1);
                            _stonesArray[point.X, point.Y - i].coloredBomb = true;
                        }
                        else if (match == 4 && i == (match + 1) / 2 && !xCheck)
                            _stonesArray[point.X, point.Y - i].ver = true;                              
                        
                        else 
                            Destroy(new Point(point.X, point.Y - i));
                    }
                }
            }
            else
            {
                for (int i = 0; i < match; i++)
                {
                    if (special)
                        Destroy(new Point(point.X - i, point.Y));
                    else
                    {
                        xCheck = FoundAndDestroy(new Point(point.X - i, point.Y), ver);
                        if (xCheck)
                        {
                            xPoint.X = point.X - i;
                            xPoint.Y = point.Y;
                        }
                        else if (match == 5 && (new Point(point.X - i, point.Y) == swapFirst || new Point(point.X - i, point.Y) == swapSecond) && !xCheck)
                        {
                            swapFirst = new Point(-1, -1);
                            swapSecond = new Point(-1, -1);
                            _stonesArray[point.X - i, point.Y].coloredBomb = true;
                        }
                        else if (match == 4 && i == (match + 1) / 2 && !xCheck)
                            _stonesArray[point.X - i, point.Y].hor = true;
                        else
                            Destroy(new Point(point.X - i, point.Y));
                    }
                }
            }
            if (xPoint.X != -1)                                                                         //bomb L-type
            {
                BombDestroy(xPoint);
                //_stonesArray[xPoint.X, xPoint.Y].coloredBomb = true;                                        
            }

        }

        private bool FoundAndDestroy(Point point, bool ver)
        {
            if (Type.Empty == _stonesArray[point.X, point.Y].type1)
                return false;

            int match = -1;
            int i = 0;

            if (ver)
            {
                while (point.X - i >= 0)
                    if (_stonesArray[point.X - i, point.Y].type1 == _stonesArray[point.X, point.Y].type1)
                    {
                        i++;
                        match++;
                    }
                    else
                        break;
                i = 0;
                while (point.X + i < 8)
                    if (_stonesArray[point.X + i, point.Y].type1 == _stonesArray[point.X, point.Y].type1)
                    {
                        i++;
                        match++;
                    }
                    else
                        break;
            }
            else
            {
                while (point.Y - i >= 0)
                    if (_stonesArray[point.X, point.Y - i].type1 == _stonesArray[point.X, point.Y].type1)
                    {
                        i++;
                        match++;
                    }
                    else
                        break;
                i = 0;
                while (point.Y + i < 8)
                    if (_stonesArray[point.X, point.Y + i].type1 == _stonesArray[point.X, point.Y].type1)
                    {
                        i++;
                        match++;
                    }
                    else
                        break;
            }
            bool res = match >= 3;

            if (res)
            {
                if (ver)
                    MatchDestroy(new Point(point.X + i - 1, point.Y), match, !ver, true);
                else
                    MatchDestroy(new Point(point.X, point.Y + i - 1), match, !ver, true);
            }

            return res;
        }

        private void BombDestroy(Point point)
        {
            System.Threading.Thread.Sleep(250);
            _stonesArray[point.X, point.Y].type1 = Type.Empty;
            _stonesArray[point.X, point.Y].coloredBomb = false;
            point.X--;
            point.Y--;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (point.X + i >= 0 && point.Y + j >= 0 && point.X + i < 8 && point.Y + j < 8)
                        Destroy(new Point(point.X + i, point.Y + j));
        }

        private void FieldCheck()
        {
            if (replacedBomb.IsReplaced)
            {
                BombDestroy(replacedBomb.point);
                replacedBomb.IsReplaced = false;
            }
            int match;
            for (int i = 0; i < 8; i++)
            {
                match = 1;
                for (int y = 1; y < 8; y++)
                {
                    if (_stonesArray[i, y - 1].type1 == _stonesArray[i, y].type1)
                        match++;

                    if (!(_stonesArray[i, y - 1].type1 == _stonesArray[i, y].type1) || y == 7)
                    {
                        if (match > 2 && _stonesArray[i, y - 1].type1 != Type.Empty)
                        {
                            if (y == 7 && _stonesArray[i, y - 1].type1 == _stonesArray[i, y].type1)
                                MatchDestroy(new Point(i, y), match, true, false);
                            else
                                MatchDestroy(new Point(i, y - 1), match, true, false);
                            FieldCheck();
                            return;
                        }
                        match = 1;
                    }
                }
                match = 1;
                for (int x = 1; x < 8; x++)
                {
                    if (_stonesArray[x - 1, i].type1 == _stonesArray[x, i].type1)
                        match++;

                    if (!(_stonesArray[x - 1, i].type1 == _stonesArray[x, i].type1) || x == 7)
                    {
                        if (match > 2 && _stonesArray[x - 1, i].type1 != Type.Empty)
                        {
                            if (x == 7 && _stonesArray[x - 1, i].type1 == _stonesArray[x, i].type1)
                                MatchDestroy(new Point(x, i), match, false, false);
                            else
                                MatchDestroy(new Point(x - 1, i), match, false, false);
                            FieldCheck();
                            return;
                        }
                        match = 1;
                    }
                }
            }
        }

        private void QuickSwap(Point point1, Point point2)
        {
            Stone stone = _stonesArray[point1.X, point1.Y];
            _stonesArray[point1.X, point1.Y] = _stonesArray[point2.X, point2.Y];
            _stonesArray[point2.X, point2.Y] = stone;
        }

        private void Swap(Point posFirst, Point posSecond)
        {
            swapFirst = posFirst;
            swapSecond = posSecond;
            isEnableToTap = false;

            QuickSwap(posFirst, posSecond);

            if ((!(SwapCheck(posFirst) || SwapCheck(posSecond))))
            {
                isEnableToTap = true;
                QuickSwap(posFirst, posSecond);

                int swapSpeed = _speed + 40;
                //int swapSpeed = _speed;

                if (_stonesArray[posFirst.X, posFirst.Y]._point.X > _stonesArray[posSecond.X, posSecond.Y]._point.X)
                {
                    _stonesArray[posFirst.X, posFirst.Y]._point.X -= swapSpeed;
                    _stonesArray[posSecond.X, posSecond.Y]._point.X += swapSpeed;
                }
                else if (_stonesArray[posFirst.X, posFirst.Y]._point.X < _stonesArray[posSecond.X, posSecond.Y]._point.X)
                {
                    _stonesArray[posFirst.X, posFirst.Y]._point.X += swapSpeed;
                    _stonesArray[posSecond.X, posSecond.Y]._point.X -= swapSpeed;
                }
                if (_stonesArray[posFirst.X, posFirst.Y]._point.Y > _stonesArray[posSecond.X, posSecond.Y]._point.Y)
                {
                    _stonesArray[posFirst.X, posFirst.Y]._point.Y -= swapSpeed;
                    _stonesArray[posSecond.X, posSecond.Y]._point.Y += swapSpeed;
                }
                else if (_stonesArray[posFirst.X, posFirst.Y]._point.Y < _stonesArray[posSecond.X, posSecond.Y]._point.Y)
                {
                    _stonesArray[posFirst.X, posFirst.Y]._point.Y += swapSpeed;
                    _stonesArray[posSecond.X, posSecond.Y]._point.Y -= swapSpeed;
                }
            }


        }

        // elements falling down
        public void Refresh()
        {
            isEnableToTap = true;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (_stonesArray[i, j]._point.X != (i ) * 100 || _stonesArray[i, j]._point.Y != (j) * 100)
                        isEnableToTap = false;

                    if (_stonesArray[i, j]._point.X > i * 100 )
                        _stonesArray[i, j]._point.X = _stonesArray[i, j]._point.X - _speed;
                    else if (_stonesArray[i, j]._point.X < i * 100)
                        _stonesArray[i, j]._point.X = _stonesArray[i, j]._point.X + _speed;

                    if (_stonesArray[i, j]._point.Y > j * 100 )
                        _stonesArray[i, j]._point.Y = _stonesArray[i, j]._point.Y - _speed;
                    else if (_stonesArray[i, j]._point.Y < j * 100 )
                        _stonesArray[i, j]._point.Y = _stonesArray[i, j]._point.Y + _speed;                 
                }
        }

        public void MouseLeftClick(int x, int y)
        {

            int devSc;
            devSc = 800;
            if (x > 0 && x < devSc && y > 0 && y < devSc && isEnableToTap)
            {
                Point newPos = new Point(x / 100, y / 100);
                if (stoneSelected && ((changePos.X == newPos.X && Math.Abs(changePos.Y - newPos.Y) == 1) || (changePos.Y == newPos.Y && Math.Abs(changePos.X - newPos.X) == 1)))
                {
                    Swap(changePos, newPos);
                    stoneSelected = false;
                }
                else
                {
                    stoneSelected = true;
                    changePos.X = x / 100;
                    changePos.Y = y / 100;

                }
            }
            else
            {
                stoneSelected = false;
            }
        }

        public void Draw(GameTime game, SpriteBatch spriteBatch)
        {
            Refresh();
            if (isEnableToTap)
            {
                FieldCheck();
                FillInTheField();
            }
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {                    
                    if (i == changePos.X && j == changePos.Y && stoneSelected)
                    {
                        spriteBatch.Draw(selected, new Rectangle(((i * 100)), ((j * 100)), 100, 100), Color.White);  //select element
                    }
                    spriteBatch.Draw(_texture, new Rectangle(_stonesArray[i, j]._point.X, _stonesArray[i, j]._point.Y, 100, 100), TextureType(_stonesArray[i, j]), Color.White);
                }
            
            if (explotion.IsBoom)
            {
                if (explotion.TicForScore())
                    Play.IncGameScore(explotion.boomList.Count);
                Rectangle rectangle = explotion.TextureRect();
                explotion.boomList.ForEach(p => spriteBatch.Draw(textureExplotion, new Rectangle((p.X - 20), (p.Y - 20), 140, 140), rectangle, Color.White));
            }

            if (time.Elapsed.Minutes < 1)
                spriteBatch.DrawString(Play.TimerFont, (59 - time.Elapsed.Seconds).ToString(), new Vector2(1000, 100), Color.Crimson);
            if (time.Elapsed.Seconds == 59)
            {
                //scene = Scene.Menu;
                isEnableToTap = false;
                //time.Stop();
            }
        }
    }
}
