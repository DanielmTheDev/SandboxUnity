using Extensions;
using UnityEngine;

namespace Player
{
    public class PlayerInputHandler
    {
        private readonly float _moveSpeed;
        private readonly float _rotationSpeed;
        private readonly CharacterController _characterController;
        private readonly Camera _playerCamera;
        private readonly Transform _playerTransform;

        private Vector2 _moveInput;
        private Vector2 _mousePosition;

        public PlayerInputHandler(float moveSpeed, float rotationSpeed, CharacterController characterController, Camera playerCamera, Transform playerTransform)
        {
            _moveSpeed = moveSpeed;
            _rotationSpeed = rotationSpeed;
            _characterController = characterController;
            _playerCamera = playerCamera;
            _playerTransform = playerTransform;
        }

        public void SetMoveInput(Vector2 moveInput) => _moveInput = moveInput;

        public void SetMousePosition(Vector2 mousePosition) => _mousePosition = mousePosition;

        public void MoveCharacter()
        {
            var move = new Vector3(_moveInput.x, 0, _moveInput.y) * (_moveSpeed * Time.deltaTime);
            _characterController.Move(move);
        }

        public void RotateTowardsMouse()
        {
            var ray = _playerCamera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(ray, out var hitInfo))
            {
                var direction = hitInfo.point - _playerTransform.position;
                direction.y = 0;

                if (!direction.IsMagnitudeNegligible())
                {
                    var targetRotation = Quaternion.LookRotation(direction);
                    _playerTransform.rotation = Quaternion.Slerp(_playerTransform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
                }
            }
        }
    }
}