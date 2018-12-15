using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	private static Loader instance = null;
	public static Loader Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		//DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
			
		SceneManager.LoadScene ("Game");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
