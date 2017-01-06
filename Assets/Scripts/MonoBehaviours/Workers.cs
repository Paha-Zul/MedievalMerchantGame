using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Workers : MonoBehaviour
    {
        public int MaxWorkers;
        public List<WorkerUnit> WorkerUnitList = new List<WorkerUnit>();

        public void AddWorker(WorkerUnit worker) {
            WorkerUnitList.Add(worker);
        }

        public void RemoveWorker(WorkerUnit worker) {
            WorkerUnitList.Remove(worker);
        }
    }
}