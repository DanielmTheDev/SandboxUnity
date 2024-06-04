using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private Vector2 _moveInput;
    private Vector2 _mousePosition;
    private CharacterController _characterController;

    private Camera _mainCamera;

    private void Awake()
    {
        Debug.Log("Awake");
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }

    public void Move(InputAction.CallbackContext context) => _moveInput = context.ReadValue<Vector2>();

    public void Look(InputAction.CallbackContext context) => _mousePosition = context.ReadValue<Vector2>();

    private void Update()
    {
        MoveCharacter();
        RotateTowardsMouse();
    }

    private void MoveCharacter()
    {
        var move = new Vector3(_moveInput.x, 0, _moveInput.y) * (moveSpeed * Time.deltaTime);
        _characterController.Move(move);
    }

    private void RotateTowardsMouse()
    {
        // Convert the mouse position to a ray from the camera
        var ray = _mainCamera.ScreenPointToRay(_mousePosition);

        // Perform the raycast to determine the hit point in the world
        if (Physics.Raycast(ray, out var hitInfo))
        {
            var direction = hitInfo.point - transform.position;
            direction.y = 0; // Keep the direction strictly horizontal

            if (direction.sqrMagnitude > 0.01f)
            {
                var targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}