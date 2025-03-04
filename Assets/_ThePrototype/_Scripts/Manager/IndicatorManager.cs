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


        private void Update()
        {
            Vector3 mousePosition = InputManager.Instance.GetSelectedMapPosition(Setting.placementLayer);
            Vector3Int gridPosition = Grid.WorldToCell(mousePosition);
            MouseIndicator.transform.position = mousePosition;
            CellIndicator.transform.position = Grid.GetCellCenterWorld(gridPosition);
        }
    }
}