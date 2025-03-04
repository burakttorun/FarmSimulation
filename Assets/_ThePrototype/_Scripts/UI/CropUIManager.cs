using System.Collections.Generic;
using ThePrototype.Scripts.Manager.SO;
using UnityEngine;
using UnityEngine.UI;

namespace ThePrototype.Scripts.Manager
{
    public class CropUIManager : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private EntityDatabaseSO _entityDatabase;

        [SerializeField] private GameObject _buttonPrefab;

        [SerializeField] private Transform _buttonContainer;

        private PlacementManager _placementManager;

        private void Start()
        {
            _placementManager = GetComponent<PlacementManager>();
            CreateButtons();
            _buttonContainer.gameObject.SetActive(false);
        }

        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
                IndicatorManager.Instance.CurrentItem == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    var hitCollider = hit.collider;
                    if (hitCollider == this.GetComponent<Collider>())
                    {
                        _buttonContainer.gameObject.SetActive(true);
                    }
                }
            }
        }

        private void CreateButtons()
        {
            foreach (var entity in _entityDatabase.entityData)
            {
                GameObject buttonObject = Instantiate(_buttonPrefab, _buttonContainer);


                Button button = buttonObject.GetComponent<Button>();
                Image buttonImage = buttonObject.GetComponent<Image>();


                buttonImage.sprite = entity.icon;


                button.onClick.AddListener(() => OnButtonClick(entity.id));
            }
        }

        private void OnButtonClick(int entityId)
        {
            if (_placementManager != null && IndicatorManager.Instance.CurrentItem == null)
            {
                _placementManager.StartCropPlacement(_entityDatabase, entityId, _buttonContainer.gameObject);
            }
        }
    }
}