using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	void OnGUI()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.LoadLevel ("MainMenu");
	}
}
