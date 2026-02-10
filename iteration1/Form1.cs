using System.Drawing.Drawing2D;
using System.Timers;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
namespace iteration1
{
    /// <summary>
    /// The main class of the form
    /// </summary>
    public partial class Form1 : Form
    {
        // Game State Variables
        private bool _isGameOver = false;
        private int _scoreCount = 0;
        private int _gcCount = 0;

        // Player Variables
        private Player _player;
        private int _playerLivesLeft = 3;
        private int _livesSpacing = 0;

        // Bullet Variables
        private List<Bullet> _bullets = [];
        private List<Bullet> _disposedBullets = [];
        private Bullet _bullet;
        private const int BulletVelocity = -5;
        private const int BulletDamage = 25;
        private DateTime _lastShot = DateTime.MinValue;
        private const int FireDelay = 200;

        // PowerUp variables
        public int dmgLevel = 0;
        public int powerLevel = 0;
        public int speedLevel = 0;
        private Random randNum = new Random();
        private List<powerUp> _activePowerups = new List<powerUp>();



        // User Interface Variables
        private Label _scoreLabel = new();
        private Rectangle _formBounds;
        private Image[] _background;
        private int _currentImageIndex = 0;
        private int BackgroundChangeCounter = 0;
        private const int BackgroundChangeDelay = 500;
        private DateTime _lastBackgroundChange = DateTime.Now;

        // Leaderboard Variables
        private Leaderboard _leaderboard = new();
        private static readonly string LeaderBoardPath
            = "C:\\Users\\yb.2415248\\OneDrive - Hereford Sixth Form College\\Computer Science\\C03 - Project\\Assets\\leaderboard.json";
        private string _username;

        // Wave Variables
        private Wave currentWave;
        private int _currentWaveIndex = 1;


        // Flags
        private bool _isMovingLeft;
        private bool _isMovingRight;
        private bool _isPressingSpace;
        private bool _menuOpen = false;

   
        // Form Loading
        public Form1(string username)
        {
            InitializeComponent();
            _username = username;
            ChangeBackground();
            StartWave(_currentWaveIndex);
        }

        // Change Background Image
        void ChangeBackground()
        {
            _background = new Image[]
            {
                Properties.Resources.BG1,
                Properties.Resources.BG2,
                Properties.Resources.BG3,
                Properties.Resources.BG4,
                Properties.Resources.BG5,
                Properties.Resources.BG6,
                Properties.Resources.BG7,
                Properties.Resources.BG8

            };

            this.BackgroundImage = _background[0];
        }

        // Form Loading
        private void Form1_Load(object sender, EventArgs e)
        {
            // Formatting Screen
            this.Size = new System.Drawing.Size(400, 600);
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.DoubleBuffered = true;
            _formBounds = ClientRectangle;

            // New Player Instance
            _player = new Player(Properties.Resources.player_one, 175, 500);

            // Load Scores
            _leaderboard.Load(LeaderBoardPath);

            // Score Label Creation
            _scoreLabel.Text = _scoreCount.ToString();
            _scoreLabel.Visible = true;
            _scoreLabel.Location = new System.Drawing.Point(this.ClientSize.Width - _scoreLabel.Width, 0);
            _scoreLabel.BackColor = System.Drawing.Color.Transparent;
            _scoreLabel.ForeColor = System.Drawing.Color.White;
            _scoreLabel.Font = new Font("Pixeloid Sans", 14);
            _scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Controls.Add(_scoreLabel);
        }

