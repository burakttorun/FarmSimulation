using System;
using BasicArchitecturalStructure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace ThePrototype.Scripts.Manager
{
    public class InputManager : BasicSingleton<InputManager>
    {
        [SerializeField] private LayerMask _placementLayerMask;

        #region CachedData

        private Camera _sceneCamera;
        private Vector3 _lastPosition;
        private Ray _ray;
        private RaycastHit _hit;

        private float _lastRaycastTime;
        private const float RaycastInterval = 0.02f;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            _sceneCamera = Camera.main;
        }

        private void Update()
        {
            CheckClick();
        }

        public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

        public Vector3 GetSelectedMapPosition()
        {
            if (Time.time - _lastRaycastTime < RaycastInterval)
                return _lastPosition;

            _lastRaycastTime = Time.time;

            if (TryGetScreenPosition(out Vector2 screenPos))
            {
                _ray = _sceneCamera.ScreenPointToRay(screenPos);
                if (Physics.Raycast(_ray, out _hit, 50, _placementLayerMask))
                {
                    _lastPosition = _hit.point;
                }
            }

            return _lastPosition;
        }

        private bool TryGetScreenPosition(out Vector2 screenPos)
        {
            screenPos = Vector2.zero;

            if (IsDesktopPlatform())
            {
                screenPos = Input.mousePosition;
                return true;
            }
            else
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    screenPos = touch.position;
                    return true;
                }
            }

            return false;
        }

        public void CheckClick()
        {
            if (IsDesktopPlatform())
            {
                HandleDesktopInput();
            }
            else
            {
                HandleTouchInput();
            }
        }

        private bool IsDesktopPlatform()
        {
            return Application.isEditor || SystemInfo.deviceType == DeviceType.Desktop;
        }

        private void HandleDesktopInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PublishClickEvent();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                PublishExitEvent();
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    PublishClickEvent();
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    PublishExitEvent();
                }
            }
        }

        private void PublishClickEvent()
        {
            EventBus<OnClick>.Publish(new OnClick { clickPosition = _lastPosition });
        }

        private void PublishExitEvent()
        {
            EventBus<OnExit>.Publish(new OnExit() { lastPosition = _lastPosition });
        }

        public Vector3 GetTouchWorldPosition()
        {
            if (Input.touchCount > 0)
            {
                Vector3 touchPosition = Input.GetTouch(0).position;
                touchPosition.z = _sceneCamera.nearClipPlane + 2f;
                return _sceneCamera.ScreenToWorldPoint(touchPosition);
            }

            return Vector3.zero;
        }
    }
}