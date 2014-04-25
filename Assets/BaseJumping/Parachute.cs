using UnityEngine;
using System.Collections;

public class Parachute : AbstractHanger {

	protected bool ParachuteActive = false;
	public int ParachuteAmount = 1;

	public Parachute(OVRPlayerController player) : base(true, player)
	{
		ModelOffsetY = -0.7f;
	}

	public override void CreateModel()
	{
		RotatePrefab = false;
		this.HangerPrefab = Instantiate(Resources.Load("Parachute")) as GameObject; 
		this.HangerPrefab.transform.Rotate(-90,0,0);
		this.UpdatePosition();
	}

	public override void Update()
	{
		this.UpdatePosition();
		if(ParachuteActive && !player.Controller.isGrounded) {
			player.FallSpeed = -0.2f;
		} else if (player.Controller.isGrounded) {
			Stop ();
		}
	}

	public override void Activate()
	{
		if(!ParachuteActive) {
			ParachuteActive = true;
			ParachuteAmount--;
			ShowPrefab();
		} else {
			Stop ();
		}
	}

	public override void Stop()
	{
		Debug.Log ("STOP");
		ParachuteActive = false;
		HidePrefab();
	}

	public override void Reset()
	{
		ParachuteAmount = 1;
	}

	public override bool OnBeforeControllerColliderHit(ControllerColliderHit hit)
	{
		return true;
	}

	public override void OnAfterControllerColliderHit(ControllerColliderHit hit)
	{

		//crash into a building eheheh
		if(ParachuteActive) {
			player.Stop();
		}
	}
}