        // Key Pressing
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Check Keys
            if (e.KeyCode == Keys.Escape)
            {
                _menuOpen = true; 
                game_Timer.Stop();
                pauseMenu pause = new pauseMenu(_scoreCount);
                pause.Show();
            }
            if (e.KeyCode == Keys.Left) _isMovingLeft = true;
            if (e.KeyCode == Keys.Right) _isMovingRight = true;
            if (e.KeyCode == Keys.Space && _isPressingSpace == false)
            {
                _isPressingSpace = true;

                // Firing Delay
                if ((DateTime.Now - _lastShot).TotalMilliseconds > FireDelay)
                {
                    _bullet = new Bullet(Properties.Resources.bulletImage, _player.PositionX + 17, _player.PositionY);
                    _bullets.Add(_bullet);
                    _lastShot = DateTime.Now;
                }
            }
        }

        // Key Release
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Check Keys
            if (e.KeyCode == Keys.Space) _isPressingSpace = false;
            if (e.KeyCode == Keys.Left) _isMovingLeft = false;
            if (e.KeyCode == Keys.Right) _isMovingRight = false;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            // Creating Graphics
            Graphics G = e.Graphics;

            // Draw Player
            G.DrawImage(_player.SpriteImage, _player.PositionX, _player.PositionY);

            // Draw Hearts (lives)
            if (_playerLivesLeft > 0)
            {
                for (int i = 0; i < _playerLivesLeft; i++)
                {
                    G.DrawImage(Properties.Resources.heartPng, i * 30, 0);
                }
            }

            // Draw Bullets
            foreach (Bullet bullet in _bullets)
            {
                G.DrawImage(bullet.SpriteImage, bullet.PositionX, bullet.PositionY);
            }

            // Draw Enemies
            currentWave?.Draw(G);

