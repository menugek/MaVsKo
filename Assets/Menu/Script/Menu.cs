using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public Texture Background;

	void OnGUI ()
	{
		// Background
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Background);

		// Button
		if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.5f, Screen.width * 0.2f, Screen.height * 0.05f), "Play Game")) {
			Application.LoadLevel("Character_controler");
		}
		if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.6f, Screen.width * 0.2f, Screen.height * 0.05f), "Instructions")) {
			Application.LoadLevel("Instruction");
		}
		if (GUI.Button (new Rect (Screen.width * 0.4f, Screen.height * 0.7f, Screen.width * 0.2f, Screen.height * 0.05f), "Quit")) {
			Application.Quit();
		}
	}
}
