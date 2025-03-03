using System.Collections.Generic;
using UnityEngine;

namespace BasicArchitecturalStructure
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> _bindings = new HashSet<IEventBinding<T>>();

        public static void Subscribe(EventBinding<T> binding) => _bindings.Add(binding);
        public static void Unsubscribe(EventBinding<T> binding) => _bindings.Remove(binding);

        public static void Publish(T eventToPublish)
        {
            foreach (IEventBinding<T> binding in _bindings)
            {
                binding.OnEvent?.Invoke(eventToPublish);
                binding.OnEventNoArgs?.Invoke();
            }
        }

        static void Clear()
        {
            _bindings.Clear();
            Debug.Log($"Clearing {typeof(T).Name} bindings. ");
        }
    }
}