using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.Manager.SO
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Farming/Building")]
    public class BuildingSO : PlaceableEntitySO
    {
        public int maxWorkers;
        public int productionRate;
    }
}