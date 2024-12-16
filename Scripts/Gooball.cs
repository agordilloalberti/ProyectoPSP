using Godot;
using System;

public partial class Gooball : Area2D
{
    [Export]
    private float speed = 150f;
    [Export]
    private float maxDistance = 300;
    
    private float distanceTraveled;
    private AnimatedSprite2D sprite; 
    private int direction = 1;
    private Killzone killzone;
    private Timer timer;
    public bool hitPlayer = false;
    

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        sprite = GetNode<AnimatedSprite2D>("Sprite");
        killzone = GetNode<Killzone>("Killzone");
    }


    public override void _Process(double delta)
    {
        Vector2 position = Position;
        distanceTraveled += speed*direction*(float)delta;
        position.X += speed*direction*(float)delta;
        Position = position;
        if (Math.Abs(distanceTraveled)>= maxDistance)
        {
            QueueFree();
        }
    }

    private void _on_body_entered2(Node body)
    {
        //Todo: make the interaction go through correctly
        if (!hitPlayer)
        {
            QueueFree();
        }
        else
        {
            timer.Start(1.5f);
        }
    }

    private void _on_timer_timeout()
    {
        QueueFree();
    }
    
    public void setDirection(bool flip, int dir)
    {
        direction = dir;
        sprite.FlipH = !flip;
    }
}
