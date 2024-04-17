using SpaceTrespassers.Models.Enemies;
using SpaceTrespassers.Models.Interfaces;

namespace SpaceTrespassers.Utils
{
    public static class GameState
    {
        public static bool movingRight;
        public static bool movingLeft;
        public static bool shooting;
        public static bool isPaused;
        public static bool menuFiredAtEnemy;
        public static bool enteredName;
        public static bool armedMissle;
        public static bool victory;

        public static int firingSpeed;
        public static int countLastHoverTicks;
        public static int countLastShotTicks;
        public static int countLastMenuShotTicks;
        public static int countLastEnemyShot;
        public static int currentLevel;
        public static GameResult outcome;
        public static Enemy seekEnemy;
        public static Enemy lastTargetedEnemy;
        public static ScoreSave currentScore;
        public static List<ScoreSave> allScores;

        public static List<IEnemy> Enemies
            => Form1.ActiveForm.Controls.OfType<IEnemy>().ToList();
        
        public static List<IShootableEnemy> ShootableEnemies
            => Form1.ActiveForm.Controls.OfType<IShootableEnemy>().ToList();

        public static List<IProjectile> Projectiles
            => Form1.ActiveForm.Controls.OfType<IProjectile>().ToList();
    }
}
