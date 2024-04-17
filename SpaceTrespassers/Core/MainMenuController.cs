using SpaceTrespassers.Models;
using SpaceTrespassers.Models.Enemies;
using SpaceTrespassers.Utils;

namespace SpaceTrespassers.Core
{
    /// <summary>
    /// A controller class that takes care of the main menu functionality.
    /// </summary>
    public class MainMenuController
    {
        private SpaceShip spaceShip;
        CoreController coreController;

        public MainMenuController(SpaceShip spaceShip, CoreController coreController)
        {
            this.spaceShip = spaceShip;
            this.coreController = coreController;
        }

        /// <summary>
        /// Main method for each tick during the main menu.
        /// - Hover enemies;
        /// - Select an enemy as the AI's target;
        /// - Calculate AI's next move for the tick;
        /// - Invoke the method for advancing all projectiles.
        /// </summary>
        public void MainLogic()
        {
            if (GameState.countLastHoverTicks > Constants.HoverAfterTicks)
            {
                coreController.HoverEnemies();
                GameState.countLastHoverTicks = 0;
            }

            if (GameState.menuFiredAtEnemy)
            {
                SetTargetEnemy();
            }

            ActAI();

            GameState.countLastHoverTicks++;
            GameState.countLastMenuShotTicks++;
            coreController.MoveLazers();
        }

        /// <summary>
        /// Method for calculating the AI's next move:
        /// - Decide to move left or right the given increment;
        /// - Calculate if an enemy is in range horizontally, and if it should fire;
        /// - Decide to fire or move.
        /// </summary>
        public void ActAI()
        {
            int moveIncrement = 60;

            int countShotLazers = GameState.Projectiles.Count();

            if (spaceShip.Left > GameState.seekEnemy.Left)
            {
                moveIncrement *= -1;
            }

            bool isInRange = Math.Abs(spaceShip.Left - GameState.seekEnemy.Left) < 30;
            bool shouldFire = isInRange && countShotLazers < GameState.Enemies.Count() + 1;

            if (GameState.countLastMenuShotTicks > 1 && !isInRange)
            {
                spaceShip.Left += moveIncrement;
            }
            else if (GameState.countLastMenuShotTicks > 5 && shouldFire)
            {
                spaceShip.Fire();
                GameState.menuFiredAtEnemy = true;
                GameState.countLastMenuShotTicks = 0;
            }
        }

        /// <summary>
        ///  Selects a random enemy as the AI's target.
        ///  For variety, repeat selection up to 3 times in case the previous enemy is selected.
        /// </summary>
        public void SetTargetEnemy()
        {
            int counter = 0;
            while (GameState.seekEnemy == GameState.lastTargetedEnemy && counter++ < 2)
            {
                Random rnd = new Random();
                int indexOfEnemy = rnd.Next(0, GameState.Enemies.Count());
                GameState.seekEnemy = GameState.Enemies[indexOfEnemy] as Enemy;
                GameState.menuFiredAtEnemy = false;
            }

            GameState.lastTargetedEnemy = GameState.seekEnemy;
        }
    }
}
