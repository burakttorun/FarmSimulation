using System.Collections.Generic;
using BasicArchitecturalStructure;
using ThePrototype.Scripts.Manager.SO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ThePrototype.Scripts.Manager
{
    public class CropUIManager : PersistentSingleton<CropUIManager>
    {
        [Header("References")] [SerializeField]
        private EntityDatabaseSO _entityDatabase;

        [SerializeField] private GameObject _buttonPrefab;

        [SerializeField] private Transform _cropButtonContainer;
        [SerializeField] private Transform _harvestButtonContainer;

        private PlacementManager _placementManager;
        private EventBinding<OnPlanted> _cropPlantedEventBinding;
        private bool _isActiveCropUI;


        #region CashedData

        private Camera _camera;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            _cropPlantedEventBinding = new EventBinding<OnPlanted>(CropPlanted);
            _camera = Camera.main;
        }

        private void CropPlanted(OnPlanted data)
        {
            if (data.plantedPlace == _placementManager)
            {
                _isActiveCropUI = false;
                _cropButtonContainer.gameObject.SetActive(_isActiveCropUI);
            }
        }

        private void OnEnable()
        {
            EventBus<OnPlanted>.Subscribe(_cropPlantedEventBinding);
        }

        private void OnDisable()
        {
            EventBus<OnPlanted>.Unsubscribe(_cropPlantedEventBinding);
        }

        private void Start()
        {
            CreateButtons();
            _cropButtonContainer.gameObject.SetActive(false);
        }

        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
                IndicatorManager.Instance.CurrentItem == null)
            {
                Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

               
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.TryGetComponent(out PlacementManager placementManager))
                    {
                        _placementManager = placementManager;
                        if (_placementManager.HasCropEntity)
                        {
                            if (placementManager.HasCropEntity.TryGetComponent(out GrowthManager growthManager))
                            {
                                if (growthManager.IsReadyForHarvest())
                                {
                                    ChangeHarvestUIVisible(true);
                                    _harvestButtonContainer.transform.position = placementManager.transform.position;
                                }
                                else
                                {
                                    ChangeHarvestUIVisible(false);
                                }
                            }
                        }
                        else
                        {
                            ChangeCropUIVisible(true);
                            ChangeHarvestUIVisible(false);
                            _cropButtonContainer.transform.position = placementManager.transform.position;
                        }
                    }
                    else
                    {
                        ChangeCropUIVisible(false);
                        ChangeHarvestUIVisible(false);
                    }
                }
            }
        }

        
        private void ChangeCropUIVisible(bool data)
        {
            _isActiveCropUI = data;
            _cropButtonContainer.gameObject.SetActive(data);
        }

        public void ChangeHarvestUIVisible(bool data)
        {
            _harvestButtonContainer.gameObject.SetActive(data);
        }
        private void CreateButtons()
        {
            foreach (var entity in _entityDatabase.entityData)
            {
                GameObject buttonObject = Instantiate(_buttonPrefab, _cropButtonContainer);


                Image buttonImage = buttonObject.GetComponent<Image>();
                buttonImage.sprite = entity.icon;


                EventTrigger eventTrigger = buttonObject.GetComponent<EventTrigger>();

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((data) => OnButtonClick(entity.id));


                eventTrigger.triggers.Add(entry);
            }
        }

        private void OnButtonClick(int entityId)
        {
            if (_placementManager != null && IndicatorManager.Instance.CurrentItem == null)
            {
                _placementManager.StartCropPlacement(_entityDatabase, entityId, _cropButtonContainer.gameObject);
            }
        }
    }
}