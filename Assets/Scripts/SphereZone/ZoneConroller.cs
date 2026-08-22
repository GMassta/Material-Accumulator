using Accumulator;
using Grid;
using UnityEngine;

namespace SphereZone
{
    public sealed class ZoneController
    {
        private readonly AccumulationZone _zone;
        private readonly RadiusOscillator _radiusOscillator;
        private readonly TrajectoryStepper _stepper;
        private readonly MaterialAccumulator _accumulator;
        private readonly IZoneInput _input;
        private readonly HeightMapData _heightMap;

        private float _time;
        private float _stepAccumulationDelta;

        private Settings _settings;

        // Кэшированный делегат: создаётся один раз в конструкторе
        private readonly TrajectoryStepper.StepHandler _onTrajectoryStep;

        public ZoneController(
            AccumulationZone zone,
            RadiusOscillator radiusOscillator,
            TrajectoryStepper stepper,
            MaterialAccumulator accumulator,
            IZoneInput input,
            HeightMapData heightMap,
            Settings settings)
        {
            _settings = settings;
            _zone = zone;
            _radiusOscillator = radiusOscillator;
            _stepper = stepper;
            _accumulator = accumulator;
            _input = input;
            _heightMap = heightMap;

            _onTrajectoryStep = OnTrajectoryStep;

            _zone.SetPosition(new Vector3(heightMap.Width * 0.5f, 0f, heightMap.Depth * 0.5f));
            _zone.SetRadius(radiusOscillator.Evaluate(0f));
        }

        public void Tick(float deltaTime)
        {
            _time += deltaTime;
            _zone.SetRadius(_radiusOscillator.Evaluate(_time));

            var previousPosition = _zone.Position;
            var newPosition = Move(previousPosition, deltaTime);

            if (_input.IsAccumulating)
            {
                //Не перемещаю зону сразу, а по шагам между начальной и конечной точкой.
                _stepAccumulationDelta = _settings.accumulationRate * deltaTime;
                
                // Шаг не длиннее половины текущего радиуса
                var maxStep = Mathf.Max(_zone.CurrentRadius * 0.5f, 0.01f);
                _stepper.Step(previousPosition, newPosition, maxStep, _onTrajectoryStep);
            }
            else
            {
                _zone.SetPosition(newPosition);
            }
        }

        private Vector3 Move(Vector3 current, float deltaTime)
        {
            var direction = _input.GetMoveDirection();
            var delta = new Vector3(direction.x, 0f, direction.y) * (_settings.zoneMove * deltaTime);
            var next = current + delta;

            next.x = Mathf.Clamp(next.x, 0f, _heightMap.Width);
            next.z = Mathf.Clamp(next.z, 0f, _heightMap.Depth);
            return next;
        }

        // Вызывается TrajectoryStepper на каждом шаге пути. Двигает зону в
        // промежуточную точку и сразу накапливает материал в ней
        private void OnTrajectoryStep(Vector3 point)
        {
            _zone.SetPosition(point);
            _accumulator.Accumulate(_zone, _stepAccumulationDelta);
        }
    }
}