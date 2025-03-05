using System;
using System.Collections;
using System.Collections.Generic;
using BasicArchitecturalStructure;
using ThePrototype.Scripts.Manager.SO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace ThePrototype.Scripts.Manager
{
    public class CropEntityManager : MonoBehaviour
    {
        [field: Header("Reference")] [SerializeField]
        private CropSO _settings;

        [field: HideInInspector] public GameObject _cropUI;
        private PlacementManager _currentPlacementEntity;
        private GrowthManager _currentGrowthManager;

        #region CashedData

        private Transform _transform;

        #endregion

        private bool _canDragable;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (_canDragable) return;
            CloseUI();

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        DragObject();
                        break;

                    case TouchPhase.Ended:
                        ReleaseItem();
                        break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Soil") && other.TryGetComponent(out PlacementManager placementManager))
            {
                _currentPlacementEntity = placementManager;
                if (_currentPlacementEntity.HasCropEntity != null) return;

                var createdEntity = Instantiate(_settings.prefab);
                _currentPlacementEntity.HasCropEntity = createdEntity;
                createdEntity.transform.position = _currentPlacementEntity.itemVisual.transform.position;
                _currentGrowthManager = createdEntity.GetComponent<GrowthManager>();
                EventBus<OnPlanted>.Publish(new OnPlanted());
            }
        }

        private void CloseUI()
        {
            _cropUI.SetActive(false);
        }

        private void DragObject()
        {
            _transform.position = InputManager.Instance.GetSelectedMapPosition();
        }

        public void ReleaseItem()
        {
            _canDragable = false;
            CloseUI();
            IndicatorManager.Instance.CurrentItem = null;
            Destroy(gameObject);
        }

        public void HarvestItem()
        {
                _currentPlacementEntity.HasCropEntity = null;
                _currentGrowthManager = null;
                Destroy(gameObject);
        }
    }
}