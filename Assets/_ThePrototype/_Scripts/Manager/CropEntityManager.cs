using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.Manager
{
    public class CropEntityManager : MonoBehaviour
    {
        [field: Header("Reference")] [field: HideInInspector]
        public GameObject _cropUI;

        private GameObject _cellIndicator;

        #region CashedData

        private Transform _transform;

        #endregion

        private bool _canDragable;

        private void Awake()
        {
            _transform = transform;
            _cellIndicator = IndicatorManager.Instance.gameObject;
        }

        private void Update()
        {
            if (_canDragable) return;


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        CloseUI();
                        break;
                    case TouchPhase.Moved:
                        DragObject();
                        break;

                    case TouchPhase.Ended:
                        ReleaseItem();
                        break;
                }
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
            _cropUI.SetActive(false);
            IndicatorManager.Instance.CurrentItem = null;
        }
    }
}