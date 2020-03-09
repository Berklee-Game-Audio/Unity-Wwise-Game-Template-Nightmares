using UnityEngine;
using System.Collections;

public class PlayerEnemiesWithinRange : MonoBehaviour {
	float minimumEnemyDistance = 100.0f;
	public int enemiesWithinRange = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag ("Enemy");
		//GameObject closest = null;
		enemiesWithinRange = 0;
		//float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < minimumEnemyDistance) {
				enemiesWithinRange++;

			}

		}

		AkSoundEngine.SetRTPCValue ("number_of_enemies_within_range", enemiesWithinRange, GameObject.Find ("WwiseGlobal"));


		//Debug.Log ("enemies within range = " + enemiesWithinRange);
	}


}
