using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] Player player;
    Transform player_Loc;
    

    private void Awake()
    {
        player_Loc = player.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(player_Loc, Vector3.up);
    }


}
