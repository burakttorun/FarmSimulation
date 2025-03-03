using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.Manager.SO
{
    [CreateAssetMenu(fileName = "New Crop", menuName = "Farming/Crop")]

    public class CropSO : PlaceableEntitySO
    {
        public GameObject[] growthStages; 
        public float growthTime;
    }
}