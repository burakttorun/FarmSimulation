using System;
using BasicArchitecturalStructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace ThePrototype.Scripts.Manager
{
    public class IndicatorManager : BasicSingleton<IndicatorManager>
    {
        [field: Header("Reference")]
        [field: SerializeField] private Grid Grid { get; set; }

        #region CashedData

        private Transform _transform;

        #endregion
        
        [HideInInspector] public GameObject CurrentItem { get; set; }
        
        
        public GridData entityData;
        private Renderer _previewRenderer;
        public Vector3Int gridPosition;
        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
        }
        private void Start()
        {
            entityData = new();
            _previewRenderer = gameObject.GetComponentInChildren<Renderer>();
        }

        private void Update()
        {
            if (InputManager.Instance.IsPointerOverUI()) return;

            Vector3 mousePosition = InputManager.Instance.GetSelectedMapPosition();
             gridPosition = Grid.WorldToCell(mousePosition);
            
            _previewRenderer.material.color = CheckPlacementValidity() ? Color.white : Color.red;
            
            _transform.position = Grid.GetCellCenterWorld(gridPosition);
            
        }
        
        public bool CheckPlacementValidity()
        {
            var instanceManager = InstantiateManager.Instance;

            return entityData.CanPlaceObejctAt(gridPosition,
                instanceManager.database.entityData[instanceManager.selectedObjectIndex].size);
        }
        
    }
}