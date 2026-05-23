using Godot;
using System;

public partial class CarController : VehicleBody3D
{
	// Called when the node enters the scene tree for the first time.
	[Export] public float MaxTorque = 600.0f*10;
	[Export] public float SteeringLimit = 0.5f;
	[Export] public float BrakeForce = 10.0f;
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _PhysicsProcess(double delta)
	{
		float fDelta = (float)delta;

		// Accelerazione e Retromarcia
		// Input.GetAxis restituisce un valore tra -1 e 1
		float throttle = Input.GetAxis("Retro", "Accelera");
		float multiplier = Input.IsActionPressed("Turbo") ? 10 : 1;
		EngineForce = throttle * MaxTorque * multiplier;

		// Sterzata con interpolazione (Lerp) per fluidità
		float steerTarget = Input.GetAxis("SterzaDestra", "SterzaSinistra") * SteeringLimit;
		Steering = Mathf.Lerp(Steering, steerTarget, 10.0f * fDelta);

		// Freno (Spazio)
		if (Input.IsActionPressed("Frena"))
		{
			Brake = BrakeForce;
			EngineForce=0;
		}
		else
		{
			Brake = 0.0f;
		}
	}
}
