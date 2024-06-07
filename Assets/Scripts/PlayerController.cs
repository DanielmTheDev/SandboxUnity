using Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Camera playerCamera;
    private Vector2 _moveInput;
    private Vector2 _mousePosition;
    private CharacterController _characterController;

    private Vector3 _cameraOffset;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraOffset = playerCamera!.transform.transform.position - transform.position;
    }

    public void Move(InputAction.CallbackContext context) => _moveInput = context.ReadValue<Vector2>();

    public void Look(InputAction.CallbackContext context) => _mousePosition = context.ReadValue<Vector2>();

    private void Update()
    {
        MoveCharacter();
        RotateTowardsMouse();
        FollowCharacterWithCamera();
    }

    private void MoveCharacter()
    {
        var move = new Vector3(_moveInput.x, 0, _moveInput.y) * (moveSpeed * Time.deltaTime);
        _characterController.Move(move);
    }

    private void RotateTowardsMouse()
    {
        var ray = playerCamera.ScreenPointToRay(_mousePosition);

        if (Physics.Raycast(ray, out var hitInfo))
        {
            var direction = hitInfo.point - transform.position;
            direction.y = 0;

            if (!direction.IsMagnitudeNegligible())
            {
                var targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    private void FollowCharacterWithCamera()
    {
        var newCameraPosition = transform.position + _cameraOffset;
        newCameraPosition.y = playerCamera.transform.position.y;
        playerCamera.transform.position = newCameraPosition;
    }
}
