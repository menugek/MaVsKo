using UnityEngine;
using System.Collections;

public class Instructi : MonoBehaviour {

	public Texture Background;
	public float placex;
	public float placey;

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);
		if (GUI.Button (new Rect (Screen.width * placex, Screen.height * placey, Screen.width * 0.2f, Screen.height * 0.05f), "Return")) {
			Application.LoadLevel("MainMenu");
		}
	}
}
