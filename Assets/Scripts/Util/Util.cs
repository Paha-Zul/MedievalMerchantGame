using System.Collections.Generic;
using Assets.Scripts.Util;
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
            ClearGraph(start);

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

        /// <summary>
        /// Finds a path from the start node to a node with a particular tag. Does not gaurantee the closest node.
        /// </summary>
        /// <param name="start">The starting PathNode to search from.</param>
        /// <param name="endSpotTypeTag">The tag for the ending node.</param>
        /// <returns>A path going from the start PathNode to the ending PathNode if found. Otherwise, returns an empty path.</returns>
        public static Transform[] FindPathToNode(PathNode start, string endSpotTypeTag)
        {
            ClearGraph(start);

            start.ParentNode = null; //Make sure to null this first
            global::Util.PathNode end = null;

            //Breadth-first search?
            var queue = new Queue<PathNode>();
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
                    if (!neighbor.gameObject.CompareTag(endSpotTypeTag)) continue;

                    end = neighbor;
                    found = true;
                    break;
                }
            }

            if (!found) return new Transform[0];

            var path = new List<Transform>();
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

        private static void ClearGraph(PathNode start)
        {
            start.ParentNode = null; //Make sure to null this first

            //Breadth-first search?
            var queue = new Queue<PathNode>();
            var closedSet = new HashSet<PathNode>();

            queue.Enqueue(start);

            //Loop until the queue is empty or we found the end.
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var neighbor in current.NeighborNodes)
                {
                    neighbor.ParentNode = null; //Set the parent of the neighbor
                    if (closedSet.Contains(neighbor)) continue;
                    closedSet.Add(neighbor);
                    queue.Enqueue(neighbor); //Enqueue the neighbor
                }
            }
        }

        public static PathNode GetSpotOfBuilding(SpotType spotType, Building building, int index = 0)
        {
            PathNode closestNode = null;

            switch (spotType)
            {
                case SpotType.Entrance:
                    closestNode = building.EntranceSpots[index].GetComponent<PathNode>();
                    break;
                case SpotType.Work:
                    closestNode = building.WorkSpots[index].GetComponent<PathNode>();
                    break;
                case SpotType.Sell:
                    closestNode = building.SellSpots[index].GetComponent<PathNode>();
                    break;
                case SpotType.None:
                    break;
                case SpotType.Walk:
                    break;
                case SpotType.Delivery:
                    break;
                case SpotType.Buy:
                    break;
                case SpotType.Curr:
                    break;
                default:
                    closestNode = building.EntranceSpots[index].GetComponent<PathNode>();
                    break;
            }

            return closestNode;
        }
    }
}