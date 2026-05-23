using Godot;
using System;

public partial class FreeLookCamera : Camera3D
{
	[Export] public float Sensitivity = 0.2f;
	[Export] public float Speed = 10.0f;

	public override void _Ready()
	{
		// Cattura il mouse per poter girare la visuale a 360 gradi senza uscire dallo schermo
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Input(InputEvent @event)
	{
		// Gestione della rotazione con il mouse
		if (@event is InputEventMouseMotion mouseMotion)
		{
			Vector3 rot = RotationDegrees;
			rot.X -= mouseMotion.Relative.Y * Sensitivity;
			rot.Y -= mouseMotion.Relative.X * Sensitivity;
			
			// Limitiamo la rotazione verticale per evitare di ribaltare la testa
			rot.X = Mathf.Clamp(rot.X, -89.0f, 89.0f);
			RotationDegrees = rot;
		}

		// Tasto di emergenza: premi ESC per liberare il mouse dal gioco
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured 
				? Input.MouseModeEnum.Visible 
				: Input.MouseModeEnum.Captured;
		}
	}

	public override void _Process(double delta)
	{
		float fDelta = (float)delta;
		Vector3 direction = Vector3.Zero;

		// Input per il movimento stile "Noclip" / Spettatore
		if (Input.IsKeyPressed(Key.W)) direction -= Transform.Basis.Z;
		if (Input.IsKeyPressed(Key.S)) direction += Transform.Basis.Z;
		if (Input.IsKeyPressed(Key.A)) direction -= Transform.Basis.X;
		if (Input.IsKeyPressed(Key.D)) direction += Transform.Basis.X;
		if (Input.IsKeyPressed(Key.Q)) direction -= Transform.Basis.Y; // Scendi
		if (Input.IsKeyPressed(Key.E)) direction += Transform.Basis.Y; // Sali

		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			GlobalPosition += direction * Speed * fDelta;
		}
	}
}
