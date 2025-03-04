using UnityEngine;

namespace BasicArchitecturalStructure
{
    public interface IEvent {}

    public struct OnClick : IEvent
    {
        public Vector3 clickPosition;
    }
    public struct OnExit : IEvent
    {
        public Vector3 lastPosition;
    }
}
