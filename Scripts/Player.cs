using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] 
	private float Speed = 130.0f;
	[Export] 
	private float JumpVelocity = -300.0f;
	[Export]
	private bool NoClip = false;
	[Export]
	private float throwCooldown = 1f;
	[Export]
	private float ladderVelocity = -100.0f;
	
	public bool goingDown = false;
	private bool throwing = false;
	private Timer throwTimer;
	private bool scriptedScene = false;
	private PackedScene dagger;
	private AnimatedSprite2D player;
	private Camera2D camera;
	private bool locked = false;
	private bool LR;
	public bool dead;
	public bool isOnLadder = false;

	public override void _Ready()
	{
		player = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		dagger = GD.Load<PackedScene>("res://Scenes/Props/dagger.tscn");
		throwTimer = GetNode<Timer>("Timer");
		camera = GetNode<Camera2D>("/root/Game/Camera2D");
		throwTimer.OneShot = true;
	}
		
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		if (!dead)
		{
			camera.GlobalPosition = GlobalPosition;
			
			shoot(LR);
			
			velocity = Jump(velocity);

			int direction = Move(velocity);

			velocity.X = direction * Speed;
			if (!isOnLadder)
			{
				velocity += GetGravity() * (float)delta;
			}

			animation(velocity);

		}
		else
		{
			velocity = death(velocity, delta);
		}
		Velocity = velocity;
		MoveAndSlide();
	}
	
	private Vector2 death(Vector2 velocity, double delta)
	{
		if (player.Animation!="death")
		{
			player.Play("death");
		}
		SetCollisionLayerValue(2,false);
		SetCollisionLayerValue(3,true);
		SetCollisionMaskValue(3,false);
		velocity.X = 0; 
		velocity += GetGravity() * (float)delta;
		return velocity;
	}

	private void _on_animation_finished()
	{
		
	}

	private void animation(Vector2 velocity)
	{
		if (!IsOnFloor() && !isOnLadder)
		{
			player.Play("roll");
		}
			
		if (velocity.X < 0)
		{
			player.FlipH = true;
			if (IsOnFloor())
			{
				player.Play("run");	
			}
			LR = true;
		}
		else if (velocity.X > 0)
		{
			player.FlipH = false;
			if (IsOnFloor())
			{
				player.Play("run");	
			}
			LR = false;
		}
		else
		{
			if (IsOnFloor() || isOnLadder)
			{
				player.Play("idle");	
			}
			player.FlipH = LR;
		}
	}

	private int Move(Vector2 velocity)
	{
		int direction;
		if (Input.IsActionPressed("move_left"))
		{
			direction = -1;
		}else if (Input.IsActionPressed("move_right"))
		{
			direction = 1;
		}
		else
		{
			direction = 0;
		}
		return direction;
	}

	private Vector2 Jump(Vector2 velocity)
	{
		if (!isOnLadder)
		{
			if (Input.IsActionJustPressed("jump")
			    && (IsOnFloor() || NoClip)
			   )
			{
				velocity.Y = JumpVelocity;
			}
		}
		else
		{
			if (Input.IsActionPressed("jump"))
			{
				goingDown = false;
				velocity.Y = ladderVelocity;
			}
			else if (Input.IsActionPressed("down"))
			{
				goingDown = true;
				velocity.Y = -ladderVelocity;
			}
			else
			{
				velocity.Y = 0;
			}
		}

		return velocity;
	}

	private void shoot(bool LR)
	{
		if (!Input.IsActionJustPressed("shoot") || throwing) return;
		Dagger instDagger = (Dagger) dagger.Instantiate();
		Vector2 offset = new Vector2(0, 5);
		instDagger.Position = GlobalPosition+offset;
		GetTree().Root.AddChild(instDagger);
		instDagger.setDirection(LR);
		throwing = true;
		throwTimer.Start(throwCooldown);
	}

	private void _on_timer_timeout()
	{
		throwing=false;
	} 

	public void enterCastle(String nextWorld)
	{
		//scriptedScene = true;
		GetTree().ChangeSceneToFile(nextWorld);
	}
}
