using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class Util
    {
        /// <summary>
        /// Finds a path from the start to end node.
        /// </summary>
        /// <param name="start">The starting PathNode to search from.</param>
        /// <param name="end">The ending PathNode to reach</param>
        /// <returns>A path going from the start PathNode to the ending PathNode if found. Otherwise, returns an empty path.</returns>
        public static Transform[] FindPathToNode(PathNode start, PathNode end)
        {
            ClearGraph(start, end);

            start.ParentNode = null; //Make sure to null this first
            end.ParentNode = null; //Make sure to also null this.

            //Breadth-first search?
            Queue<PathNode> queue = new Queue<PathNode>();
            queue.Enqueue(start);
            var found = false;

            //Loop until the queue is empty or we found the end.
            while (queue.Count > 0 && !found)
            {
                var current = queue.Dequeue();
                foreach (var neighbor in current.NeighborNodes)
                {
                    if (neighbor.ParentNode != null) continue; //Don't do anything if the parent is already set.
                    neighbor.ParentNode = current; //Set the parent of the neighbor
                    queue.Enqueue(neighbor); //Enqueue the neighbor
                    if (neighbor != end) continue;
                    found = true;
                    break;
                }
            }

            if (!found) return new Transform[0];

            List<Transform> path = new List<Transform>();
            start.ParentNode = null; //Do this so our starting node doesn't loop back to anything. We use this as our flag to stop.
            var curr = end;
            while (curr != null)
            {
                path.Add(curr.transform);
                curr = curr.ParentNode;
            }

            path.Reverse();

            return path.ToArray();
        }

        private static void ClearGraph(PathNode start, PathNode end)
        {
            start.ParentNode = null; //Make sure to null this first
            end.ParentNode = null; //Make sure to also null this.

            //Breadth-first search?
            Queue<PathNode> queue = new Queue<PathNode>();
            queue.Enqueue(start);

            //Loop until the queue is empty or we found the end.
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var neighbor in current.NeighborNodes)
                {
                    if (neighbor.ParentNode == null) continue; //Don't do anything if the neighbor node was already set.
                    neighbor.ParentNode = null; //Set the parent of the neighbor
                    queue.Enqueue(neighbor); //Enqueue the neighbor
                }
            }
        }

        public static PathNode GetSpotOfBuilding(string spotName, Building building, int index = 0)
        {
            PathNode closestNode = null;

            if (!spotName.Equals(""))
            {
                //TODO Handle multiple spots here in the array...
                if (spotName.Equals("work"))
                {
                    closestNode = building.workSpots[index].GetComponent<PathNode>();
                }
                else if (spotName.Equals("sell"))
                {
                    closestNode = building.sellSpots[index].GetComponent<PathNode>();
                }
                else if (spotName.Equals("entrance"))
                {
                    closestNode = building.entranceSpots[index].GetComponent<PathNode>();
                }
            }

            return closestNode;
        }
    }
}