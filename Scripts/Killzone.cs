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
        //TODO: make a game over screen and set it to change here as well as make it reload scene 1 or menu by player choice
        GetTree().ReloadCurrentScene();
        GameManager.reset();
        Engine.TimeScale = 1;
        Player.dead = false;
    }
}
