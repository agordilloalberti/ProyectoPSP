using Godot;
using System;

public partial class Killzone : Area2D
{
    [Export] 
    private float time = 1.5f;
    private Timer timer;
    private Player player; 
    
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
    }

    private void _on_body_entered(Player player)
    {
        this.player = player;
        player.dead = true;
        Engine.TimeScale = 0.5;
        timer.Start(time);
    }

    private void _on_timer_timeout()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Ui/death_screen.tscn");
        GameManager.reset();
        Engine.TimeScale = 1;
        player.dead = false;
    }
}
