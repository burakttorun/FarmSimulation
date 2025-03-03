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

    public abstract class PlaceableEntitySO : ScriptableObject
    {
        public string objectName;
        public EntityType objectType;
        public GameObject prefab;
        public int cost;
        public Vector2Int size;
    }

    [CreateAssetMenu(fileName = "EntityDatabase", menuName = "PlaceableEntity/Database")]
    public class EntityDatabaseSO : ScriptableObject
    {
        public List<PlaceableEntitySO> entityData;
    }
}