using System;
using System.Collections;
using System.Collections.Generic;
using BasicArchitecturalStructure;
using ThePrototype.Scripts.Manager.SO;
using UnityEngine;

namespace ThePrototype.Scripts.Manager
{
    public class PlacementManager : MonoBehaviour
    {
        [Header("Reference")] [SerializeField] private EntityDatabaseSO _database;

        [SerializeField] private GameObject _gridVisualization, _cellIndicator;
        [SerializeField] private Grid _grid;

        private EventBinding<OnClick> _onClickEventBinding;
        private EventBinding<OnExit> _onExitEventBinding;

        private int _selectedObjectIndex = -1;

        private void Awake()
        {
            _onClickEventBinding = new EventBinding<OnClick>(PlaceStructure);
            _onExitEventBinding = new EventBinding<OnExit>(StopPlacement);
        }


        private void Start()
        {
            StopPlacement();
        }

        public void StartPlacement(int id)
        {
            StopPlacement();
            _selectedObjectIndex = _database.entityData.FindIndex(x => x.id == id);
            if (_selectedObjectIndex < 0)
            {
                Debug.LogError($"No Id found {id}");
                return;
            }

            _gridVisualization.SetActive(true);
            _cellIndicator.SetActive(true);

            EventBus<OnClick>.Subscribe(_onClickEventBinding);
            EventBus<OnExit>.Subscribe(_onExitEventBinding);
        }

        private void StopPlacement()
        {
            _selectedObjectIndex = -1;
            _gridVisualization.SetActive(false);
            _cellIndicator.SetActive(false);

            EventBus<OnClick>.Unsubscribe(_onClickEventBinding);
            EventBus<OnExit>.Unsubscribe(_onExitEventBinding);
        }

        private void PlaceStructure(OnClick data)
        {
            if (InputManager.Instance.IsPointerOverUI()) return;
            
            var placementItem = _database.entityData[_selectedObjectIndex];
            Vector3 mousePosition = InputManager.Instance.GetSelectedMapPosition(placementItem.placementLayer);
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
            GameObject newObject = Instantiate(placementItem.prefab);
            newObject.transform.position = _grid.GetCellCenterWorld(gridPosition);
        }
    }
}