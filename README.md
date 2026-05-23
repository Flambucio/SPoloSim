# SPoloSim
<h1 align="center">🚗 SPoloSim 🚗</h1>

<p align="center">
  <img src="https://img.shields.io/badge/Godot-4.5-%23478cbf?logo=godot-engine&logoColor=white" alt="Godot">
  <img src="https://img.shields.io/badge/Language-C%23-%23178600?logo=c-sharp&logoColor=white" alt="C#">
</p>

## 📝 Descrizione
Un simulatore di guida 3D costruito con **Godot Engine 4** e programmato interamente in **C#**. Il progetto permette la guida di una **sedan rossa** in giro per il comune di **San Polo dei Cavalieri** con i modelli 3d ufficiali di **OpenStreetMap**

## 🚀 Come Avviare il Progetto
1. Scarica il file zip
2. Estrai il file zip
3. Apri il file .exe

## Come e stato realizzato

1. Per prima cosa ho avuto bisogno di **modelli 3d**, l'auto l'ho presa da un **sito di asset  online**, invece il modello del paese l'ho preso da **OpenStreetMap**, un database gratuito di modelli 3d di **tutto il mondo**, tuttavia l'unico difetto e che non ha le **altezze** quindi la mappa e **piatta**.

2. Ho importato tutto su **godot** e ho gestito le posizioni di dove doveva partire la **macchina** creando ovviamente tutti i **nodi** necessari di ruote e scocca

3. Ho applicato i png dei materiali sulle **mesh** dei vari elementi (muri, asfalto, ecc...)

4. Ho creato 2 **script** per permettere il movimento della telecamera e della macchina

```csharp
//codice base della camera
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

```

```csharp
//codice base della macchina
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
```

5. Dopodiche ho dovuto fare **BugFixing** e finire il tutto
