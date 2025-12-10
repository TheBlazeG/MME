using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    Player inputActions;
    CharacterController characterController;
    [SerializeField]Transform playerCamera;
    float sensibility = 2;
    float pitch = 0f;
    public float jumpForce=20;
    CinemachineVolumeSettings volume;
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

        Look( inputActions.Keyboard.Look.ReadValue<Vector2>()*Time.deltaTime);
        
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        characterController.Move(characterController.velocity + new Vector3(0, jumpForce, 0));
    }

    void Look(Vector2 lookDirection) 
    {
    
        gameObject.transform.Rotate(Vector3.up*lookDirection.x);
        pitch -= lookDirection.y*sensibility;
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        
        playerCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
