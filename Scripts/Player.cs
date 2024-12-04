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
	private int throwCooldown = 1;

	private bool throwing = false;
	private Timer throwTimer;
	private bool scriptedScene = false;
	private PackedScene dagger;
	private AnimatedSprite2D player;
	private Camera2D camera;
	private bool locked = false;
	private static bool LR;
	public static bool dead;

	public override void _Ready()
	{
		player = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		dagger = GD.Load<PackedScene>("res://Scenes/dagger.tscn");
		throwTimer = GetNode<Timer>("Timer");
		camera = GetNode<Camera2D>("Camera2D");
		throwTimer.OneShot = true;
	}
		
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!scriptedScene)
		{
			if (!dead)
			{
				shoot(LR);

				velocity = Jump(velocity);

				int direction = Move(velocity);

				velocity.X = direction * Speed;

				velocity = animation(velocity, delta);

			}
			else
			{
				velocity = death(velocity, delta);
			}
		}
		else
		{
			velocity = new Vector2();
			velocity = animation(velocity, delta);
			velocity += GetGravity() * (float)delta;
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

		if (!locked)
		{
			//TODO Make the camera fixed after player death
			camera.GlobalPosition = player.GlobalPosition;
			locked = true;
		}
		else
		{
			camera.GlobalPosition = camera.GlobalPosition;
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

	private Vector2 animation(Vector2 velocity,Double delta)
	{
		if (!IsOnFloor())
		{
			player.Play("roll");
			velocity += GetGravity() * (float)delta;
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
			if (IsOnFloor())
			{
				player.Play("idle");	
			}
			player.FlipH = LR;
		}

		return velocity;
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
		if (Input.IsActionJustPressed("jump")
		    && (IsOnFloor() || NoClip)
		    )
		{
			velocity.Y = JumpVelocity;
		}
		return velocity;
	}

	private void shoot(bool LR)
	{
		if (Input.IsActionJustPressed("shoot") && !throwing)
		{
			Dagger instDagger = (Dagger) dagger.Instantiate();
			Vector2 offset = new Vector2(0, 5);
			instDagger.Position = GlobalPosition+offset;
			GetTree().Root.AddChild(instDagger);
			instDagger.setDirection(LR);
			throwing = true;
			throwTimer.Start(throwCooldown);
		}
	}

	private void _on_timer_timeout()
	{
		throwing=false;
	} 

	public void enterCastle()
	{
		//scriptedScene = true;
		GD.Print(GetTree().CurrentScene.ToString());
		GetTree().ChangeSceneToFile("res://Scenes/parte_2.tscn");
	}
}
