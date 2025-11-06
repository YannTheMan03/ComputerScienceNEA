using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace iteration1
{
    /// <summary>
    /// Wave class to create each individual wave on the screen.
    /// </summary>
    /// 
    public enum Direction
    {
        Left = -1,
        Right = 1
    }
    public class Wave
    {
        // Protected variables

        protected int _enemyCount = 0;
        protected int _rowCount = 0;
        public List<Enemy> enemies { get; private set; }

        private Direction _moveDirection = Direction.Right;
        private int _speed = 2;      // horizontal speed
        private int _stepDown = 20;  // pixels to move down when hitting edge
        private int _screenWidth = 400; // your form width




        public Wave(int waveNumber)
        {
            switch (waveNumber)
            {
                case 1: _enemyCount = 4; _rowCount = 1; break;
                case 2: _enemyCount = 8; _rowCount = 2; break;
                case 3: _enemyCount = 12; _rowCount = 3; break;
                case 4: _enemyCount = 16; _rowCount = 4; break;
                case 5: _enemyCount = 20; _rowCount = 5; break;
                default: _enemyCount = 4; _rowCount = 1; break;
            }
            enemies = new List<Enemy>();
            SpawnEnemies(_enemyCount, _rowCount);


        }
        public void SpawnEnemies(int enemyCount, int rowCount)
        {
            int spacingX = 10;
            int spacingY = 20;
            int screenWidth = 400;
            int enemyWidth = Properties.Resources.enemyPng.Width;
            int enemyHeight = Properties.Resources.enemyPng.Height;


            int enemiesPerRow = Math.Min(enemyCount, screenWidth / (enemyWidth + spacingX));
            int totalRowWidth = enemiesPerRow * enemyWidth + (enemiesPerRow - 1) * spacingX;
            int startX = (screenWidth - totalRowWidth) / 2;
            int startY = 50;

            int spawned = 0;

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < enemiesPerRow && spawned < enemyCount; col++)
                {
                    int x = startX + col * (enemyWidth + spacingX);
                    int y = startY + row * (enemyHeight + spacingY);

                    Enemy enemy = new Enemy(Properties.Resources.enemyPng, x, y, 50, _speed);
                    enemies.Add(enemy);
                    spawned++;
                }
            }
        }
        public void Update()
        {
            bool hittingEdge = false;

            foreach (var enemy in enemies)
            {
                enemy.PositionX += (int)_moveDirection * _speed;
                if( enemy.PositionX <= 0 || enemy.PositionX + enemy.SpriteImage.Width >= _screenWidth)
                {
                    hittingEdge = true;
                }
            }
            if (hittingEdge)
            {
                _moveDirection = _moveDirection == Direction.Right ? Direction.Left : Direction.Right;

                foreach (var enemy in enemies)
                {
                    enemy.PositionY += _stepDown;
                }
            }
            enemies.RemoveAll(e => !e.IsAlive);
        }
        public void Draw(Graphics g)
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw(g);
            }
        }
    }      

}



