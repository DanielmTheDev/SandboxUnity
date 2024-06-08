using UnityEngine;

internal class GravityApplier
{
    private readonly CharacterController _characterController;
    private const float GRAVITY = -9.81f;
    private Vector3 _velocity;

    public GravityApplier(CharacterController characterController)
        => _characterController = characterController;

    public void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
            return;
        }

        _velocity.y += GRAVITY * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}