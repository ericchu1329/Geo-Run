/*
NAME: GPEJ
MEMBERS: Eric Chu, Jake Wong
COURSE: CPSC 254-01

FILE DESCRIPTION:
This file contains the PlayerController class, which contains methods that allow 
a user to control the player model and algorithms that manipulate the player's 
velocity.
*/
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats player_stats;

    // Three lane system
    private enum LanePosition {Left, Middle, Right}

    // Player Components
    private Animator player_animator;
    private CharacterController player_controller;

    // Lane Variables
    [SerializeField] private float lane_distance;
    private LanePosition current_lane_pos;
    private float target_lane_xpos;
    private float distance_to_lane;

    // Movement Variables
    [SerializeField] private float forward_speed;
    [SerializeField] private float horizontal_speed;
    [SerializeField] private float jump_force;
    [SerializeField] private float fall_multiplier;
    private float vertical_velocity;
    private float artificial_gravity;

    private Ailments current_ailment;

    private float burn_multiplier = 1f;
    private float chill_multiplier = 1f;
    private float grasp_multiplier = 1f;

    private void Awake()
    {
        player_controller = GetComponent<CharacterController>();
        player_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        current_lane_pos = LanePosition.Middle;
    }

    private void Update()
    {
        if (!GameStateManager.instance.IsLost())
        {
            GetCurrentAilment();
            player_stats.SetDistanceTraveled((int)transform.position.z);
            // User press Right
            if (Input.GetKeyDown(KeyCode.D) && !GameStateManager.instance.IsPaused())
            {
                if (current_lane_pos == LanePosition.Left)
                {
                    player_animator.SetTrigger("move_right");
                    current_lane_pos = LanePosition.Middle;
                    target_lane_xpos = 0f;
                    AudioManager.instance.PlayMoveSFX();
                }
                else if (current_lane_pos == LanePosition.Middle)
                {
                    player_animator.SetTrigger("move_right");
                    current_lane_pos = LanePosition.Right;
                    target_lane_xpos = lane_distance;
                    AudioManager.instance.PlayMoveSFX();
                }
            }
            // User press Left
            if (Input.GetKeyDown(KeyCode.A) && !GameStateManager.instance.IsPaused())
            {
                if (current_lane_pos == LanePosition.Right)
                {
                    player_animator.SetTrigger("move_left");
                    current_lane_pos = LanePosition.Middle;
                    target_lane_xpos = 0f;
                    AudioManager.instance.PlayMoveSFX();
                }
                else if (current_lane_pos == LanePosition.Middle)
                {
                    player_animator.SetTrigger("move_left");
                    current_lane_pos = LanePosition.Left;
                    target_lane_xpos = -lane_distance;
                    AudioManager.instance.PlayMoveSFX();
                }
            }

            // User press Jump 
            if (player_controller.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !GameStateManager.instance.IsPaused())
                {
                    vertical_velocity = jump_force;
                    AudioManager.instance.PlayJumpSFX();
                }
            }
            // Calculating Falling Physics
            else
            {
                artificial_gravity = Mathf.Lerp(fall_multiplier, fall_multiplier * 1.2f, Time.deltaTime);
                vertical_velocity -= jump_force * artificial_gravity * Time.deltaTime;
            }

            // Movement vector for move()
            Vector3 motion_vector = new Vector3(distance_to_lane - transform.position.x,
                vertical_velocity * grasp_multiplier * Time.deltaTime, forward_speed * chill_multiplier * burn_multiplier * Time.deltaTime);

            // The distance between the lane will update as we approach the target lane
            distance_to_lane = Mathf.Lerp(distance_to_lane, target_lane_xpos,
                horizontal_speed * chill_multiplier * burn_multiplier * Time.deltaTime);

            player_controller.Move(motion_vector);

            // Ends the game if the player has collided with terrain and stops moving forward.
            if (GameStateManager.instance.isRunning() && player_controller.velocity.magnitude <= 0f && !GameStateManager.instance.IsGracePeriod())
            {
                GameStateManager.instance.TransitionToLostState();
            }
        }
    }

    // Gets the current inflicted Ailment on the player
    private void GetCurrentAilment()
    {
        current_ailment = player_stats.GetCurrentAilment();

        if (current_ailment == Ailments.Burning)
        {
            chill_multiplier = grasp_multiplier = 1f;
            burn_multiplier = player_stats.GetBurnMultiplier();
        }
        else if (current_ailment == Ailments.Chilled)
        {
            burn_multiplier = grasp_multiplier = 1f;
            chill_multiplier = player_stats.GetChillMultiplier();
        } 
        else if (current_ailment == Ailments.Grasped)
        {
            burn_multiplier = chill_multiplier = 1f;
            grasp_multiplier = player_stats.GetGraspMultiplier();
        } 
        // return ailment multipliers back to normal if no ailment is inflicted on the player
        else
        {
            burn_multiplier = chill_multiplier = grasp_multiplier = 1f;
        }
    }
}
