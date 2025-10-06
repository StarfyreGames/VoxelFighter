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
            Debug.Log($"<color=red>ENTER THE BOSS!</color>");

        }
    }
}
