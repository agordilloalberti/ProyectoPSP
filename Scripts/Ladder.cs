using Godot;
using System;

public partial class Ladder : Area2D
{ 
    [Export]
    private bool upperOne = false;

    
    private Player player;
    private CollisionShape2D platform;
    
    public override void _Ready()
    {
        player = GetTree().Root.GetNode<Player>("Game/Player");
        platform = GetNode<CollisionShape2D>("platform/platform");
        if (!upperOne)
        {
            platform.Disabled = true;
        }
    }


    public override void _Process(double delta)
    {
        if (player.goingDown)
        {
            platform.Disabled = true;
        }
        else if(upperOne)
        {
            platform.Disabled = false;
        }
    }

    private void _on_body_entered(Node node)
    {
        if (node is not Player p) return;
        player = p;
        player.isOnLadder = true;
    }

    private void _on_body_exited(Node node)
    {
        if (node is not Player p) return;
        player = p;
        player.isOnLadder = false;
    }
}
