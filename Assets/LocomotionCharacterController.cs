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
    //[SerializeField] private Transform camera2; //Transform of the camera for shooting
    private float angleOffset = 0f; //Offset of the camera based on the character
    [SerializeField] private float turnSmooth = 0.25f; //Turning speed


    //InputSystem
    private float inputHor, inputVer = 0f; //Horizontal and Vertical Shifting
    private bool inputJump = false; //Jump

    //Animations
    private Animator anim; //Reference of the animation system
    private float animVelocity; //parameter for the intensity of walking/running

    //Jumps animations
    [SerializeField] private LayerMask everythingButPlayer; //every layers except for the player
    [SerializeField] private float distanceFromGround = 0.25f; //Which distance is considered falling
    private float capsuleScale; //character's height by default
    private CapsuleCollider capsule; //Reference to the capsule collider

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); //Controller cache
        anim = GetComponent<Animator>(); //Animator cache
        capsuleScale = controller.height; //Capsule height by default cache
        capsule = GetComponent<CapsuleCollider>(); //Capsule collider cache
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

    void ScaleCharacter(float fraction)
    {
        controller.height = capsuleScale * fraction; //Change for the % of the original height
        controller.center = new Vector3(0, controller.height * 0.5f, 0);

        capsule.height = capsuleScale * fraction; //Change for the % of the original height
        capsule.center = new Vector3(0, capsule.height * 0.5f, 0);
    }

    bool CheckIfGrounded()
    {
        if (controller.isGrounded)
        {
            return true; //already on the ground
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit, everythingButPlayer))
        {
            if (hit.distance < distanceFromGround)
            {
                return true; //ground is close
            }
            else
            {
                return false; //ground is far
            }
        }
        return false; //no ground under player
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the movement on the ground
        groundedPlayer = controller.isGrounded; //if the character is on the ground
        if (groundedPlayer)
        {
            if (controller.height != capsuleScale) //grounded yet capsule height is reduced
            {
                ScaleCharacter(1f); //return the capsule to its original height
            }

            playerVelocity = new Vector3(inputHor, 0, inputVer); //Moving left to right, up and down

            //Change direction
            if (camera.gameObject.activeInHierarchy)
            {
                angleOffset = camera.eulerAngles.y; //Save the degrees in Y axis of the camera
            }
            //else if(camera2.gameObject.activeInHierarchy)
            //{
            //    angleOffset = camera2.eulerAngles.y; //Save the degrees in Y axis of the camera
            //}
            
            moveAngle = (Mathf.Atan2(playerVelocity.x, playerVelocity.z) * Mathf.Rad2Deg) + angleOffset; //Movement angle
            lookAngle = Mathf.LerpAngle(transform.eulerAngles.y, moveAngle, turnSmooth); //Turn progressively toward desired angle
            animVelocity = playerVelocity.magnitude;

            if (playerVelocity.magnitude >= 0.1f) //Change angle only if we're moving
            {
                transform.rotation = Quaternion.Euler(0f, lookAngle, 0f); //Turn on Y axis
                Vector3 forward = Vector3.forward * playerVelocity.magnitude; //Find the front based on the orientation
                playerVelocity = Quaternion.Euler(0f, moveAngle, 0f) * forward; //Transpose the force with angle movement
            }
            
            playerVelocity *= speed; //Increase velocity of the character

            //Jump
            if (inputJump)
            {
                inputJump = false;
                anim.SetTrigger("Jump"); //play jump animation
                playerVelocity.y = jumpIntensity; //Force up
                ScaleCharacter(0.75f); //in the air, the character height is 75% its original height
            }
        }

        //Animations
        anim.SetFloat("Magnitude", animVelocity); //Speed control of the animation walking and running
        anim.SetBool("Grounded", CheckIfGrounded());
        anim.SetFloat("Y", playerVelocity.y);
        if (playerVelocity.y < 0 && controller.height == capsuleScale && !CheckIfGrounded())
        {
            ScaleCharacter(0.75f); //in the air, the character height is 75% its original height
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
