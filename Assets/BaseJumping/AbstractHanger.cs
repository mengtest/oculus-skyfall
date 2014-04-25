using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractHanger : MonoBehaviour {

	public bool isUsable; //whether the glider/hanger/whatever needs to be triggered by calling the Activate function

	protected OVRPlayerController player;

	protected GameObject HangerPrefab;
	protected bool PrefabHidden = true;
	protected bool RotatePrefab = true;

	protected float ModelOffsetX = 0;
	protected float ModelOffsetY = 0;
	protected float ModelOffsetZ = 0;

	public AbstractHanger(bool _isUsable, OVRPlayerController _player)
	{
		isUsable = _isUsable;
		player = _player;
		CreateModel ();
		HidePrefab ();
	}
	
	protected void HidePrefab()
	{
		SetVisible (false);
		PrefabHidden = true;
	}
	
	protected void ShowPrefab()
	{
		SetVisible (true);
		PrefabHidden = false;

		if(RotatePrefab) {
			Vector3 cRot = player.CameraController.CameraRight.transform.rotation.ToEulerAngles();
			this.HangerPrefab.transform.Rotate(0,cRot.y,0);
		}
	}
	
	private void SetVisible(bool value)
	{
		Renderer[] renderers = HangerPrefab.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers) {
			r.enabled = value;
		}
	}

	protected void SetModelPosition(Vector3 position)
	{
		if(!PrefabHidden) {
			HangerPrefab.transform.position = new Vector3(position.x + ModelOffsetX, position.y +  ModelOffsetY, position.z + ModelOffsetZ);

			if(RotatePrefab) {
				Vector3 cRot = player.CameraController.CameraRight.transform.rotation.eulerAngles;
				Vector3 pRot = this.HangerPrefab.transform.rotation.eulerAngles;
				float tRot = MakeValidAngle(cRot.y) - MakeValidAngle(pRot.y);
				//Debug.Log("Camera Angle: " + cRot.y + " Prefab Angle " + pRot.y + " Rotation: " + rotation);
				this.HangerPrefab.transform.Rotate(0,tRot,0);
			}
		}
	}

	public void UpdatePosition()
	{
		SetModelPosition (player.transform.position);
	}

	private float MakeValidAngle(float i)
	{
		while(i >= 360) i -= 360;
		while(i < 0) i += 360;
		return i;
	}

	public abstract void Reset();

	public abstract void CreateModel();

	public abstract void Activate();

	public abstract void Update();

	public abstract void Stop();

	public abstract bool OnBeforeControllerColliderHit(ControllerColliderHit hit);

	public abstract void OnAfterControllerColliderHit(ControllerColliderHit hit);

	public virtual void CleanUp()
	{
		GameObject.Destroy(HangerPrefab);
	}

}
