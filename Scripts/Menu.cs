using Godot;
using System;

public partial class Menu : Node
{

    private void _on_start_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Levels/world1.tscn");
    }
    
    private void _on_exit_pressed()
    {
        GetTree().Quit();
    }
}