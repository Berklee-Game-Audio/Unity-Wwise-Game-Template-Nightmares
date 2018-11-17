using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
	public class TimeManager : MonoBehaviour {

		public float timeRemaining = 120.0f;
		public PlayerHealth health;
		//public bool gameIsRunning = false;

		float initialSettingOfTimeRemaining;

		Text text;


		void Awake ()
		{
			text = GetComponent <Text> ();
			initialSettingOfTimeRemaining = timeRemaining;

		}



		void Update ()
		{
			//Debug.Log (Time.timeScale);
			if (Time.timeScale == 1 && health.gameIsRunning) {

				timeRemaining -= Time.deltaTime;
				text.text = "Time Until Dawn: " + (int)timeRemaining;
			}

			if (timeRemaining <= 0.5 && health.gameIsRunning) {
				health.Win ();

			}

			AkSoundEngine.SetRTPCValue ("time_remaining_percentage", timeRemaining / initialSettingOfTimeRemaining * 100.0f, GameObject.Find ("WwiseGlobal"));

			//Debug.Log ("time_remaining_percentage: " + timeRemaining / initialSettingOfTimeRemaining * 100.0f);

		}



	}
}
