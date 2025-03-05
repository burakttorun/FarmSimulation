
using UnityEngine;

namespace ThePrototype.Scripts.Manager
{
    public class HarvestEntityManager : MonoBehaviour
    {
        
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
            
            if ( other.TryGetComponent(out CropEntityManager cropEntityManager))
            {
                // Destroy(other.gameObject);
                cropEntityManager.HarvestItem();
            }
        }
        
        private void DragObject()
        {
            _transform.position = InputManager.Instance.GetSelectedMapPosition();
        }

        public void ReleaseItem()
        {
            _canDragable = false;
            IndicatorManager.Instance.CurrentItem = null;
            Destroy(gameObject);
        }
    }
}