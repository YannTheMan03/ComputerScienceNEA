using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using iteration1.Properties;

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

        // Private variables
        private Direction _moveDirection = Direction.Right;
        private int _speed = 1;
        private int _stepDown = 20;
        private int _screenWidth = 400;
        private int _health = 0;
        private int _waveNum = 0;
        private int _maxBossHealth = 0; // Store max health for boss health bar

        // Boss shooting state
        private enum BossShootPhase { BigShot, Pause, Spray }
        private BossShootPhase _bossPhase = BossShootPhase.BigShot;
        private int _bossShootTimer = 0;
        private int _sprayBulletsLeft = 0;
        private const int BossPauseDuration = 60;  
        private const int BossSprayCount = 12;     
        private const int BossSprayInterval = 5;   

        private Random rnd = new Random();

        public List<Bullet> EnemyBullets { get; private set; }
        public List<Bullet> DisposedEnemyBullets { get; set; }
        public Bullet enemyBulletInstance { get; set; }


        // Choosing the wave
        public Wave(int waveNumber)
        {
            switch (waveNumber)
            {
                case 1: _enemyCount = 4; _rowCount = 1; _speed = 1; _health = 50; break;
                case 2: _enemyCount = 8; _rowCount = 2; _speed = 2; _health = 50; break;
                case 3: _enemyCount = 12; _rowCount = 3; _speed = 2; _health = 100; break;
                case 4: _enemyCount = 16; _rowCount = 4; _speed = 3; _health = 100; break;
                case 5: _enemyCount = 20; _rowCount = 5; _speed = 3; _health = 150; break;
                case 6:
                    _enemyCount = 1;
                    _rowCount = 1;
                    _health = 10000;
                    _maxBossHealth = 10000; // Store max health for health bar calculation
                    _waveNum = 6;
                    break;
                default: _enemyCount = 4; _rowCount = 1; _speed = 1; break;
            }
            enemies = new List<Enemy>();
            EnemyBullets = new List<Bullet>();
            DisposedEnemyBullets = new List<Bullet>();
            SpawnEnemies(_enemyCount, _rowCount, _waveNum);


        }
        // Spawing the enemies
        public void SpawnEnemies(int enemyCount, int rowCount, int waveNum)
        {
            int spacingX = 10;
            int spacingY = 20;
            int screenWidth = 400;
            int enemyWidth = Properties.Resources.enemyPng.Width;
            int enemyHeight = Properties.Resources.enemyPng.Height;
            int enemiesPerRow = 6;
            int totalRowWidth = enemiesPerRow * enemyWidth + (enemiesPerRow - 1) * spacingX;
            int startX = (screenWidth - totalRowWidth) / 2;
            int startY = 50;
            int spawned = 0;

            if (_waveNum == 6)
            {
                int bossWidth = Properties.Resources._210_2102512_galaga_boss_png_transparent_png_removebg_preview1.Width;
                int bossX = (screenWidth - bossWidth) / 2;
                Enemy boss = new Enemy(
                    Properties.Resources._210_2102512_galaga_boss_png_transparent_png_removebg_preview1,
                    bossX, startY, _health, _speed
                );
                enemies.Add(boss);
                return;
            }

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < enemiesPerRow && spawned < enemyCount; col++)
                {
                    int x = startX + col * (enemyWidth + spacingX);
                    int y = startY + row * (enemyHeight + spacingY);
                    enemies.Add(new Enemy(Properties.Resources.enemyPng, x, y, _health, _speed));
                    spawned++;
                }
            }
        }

        // Updating the positions.
        public void Update()
        {
            bool hittingEdge = false;

            foreach (var enemy in enemies)
            {
                enemy.PositionX += (int)_moveDirection * _speed;
                if (enemy.PositionX <= 0 || enemy.PositionX + enemy.SpriteImage.Width >= _screenWidth)
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
            foreach (var bullet in EnemyBullets)
            {
                bullet.PositionY += 3;
            }
            EnemyBullets.RemoveAll(b => b.PositionY > 600);
            if (_waveNum != 6)
            {
                foreach (var enemy in enemies)
                {
                    if (rnd.Next(0, 1000) < 3)
                    {
                        EnemyBullets.Add(new Bullet(Properties.Resources.bulletImage___Copy, enemy.PositionX, enemy.PositionY));
                    }
                }
            }
            else if (_waveNum == 6 && enemies.Count > 0)
            {
                Enemy boss = enemies[0];
                _bossShootTimer++;

                switch (_bossPhase)
                {
                    case BossShootPhase.BigShot:
                        EnemyBullets.Add(new Bullet(
                            Properties.Resources.bulletImage___Copy,
                            boss.PositionX + boss.SpriteImage.Width / 2 - 5,
                            boss.PositionY + boss.SpriteImage.Height
                        ));
                        _bossPhase = BossShootPhase.Pause;
                        _bossShootTimer = 0;
                        break;

                    case BossShootPhase.Pause:
                        if (_bossShootTimer >= BossPauseDuration)
                        {
                            _bossPhase = BossShootPhase.Spray;
                            _sprayBulletsLeft = BossSprayCount;
                            _bossShootTimer = 0;
                        }
                        break;

                    case BossShootPhase.Spray:
                        if (_bossShootTimer % BossSprayInterval == 0 && _sprayBulletsLeft > 0)
                        {
                            int bulletsFired = BossSprayCount - _sprayBulletsLeft;
                            float step = (float)boss.SpriteImage.Width / (BossSprayCount - 1);
                            int sprayX = (int)(boss.PositionX + step * bulletsFired);
                            EnemyBullets.Add(new Bullet(
                                Properties.Resources.bulletImage___Copy,
                                sprayX,
                                boss.PositionY + boss.SpriteImage.Height
                            ));
                            _sprayBulletsLeft--;
                        }
                        if (_sprayBulletsLeft <= 0 && _bossShootTimer >= BossSprayCount * BossSprayInterval)
                        {
                            _bossPhase = BossShootPhase.BigShot;
                            _bossShootTimer = 0;
                        }
                        break;
                }
            }
        }

        // Drawing to the screen
        public void Draw(Graphics g)
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw(g);
            }
            foreach (var bullet in EnemyBullets)
            {
                bullet.Draw(g);
            }

            // Draw boss health bar if this is wave 6 and boss exists
            if (_waveNum == 6 && enemies.Count > 0)
            {
                DrawBossHealthBar(g, enemies[0]);
            }
        }

        /// <summary>
        /// Draws a health bar above the boss enemy
        /// </summary>
        private void DrawBossHealthBar(Graphics g, Enemy boss)
        {
            // Health bar dimensions
            int barWidth = 200;
            int barHeight = 20;
            int barX = (_screenWidth - barWidth) / 2; // Center the health bar
            int barY = boss.PositionY - 30; // Position above the boss

            // Calculate health percentage
            float healthPercent = (float)boss.Health / _maxBossHealth;
            int fillWidth = (int)(barWidth * healthPercent);

            // Determine bar color based on health percentage
            Color barColor;
            if (healthPercent > 0.6f)
                barColor = Color.Green;
            else if (healthPercent > 0.3f)
                barColor = Color.Yellow;
            else
                barColor = Color.Red;

            // Draw background (empty part)
            using (SolidBrush backgroundBrush = new SolidBrush(Color.DarkGray))
            {
                g.FillRectangle(backgroundBrush, barX, barY, barWidth, barHeight);
            }

            // Draw health fill
            using (SolidBrush healthBrush = new SolidBrush(barColor))
            {
                g.FillRectangle(healthBrush, barX, barY, fillWidth, barHeight);
            }

            // Draw border
            using (Pen borderPen = new Pen(Color.White, 2))
            {
                g.DrawRectangle(borderPen, barX, barY, barWidth, barHeight);
            }

            // Draw health text
            string healthText = $"{boss.Health} / {_maxBossHealth}";
            using (Font font = new Font("Pixeloid Sans", 10, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                SizeF textSize = g.MeasureString(healthText, font);
                float textX = barX + (barWidth - textSize.Width) / 2;
                float textY = barY + (barHeight - textSize.Height) / 2;

                // Draw text shadow for better visibility
                using (SolidBrush shadowBrush = new SolidBrush(Color.Black))
                {
                    g.DrawString(healthText, font, shadowBrush, textX + 1, textY + 1);
                }
                g.DrawString(healthText, font, textBrush, textX, textY);
            }
        }
    }
}