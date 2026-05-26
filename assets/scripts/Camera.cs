using Godot;
using System;

public partial class Camera : Camera3D
{
	[Export] public NodePath TargetPath;
	[Export] public Vector3 Offset = new Vector3(0, 3, 6);
	[Export] public float LerpSpeed = 5.0f;
	
	private Node3D _target;
	public override void _Ready()
	{
		if (TargetPath != null)
		{
			_target = GetNode<Node3D>(TargetPath);
		}
	}

	public override void _Process(double delta)
	{
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (_target == null) return;

		float fDelta = (float)delta;

		Vector3 targetPos = _target.GlobalTransform.Origin + (_target.GlobalTransform.Basis * Offset);
		Vector3 currentPos = GlobalTransform.Origin;
		GlobalTransform = new Transform3D(GlobalTransform.Basis, currentPos.Lerp(targetPos, LerpSpeed * fDelta));
		LookAt(_target.GlobalTransform.Origin, Vector3.Up);
	}
}
