using UnityEngine;
using System.Collections;

public class MovesoundConnector : MonoBehaviour {

    [SerializeField]    private float m_StepInterval;
    [SerializeField]    private float m_WalkSpeed;
    [SerializeField]    private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.

    private CharacterController m_CharacterController;
    private AudioSource m_AudioSource;
    private Vector2 m_Input;
    private float m_StepCycle;
    private float m_NextStep;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProgressStepCycle(m_WalkSpeed);
        Debug.Log("pgtrkpo");
    }

    private void ProgressStepCycle(float speed)
    {
        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed *  1f )) * Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio();
    }

    private void PlayFootStepAudio()
    {
        if (!m_CharacterController.isGrounded)
        {
            return;
        }
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }

    public void SetFootstepSounds(AudioClip[] a_NewFootstepSounds)
    {
        m_FootstepSounds = a_NewFootstepSounds;
        Debug.Log("piou");
    }
}