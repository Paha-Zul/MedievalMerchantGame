using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class Finder {

        /// <summary>
        /// Finds the closest Building of a type to a given point.
        /// </summary>
        /// <typeparam name="T">The type of Building to find.</typeparam>
        /// <param name="buildingList">The list of Buildings to search through.</param>
        /// <param name="point">The point to search closest to.</param>
        /// <returns>The closest Building if found, null otherwise.</returns>
        public static T FindClosestBuildingOfType<T>(List<Building> buildingList, Vector3 point) where T:Building {
            T closestBuilding = null;
            float closestDst = float.MaxValue;

            //Loop through each building and find the closest one that matches the type.
            foreach(Building building in buildingList) {
                if(building is T) {
                    var dst = Vector3.Distance(point, building.transform.position);
                    if(dst <= closestDst) {
                        closestBuilding = building as T;
                        closestDst = dst;
                    }
                }
            }

            return closestBuilding;
        }

        /// <summary>
        /// Finds the closest stockpile, to the point given, with the required item and amount.
        /// </summary>
        /// <param name="buildingList">The list of buildings to search through.</param>
        /// <param name="point">The point to be closest to.</param>
        /// <param name="itemName">The name of the Item.</param>
        /// <param name="amount">The amount needed of the Item.</param>
        /// <returns>The closest Stockpile with the item, or null if no stockpile could be found, or no stockpile with the item was found.</returns>
        public static Stockpile FindClosestStockpileWithItem(List<Building> buildingList, Vector3 point, string itemName, int amount = 1) {
            Stockpile closestBuilding = null;
            float closestDst = float.MaxValue;

            //Loop through each building and find the closest one that matches the type.
            foreach (Building building in buildingList) {
                if (building is Stockpile) {
                    var dst = Vector3.Distance(point, building.transform.position);
                    if (dst <= closestDst && building.MyUnit.inventory.GetItemAmount(itemName) >= amount) {
                        closestBuilding = building as Stockpile;
                        closestDst = dst;
                    }
                }
            }

            return closestBuilding;
        }
    }
}
