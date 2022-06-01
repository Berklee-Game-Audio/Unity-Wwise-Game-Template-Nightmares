using UnityEngine;

namespace CompleteProject
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.
		public TimeManager timer;
		//public GameObject timerText;

        Animator anim;                          // Reference to the animator component.


        void Awake ()
        {
            // Set up the reference.
            anim = GetComponent <Animator> ();
        }


        void Update ()
        {
            // If the player has run out of health...
			if (playerHealth.currentHealth <= 0) {
				// ... tell the animator the game is over.
				anim.SetTrigger ("GameOverLose");
			}  

			if(playerHealth.currentHealth > 0){

				if (TimeManager.timeRemaining < 0.5) {
					anim.SetTrigger ("GameOverWin");

				}

			}
        }
    }
}