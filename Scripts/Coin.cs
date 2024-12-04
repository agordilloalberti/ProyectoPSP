using Godot;
using System;

public partial class Coin : Area2D
{
	[Export]
	private int points = 10;
	private void _on_body_entered(Node2D area)
	{
		QueueFree();
		GameManager.addPoints(points);
	}
}
