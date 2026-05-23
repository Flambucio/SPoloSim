using Godot;
using System;

public partial class Camera : Camera3D
{
	// Called when the node enters the scene tree for the first time.
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (_target == null) return;

		float fDelta = (float)delta;

		// Calcola la posizione desiderata basandosi sulla trasformazione globale del target
		Vector3 targetPos = _target.GlobalTransform.Origin + (_target.GlobalTransform.Basis * Offset);

		// Spostamento fluido (Lerp)
		Vector3 currentPos = GlobalTransform.Origin;
		GlobalTransform = new Transform3D(GlobalTransform.Basis, currentPos.Lerp(targetPos, LerpSpeed * fDelta));

		// Guarda sempre l'auto
		LookAt(_target.GlobalTransform.Origin, Vector3.Up);
	}
}
