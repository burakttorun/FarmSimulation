using UnityEngine;

namespace ThePrototype.Scripts.Manager.SO
{
    [CreateAssetMenu(fileName = "PlaceableEntity", menuName = "Farming/Soil")]
    public class PlaceableEntitySO : ScriptableObject
    {
        public int id;
        public string objectName;
        public EntityType objectType;
        public GameObject prefab;
        public int cost;
        public Vector2Int size;
        public LayerMask placementLayer;
        public Sprite icon;
    }
}