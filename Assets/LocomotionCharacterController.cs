using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocomotionCharacterController : MonoBehaviour
{

    private CharacterController controller; //Reference to our component

    //Movement
    private Vector3 playerVelocity; //where we want our character to be in the next Update
    [SerializeField] private float speed = 6f; //Speed multiplicator

    //Jump and landing
    private bool groundedPlayer;
    [SerializeField] private float jumpIntensity = 8.0f; //Jump intensity
    [SerializeField] private float gravity = 20.0f; //Gravity

    //Rotation
    private float moveAngle = 0f; //Movement angle of the character
    private float lookAngle = 0f; //Orientation angle of the character
    [SerializeField] private Transform camera; //Transform of the camera
    private float angleOffset = 0f; //Offset of the camera based on the character
    [SerializeField] private float turnSmooth = 0.25f; //Turning speed


    //InputSystem
    private float inputHor, inputVer = 0f; //Horizontal and Vertical Shifting
    private bool inputJump = false; //Jump

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); //Controller cache
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();
        inputHor = v.x;
        inputVer = v.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        inputJump = context.performed;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the movement on the ground
        groundedPlayer = controller.isGrounded; //if the character is on the ground
        if (groundedPlayer)
        {
            playerVelocity = new Vector3(inputHor, 0, inputVer); //Moving left to right, up and down

            //Change direction
            angleOffset = camera.eulerAngles.y; //Save the degrees in Y axis of the camera
            moveAngle = (Mathf.Atan2(playerVelocity.x, playerVelocity.z) * Mathf.Rad2Deg) + angleOffset; //Movement angle
            lookAngle = Mathf.LerpAngle(transform.eulerAngles.y, moveAngle, turnSmooth); //Turn progressively toward desired angle

            if (playerVelocity.magnitude >= 0.1f) //Change angle only if we're moving
            {
                transform.rotation = Quaternion.Euler(0f, lookAngle, 0f); //Turn on Y axis
                Vector3 forward = Vector3.forward * playerVelocity.magnitude; //Find the front based on the orientation
                playerVelocity = Quaternion.Euler(0f, moveAngle, 0f) * forward; //Transpose the force with angle movement
            }
            
            playerVelocity *= speed; //Increase velocity of the character

            if (inputJump)
            {
                inputJump = false;
                playerVelocity.y = jumpIntensity; //Force up
            }
        }

        //Apply gravity
        playerVelocity.y -= gravity * Time.deltaTime;

        //Move the controller
        controller.Move(playerVelocity * Time.deltaTime); //Where to bring the character in the next update
    }
}

// Reference
// 1- Copied from the Locomotion_CharacterController script from class project Integration de personnage
// https://youtu.be/AmBlouhDvr8
// https://youtu.be/3EAS9OkvJIk

//current :https://youtu.be/3EAS9OkvJIk?t=440