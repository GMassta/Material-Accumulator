using UnityEngine;

namespace SphereZone
{
    public interface IZoneInput
    {
        public bool IsAccumulating { get; }
        public Vector2 GetMoveDirection();
    }
}