using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    Player inputActions;
    CharacterController characterController;
    [SerializeField]Transform playerCamera;
    public float jumpForce=20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new Player();
        inputActions.Keyboard.Enable();
        characterController = GetComponent<CharacterController>();

        inputActions.Keyboard.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        inputActions.Keyboard.Jump.performed -= Jump;

    }

    // Update is called once per frame
    void Update()
    {
       Vector2 movement = inputActions.Keyboard.Move.ReadValue<Vector2>();
        Debug.Log(movement);
        characterController.Move(movement);

        Vector2 lookDirection= inputActions.Keyboard.Look.ReadValue<Vector2>();
        gameObject.transform.Rotate(new Vector3(lookDirection.y,0,0));
        playerCamera.Rotate(new Vector3(0,-lookDirection.x,0));
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        characterController.Move(characterController.velocity + new Vector3(0, jumpForce, 0));
    }
}
