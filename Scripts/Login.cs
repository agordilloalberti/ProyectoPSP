using Godot;
using System;

public partial class Login : Control
{

	private TextEdit usuario;
	private HttpRequest GET;
	private HttpRequest POST;
	public override void _Ready()
	{
		usuario = GetNode<TextEdit>("usuario");
		GET = GetNode<HttpRequest>("GET");
		POST = GetNode<HttpRequest>("POST");
	}

	private void _on_login_pressed()
	{
		
	}

	private void _on_volver_pressed()
	{
		
	}

	private void _on_registrar_pressed()
	{
		
	}
}
