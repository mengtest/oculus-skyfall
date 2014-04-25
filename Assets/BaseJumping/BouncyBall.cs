using UnityEngine;
using System.Collections;

public class BouncyBall : AbstractHanger {

	private float SpeedDecay = 0.2f;
	private bool bounce = false;

	public BouncyBall(OVRPlayerController player) : base(true, player)
	{
		ModelOffsetY = -1;
		ShowPrefab();
	}

	public override void Activate()
	{
		if(player.FallSpeed < 0)
			player.FallSpeed *= 1.5f;
		else if (player.FallSpeed == 0)
			player.FallSpeed = 1.0f;
	}

	public override void Update()
	{
		this.UpdatePosition();
		if(!bounce && player.FallSpeed < -1) bounce = true;
	}

	public override void CreateModel()
	{
		this.HangerPrefab = Instantiate(Resources.Load("Bouncy Sphere")) as GameObject; 
		this.HangerPrefab.transform.Rotate(0,0,0);
		this.UpdatePosition();
	}

	public override void Stop()
	{

	}

	public override void Reset() 
	{
		bounce = false;
	}

	public void Bounce()
	{
		float fs = player.FallSpeed;
		
		if(fs > -0.25f) bounce = false; 
		
		if(bounce) {
			fs *= 1 - SpeedDecay;
			player.FallSpeed = -1 * fs; //hhhehehe bouncy :)))
		}
	}
	
	public override bool OnBeforeControllerColliderHit(ControllerColliderHit hit)
	{
		this.Bounce ();
		return false;
	}
	
	public override void OnAfterControllerColliderHit(ControllerColliderHit hit) 
	{

	}

}