            // Draw Active Powerups
            foreach (var powerup in _activePowerups)
            {
                G.DrawImage(powerup.SpriteImage, powerup.PositionX, powerup.PositionY);
            }
        }

        private void OnGameTick(object sender, EventArgs e)
        {
            // Player movement
            if (_isMovingLeft && _player.PositionX > 7)
                _player.PositionX -= 7;
            if (_isMovingRight && _player.PositionX < _formBounds.Width - _player.SpriteImage.Width)
                _player.PositionX += 7;

            // Update bullet positions
            foreach (Bullet bullet in _bullets)
                bullet.PositionY += BulletVelocity;

            // Remove bullets that are off-screen
            _bullets.RemoveAll(bullet => !bullet.HitBox.IntersectsWith(_formBounds));

            // Update Enemies and Wave
            if (currentWave != null)
            {
                currentWave.Update();

                // Handle bullet-enemy collisions
                foreach (var bullet in _bullets.ToList())
                {
                    foreach (var enemy in currentWave.enemies.ToList())
                    {
                        if (bullet.HitBox.IntersectsWith(enemy.HitBox))
                        {
                            enemy.TakeDamage(BulletDamage);
                            _bullets.Remove(bullet);
                            _scoreCount += 10;

                            // Enemy died - handle death
                            if (enemy.Health <= 0)
                            {
                                currentWave.enemies.Remove(enemy);

                                // Spawn powerup when enemy dies
                                powerUp newPowerup = spawnPowerup(powerLevel, speedLevel, dmgLevel,
                                    enemy.PositionX, enemy.PositionY);
                                if (newPowerup != null)
                                {
                                    _activePowerups.Add(newPowerup);
                                }
                            }
                            break;
                        }
                    }
                }

                // Handle enemy-player collisions
                foreach (var enemy in currentWave.enemies.ToList())
                {
                    if (enemy.HitBox.IntersectsWith(_player.HitBox) || enemy.PositionY > 550)
                    {
                        currentWave.enemies.Remove(enemy);
                        _playerLivesLeft -= 1;
                    }
                }

                // Handle enemy bullets
                foreach (var enemyBullet in currentWave.EnemyBullets.ToList())
                {
                    enemyBullet.PositionY += 2;

                    if (enemyBullet.HitBox.IntersectsWith(_player.HitBox))
                    {
                        _playerLivesLeft--;
                        currentWave.EnemyBullets.Remove(enemyBullet);
                    }
                    else if (enemyBullet.PositionY > _formBounds.Height)
                    {
                        currentWave.EnemyBullets.Remove(enemyBullet);
                    }
                }

                // Check if wave is complete
                if (currentWave.enemies.Count == 0)
                {
                    _currentWaveIndex++;
                    StartWave(_currentWaveIndex);
                }
            }

            // Update Powerups
            foreach (var powerup in _activePowerups.ToList())
            {
                powerup.PositionY += 3; // Make them fall

                // Check collision with player
                if (powerup.HitBox.IntersectsWith(_player.HitBox))
                {
                    // Apply powerup effect based on type
                    switch (powerup.identifier)
                    {
                        case 1: // power
                            powerLevel++;
                            break;
                        case 2: // speed
                            speedLevel++;
                            break;
                        case 3: // damage
                            dmgLevel++;
                            break;
                    }
                    _activePowerups.Remove(powerup);
                }
                // Remove if off screen
                else if (powerup.PositionY > _formBounds.Height)
                {
                    _activePowerups.Remove(powerup);
                }
            }

            // Background Change
            if ((DateTime.Now - _lastBackgroundChange).TotalMilliseconds >= BackgroundChangeDelay)
            {
                _currentImageIndex = (_currentImageIndex + 1) % _background.Length;
                this.BackgroundImage = _background[_currentImageIndex];
                _lastBackgroundChange = DateTime.Now;
            }

            // Update score display
            _scoreLabel.Text = _scoreCount.ToString();

            // Check game over conditions
            if ((_playerLivesLeft <= 0) || (_currentWaveIndex == 6))
            {
                game_Timer.Stop();
                GameOver();
            }

            // Redraw Screen
            this.Invalidate();
        }

        // End Game
        private void GameOver()
        {
            var topScores = _leaderboard.GetTopScores(5);
            string message = " Leaderboard: \n\n";

            _leaderboard.AddOrUpdateScore(_username, _scoreCount);
            _leaderboard.Save(LeaderBoardPath);

            int rank = 1;
            foreach (var entry in topScores)
            {
                string medal = rank switch
                {
                    1 => "🏆",
                    _ => "     "
                };
                message += $"{medal} {rank}. {entry.Key}: {entry.Value}\n";
                rank++;
            }

            MessageBox.Show(message, "Game Over");
            
            game_Timer.Stop();
            this.Close();
        }

        // Working on waves
        private void StartWave(int _currentWaveIndex)
        {
            currentWave = new Wave(_currentWaveIndex);
            this.Invalidate();
        }

        private powerUp spawnPowerup(int powerLevel, int speedLevel, int damageLevel, int enemyX, int enemyY)
        {
            if (randNum.Next(1, 101) > 90)
                return null;

            int type = randNum.Next(1, 4); 
            switch (type)
            {
                case 1: //power
                    return new powerUp(
                        Properties.Resources.pixil_frame_0__1_,
                        enemyX,
                        enemyY,
                        1,  // ← Changed: always worth 1 level
                        1
                    );
                case 2: //speed
                    return new powerUp(
                        Properties.Resources.pixil_frame_0__2_,
                        enemyX,
                        enemyY,
                        1,  // ← Changed: always worth 1 level
                        2
                    );
                case 3: // damage
                    Bitmap sprite;
                    switch (damageLevel)
                    {
                        case 0:
                            sprite = Properties.Resources.pixil_frame_0__3_;
                            break;
                        case 1:
                            sprite = Properties.Resources.pixil_frame_0__4_;
                            break;
                        case 2:
                            sprite = Properties.Resources.pixil_frame_0__5_;
                            break;
                        default:
                            sprite = Properties.Resources.pixil_frame_0__3_;
                            break;
                    }
                    return new powerUp(
                        sprite,
                        enemyX,
                        enemyY,
                        1,  // ← Changed: always worth 1 level
                        3
                    );
            }
            return null;
        }
    }
}
