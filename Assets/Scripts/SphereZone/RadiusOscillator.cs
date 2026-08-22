using UnityEngine;

namespace SphereZone
{
    public sealed class RadiusOscillator
    {
        private readonly Settings _settings;

        public RadiusOscillator(Settings settings)
        {
            _settings = settings;
        }

        public float Evaluate(float time)
        {
            var phase = Mathf.Repeat(time * _settings.frequency, 1f);
            var curveValue = _settings.curve.Evaluate(phase);
            return Mathf.Max(0f, _settings.baseRadius + _settings.amplitude * curveValue);
        }
    }
}