using UnityEngine;

namespace Util
{
    public class PathNode:MonoBehaviour
    {
        public PathNode[] NeighborNodes;

        [HideInInspector] public PathNode ParentNode = null;

        void OnDrawGizmos() {
            foreach (var neighbor in NeighborNodes)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}