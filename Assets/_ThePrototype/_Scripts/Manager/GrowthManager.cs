using System.Collections;
using System.Collections.Generic;
using BasicArchitecturalStructure;
using ThePrototype.Scripts.Manager.SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace ThePrototype.Scripts.Manager
{
    public class GrowthManager : MonoBehaviour
    {
        [field: Header("Reference")] [SerializeField]
        private List<GameObject> growthStages;

        [SerializeField] private CropSO _setting;

        private EventBinding<OnPlanted> _cropPlantedEventBinding;
        private bool _isAlreadyGrown = false;
        private bool _isReadyForHarvest = false;
        private Collider _collider;

        private void Awake()
        {
            _cropPlantedEventBinding = new EventBinding<OnPlanted>(CropPlanted);
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            EventBus<OnPlanted>.Subscribe(_cropPlantedEventBinding);
        }

        private void OnDisable()
        {
            EventBus<OnPlanted>.Unsubscribe(_cropPlantedEventBinding);
        }

        private void CropPlanted()
        {
            if (_isAlreadyGrown) return;
            
            _collider.enabled = false;

            growthStages.ForEach(x => x.SetActive(false));
            growthStages[0].SetActive(true);
            StartCoroutine(HandleGrowth());
        }

        private IEnumerator HandleGrowth()
        {
            _isAlreadyGrown = true;
            for (int i = 1; i < growthStages.Count; i++)
            {
                yield return new WaitForSeconds(_setting.growthTime);
                growthStages[i - 1].SetActive(false);
                growthStages[i].SetActive(true);
            }
            
            _isReadyForHarvest = true;
            _collider.enabled = true;
        }
        
        public bool IsReadyForHarvest()
        {
            return _isReadyForHarvest;
        }
    }
}