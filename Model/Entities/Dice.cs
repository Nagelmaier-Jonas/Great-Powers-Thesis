namespace Model.Entities;

public static class Dice{
    private static Random random = new Random();

    public static int Roll() => random.Next(1, 7);
}