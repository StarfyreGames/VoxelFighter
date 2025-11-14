using UnityEngine;

public class LevelEndMarker : MonoBehaviour
{
    public GameObject Scroller;
    public TerrainScroller actscroller;
    //public BossController boss;

    private void Start()
    {
        Debug.Log($"Scroller = {Scroller}");
        actscroller = Scroller.GetComponent<TerrainScroller>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            actscroller.scrolling = false;
            PlayerManager.Instance.player.engagedBoss = true;
            Debug.Log($"<color=red>ENTER THE BOSS!</color>");

        }
    }

    //TODO : Here we need to start thinking about the Boss Fight. perhaps how it moves (waypoints?) and possible action patterns (when does it go in what direction, when does it fire its guns. What guns does it fire?)
    //We must also REMEMBER TO RESET THE ENGAGEDBOSS TAG!
}
