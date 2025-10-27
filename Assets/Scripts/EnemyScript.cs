using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private void Update()
    {
        Vector3 myPos = transform.position;
        myPos.y = PlayerScript.Instance.yPos;
        transform.position = myPos;
    }

}
