using Player.Scripts;
using UnityEngine;

public class Leveller : MonoBehaviour
{
    public PlayerScript player;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerScript>();
    }

    void Start()
    {
        Vector3 myPos = gameObject.transform.position;
        myPos.y = player.yPos;
        gameObject.transform.position = myPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myPos = gameObject.transform.position;
        myPos.y = player.yPos;
        gameObject.transform.position = myPos;
    }
}
