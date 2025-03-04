using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.Manager.SO
{
    public enum EntityType
    {
        Soil = 0,
        Crop = 1,
        Building = 2
    }
    

    [CreateAssetMenu(fileName = "EntityDatabase", menuName = "EntityDatabase/Database")]
    public class EntityDatabaseSO : ScriptableObject
    {
        public List<PlaceableEntitySO> entityData;
    }
}