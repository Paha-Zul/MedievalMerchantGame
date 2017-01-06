using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    public class PathNode:MonoBehaviour
    {
        public List<PathNode> NeighborNodes;

        [HideInInspector] public PathNode ParentNode = null;

        void OnDrawGizmos() {
            for(int i = 0; i < NeighborNodes.Count; i++) {
                if (NeighborNodes[i] == null) continue;
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, NeighborNodes[i].transform.position);
            }
        }

        /// <summary>
        /// Called when the script is loaded or something in the inspector changes
        /// </summary>
        void OnValidate()
        {

            //Take each neighbor we have and try to add ourselves into the other neighbors
            foreach (var neighbor in NeighborNodes)
            {
                if (neighbor == null) continue;

                var script = neighbor.GetComponent<PathNode>();
                if(!script.NeighborNodes.Contains(this)) script.NeighborNodes.Add(this);
            }
        }
    }
}