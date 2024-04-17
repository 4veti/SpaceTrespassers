# A Windows Forms 2D game for a uni project

## **Summary**
This was one of the projects for my .NET Programming classes. The assignment was to use Windows Forms to create a game with a bunch of requirements. I know that this platform isn't made for games, but it was still a great learning experience. 

## **Overview**
In the main menu contains a single **Play** button. Below it there are simple instructions on how to play, and on the left the local scoreboard. There are 3 total levels, each harder than the previous. The *Player* loses after one hit. Enemies can take 1, 2, or 5 hits to be destroyed. The *Medium* green enemies that require 2 hits change their sprite to a broken version after the first hit. After the game ends either by a loss or a win, the player can type in a nickname and save their score in the local scoreboard.

## **Some key points and functionalities:**

1. A simple AI plays the game on the main-menu screen, and after the player loses;
2. There is a level system that makes it easy to add new levels and distribute the different enemies on the screen;
3. The enemy characters, projectiles, and the spaceship are created using good OOP practices and SOLID principles;
4. The game has multiple states (*MainMenu*, *Playing*, *Victory*, and *Defeat*);
5. Each level increases your and the enemies' fire rates;
6. A scoreboard of the best 6 scores is saved locally as a json text file;
7. The enemies' spacing adjusts at the game's launch to accomodate different screen sizes.

## **Screenshots**
![image](https://github.com/4veti/SpaceTrespassers/assets/37193765/7e715519-9b7a-48d7-a641-3b8f62340c5a)
![image](https://github.com/4veti/SpaceTrespassers/assets/37193765/55dc5a50-89b7-42ce-8941-9db680159baf)
![image](https://github.com/4veti/SpaceTrespassers/assets/37193765/ecc5e9ab-001e-4950-b652-3a850b74e426)
![image](https://github.com/4veti/SpaceTrespassers/assets/37193765/b580adf2-b934-4240-ba04-db57c52ae9d5)
![image](https://github.com/4veti/SpaceTrespassers/assets/37193765/ac5f3af3-9ba1-4f85-b624-0255c34e0b93)
