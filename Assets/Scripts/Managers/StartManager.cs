using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartManager : MonoBehaviour {

	Canvas canvas;

	void Start()
	{
		canvas = GetComponent<Canvas>();
		Time.timeScale = 0;
	}

	void Update()
	{
		if (canvas.enabled) {
			Time.timeScale = 0;
		}
	}

	public void StartGame()
	{
		//SceneManager.LoadScene (0);
		Time.timeScale = 1;
		AkSoundEngine.PostEvent ("mx_game_music", GameObject.Find ("WwiseGlobal"));
		//canvas.enabled = true;
		//Lowpass ();


	}



	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
