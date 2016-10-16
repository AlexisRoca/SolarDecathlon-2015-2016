using UnityEngine;
using System.Collections;
//using UnityStandardAssets.Characters.FirstPerson;

public class FloorDetector : MonoBehaviour {

    private Collider _currentCollider;
    private Vector3 _intersectionPoint;
    public GroundProperty Ground { get; private set; }

    private void Update()
    {
        _intersectionPoint = _currentCollider == null
            ? transform.position
            : _currentCollider.ClosestPointOnBounds(transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == _currentCollider)
            return;

        foreach (GroundProperty ground in Floor.Instance.Grounds)
            if (other.gameObject.GetComponent<Renderer>().material.mainTexture == ground.Texture && Ground == null)
            {
                Ground = ground;
                Debug.Log(Ground.Texture.ToString());
                _currentCollider = other;
                gameObject.GetComponent<MovesoundConnector>().SetFootstepSounds(Ground.ArrayAudioClip);
                return;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != _currentCollider)
            return;

        Ground = null;
        _currentCollider = null;
    }
}
