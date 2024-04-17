using Newtonsoft.Json;
using SpaceTrespassers.Models;
using SpaceTrespassers.Models.Enemies;
using SpaceTrespassers.Models.Interfaces;
using SpaceTrespassers.Models.Projectiles;
using SpaceTrespassers.Utils;
using System.Globalization;
using System.Text;

namespace SpaceTrespassers.Core
{
    public class CoreController
    {
        SpaceShip spaceShip;

        public CoreController(SpaceShip spaceShip)
        {
            this.spaceShip = spaceShip;
        }

        /// <summary>
        /// Main method for each tick.
        /// - Invoke player move processing method;
        /// - Hover enemies;
        /// - Make the spaceship fire if it should;
        /// - Make an enemy fire;
        /// - Invoke method for advancing all projectiles.
        /// </summary>
        public void MainLogic()
        {
            ProcessPlayerMove();

            if (GameState.countLastHoverTicks > Constants.HoverAfterTicks)
            {
                HoverEnemies();
                GameState.countLastHoverTicks = 0;
            }

            if (GameState.shooting && GameState.countLastShotTicks > GameState.firingSpeed)
            {
                spaceShip.Fire();
                GameState.countLastShotTicks = 0;
            }

            if (GameState.countLastEnemyShot > Constants.DefaultEnemyFiringSpeed - ((GameState.currentLevel - 1) * 3))
            {
                for (int i = 0; i < GameState.currentLevel; i++)
                {
                    ChooseEnemyToFire();
                    GameState.countLastEnemyShot = 0;
                }
            }

            MoveLazers();

            GameState.countLastEnemyShot++;
            GameState.countLastHoverTicks++;
            GameState.countLastShotTicks++;
        }

        /// <summary>
        /// Handle user input for moving the ship horizontally.
        /// </summary>
        public void ProcessPlayerMove()
        {
            if (GameState.movingRight && !GameState.movingLeft && spaceShip.Left < Form1.ActiveForm.Width - (spaceShip.Width * 1.4))
            {
                spaceShip.MoveRight();
            }

            if (GameState.movingLeft && !GameState.movingRight && spaceShip.Left > spaceShip.Width * 0.3)
            {
                spaceShip.MoveLeft();
            }
        }

        /// <summary>
        /// Choose a random enemy to fire a projectile and invoke it's firing method.
        /// </summary>
        public void ChooseEnemyToFire()
        {
            Random rnd = new Random();

            int randomEnemyIndex = rnd.Next(0, GameState.ShootableEnemies.Count());
            IShootableEnemy targetEnemy = GameState.ShootableEnemies[randomEnemyIndex];
            targetEnemy.FireAttack();
        }

        /// <summary>
        /// Advance all projectiles by invoking their Move method.
        /// </summary>
        public void MoveLazers()
        {
            foreach (IProjectile projectile in GameState.Projectiles)
            {
                projectile.Move();
            }
        }

        /// <summary>
        /// Hover all enemies by invoking their Hover method.
        /// </summary>
        public void HoverEnemies()
        {
            foreach (IEnemy enemy in GameState.Enemies)
            {
                enemy.Hover();
            }
        }

        /// <summary>
        /// - Saves the current score;
        /// - Adds it to the scoreboard;
        /// - Sorts the scoreboard;
        /// - Removes the last score if their count is greater than 6.
        /// </summary>
        public void SaveScore()
        {
            GameState.currentScore.SaveScore();
            GameState.allScores.Add(GameState.currentScore);
            GameState.allScores = GameState.allScores.OrderByDescending(x => x.Points).ThenBy(x => DateTime.ParseExact(x.ScoredOn, "HH:mm:ss - dd.MM.yyyy", CultureInfo.InvariantCulture)).ToList();

            if (GameState.allScores.Count > 6)
            {
                GameState.allScores.RemoveAt(GameState.allScores.Count - 1);
            }

            File.WriteAllText(Constants.ScoresFilePath, JsonConvert.SerializeObject(GameState.allScores));
        }

