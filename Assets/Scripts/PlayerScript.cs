using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float yPos = 0;

    public static PlayerScript Instance { get; private set; }

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
    }

    private void LateUpdate()
    {
        yPos = transform.position.y;
    }
}

