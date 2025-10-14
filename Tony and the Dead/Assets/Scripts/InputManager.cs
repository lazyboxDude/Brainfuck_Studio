using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    //Referencing the PlayerInput script
    public PlayerInput playerInput;
    //Refrencing a specific Action Map from the PlayerInput Script
    public PlayerInput.OnFootActions onFootActions;

    //Referencing the PlayerMotor Script
    private PlayerMotor motor;
    private PlayerLook look;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //create Instance of the PlayerInput and its Actions Maps
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFootActions.Jump.performed += ctx => motor.Jump();

        onFootActions.Crouch.performed += ctx => motor.Crouch();
        onFootActions.Sprint.performed += ctx => motor.Sprint();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from movement action
        motor.ProcessMove(onFootActions.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFootActions.Look.ReadValue<Vector2>());

    }
    private void OnEnable()
    {
        //Enable OnFoot Action Map
        onFootActions.Enable();

    }

    private void OnDisable()
    {
        //Disable OnFoot Action Map
        onFootActions.Disable();
    }
}
