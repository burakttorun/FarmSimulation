using System;
using System.Collections;
using System.Collections.Generic;
using ThePrototype.Scripts.Manager.SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace ThePrototype.Scripts.Manager
{
    public class PlacementManager : MonoBehaviour
    {
        [field: Header("Reference")] public GameObject placementUI;
        public PlaceableEntitySO setting;
        public Transform itemVisual;
        private GameObject _cellIndicator;

        #region CashedData

        private Transform _transform;
        private Camera _camera;

        #endregion

        private bool _canDragable = true;
        private bool _rotated;
        public GameObject HasCropEntity { get; set; }

        private float _pressDuration = 0.75f;
        private float _pressTimer = 0f;
        private bool _isPressed = false;
        private List<GameObject> _placedEntity = new();

        private void Awake()
        {
            _camera = Camera.main;
            _transform = transform;
            _cellIndicator = IndicatorManager.Instance.gameObject;
        }


        private void Update()
        {
            HandleObjectPress();
            if (!_canDragable) return;


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        DragObject();
                        break;
                    
                    case TouchPhase.Moved:
                        DragObject();
                        break;

                    case TouchPhase.Ended:
                        OpenSelectionUI();
                        break;
                }
            }
        }

        private void DragObject()
        {
            _transform.position = _cellIndicator.transform.position;
        }

        private void OpenSelectionUI()
        {
            _canDragable = false;
            placementUI.SetActive(true);
        }

        public void PlacedItem()
        {
            if (IndicatorManager.Instance.CheckPlacementValidity() == false) return;


            _placedEntity.Add(gameObject);
            AddEntityData();
            ReleaseItem();
        }

        public void ReleaseItem()
        {
            _canDragable = false;
            placementUI.SetActive(false);
            IndicatorManager.Instance.CurrentItem = null;
        }


        public void DeleteItem()
        {
            if (HasCropEntity != null) return;
            if (_placedEntity.Contains(gameObject))
            {
                _placedEntity.Remove(gameObject);
                IndicatorManager.Instance.entityData.RemoveObjectAt(IndicatorManager.Instance.gridPosition);
            }

            ReleaseItem();
            Destroy(gameObject);
        }

        public void RotateItem()
        {
            if (HasCropEntity != null) return;

            if (!_rotated)
            {
                itemVisual.transform.Rotate(new Vector3(0, 90, 0));
            }
            else
            {
                itemVisual.transform.Rotate(Vector3.zero);
            }
        }

        public void ReplaceItem()
        {
            if (HasCropEntity != null) return;

            _canDragable = true;
            placementUI.SetActive(false);
            IndicatorManager.Instance.CurrentItem = gameObject;
        }

        public void StartCropPlacement(EntityDatabaseSO databaseSo, int id, GameObject cropUI)
        {
            if (HasCropEntity != null) return;
            var selectedObjectIndex = databaseSo.entityData.FindIndex(x => x.id == id);
            if (selectedObjectIndex < 0)
            {
                Debug.LogError($"No Id found {id}");
                return;
            }

            var placementItem = databaseSo.entityData[selectedObjectIndex];

            InstantiateManager.Instance.CreateCropEntity(cropUI, placementItem.prefab);
        }

        private void HandleObjectPress()
        {
            if (IndicatorManager.Instance.CurrentItem != null) return;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && IsPointerOverObject(touch))
                {
                    _isPressed = true;
                    _pressTimer = 0f;
                }

                if (_isPressed)
                {
                    _pressTimer += Time.deltaTime;
                    if (_pressTimer >= _pressDuration)
                    {
                        OpenSelectionUI();
                        _isPressed = false;
                    }
                }

                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _isPressed = false;
                    _pressTimer = 0f;
                }
            }
        }


        private bool IsPointerOverObject(Touch touch)
        {
            Ray ray = _camera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == _transform)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddEntityData()
        {
            var instanceManager = InstantiateManager.Instance;
            var indicatorManager = IndicatorManager.Instance;
            var data = instanceManager.database.entityData;
            indicatorManager.entityData.AddObjectAt(indicatorManager.gridPosition,
                data[instanceManager.selectedObjectIndex].size,
                data[instanceManager.selectedObjectIndex].id,
                _placedEntity.Count - 1
            );
        }
    }
}