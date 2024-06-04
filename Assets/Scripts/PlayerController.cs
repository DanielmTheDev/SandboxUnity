using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 _moveInput;
    private CharacterController _characterController;

    private void Awake()
    {
        Debug.Log("Awake");
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        var move = new Vector3(_moveInput.x, 0, _moveInput.y) * (moveSpeed * Time.deltaTime);
        _characterController.Move(move);
    }
}