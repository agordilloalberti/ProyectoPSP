

using Godot;
using System;

public partial class Slime : CharacterBody2D
{
	[Export]
	private float SPEED = 45;

	private bool dead = false;
	private int dir = 1;
	private AnimatedSprite2D sprite;
	private RayCast2D rayCastRight;
	private RayCast2D rayCastLeft;
	private RayCast2D rayCastRightDown;
	private RayCast2D rayCastLeftDown;
	private Killzone killzone;
	
	public override void _Ready()
	{
		 sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		 rayCastRight = GetNode<RayCast2D>("RayCastRight");
		 rayCastLeft = GetNode<RayCast2D>("RayCastLeft");
		 rayCastRightDown = GetNode<RayCast2D>("RayCastRightDown");
		 rayCastLeftDown = GetNode<RayCast2D>("RayCastLeftDown");
		 killzone = GetNode<Killzone>("Killzone");
	}
	public override void _Process(double delta)
	{
		
		if (rayCastRight.IsColliding() || !rayCastRightDown.IsColliding())
		{
			dir = -1;
			sprite.FlipH = true;
		}
		else if (rayCastLeft.IsColliding() || !rayCastLeftDown.IsColliding())
		{
			dir = 1;
			sprite.FlipH = false;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		if (!dead)
		{
			Vector2 direction = new Vector2(dir, 0);
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * SPEED;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, SPEED);
			}

			if (!IsOnFloor())
			{
				velocity += GetGravity() * (float)delta;
			}
		}
		else
		{
			velocity = Vector2.Zero;
		}
		Velocity = velocity;
		MoveAndSlide();
	}


	public void die()
	{
		SetCollisionLayerValue(3,false);
		SetCollisionMaskValue(2,false);
		killzone.SetCollisionMaskValue(2,false);
		dead = true;
		sprite.Play("death");
	}

	private void _on_animation_finished()
	{
		QueueFree();
	}
}
