using UnityEngine;

public class TerrainScroller : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public bool scrolling = true;

    private void Update()
    {
        if (scrolling)
            transform.position += Vector3.back * scrollSpeed * Time.deltaTime;
    }
}
