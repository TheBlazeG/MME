using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{
    Player inputActions;
    CharacterController characterController;
    [SerializeField] float speed = 2.0f;
    [SerializeField]Transform playerCamera;
    public float sensibility = 2;
    float pitch = 0f;
    public float jumpForce=20;
    public float gravity = 9.81f;
    Vector3 velocity;
    [SerializeField]CinemachineVolumeSettings volume;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new Player();
        inputActions.Keyboard.Enable();
        characterController = GetComponent<CharacterController>();

        inputActions.Keyboard.Jump.performed += Jump;
        inputActions.Keyboard.Switch.performed += SwitchPerspective;
        Cursor.lockState= CursorLockMode.Locked;
        Events.SwitchReality += FlipVolume;
        
    }

    private void OnDisable()
    {
        inputActions.Keyboard.Jump.performed -= Jump;
        Events.SwitchReality -= FlipVolume;

    }

    // Update is called once per frame
    void Update()
    {
        
       Vector3 movementInput = new Vector3( inputActions.Keyboard.Move.ReadValue<Vector2>().x,0, inputActions.Keyboard.Move.ReadValue<Vector2>().y)*Time.deltaTime;
        Vector3 movement = (transform.forward * movementInput.z + transform.right * movementInput.x);
        Debug.Log(movement);
        characterController.Move(movement*speed);

        Look( inputActions.Keyboard.Look.ReadValue<Vector2>()*Time.deltaTime);
        if (!characterController.isGrounded)
        {

            velocity += Vector3.down*gravity*Time.deltaTime;
            characterController.Move(velocity);
        }
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        if (characterController.isGrounded)
        velocity.y= jumpForce*.1f;
    }

    void FlipVolume()
    {
        if (volume.enabled==true)
        {
            volume.enabled = false;
        }
        else
        {
        volume.enabled = true;
            
        }
    }

    void SwitchPerspective(InputAction.CallbackContext ctx)
    {
        Events.instance.CallRealityEvent();
    }

    void Look(Vector2 lookDirection) 
    {
    
        gameObject.transform.Rotate(Vector3.up*lookDirection.x);
        pitch -= lookDirection.y*sensibility;
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        
        playerCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
