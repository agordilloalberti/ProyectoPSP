using Godot;
using System;

public partial class WinScreen : Control
{
    private void _on_back_title_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/UI/menu.tscn");
    }

    private void _on_exit_game_pressed()
    {
        GetTree().Quit();
    }

    private void _on_statistics_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/UI/statistics.tscn");
    }
}
