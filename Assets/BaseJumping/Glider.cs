using UnityEngine;
using System.Collections;

public class Glider : Parachute {

	public Glider(OVRPlayerController player) : base(player)
	{
		ModelOffsetY = 1.1f;
	}

	public override void Update()
	{
		this.SetModelPosition(player.transform.position);
		if(ParachuteActive && !player.Controller.isGrounded) {
			player.FallSpeed = -0.075f;
			player.MoveThrottle += player.DirXform.TransformDirection(Vector3.forward / 4);
		} else if (player.Controller.isGrounded) {
			Stop ();
		}
	}



	public override void CreateModel()
	{
		this.HangerPrefab = Instantiate(Resources.Load("HangGlider")) as GameObject; 
		this.SetModelPosition(player.transform.position);
		Vector3 cRot = player.CameraController.CameraRight.transform.rotation.eulerAngles;
		this.HangerPrefab.transform.Rotate(0,cRot.x,0);
	}
}
