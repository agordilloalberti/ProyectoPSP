using Godot;
using System;

public partial class GameManager : Node
{
    public static int points=0;
    private static Label scoreLabel;

    public override void _Ready()
    {
    }

    public static void addPoints(int i)
    {
        points += i;
    }

    public static void reset()
    {
        points = 0;
    }
}
