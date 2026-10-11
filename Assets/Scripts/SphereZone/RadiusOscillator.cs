using UnityEngine;

namespace SphereZone
{
    public sealed class RadiusOscillator
    {
        private readonly IRadiusOscillatorSettings _settings;

        public RadiusOscillator(IRadiusOscillatorSettings settings)
        {
            _settings = settings;
        }

        public float Evaluate(float time)
        {
            var phase = Mathf.Repeat(time * _settings.Frequency, 1f);
            var curveValue = _settings.Curve.Evaluate(phase);
            return Mathf.Max(0f, _settings.BaseRadius + _settings.Amplitude * curveValue);
        }
    }
}