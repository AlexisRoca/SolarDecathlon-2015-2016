using UnityEngine;

namespace Assets.Scripts.Interaction
{
    [RequireComponent(typeof(VRActor))]
    [RequireComponent(typeof(Animator))]
    public class DoorBehaviour : MonoBehaviour
    {
        private bool _isOpen;
        private Animator _animator;
        private Renderer _renderer;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _renderer = GetComponentInChildren<Renderer>();
        }

        protected void OnMVRWandEnter(VRSelection iSelection)
        {
            _renderer.material.color = Color.green;
        }

        protected void OnMVRWandExit(VRSelection iSelection)
        {
            _renderer.material.color = Color.white;
        }

        protected void OnMVRWandButtonPressed(VRSelection iSelection)
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                return;

            _isOpen = !_isOpen;
            _animator.SetBool("IsOpen", _isOpen);
        }
    }
}
