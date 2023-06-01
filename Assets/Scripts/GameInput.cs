using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractDifferentAction;

    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Interact.performed += Interact_Performed;
        playerInputAction.Player.InteractDifferent.performed += InteractDifferent_Performed;
    }

    private void InteractDifferent_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractDifferentAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized()
    {
        var inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
