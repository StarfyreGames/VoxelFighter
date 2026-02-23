using System.Collections.Generic;
using UnityEngine;

namespace Ship.Model
{
    [CreateAssetMenu]
    public class WaypointRoute : ScriptableObject
    {
        [SerializeField] public List<WaypointTrack> waypointTracks;
    }
}