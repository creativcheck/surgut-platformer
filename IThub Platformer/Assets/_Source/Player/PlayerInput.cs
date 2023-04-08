using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput
    {
        private readonly PlayerControls _controls;
        private Player _player;

        public PlayerInput(Player player)
        {
            _player = player;
            _controls = new PlayerControls();
            Bind();
        }

        private void Bind()
        {
            _controls.Player.Enable();

            _controls.Player.Move.started += OnMoveInput;
            _controls.Player.Move.canceled += OnMoveInput;
            _controls.Player.Jump.started += OnJumpImput;
            _controls.Player.Jump.canceled += OnJumpImput;
            _controls.Player.Fire.started += OnFireInput;
            _controls.Player.Fire.canceled += OnFireInput;
        }

        private void OnFireInput(InputAction.CallbackContext context)
        {
            _player.SetFire(context.ReadValueAsButton());
        }

        public void UnBind()
        {
            _controls.Player.Move.started -= OnMoveInput;
            _controls.Player.Move.canceled -= OnMoveInput;
            _controls.Player.Jump.started -= OnJumpImput;
            _controls.Player.Jump.canceled -= OnJumpImput;
            _controls.Player.Fire.started -= OnFireInput;
            _controls.Player.Fire.canceled -= OnFireInput;
        }

        private void OnJumpImput(InputAction.CallbackContext context)
        {
            _player.SetJump(context.ReadValueAsButton());
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();

            if (inputVector.x > 0)
            {

            }
            else if (inputVector.x < 0)
            {

            }

            _player.SetInput(inputVector);
        }
    }
}


