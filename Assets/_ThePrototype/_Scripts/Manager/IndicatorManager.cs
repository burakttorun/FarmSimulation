using System;
using UnityEngine;

namespace ThePrototype.Scripts.Manager
{
    [Serializable]
    public struct IndicatorSetting
    {
        public LayerMask placementLayer;
    }

    public class IndicatorManager : MonoBehaviour
    {
        [field: Header("Reference")]
        [field: SerializeField]
        private GameObject MouseIndicator { get; set; }

        [field: SerializeField] private GameObject CellIndicator { get; set; }
        [field: SerializeField] private Grid Grid { get; set; }

        [field: Header("Settings")]
        [field: SerializeField]
        public IndicatorSetting Setting { get; private set; }

        private Vector3 _lastMousePosition;
        private Vector3Int _lastGridPosition;

        private void Update()
        {
            Vector3 mousePosition = InputManager.Instance.GetSelectedMapPosition(Setting.placementLayer);
            if (mousePosition == _lastMousePosition) return;

            _lastMousePosition = mousePosition;
            Vector3Int gridPosition = Grid.WorldToCell(mousePosition);
            MouseIndicator.transform.position = mousePosition;
            
            if (gridPosition != _lastGridPosition)
            {
                _lastGridPosition = gridPosition;
                CellIndicator.transform.position = Grid.GetCellCenterWorld(gridPosition);
            }
        }
    }
}