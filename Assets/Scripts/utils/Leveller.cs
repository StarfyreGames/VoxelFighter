using UnityEngine;

public class Leveller : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 myPos = transform.position;
        myPos.y = PlayerScript.Instance.yPos;
        transform.position = myPos;
    }
}
