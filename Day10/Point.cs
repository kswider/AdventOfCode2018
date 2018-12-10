namespace Day10
{
    public class Point
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        private int velX;
        private int velY;

        public Point(int posX, int posY, int velX, int velY)
        {
            PosX = posX;
            PosY = posY;
            this.velX = velX;
            this.velY = velY;
        }

        public void Move()
        {
            PosX += velX;
            PosY += velY;
        }
    }
}