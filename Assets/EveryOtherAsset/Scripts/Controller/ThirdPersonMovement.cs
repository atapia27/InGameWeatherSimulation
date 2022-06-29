using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    public CharacterStats myPlayerStats;
    
    public float walkSpeed = 3;
    public float runSpeed = 2;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
   
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public Vector3 velocity;









    //JUMP VARIABLES
    float gravity = -9.8f;
    float jumpSpeed = 8f ;
    float jumpAbortSpeed = 1f;








    //Turn animators into int Hashes
    int isWalkingHash = Animator.StringToHash("isWalking");
    int isRunningHash = Animator.StringToHash("isRunning");
    int isJumpingHash = Animator.StringToHash("isJumping");
    int isAttackingHash = Animator.StringToHash("isAttacking");
    int isRollingHash = Animator.StringToHash("isRolling");

    public bool readyToJump = false;










    //ATTACK VARIABLES
    public float attackSpeed = .1f;
    public float attackCooldown = 0f;
    public float attackDelay = .2f;


    //ROLL VARIABLES
    public float rollCooldown = 0;
    public float rollCooldownSpeed = .5f;


    private void Start()
    {
        myPlayerStats = GetComponent<CharacterStats>();
    }


    // Update is called once per frame
    void Update()
    {
        //Sets the coodldown for the attack
        attackCooldown -= Time.deltaTime;
        rollCooldown -= Time.deltaTime;
        



        //OTHER INPUT VARIABLES
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        



        //CHECK STATE OF ANIMATION VARIABLES
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isAttacking = animator.GetBool(isAttackingHash);
        bool isRolling = animator.GetBool(isRollingHash);




        //CHECK STATE OF INPUT VARIABLES 
        bool forwardPressed = direction.magnitude > 0.1f;
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetButton("Jump");
        bool attackPressed = Input.GetMouseButtonDown(0);
        bool rollPressed = Input.GetMouseButtonDown(1);



        //CHECK IF GROUNDED
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);








        //ROLLING
        if(rollCooldown <= 0)
        {
            if(rollPressed && isGrounded && isRunning)
            {
                animator.SetBool(isRollingHash, true);
                rollCooldown = rollCooldownSpeed;
                myPlayerStats.isInvincible = true;
            }
        }
        else
        {
            animator.SetBool(isRollingHash, false);

        }

        //THE NUMBER REPRESENTS THE AMOUTN OF IMMUNITY TIME
        if (rollCooldown <= rollCooldownSpeed - .35)
        {
            myPlayerStats.isInvincible = false;
        }




        //ATTACKING

        if (attackCooldown <= 0)
        {
            if (attackPressed && isGrounded)
            {
                animator.SetBool(isAttackingHash, true);
                attackCooldown = attackSpeed;
            }

        }
        else
        {
            animator.SetBool(isAttackingHash, false);

        }













        //WALKING & RUNNING          

        //forward AND run is pressed
        if ((forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);

            doMovement(direction, runSpeed);

        }

        //forward is pressed
        //isWalking = TRUE
        if (forwardPressed)
        {
            //Set walking animation 
            animator.SetBool(isWalkingHash, true);

            doMovement(direction, walkSpeed);

        }


        //forward is not pressed
        //isWalking = False
        if (!forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);

        }


        //stops walking OR running
        if ((!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }








        //JUMPING
        if(!jumpPressed && isGrounded)
        {
            animator.SetBool(isJumpingHash, false);
            readyToJump = true;

        }

        if (isGrounded)
        {
            // When grounded we apply a slight negative vertical speed to make player "stick" to the ground.
            velocity.y = -2f;
        }

        if (jumpPressed && isGrounded && readyToJump)
        {
            animator.SetBool(isJumpingHash, true);
            //velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
            velocity.y = jumpSpeed;
        }
        
        if(!jumpPressed && velocity.y > 0)
        {

            velocity.y -= jumpAbortSpeed; 

        }

        if(Mathf.Approximately(velocity.y, 0f))
        {
            velocity.y = 0f;
        }


        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }


        controller.Move(velocity * Time.deltaTime);







    }

    void doMovement(Vector3 direction, float speed)
    {
        //IN CHARGE OF ROTATION FOR PLAYER
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        //IN CHARGE OF MOVEMENT FOR THE PLAYER
        Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }








    IEnumerator turnOffAttack()
    {

        yield return new WaitForSeconds(attackDelay);

        if (animator.GetBool(isAttackingHash) == true)
        {
            animator.SetBool(isAttackingHash, false);
        }


    }

}
