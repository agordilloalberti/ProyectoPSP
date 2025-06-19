using Godot;
using System;
using System.Text.Json;

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
		var user = usuario.Text;
		
		GET.RequestCompleted += OnLoginRequestCompleted;
		
		GET.Request("http://localhost:5256/api/players/" + user);

	}

	private void OnLoginRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		var json = System.Text.Encoding.UTF8.GetString(body);

		try
		{
			var player = JsonSerializer.Deserialize<JsonElement>(json);
			string name = player.GetProperty("name").GetString();
			double score = player.GetProperty("score").GetDouble();

			GD.Print($"Player found: {name} with score {score}");
		}
		catch
		{
			GD.Print("User not found or error parsing.");
		}
		
		GET.RequestCompleted -= OnLoginRequestCompleted;
	}


	private void _on_volver_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/UI/menu.tscn");
	}

	private void _on_registrar_pressed()
	{
		var user = usuario.Text;
		var data = new { name = user, score = 0.0 };
		var json = JsonSerializer.Serialize(data);

		string[] headers = { "Content-Type: application/json" };

		POST.RequestCompleted += OnRegisterCompleted;

		POST.Request(
			"http://localhost:5256/api/players",
			headers,
			HttpClient.Method.Post,
			json
		);
	}

	private void OnRegisterCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		GD.Print($"Register completed: code {responseCode}");

		if (responseCode == 201)
			GD.Print("User successfully registered.");
		else
			GD.Print("User registration failed.");

		POST.RequestCompleted -= OnRegisterCompleted;
	}

}
