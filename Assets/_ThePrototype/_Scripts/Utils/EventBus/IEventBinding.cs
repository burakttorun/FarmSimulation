using System;

namespace BasicArchitecturalStructure
{
    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }

    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        private Action<T> _onEvent = _ => { };
        private Action _onEventNoArgs = () => { };

        Action<T> IEventBinding<T>.OnEvent
        {
            get => _onEvent;
            set => _onEvent = value;
        }

        Action IEventBinding<T>.OnEventNoArgs
        {
            get => _onEventNoArgs;
            set => _onEventNoArgs = value;
        }

        public EventBinding(Action onEventNoArgs) => _onEventNoArgs = onEventNoArgs;

        public EventBinding(Action<T> onEvent) => _onEvent = onEvent;

        public void Add(Action @event) => _onEventNoArgs += @event;
        public void Add(Action<T> @event) => _onEvent += @event;

        public void Remove(Action @event) => _onEventNoArgs -= @event;
        public void Remove(Action<T> @event) => _onEvent -= @event;
    }
}