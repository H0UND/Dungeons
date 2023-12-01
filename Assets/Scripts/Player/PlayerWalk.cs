using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _moveInput;
    private bool _IsCanMove = true;
    private float _turmSmoothVelocity;
    private float _turmSmoothTime = 0.1f;

    [SerializeField]
    private float _walkSpeed = 1f;

    public bool IsMoving;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void SetActions(PlayerInput.PlayerActions actions)
    {
        actions.Move.performed += MoveInput;
        actions.Move.canceled += MoveInput;
    }

    private void MoveInput(InputAction.CallbackContext context)
    {
        var vector = context.ReadValue<Vector2>();
        _moveInput = new Vector3(vector.x, 0, vector.y);
    }

    private void Update()
    {
        if (!_IsCanMove) return;

        _characterController.Move(_moveInput * Time.deltaTime * _walkSpeed);

        if (_moveInput != Vector3.zero)
        {
            IsMoving = true;
            var targetAngle = Mathf.Atan2(_moveInput.x, _moveInput.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turmSmoothVelocity, _turmSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else
        {
            IsMoving = false;
        }
    }
}