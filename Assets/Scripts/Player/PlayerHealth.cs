using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth = 100;                            // The amount of health the player starts the game with.
        public int currentHealth;                                   // The current health the player has.
        public Slider healthSlider;                                 // Reference to the UI's health bar.
        public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
        public AudioClip deathClip;                                 // The audio clip to play when the player dies.
		public AudioClip winClip;                                 // The audio clip to play when the player wins.
		public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


        Animator anim;                                              // Reference to the Animator component.
        AudioSource playerAudio;                                    // Reference to the AudioSource component.
        PlayerMovement playerMovement;                              // Reference to the player's movement.
        PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
        bool isDead;                                                // Whether the player is dead.
        bool damaged;                                               // True when the player gets damaged.

		public Canvas startScreenCanvas;
		public bool gameIsRunning = false;

		public float winScreenWaitTime = 5.0f;
		public float loseScreenWaitTime = 5.0f;

		private bool restartQueued = false;
		private float waitingTime = 0.0f;
		private float timeUntilRestart = 5.0f;

        void Awake ()
        {
            // Setting up the references.
            anim = GetComponent <Animator> ();
            playerAudio = GetComponent <AudioSource> ();
            playerMovement = GetComponent <PlayerMovement> ();
            playerShooting = GetComponentInChildren <PlayerShooting> ();

            // Set the initial health of the player.
            currentHealth = startingHealth;
			gameIsRunning = true;
        }


        void Update ()
        {
            // If the player has just been damaged...
            if(damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;

			if (restartQueued) {
				waitingTime = waitingTime + Time.deltaTime;
				if (waitingTime > timeUntilRestart) {
					RestartLevelNew ();
				}

			}
        }


        public void TakeDamage (int amount)
        {
			if (!gameIsRunning)
				return;

			// Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            // Set the health bar's value to the current health.
            healthSlider.value = currentHealth;

            // Play the hurt sound effect.
            playerAudio.Play ();
			AkSoundEngine.PostEvent ("sfx_player_hit", GameObject.Find ("WwiseGlobal"));

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if(currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death ();
            }

			AkSoundEngine.SetRTPCValue ("player_health", currentHealth, GameObject.Find ("WwiseGlobal"));

			//put in a script which checks the time remaining


        }


        void Death ()
        {
			if (!gameIsRunning) {
				return;
			}

			gameIsRunning = false;
			// Set the death flag so this function won't be called again.
            isDead = true;

            // Turn off any remaining shooting effects.
            playerShooting.DisableEffects ();

            // Tell the animator that the player is dead.
            anim.SetTrigger ("Die");

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            playerAudio.clip = deathClip;
            playerAudio.Play ();
			AkSoundEngine.PostEvent ("sfx_player_death", GameObject.Find ("WwiseGlobal"));
			AkSoundEngine.PostEvent ("mx_game_lose", GameObject.Find ("WwiseGlobal"));

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            playerShooting.enabled = false;

			restartQueued = true;
			waitingTime = 0.0f;
			timeUntilRestart = loseScreenWaitTime;
        }


		public void Win ()
		{
			if (!gameIsRunning) {
				return;
			}

			gameIsRunning = false;
			// Set the death flag so this function won't be called again.
			isDead = true;

			// Turn off any remaining shooting effects.
			playerShooting.DisableEffects ();

			// Tell the animator that the player is dead. - we need to call this since the restartlevel is embedded in it
			anim.SetTrigger ("Die");

			// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
			playerAudio.clip = winClip;
			playerAudio.Play ();
			AkSoundEngine.PostEvent ("mx_game_win", GameObject.Find ("WwiseGlobal"));

			// Turn off the movement and shooting scripts.
			playerMovement.enabled = false;
			playerShooting.enabled = false;

			restartQueued = true;
			waitingTime = 0.0f;
			timeUntilRestart = winScreenWaitTime;
		}



        public void RestartLevel ()
        {
            // Reload the level that is currently loaded.
			Debug.Log("RESTART LEVEL CALLED WITHIN THE PLAYER HEALTH CS - disabled");
			//AkSoundEngine.PostEvent ("mx_menu_music", GameObject.Find ("WwiseGlobal"));
			//SceneManager.LoadScene (1);
			//Time.timeScale = 0;
			//currentHealth = 100;
			//startScreenCanvas.enabled = true;
        }


		public void RestartLevelNew ()
		{
			// Reload the level that is currently loaded.
			Debug.Log("RestartLevelNew");
			restartQueued = false;
			AkSoundEngine.PostEvent ("mx_menu_music", GameObject.Find ("WwiseGlobal"));
			SceneManager.LoadScene (1);
			Time.timeScale = 0;
			currentHealth = 100;
			startScreenCanvas.enabled = true;
		}
    }
}