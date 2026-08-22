using UnityEngine;
using VContainer;

namespace SphereZone
{
    public sealed class KeyboardZoneInput : IZoneInput
    {
        [Inject] private InputSystem_Actions _input;

        public KeyboardZoneInput(InputSystem_Actions input)
        {
            _input = input;
            _input.Enable();
        }
        
        public Vector2 GetMoveDirection()
        {
            var moveVector = _input.Control.Move.ReadValue<Vector2>();
            return moveVector.sqrMagnitude > 1f ? moveVector.normalized : moveVector;
        }

        public bool IsAccumulating => _input.Control.Accumulate.inProgress;
    }
}