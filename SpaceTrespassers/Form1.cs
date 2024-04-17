using SpaceTrespassers.Core;
using SpaceTrespassers.Models;
using SpaceTrespassers.Utils;
using System.Diagnostics;
using System.Text;

namespace SpaceTrespassers;

public partial class Form1 : Form
{
    private SpaceShip spaceShip;
    private CoreController coreController;
    private MainMenuController mainMenuController;

    //private long largestElapsed;
    private Stopwatch sw = new Stopwatch();                                                     //

    public Form1()
    {
        InitializeComponent();

        this.spaceShip = new SpaceShip();
        this.coreController = new CoreController(spaceShip);
        this.mainMenuController = new MainMenuController(spaceShip, coreController);

        Controls.Add(spaceShip);
        GameState.currentLevel = 1;
        GameState.currentScore = new ScoreSave();
    }

    /// <summary>
    /// Main method for the Form's initialization.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_Load(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Maximized;
        InitializeMainMenu();
        //benchmark.Visible = true;                                                             //
    }

    /// <summary>
    /// The main method for each tick.
    /// Invokes the MainLogic method of the core controller.
    /// Resolves all collisions and takes action accordingly.
    /// </summary>
    private void MainGameTimerEvent(object sender, EventArgs e)
    {
        //sw.Start();                                                                           //

        coreController.MainLogic();
        ResolveCollisions();

        //sw.Stop();                                                                            //
        //if (sw.ElapsedMilliseconds > 20)                                                      //
        //{                                                                                     //
        //    if (sw.ElapsedMilliseconds > largestElapsed)                                      //
        //    {                                                                                 //
        //        largestElapsed = sw.ElapsedMilliseconds;                                      // Benchmarking
        //    }                                                                                 //
        //}                                                                                     //
        //                                                                                      //
        //benchmark.Text = $"Elapsed: {sw.ElapsedMilliseconds}ms, largest: {largestElapsed}";   //
        //sw.Reset();                                                                           //
    }

    /// <summary>
    /// The method for each tick, handling main menu logic.
    /// If no enemies are present, switches to the next main menu level.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainMenuTimerEvent(object sender, EventArgs e)
    {
        if (!GameState.Enemies.Any())
        {
            GameState.outcome = GameResult.MainMenu;
            GameState.lastTargetedEnemy = null;
            Levels.MenuLevels.Enqueue(Levels.MenuLevels.Dequeue());

            mainMenuTimer.Stop();
            LoadLevel(Levels.MenuLevels.Peek());
            mainMenuTimer.Start();
        }

        ResolveCollisions();
        mainMenuController.MainLogic();

    }

    /// <summary>
    /// Handles the visibility and text of all lables and buttons.
    /// Starts the main menu timer.
    /// </summary>
    private void InitializeMainMenu()
    {
        scoreboard.Text = coreController.LoadScoreboard();
        pausedText.Text = String.Empty;
        controlsInfo.Text = $"-Use arrow keys to move" +
                            $"{Environment.NewLine}-Press 'Space' to shoot" +
                            $"{Environment.NewLine}-Press 'M' to prime a Missle";
        scoreText.Text = "Computer playing";

        startButton.Left = (this.Width / 2) - startButton.Width / 2;
        startButton.Top = this.Height / 2 + 100;
        controlsInfo.Top = startButton.Top + 150;
        controlsInfo.Left = startButton.Left - 70;
        gameOverLabel.Top = this.Height / 2 - 175;
        pausedText.Top = this.Height / 2 - 150;
        spaceShip.Left = (this.Width / 2) - spaceShip.Width / 2;
        spaceShip.Top = this.Height - 150;
        pauseInstructions.Left = this.Width - pauseInstructions.Width - 50;


        gameOverLabel.Visible = false;
        pausedText.Visible = false;
        pauseInstructions.Visible = false;
        controlsInfo.Visible = true;
        GameState.menuFiredAtEnemy = true;
        GameState.outcome = GameResult.MainMenu;
        playerNameBox.Visible = false;

        mainMenuTimer.Start();
    }

    /// <summary>
    /// Displays the final message after the game ends.
    /// Saves the score.
    /// </summary>
    private void DisplayFinalMessage()
    {
        GameState.currentScore.SetName(playerNameBox.Text);

        if (GameState.outcome == GameResult.Victory || GameState.victory)
        {
            gameOverLabel.Text += $"{Environment.NewLine}Good job, {GameState.currentScore.PlayerName}! Your final score is: {GameState.currentScore.Points}";
        }
        else if (GameState.outcome == GameResult.Defeat || GameState.victory == false)
        {
            int enemiesLeft = GameState.Enemies.Count();

            gameOverLabel.Text += $"{Environment.NewLine}Points: {GameState.currentScore.Points}. Enemies left: {enemiesLeft}" +
                                  $"{Environment.NewLine}Better luck next time, {GameState.currentScore.PlayerName}!" +
                                  $"{Environment.NewLine}Press 'Esc' to exit.";
        }

        GameState.outcome = GameResult.MainMenu;
        gameOverLabel.Update();
        playerNameBox.Dispose();
        coreController.SaveScore();
    }

    /// <summary>
    /// Restarts the game cycle with a new level if needed.
    /// Handles label texts, and starts the AI playing if game ends.
    /// </summary>
    private void EndGame()
    {
        if (GameState.outcome != GameResult.Defeat && GameState.currentLevel < Constants.MaxLevels)
        {
            GameState.currentLevel++;
            GameRestart();
            return;
        }

        pauseInstructions.Dispose();
        StringBuilder result = new StringBuilder();

        if (GameState.outcome == GameResult.Victory)
        {
            result.AppendLine("VICTORY! Enter your name: ");
            playerNameBox.Left = gameOverLabel.Left;
        }
        else if (GameState.outcome == GameResult.Defeat)
        {
            result.AppendLine("DEFEAT! Enter your name: ");
            playerNameBox.Left = gameOverLabel.Left + gameOverLabel.Width;
        }

        playerNameBox.Top = gameOverLabel.Top + 15;
        playerNameBox.Visible = true;

        gameOverLabel.Text = result.ToString().TrimEnd();
        gameOverLabel.Left = this.Width / 2 - (gameOverLabel.Width / 2);

        gameOverLabel.Visible = true;

        scoreText.Text = "Computer playing";
        mainMenuTimer.Start();
    }

    /// <summary>
    /// Calculates collisions via the core controller, and takes action accordingly.
    /// Invokes EndGame method if game should end;
    /// </summary>
    private void ResolveCollisions()
    {
        coreController.CalculateCollisions();

        bool isGameEnded = GameState.outcome == GameResult.Victory || GameState.outcome == GameResult.Defeat;

        if (GameState.outcome == GameResult.Playing)
        {
            scoreText.Text = GameState.currentScore.ToString();
        }

        if (isGameEnded)
        {
            gameTimer.Stop();
            EndGame();
        }
    }

    /// <summary>
    /// Updates labels for when the game should unpause.
    /// Does the 3, 2, 1, GO sequence and continues the timer.
    /// </summary>
    private void UnpauseSequence()
    {
        scoreText.Text = GameState.currentScore.ToString();
        this.Update();

        for (int i = 3; i >= 1; i--)
        {
            pausedText.Text = $"{i}";
            pausedText.Left = this.Width / 2 - (pausedText.Width / 2);
            pausedText.Update();
            Thread.Sleep(1000);
        }

        pausedText.Text = "GO!";
        pausedText.Left = this.Width / 2 - (pausedText.Width / 2);
        pausedText.Update();
        Thread.Sleep(300);

        pausedText.Text = String.Empty;
        pausedText.Visible = false;
        pauseInstructions.Visible = true;

        sw.Start();
    }

    /// <summary>
    /// Stops the timer and updates labels accordingly.
    /// </summary>
    private void PauseMenu()
    {
        sw.Stop();

        pausedText.Text = "Paused!";
        scoreText.Text += " Press 'Space' to unpause!";
        pausedText.Left = this.Width / 2 - (pausedText.Width / 2);
        pausedText.Visible = true;
        pauseInstructions.Visible = false;
    }

    /// <summary>
    /// Gets rid of projectiles and enemies.
    /// Restarts spaceship position, and saves the score.
    /// Adjusts firing speed, starts the timer, and loads the next level.
    /// </summary>
    private void GameRestart()
    {
        foreach (Control projectile in GameState.Projectiles)
        {
            projectile.Dispose();
        }

        foreach (Control enemy in GameState.Enemies)
        {
            Controls.Remove(enemy);
            enemy.Dispose();
        }

        spaceShip.Left = (this.Width / 2) - spaceShip.Width / 2;
        spaceShip.Top = this.Height - 150;
        GameState.outcome = GameResult.Playing;
        pauseInstructions.Visible = true;
        controlsInfo.Visible = false;


        if (GameState.currentLevel == 1)
        {
            scoreText.Visible = true;
            scoreboard.Dispose();
            startButton.Dispose();
            GameState.currentScore = new ScoreSave();
        }

        GameState.firingSpeed = Constants.DefaultFiringSpeed - (GameState.currentLevel * 3);


        gameTimer.Start();
        LoadLevel(Levels.GetLevel(GameState.currentLevel));
    }

    /// <summary>
    /// Spawns enemies via the core controller, rolls the unpause sequence for a smoother transition.
    /// </summary>
    /// <param name="positions"></param>
    private void LoadLevel(int[] positions)
    {
        coreController.SpawnEnemies(positions);

        if (gameTimer.Enabled == true)
        {
            PauseMenu();
            this.Update();
            UnpauseSequence();
        }
    }

    /// <summary>
    /// Stops the main menu timer, runs the GameRestart method to start the game.
    /// </summary>
    private void StartButtonClicked(object sender, MouseEventArgs e)
    {
        mainMenuTimer.Stop();
        GameRestart();
    }

    /// <summary>
    /// Handles all tracked key presses, and updates their global constants.
    /// </summary>
    private void KeyIsDown(object sender, KeyEventArgs key)
    {
        if (key.KeyCode == Keys.Escape)
        {
            Environment.Exit(0);
        }

        if (key.KeyCode == Keys.Right)
        {
            GameState.movingRight = true;
        }
        else if (key.KeyCode == Keys.Left)
        {
            GameState.movingLeft = true;
        }

        if (key.KeyCode == Keys.Space)
        {
            GameState.shooting = true;
        }

        if (key.KeyCode == Keys.P && GameState.isPaused is false && gameTimer.Enabled is true)
        {
            GameState.isPaused = true;
            gameTimer.Stop();
            PauseMenu();
        }

        if (GameState.isPaused && key.KeyCode == Keys.Space)
        {
            GameState.isPaused = false;
            UnpauseSequence();
            gameTimer.Start();
        }

        if (key.KeyCode == Keys.M && !GameState.armedMissle)
        {
            spaceShip.ArmMissle();
        }
    }

    /// <summary>
    /// Handles all tracked key un-presses, and updates their global constants.
    /// </summary>
    private void KeyIsUp(object sender, KeyEventArgs key)
    {
        if (key.KeyCode == Keys.Left)
        {
            GameState.movingLeft = false;
        }

        if (key.KeyCode == Keys.Right)
        {
            GameState.movingRight = false;
        }

        if (key.KeyCode == Keys.Space)
        {
            GameState.shooting = false;
        }
    }

    /// <summary>
    /// Tracks if the player hse entered their name and displays the final message.
    /// </summary>
    private void playerNameBox_KeyDown(object sender, KeyEventArgs key)
    {
        if (GameState.enteredName is false && playerNameBox.Text.Length > 1 && key.KeyCode == Keys.Enter)
        {
            GameState.enteredName = true;
            DisplayFinalMessage();
        }
    }
}