using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        //this should probably contain the lives, score and respawn mechanics of the player. 
        [Header("Player Variables")]
        //[SerializeField] public int maxShieldPoints = 5; //will replace or add to this with shields later
        [SerializeField] public int lives = 2;
        [SerializeField] public GameObject playerPrefab;
        [SerializeField] public int playerLives;
        [SerializeField] public int score;

        [Header("Other Variables")]
        [SerializeField] public GameObject Scroller;
        [SerializeField] public TerrainScroller actscroller;

        [Header("Unity UI Variables")]//will be moved to player manager
        [SerializeField] public Slider shieldSlider;
        [SerializeField] public TextMeshProUGUI pickUpInfo;
        [SerializeField] public TextMeshProUGUI scoreDisplay;

        public static PlayerManager Instance { get; private set; }
        [SerializeField] public PlayerScript player;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            player = FindFirstObjectByType<PlayerScript>();

            Debug.Log($"Scroller = {Scroller}");
            actscroller = Scroller.GetComponent<TerrainScroller>();
            shieldSlider.maxValue = player.maxShieldPoints;
        }

        private void Start()
        {
            pickUpInfo.text = $"Monitoring Systems";
        }

        private void Update()
        {
            shieldSlider.value = player.shieldPoints;
            scoreDisplay.text = DisplayScore(score);
        
        }

        public void TakeDamage(int dmg)
        {
            //add in call to animator to show hit effect here
            //This will be changed to account for shielding later in the development. 
            //We should allow for upgrades to the players shields and hull etc - Tyrian
            if (player.iAmInvincible || player.alreadyHit)
            {
                Debug.Log($"<color=blue>Player can't take damage right now</color>");
            }
            else
            {
                Debug.Log($"{player.name} taking <color=green> {dmg} </color> damage to <color=cyan>{player.shieldPoints} </color>total HP");
                player.shieldPoints -= dmg;

                if (player.shieldPoints <= 0)
                {
                    player.shieldPoints = 0;
                    Debug.Log("<color=orange>PLAYER KILLED</color>");
                    KillMe();
                }

                //Update UI SLIder
                shieldSlider.value = player.shieldPoints;
                Debug.Log($"{player.name} now has <color=orange>{player.shieldPoints}</color> out of <color=cyan>{player.maxShieldPoints}</color> total HP.");
                StartCoroutine(StopMultiCrash());

            }

        }

        IEnumerator StopMultiCrash()
        {
            player.alreadyHit = true;
            yield return new WaitForSeconds(1.5f); //stops further collision registration
            player.alreadyHit = false;
        }

        public void KillMe()
        {
            //add a call to explosion animation here with a wait
            Vector3 myPos = transform.position;

            playerLives--;
            if (playerLives > 0)
            {
                player.iAmInvincible = true;
                Debug.Log($"Player loses a Life!");
                StartCoroutine(LoseLife());
            }
            else if (playerLives >= 0)
            {
                Debug.Log($"<color=red>PLAYER DESTROYED GAME OVER</color>");
                GameManager.Instance.PopUpScreen.SetActive(true);
                GameManager.Instance.PopUpText.text = $"Player Destroyed\nGAME OVER!";            
                StartCoroutine(EndGame());
            }

        }

        IEnumerator LoseLife()
        {
            actscroller.scrolling = false;
            player.gameObject.SetActive(false);
            Debug.Log($"<color=green>Player Respawning</color>");
            yield return new WaitForSeconds(2f);
            //"respawn" player
            player.gameObject.SetActive(true);
            //check if fighting boss
            if (!player.engagedBoss)
                actscroller.scrolling = true;
            else
                actscroller.scrolling = false;

            //add a blinking effect or glowing effect to show invulnerability

            yield return new WaitForSeconds(2f);

            //end invulnerability flash here

            player.iAmInvincible = false;
            player.shieldPoints = player.maxShieldPoints;
            shieldSlider.value = player.shieldPoints;
            Debug.Log($"Player no longer invincible");
        }

        public void AddToScore(int scoreToAdd)
        {
            score += scoreToAdd;
        }

        public string DisplayScore(int score)
        {
            string scoreDisplay ="";
            scoreDisplay = score.ToString("D9");
            return scoreDisplay;
        }

        public IEnumerator EndGame()
        {
        
            yield return new WaitForSeconds(5f); //wait for explosion effect

            //FADE OVER 15 SECONDS\\
            float duration = 15f;
            float elapsed = 0f;

            Color c = GameManager.Instance.FadeScreen.color;
            while(elapsed < duration) 
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsed / duration);
                GameManager.Instance.FadeScreen.color = new Color(c.r, c.g, c.b, alpha);
                yield return null;
            }       
        
            SceneManager.LoadScene("Title");
            GameManager.Instance.KillGame();
            Destroy(this);
        }

    }
}
