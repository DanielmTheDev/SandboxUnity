using Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IHittable
    {
        public Camera playerCamera;
        public float moveSpeed = 5f;
        public float rotationSpeed = 10f;

        private CharacterController _characterController;
        private GravityApplier _gravityApplier;
        private PlayerInputHandler _inputHandler;
        private Animator _animator;
        private InputActionMap _playerInput;

        private Vector3 _cameraOffset;
        private bool _isDead;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _gravityApplier = new(_characterController);
            _inputHandler = new(moveSpeed, rotationSpeed, _characterController, playerCamera, transform);
            _cameraOffset = playerCamera!.transform.transform.position - transform.position;
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        }

        public void Move(InputAction.CallbackContext context) => _inputHandler.SetMoveInput(context.ReadValue<Vector2>());

        public void Look(InputAction.CallbackContext context) => _inputHandler.SetMousePosition(context.ReadValue<Vector2>());

        public void OnHit()
        {
            _playerInput.Disable();
            _animator.SetTrigger(AnimatorExtensions.HitTrigger);
        }

        private void Update()
        {
            _inputHandler.MoveCharacter();
            _inputHandler.RotateTowardsMouse();
            FollowCharacterWithCamera();
            _gravityApplier.ApplyGravity();
        }

        private void FollowCharacterWithCamera()
        {
            var newCameraPosition = transform.position + _cameraOffset;
            newCameraPosition.y = playerCamera.transform.position.y;
            playerCamera.transform.position = newCameraPosition;
        }
    }
}