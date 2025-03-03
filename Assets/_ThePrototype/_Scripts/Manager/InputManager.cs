using BasicArchitecturalStructure;
using UnityEngine;

namespace ThePrototype.Scripts.Manager
{
    public class InputManager : BasicSingleton<InputManager>
    {
        #region CachedData

        private Camera _sceneCamera;
        private Vector3 _lastPosition;
        private Ray _ray;
        private RaycastHit _hit;

        private float _lastRaycastTime;
        private float _raycastInterval = 0.02f;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            _sceneCamera = Camera.main;
        }

        public Vector3 GetSelectedMapPosition(LayerMask placementLayerMask)
        {
            if (Time.time - _lastRaycastTime < _raycastInterval)
                return _lastPosition;

            _lastRaycastTime = Time.time;

            bool hasInput = false;
            Vector2 screenPos = Vector2.zero;

            if (Application.isEditor || SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (Input.GetMouseButton(0))
                {
                    hasInput = true;
                    screenPos = Input.mousePosition;
                }
            }
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    hasInput = true;
                    screenPos = touch.position;
                }
            }

            if (hasInput)
            {
                _ray = _sceneCamera.ScreenPointToRay(screenPos);
                if (Physics.Raycast(_ray, out _hit, 100, placementLayerMask))
                {
                    _lastPosition = _hit.point;
                }
            }

            return _lastPosition;
        }
    }
}