using UnityEngine;

namespace Accumulator
{
    public sealed class TrajectoryStepper
    {
        public delegate void StepHandler(Vector3 point);

        public void Step(Vector3 from, Vector3 to, float maxStepLength, StepHandler onStep)
        {
            var distance = Vector3.Distance(from, to);
            if (distance <= maxStepLength)
            {
                onStep(to);
                return;
            }

            maxStepLength = Mathf.Max(maxStepLength, 0.001f); // защита от деления на 0
            var steps = Mathf.Max(1, Mathf.CeilToInt(distance / maxStepLength));

            for (var i = 1; i <= steps; i++)
            {
                var t = (float)i / steps;
                onStep(Vector3.Lerp(from, to, t));
            }
        }
    }
}