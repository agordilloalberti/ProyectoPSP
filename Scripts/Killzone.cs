using Godot;
using System;

public partial class Killzone : Area2D
{
    [Export] 
    private float time = 1.5f;
    private Timer timer;
    
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
    }

    private void _on_body_entered(Node2D area)
    {
        Player.dead = true;
        Engine.TimeScale = 0.5;
        timer.Start(time);
    }

    private void _on_timer_timeout()
    {
        GetTree().ChangeSceneToFile("res://Scenes/death_screen.tscn");
        GameManager.reset();
        Engine.TimeScale = 1;
        Player.dead = false;
    }
}
