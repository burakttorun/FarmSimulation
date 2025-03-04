using System;
using BasicArchitecturalStructure;
using UnityEngine;

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
        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
        }

        private void Update()
        {
            if (InputManager.Instance.IsPointerOverUI()) return;

            Vector3 mousePosition = InputManager.Instance.GetSelectedMapPosition();
            Vector3Int gridPosition = Grid.WorldToCell(mousePosition);
            _transform.position = Grid.GetCellCenterWorld(gridPosition);
        }
    }
}