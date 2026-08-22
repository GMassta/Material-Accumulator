using UnityEngine;

namespace SphereZone
{
    public interface IZoneView
    {
        public void SetPosition(Vector3 position);
        public void SetRadius(float radius);
    }
}