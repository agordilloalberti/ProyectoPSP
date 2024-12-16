using Godot;
using System;

public partial class Gooball : Area2D
{
    [Export]
    private float speed = 150f;
    [Export]
    private float maxDistance = 300;
    
    private float distanceTraveled;
    private Sprite2D sprite; 
    private int direction = 1;
    

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite");
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
        if (body is Player player)
        {
            player.dead=true;
        }
        QueueFree();
    }
    
    public void setDirection(bool flip, int dir)
    {
        direction = dir;
        sprite.FlipH = flip;
    }
}
