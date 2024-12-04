using Godot;
using System;

public partial class ladder : Area2D
{
    private void _on_body_entered(Node node)
    {
        if (node is not Player player) return;
        GD.Print("is on ladder");
        player.isOnLadder = true;
    }

    private void _on_body_exited(Node node)
    {
        if (node is not Player player) return;
        GD.Print("is on ladder");
        player.isOnLadder = false;
    }
}