        /// <summary>
        /// Calculates collisions between projectiles and characters.
        /// Goes through each enemy, and then for it checks each projectile.
        /// Game is over if the spaceship is hit.
        /// If no enemies are left, the level is won.
        /// A point is added if an enemy is hit.
        /// (I have some custom 2d collision logic because of the weird hitboxes of the image resources.)
        /// </summary>
        public void CalculateCollisions()
        {
            foreach (Enemy enemy in GameState.Enemies)
            {
                foreach (PictureBox projectile in GameState.Projectiles)
                {
                    bool isSpaceshipHit = projectile.GetType() == typeof(EnemyProjectile) && spaceShip.Bounds.IntersectsWith(projectile.Bounds);
                    if (isSpaceshipHit)
                    {
                        GameState.outcome = GameResult.Defeat;
                        return;
                    }

                    bool isHitVertical = projectile.Top <= enemy.Top + enemy.Height;

                    if (!isHitVertical) continue;

                    bool isHitHorizontal = projectile.Left >= enemy.Left - 12 && projectile.Left <= enemy.Left + enemy.Width + 12;
                    bool isEnemyHit = (projectile.GetType() == typeof(Laser) || projectile.GetType() == typeof(Missle)) && isHitHorizontal && isHitVertical;

                    if (isEnemyHit)
                    {
                        int damage = projectile.GetType() == typeof(Missle) ? 5 : 1;
                        enemy.TakeDamage(damage);
                        projectile.Dispose();

                        if (GameState.outcome == GameResult.Playing)
                        {
                            GameState.currentScore.AddPoints(1);
                        }
                    }
                }
            }

            if (GameState.outcome == GameResult.Playing && !GameState.Enemies.Any())
            {
                GameState.outcome = GameResult.Victory;
                GameState.victory = true;
                return;
            }
        }

        /// <summary>
        /// Calculates the spacing between each enemy depending on the screen's resolution.
        /// </summary>
        public int GetEnemySpacing()
        {
            int screenWidthDifference = (Form1.ActiveForm.Width - 1920) / 15;
            int defaultSpacing = 120;
            int result = defaultSpacing + screenWidthDifference;

            return result;
        }

        /// <summary>
        /// Spawns each enemy according to the level seed in the 'positions' array.
        /// Each row has 14 possible places for an enemy, so the horizontal row position is calculated via the '%' (modulo) operator.
        /// </summary>
        /// <param name="positions">
        /// This is an array with all enemies for the level and their types:
        /// 0 - nothing
        /// 1 - basic enemy
        /// 2 - medium enemy
        /// 3 - armoured enemy
        /// </param>
        public void SpawnEnemies(int[] positions)
        {
            int enemySpacing = GetEnemySpacing();
            int row = 1;

            for (int i = 0; i < positions.Length; i++)
            {
                if (i % 14 == 0 && i != 0)
                {
                    row++;
                }

                if (positions[i] > 0)
                {
                    int positionInRow = (i + 1) % 14;
                    if (positionInRow == 0)
                    {
                        positionInRow = 14;
                    }

                    positionInRow *= enemySpacing;

                    if (row % 2 != 0)
                    {
                        positionInRow -= 60;             
                    }                                      

                    if (positions[i] == 1)
                    {
                        Form1.ActiveForm.Controls.Add(new BasicEnemy(positionInRow, row * 100));
                    }
                    else if (positions[i] == 2)
                    {
                        Form1.ActiveForm.Controls.Add(new MediumEnemy(positionInRow, row * 100));
                    }
                    else if (positions[i] == 3)
                    {
                        Form1.ActiveForm.Controls.Add(new ArmouredEnemy(positionInRow, row * 100));
                    }

                    // DEBUG
                    // this.Update();
                    // DEBUG
                }
            }
        }

        /// <summary>
        /// Reads a json text file from the file system, or creates it if it doesn't exist.
        /// Deserializes the json file into a c# object.
        /// Returns formatted and numbered scores as a string.
        /// </summary>
        /// <returns></returns>
        public string LoadScoreboard()
        {
            if (!File.Exists("../../../scores.txt"))
            {
                GameState.allScores = new List<ScoreSave>();
                return "Be the first to score!";
            }

            GameState.allScores = JsonConvert.DeserializeObject<List<ScoreSave>>(File.ReadAllText(Constants.ScoresFilePath));

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < GameState.allScores.Count; i++)
            {
                result.AppendLine($"{i + 1}. {GameState.allScores[i].ScoreBoardInfo()}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
