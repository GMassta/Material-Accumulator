using UnityEngine;

namespace SphereZone
{
    public sealed class ZoneView : MonoBehaviour, IZoneView
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = new Vector3(position.x, 0f, position.z);
        }

        public void SetRadius(float radius)
        {
            var diameter = radius * 2f;
            transform.localScale = new Vector3(diameter, diameter, diameter);
        }
    }
}