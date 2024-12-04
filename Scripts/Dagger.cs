using Godot;
using System;

public partial class Dagger : Area2D
{
    [Export]
    private float speed = 150f;
    [Export]
    private float time = 1f;
    [Export]
    private float maxDistance = 300;
    
    private float distanceTraveled;
    private Timer timer;
    private Sprite2D sprite; 
    private int direction = 1;
    

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite");
        timer = GetNode<Timer>("Timer");
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

    private void _on_body_entered(Node body)
    {
        if (body is Slime slime)
        {
            slime.die();
            QueueFree();
        }
        speed = 0f;
        timer.Start(time);
    }

    private void _on_timer_timeout()
    {
        QueueFree();
    }
    
    public void setDirection(bool LR)
    {
        sprite.FlipH = LR;
        direction = LR ? -1 : 1;
    }
}
