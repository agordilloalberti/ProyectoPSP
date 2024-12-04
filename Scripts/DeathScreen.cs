using Godot;
using System;

public partial class DeathScreen : Control
{

    private void _on_back_title_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
    }
    
    private void _on_back_scene_1_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/principal.tscn");
    }
}
