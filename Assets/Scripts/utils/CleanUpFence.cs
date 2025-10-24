using JetBrains.Annotations;
using UnityEngine;

namespace utils
{
    /**
     * A helper class that uses the CleanUpFence Game Object loaded into the scene to
     * expose a facade API for working with the cleanup fence. Thing outside the fence
     * area considered ready for cleanup.
     */
    public static class CleanUpFence
    {
        [CanBeNull] private static readonly ViewportArea Instance =
            GameObject.Find("CleanUpFence")?.GetComponent<ViewportArea>();
        
        public static bool ShouldDestroy(GameObject gameObject)
        {
            // If the game object was not found - just return false as we don't know
            return Instance?.IsOutOfViewportArea(gameObject.transform.position) ?? false;
        }
    }
}