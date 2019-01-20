using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles simple steering of the wheelchair via keyboard or VR controllers
/// Currently only reads input from left VR controller
public class WheelchairMovement : MonoBehaviour {

    // Parameters, editable in the component view
    public float m_MovementAcceleration = 1f; // rate of gaining movement speed
    public float m_MaxMovementSpeed = 2f; // terminal velocity of wheelchair
    public float m_MovementDecceleration = 2.5f; // rate of losing movement speed on its own
    public float m_RotationAcceleration = 10f; // rate of gaining turning speed
    public float m_MaxRotationSpeed = 20f; // maximum turning rate of wheelchair
    public float m_RotationDeccelration = 25f; // rate of losing turning speed on its own

    ///public GameObject m_Remote; // remote for steering the wheelchair

    // Variables for holding user input
    private float m_MovementInput;
    private float m_RotationInput;
    private float m_BreakInput;

    // Variables for holding speed
    private float m_MovementSpeed = 0f; // rate of forward/backwards motion
    private float m_RotationSpeed = 0f; // rate of left/right rotation

    private Rigidbody rb; // Unity's physics component

    // Awake is run once, when game starts
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get user input
        ///m_MovementInput = Input.GetAxis("Vertical"); // W, S; moving joystick up or down
        ///m_RotationInput = Input.GetAxis("Horizontal"); // A, D; moving joystick left or right
        m_MovementInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y + OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;
        m_RotationInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x + OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

        m_BreakInput = Input.GetAxis("Break"); // space
    }

    // FixedUpdate is called once per frame to calculate the physics
    void FixedUpdate()
    {
        Move();
        Turn();
    }

    // Forward/backwards motion
    private void Move()
    {
        m_MovementSpeed = m_MovementSpeed + m_MovementInput * m_MovementAcceleration * Time.fixedDeltaTime; // when forward/backward motion button is pressed speed is increased at a rate controlled by acceleration variable
        if (m_MovementSpeed > m_MaxMovementSpeed) m_MovementSpeed = m_MaxMovementSpeed; // caps the forward speed at max speed
        if (m_MovementSpeed < -m_MaxMovementSpeed) m_MovementSpeed = -m_MaxMovementSpeed; // caps the backward speed at max speed

        // when forward/backward motion button is not pressed speed is decreased at rate controlled by decceleration variable
        if (m_MovementInput == 0)
        {
            // speed can be forward or backward
            if (m_MovementSpeed > 0)
            {
                m_MovementSpeed = m_MovementSpeed - m_MovementDecceleration * Time.fixedDeltaTime;
                if (m_MovementSpeed < 0) m_MovementSpeed = 0; // caps decreasing speed on 0
            }
            if (m_MovementSpeed < 0)
            {
                m_MovementSpeed = m_MovementSpeed + m_MovementDecceleration * Time.fixedDeltaTime;
                if (m_MovementSpeed > 0) m_MovementSpeed = 0; // caps decreasing speed on 0
            }
        }

        // break button stops motion faster
        if (m_MovementInput == 0)
        {
            // speed can be forward or backward
            if (m_MovementSpeed > 0)
            {
                m_MovementSpeed = m_MovementSpeed - 5 * m_MovementDecceleration * Time.fixedDeltaTime;
                if (m_MovementSpeed < 0) m_MovementSpeed = 0; // caps decreasing speed on 0
            }
            if (m_MovementSpeed < 0)
            {
                m_MovementSpeed = m_MovementSpeed + 5 * m_MovementDecceleration * Time.fixedDeltaTime;
                if (m_MovementSpeed > 0) m_MovementSpeed = 0; // caps decreasing speed on 0
            }
        }

        Vector3 movement; // vector of forward/backwards motion
        movement = transform.forward * m_MovementSpeed * Time.fixedDeltaTime; // Time.deltaTime allows for parameter to be speed in meters per second instead of meters per frame

        rb.MovePosition(rb.position + movement); // transforms object's position
    }

    // Left/right turning
    private void Turn()
    {
        m_RotationSpeed = m_RotationSpeed + m_RotationInput * m_RotationAcceleration * Time.fixedDeltaTime; // when rotation button is pressed rotation speed is increased at a rate controlled by acceleration variable
        if (m_RotationSpeed > m_MaxRotationSpeed) m_RotationSpeed = m_MaxRotationSpeed; // caps the speed at max speed

        // when rotation button is not pressed speed is decreased at rate controlled by decceleration variable
        if (m_RotationInput == 0)
        {
            // speed can be left or right
            if (m_RotationSpeed > 0)
            {
                m_RotationSpeed = m_RotationSpeed - m_RotationDeccelration * Time.fixedDeltaTime;
                if (m_RotationSpeed < 0) m_RotationSpeed = 0; // caps decreasing speed on 0
            }
            if (m_RotationSpeed < 0)
            {
                m_RotationSpeed = m_RotationSpeed + m_RotationDeccelration * Time.fixedDeltaTime;
                if (m_RotationSpeed > 0) m_RotationSpeed = 0; // caps decreasing speed on 0
            }
        }

        // break button stops motion faster
        if (m_RotationInput == 0)
        {
            // speed can be left or right
            if (m_RotationSpeed > 0)
            {
                m_RotationSpeed = m_RotationSpeed - 5 * m_RotationDeccelration * Time.fixedDeltaTime;
                if (m_RotationSpeed < 0) m_RotationSpeed = 0; // caps decreasing speed on 0
            }
            if (m_RotationSpeed < 0)
            {
                m_RotationSpeed = m_RotationSpeed + 5 * m_RotationDeccelration * Time.fixedDeltaTime;
                if (m_RotationSpeed > 0) m_RotationSpeed = 0; // caps decreasing speed on 0
            }
        }

        float turn = m_RotationSpeed * Time.fixedDeltaTime; // Time.deltaTime allows for parameter to be speed in meters per second instead of meters per frame

        Quaternion rotation; // quaternion of left/right rotation
        rotation = Quaternion.Euler(0f, turn, 0f); // we can only turn left/right, rotating around the Y axis

        rb.MoveRotation(rb.rotation * rotation); // transforms objects' rotation
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Floor")
        {
            ///m_MovementSpeed = 0;
            ///m_RotationSpeed = 0;
        }
    }
}
