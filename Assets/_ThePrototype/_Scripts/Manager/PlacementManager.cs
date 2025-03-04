using System;
using System.Collections;
using System.Collections.Generic;
using ThePrototype.Scripts.Manager.SO;
using UnityEngine;

namespace ThePrototype.Scripts.Manager
{
    public class PlacementManager : MonoBehaviour
    {
        [field: Header("Reference")] [field: SerializeField]
        private GameObject _placementUI;

        [field: SerializeField] private Transform _itemVisual;

        private GameObject _cellIndicator;

        #region CashedData

        private Transform _transform;

        #endregion

        private bool _canDragable = true;
        private bool _rotated;
        public GameObject HasCropEntity { get; set; }


        private void Awake()
        {
            _transform = transform;
            _cellIndicator = IndicatorManager.Instance.gameObject;
        }

        private void Update()
        {
            if (!_canDragable) return;


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
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
            _placementUI.SetActive(true);
        }

        public void ReleaseItem()
        {
            _canDragable = false;
            _placementUI.SetActive(false);
            IndicatorManager.Instance.CurrentItem = null;
        }

        public void DeleteItem()
        {
            ReleaseItem();
            Destroy(gameObject);
        }

        public void RotateItem()
        {
            if (HasCropEntity != null) return;

            if (!_rotated)
            {
                _itemVisual.transform.Rotate(new Vector3(0, 90, 0));
            }
            else
            {
                _itemVisual.transform.Rotate(Vector3.zero);
            }
        }

        public void ReplaceItem()
        {
            if (HasCropEntity != null) return;

            _canDragable = true;
            _placementUI.SetActive(false);
            IndicatorManager.Instance.CurrentItem = gameObject;
        }

        public void StartCropPlacement(EntityDatabaseSO databaseSo, int id,GameObject cropUI)
        {
            if (HasCropEntity != null) return;
            var selectedObjectIndex = databaseSo.entityData.FindIndex(x => x.id == id);
            if (selectedObjectIndex < 0)
            {
                Debug.LogError($"No Id found {id}");
                return;
            }

            var placementItem = databaseSo.entityData[selectedObjectIndex];

            GameObject newItem = Instantiate(placementItem.prefab);
            newItem.GetComponent<CropEntityManager>()._cropUI = cropUI;
            newItem.transform.position = _cellIndicator.transform.position;
            IndicatorManager.Instance.CurrentItem = newItem;
            _cellIndicator.SetActive(true);
            
        }
    }
}