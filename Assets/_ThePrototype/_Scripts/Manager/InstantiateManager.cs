using System;
using System.Collections;
using System.Collections.Generic;
using BasicArchitecturalStructure;
using ThePrototype.Scripts.Manager.SO;
using UnityEngine;

namespace ThePrototype.Scripts.Manager
{
    public class InstantiateManager : BasicSingleton<InstantiateManager>
    {
        [Header("Reference")] [SerializeField] private EntityDatabaseSO _database;
        [SerializeField] private GameObject _gridVisualization, _cellIndicator, _harvestEntity;

        private int _selectedObjectIndex = -1;

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
            CreateItem();
        }

        private void StopPlacement()
        {
            _selectedObjectIndex = -1;
            _gridVisualization.SetActive(false);
            _cellIndicator.SetActive(false);
        }

        private void CreateItem()
        {
            if (InputManager.Instance.IsPointerOverUI() || IndicatorManager.Instance.CurrentItem != null) return;

            var placementItem = _database.entityData[_selectedObjectIndex];

            GameObject newItem = Instantiate(placementItem.prefab);
            newItem.transform.position = _cellIndicator.transform.position;
            IndicatorManager.Instance.CurrentItem = newItem;
        }

        public void CreateCropEntity(GameObject cropUI, GameObject placementItem)
        {
            GameObject newItem = Instantiate(placementItem);
            Destroy(newItem.GetComponent<GrowthManager>());
            newItem.GetComponent<CropEntityManager>()._cropUI = cropUI;
            newItem.transform.position = _cellIndicator.transform.position;
            IndicatorManager.Instance.CurrentItem = newItem;
            _cellIndicator.SetActive(true);
        }

        public void CreateHarvestEntity()
        {
            GameObject createdItem = Instantiate(_harvestEntity);
            createdItem.transform.position = _cellIndicator.transform.position;
            IndicatorManager.Instance.CurrentItem = createdItem;
            _cellIndicator.SetActive(true);
        }
    }
}