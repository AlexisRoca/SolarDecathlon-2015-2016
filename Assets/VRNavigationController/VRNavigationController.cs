/* VRNavigationController
 * MiddleVR
 * (c) MiddleVR
 */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class VRNavigationController : MonoBehaviour
{

    public string MovementDirectionNodeName = "HandNode";
    public float HeadProtectionRadius = 0.2f;
    public float WalkSpeed = 1.0f;
    public float RotationSpeed = 100.0f;
    public bool Straffe = false;
    public float GravityMultiplier = 10.0f;

    private GameObject m_Head;
    private GameObject m_MovementDirectionNode;
    private CharacterController m_CharacterController;

    private CollisionFlags m_CollisionFlags;
    private Vector2 m_Input;
    private Vector3 m_Movement = Vector3.zero;

    private void Start()
    {
        m_CharacterController = this.GetComponent<CharacterController>();
        m_CharacterController.height = HeadProtectionRadius + (m_Head.transform.position.y - this.transform.position.y);

        Vector3 center = this.transform.InverseTransformVector(m_Head.transform.position - this.transform.position);
        center.y = m_CharacterController.height / 2.0f;

        m_CharacterController.center = center;
    }


    private void Update()
    {
        if (m_Head == null)
        {
            m_Head = GameObject.Find("HeadNode");
            return;
        }

        m_CharacterController.height = HeadProtectionRadius + (m_Head.transform.position.y - this.transform.position.y);

        Vector3 center = this.transform.InverseTransformVector(m_Head.transform.position - this.transform.position);
        center.y = m_CharacterController.height / 2.0f;

        Vector3 headMovement = center - m_CharacterController.center;
        headMovement.y = 0.0f;

        m_CharacterController.center = center;

        this.transform.position -= headMovement;
        m_CharacterController.Move(headMovement);


        // Follow ground
        if (m_CharacterController.isGrounded)
        {
            m_Movement.y = 0f;
        }
        else
        {
            m_Movement.y = 0f;
        }

    }


    private void FixedUpdate()
    {
        float speed;
        GetInput(out speed);

        if (m_MovementDirectionNode == null)
        {
            m_MovementDirectionNode = GameObject.Find(MovementDirectionNodeName);
            return;
        }

        Vector3 movementForward = m_MovementDirectionNode.transform.forward;
        Vector3 movementRight = m_MovementDirectionNode.transform.right;

        // No fly for the moment
        movementForward.y = 0.0f;
        movementForward.Normalize();
        movementRight.y = 0.0f;
        movementRight.Normalize();

        Vector3 desiredMove;
        if (Straffe)
        {
            desiredMove = movementForward * m_Input.y + movementRight * m_Input.x;
        }
        else
        {
            desiredMove = movementForward * m_Input.y;
            this.transform.RotateAround(m_Head.transform.position, Vector3.up, m_Input.x * RotationSpeed * Time.fixedDeltaTime);
        }

        m_Movement.x = desiredMove.x * speed;
        m_Movement.z = desiredMove.z * speed;


        if (!m_CharacterController.isGrounded)
        {
            m_Movement += Physics.gravity * GravityMultiplier * Time.fixedDeltaTime;
        }

        m_CollisionFlags = m_CharacterController.Move(m_Movement * Time.fixedDeltaTime);

    }

    private void GetInput(out float speed)
    {
        // Read input
        float horizontal = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
        float vertical = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();

        speed = WalkSpeed;
        m_Input = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (m_CollisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }

        body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
}
