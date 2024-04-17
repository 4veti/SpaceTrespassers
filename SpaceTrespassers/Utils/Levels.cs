namespace SpaceTrespassers.Utils;

public static class Levels
{
    private static Queue<int[]> mainMenuLevels = new Queue<int[]>(new[]
    {

        new int[] {0, 1, 2, 1, 0, 0, 1, 1, 0, 0, 1, 2, 1, 0,
                     0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0,
                   2, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 2,},

        new int[] {1, 1, 1, 1, 0, 0, 2, 2, 0, 0, 1, 1, 1, 1,
                     0, 1, 1, 0, 0, 0, 2, 0, 0, 0, 1, 1, 0, 0,
                   0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0,},

        new int[] {0, 1, 1, 1, 0, 2, 1, 1, 2, 0, 1, 1, 1, 0,
                     0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0,
                   0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0,},
    });

    private static int[][] levels = new int[3][]
    {
        new int[] {0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0,
                     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},

         new int[] {0, 2, 2, 2, 0, 0, 2, 3, 2, 0, 2, 2, 2, 0,
                      1, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,},

         new int[] {3, 2, 2, 3, 0, 0, 0, 3, 0, 0, 3, 2, 2, 3,
                      1, 2, 1, 0, 0, 1, 1, 1, 1, 0, 1, 2, 1, 0,
                    0, 1, 0, 0, 0, 0, 1, 3, 1, 0, 0, 0, 1, 0,}
    };

    public static Queue<int[]> MenuLevels
    {
        get => mainMenuLevels;
        set => mainMenuLevels = value;
    }

    public static int MaxLevel => levels.Length;

    public static int[] GetLevel(int number)
    {
        if (number >= 1 && number <= 3)
        {
            return levels[number - 1];
        }

        return levels[1];
    }
}
