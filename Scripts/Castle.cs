using Godot;
using System;
using System.IO;

public partial class Castle : Area2D
{
    [Export]
    private String nextWorldName;

    private bool closed = false;
    private String NextWorld;
    private AnimatedSprite2D castle;
    public override void _Ready()
    {
        castle = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        castle.Stop();
        if (nextWorldName != "closed" && nextWorldName != "win")
        {
            NextWorld = "res://Scenes/Levels/" + nextWorldName + ".tscn";
        }else if (nextWorldName == "win")
        {
            NextWorld = "res://Scenes/UI/win_screen.tscn";
        }
        else
        {
            closed = true;
            castle.Frame = 1;
        }
    }

    private void _on_body_entered(Player player)
    {
        if (!closed)
        {
            player.enterCastle(NextWorld);
        }
    }
}
