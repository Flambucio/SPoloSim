using Godot;
using System;

public partial class CarController : VehicleBody3D
{
	[Export] public float MaxTorque = 600.0f*10;
	[Export] public float SteeringLimit = 0.5f;
	[Export] public float BrakeForce = 10.0f;
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
	}
	
	public override void _PhysicsProcess(double delta)
	{
		float fDelta = (float)delta;
		float throttle = Input.GetAxis("Retro", "Accelera");
		float multiplier = Input.IsActionPressed("Turbo") ? 10 : 1;
		EngineForce = throttle * MaxTorque * multiplier;

		float steerTarget = Input.GetAxis("SterzaDestra", "SterzaSinistra") * SteeringLimit;
		Steering = Mathf.Lerp(Steering, steerTarget, 10.0f * fDelta);

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
