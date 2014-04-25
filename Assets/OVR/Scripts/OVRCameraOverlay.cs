using UnityEngine;
using System.Collections;

public class OVRCameraOverlay {

	private Texture2D overlayTexture;
	private float alphaFadeValue = 0;
	private bool FadeIn = false;
	private bool FadeOut = false;
	private float FadePerSecond = 0;

	public OVRCameraOverlay()
	{
		createTexture ();
	}

	private void createTexture()
	{
		//overlayTexture = LoadAssetAtPath("Assets/MyTextures/TextureBlack.jpg", typeof(Texture2D)) as Texture2D;
		string texture = "Assets/Resources/MyTextures/TextureBlack.jpg";
		overlayTexture = (Texture2D)Resources.LoadAssetAtPath(texture, typeof(Texture2D));
	}

	public void drawTexture()
	{

		if(FadeIn) {
			alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / FadePerSecond);

			if(alphaFadeValue <= 0) {
				FadeIn = false;
				alphaFadeValue = 0;
			}
		} else if (FadeOut) {
			alphaFadeValue += Mathf.Clamp01(Time.deltaTime / FadePerSecond);

			if(alphaFadeValue >= 1) {
				FadeOut = false;
				alphaFadeValue = 1;
			}
		}

		//Debug.Log("Alpha: " + alphaFadeValue);
		GUI.color = new Color(0, 0, 0, alphaFadeValue);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlayTexture);
	}

	public void fadeOut(float durationInSeconds)
	{
		FadeOut = true;
		FadeIn = false;
		FadePerSecond = durationInSeconds;
	}

	public void fadeIn(float durationInSeconds)
	{
		FadeIn = true;
		FadeOut = false;
		FadePerSecond = durationInSeconds;
	}
}
