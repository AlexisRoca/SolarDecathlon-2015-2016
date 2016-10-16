using UnityEngine;

namespace Assets.Scripts.Interaction
{
    [RequireComponent(typeof(VRActor))]
    public class LightSwitchBehaviour : MonoBehaviour
    {
        public Light Light;

        private const string ButtonName = "Model/Button";
        private Renderer _buttonRenderer;

        void Awake()
        {
            GameObject button = transform.Find(ButtonName).gameObject;
            _buttonRenderer = button.GetComponent<Renderer>();
        }

        protected void OnMVRWandEnter(VRSelection iSelection)
        {
            _buttonRenderer.material.color = Color.green;
        }

        protected void OnMVRWandExit(VRSelection iSelection)
        {
            _buttonRenderer.material.color = Color.white;
        }

        protected void OnMVRWandButtonPressed(VRSelection iSelection)
        {
            Light.enabled = !Light.enabled;
        }
    }
}