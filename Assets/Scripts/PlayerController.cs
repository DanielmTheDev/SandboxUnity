using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private CharacterController characterController;

    private void Awake()
    {
        Debug.Log("Awake");
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue context)
    {
        Debug.Log("Move");
    }

    public void PlayerJumpControl (InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
    }

    private void OnHit()
    {
        Debug.Log("Hit");
    }
}