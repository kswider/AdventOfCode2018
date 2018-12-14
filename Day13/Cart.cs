using System;

namespace Day13
{
    public class Cart
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        private TurnDirection nextTurn = 0;
        private int velX;
        private int velY;
        private Direction movingDirection;
        public Cart(int x, int y, Direction direction)
        {
            PositionX = x;
            PositionY = y;
            movingDirection = direction;
            UpdateVelocity();
        }

        public void Move(char[,] map)
        {
            switch(map[PositionY,PositionX])
            {
                case '-':
                    MakeMove();
                    break;
                case '|':
                    MakeMove();
                    break;
                case '\\':
                    ChangeDirection('\\');
                    MakeMove();
                    break;
                case '/':
                    ChangeDirection('/');
                    MakeMove();
                    break;
                case '+':
                    ChangeDirection('+');
                    MakeMove();
                    break;

            }
        }

        private void MakeMove()
        {
            PositionX += velX;
            PositionY += velY;
        }

        private void ChangeDirection(char c)
        {
            switch(c)
            {
                case '\\':
                    switch(this.movingDirection)
                    {
                        case Direction.LEFT:
                            movingDirection = Direction.UP;
                            break;
                        case Direction.RIGHT:
                            movingDirection = Direction.DOWN;
                            break;
                        case Direction.UP:
                            movingDirection = Direction.LEFT;
                            break;
                        case Direction.DOWN:
                            movingDirection = Direction.RIGHT;
                            break;                       
                    }
                    break;
                case '/':
                    switch(this.movingDirection)
                    {
                        case Direction.LEFT:
                            movingDirection = Direction.DOWN;
                            break;
                        case Direction.RIGHT:
                            movingDirection = Direction.UP;
                            break;
                        case Direction.UP:
                            movingDirection = Direction.RIGHT;
                            break;
                        case Direction.DOWN:
                            movingDirection = Direction.LEFT;
                            break;                     
                    }
                    break;
                case '+':

                    switch(nextTurn)
                    {
                        case TurnDirection.LEFT:
                            movingDirection = (Direction) mod(((int)movingDirection - 1),  4);
                            if((int)movingDirection == -1)
                            {
                                Console.Write("?");
                            }                           
                            break;
                        case TurnDirection.RIGHT:
                            movingDirection = (Direction) mod(((int)movingDirection + 1),  4);
                            break;
                            default:
                                break;                          
                    }
                    nextTurn = (TurnDirection)((int)(nextTurn + 1) % 3);
                    break;
            }
            UpdateVelocity();
        }

        private int mod(int x, int m)  => (x%m + m)%m;

        private void UpdateVelocity()
        {
            switch(movingDirection)
            {
                case Direction.LEFT:
                    velX = -1;
                    velY = 0;
                    break;
                case Direction.RIGHT:
                    velX = 1;
                    velY = 0;
                    break;
                case Direction.UP:
                    velX = 0;
                    velY = -1;
                    break;
                case Direction.DOWN:
                    velX = 0;
                    velY = 1;
                    break;
            }
        }
    }

    public enum TurnDirection
    {
        LEFT = 0,
        STRAIGHT = 1,
        RIGHT = 2
    }

    public enum Direction
    {
        LEFT = 0,
        UP = 1,
        RIGHT = 2,
        DOWN = 3
    }
}