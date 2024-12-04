using Godot;
using System;

public partial class Castle : Area2D
{
    
    public override void _Ready()
    {
        // player = GetTree().Root.GetNode<Player>("Player");
    }

    private void _on_body_entered(Player player)
    {
        player.enterCastle();
    }
}
