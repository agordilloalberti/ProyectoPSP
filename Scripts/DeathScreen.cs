using Godot;
using System;

public partial class DeathScreen : Control
{

    private void _on_back_title_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/UI/Menu.tscn");
    }
    
    private void _on_back_scene_1_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Levels/world1.tscn");
    }
}
