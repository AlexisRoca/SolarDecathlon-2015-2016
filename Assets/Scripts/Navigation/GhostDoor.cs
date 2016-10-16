using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostDoor : MonoBehaviour
{
    public Material[] IgnoredMaterials;
    public float NearDistance = 1.5f;
    public float FarDistance = 2.5f;
    public float NearAngle = 30f;
    public float FarAngle = 60f;
    private IEnumerable<Material> _materials;
    private Vector3 _rectifiedPosition;
    private Camera _userCamera;
    private bool _wasNearEnough;

    void Start()
    {
        _rectifiedPosition = Vector3.zero;
        foreach (Transform child in transform)
            _rectifiedPosition += child.position;
        _rectifiedPosition /= transform.childCount;

        _userCamera = Camera.allCameras.First();

        _materials = GetComponentsInChildren<Renderer>()
            .Select(x => x.material).Where(x => IgnoredMaterials.All(y => x.mainTexture != y.mainTexture)).ToArray();
    }
	
	void Update()
	{
        Vector3 distance = _rectifiedPosition - _userCamera.transform.position;

	    bool isNearEnough = distance.magnitude <= FarDistance;
	    if (!_wasNearEnough || isNearEnough != _wasNearEnough)
	    {
	        _wasNearEnough = isNearEnough;
	        return;
	    }

	    float transparencyAmount;
	    if (isNearEnough)
	    {
            float angle = Vector3.Angle(_userCamera.transform.forward, distance);

	        float distanceAmount = Mathf.InverseLerp(NearDistance, FarDistance, distance.magnitude);
	        float angleAmount = Mathf.InverseLerp(NearAngle, FarAngle, angle);

	        transparencyAmount = distanceAmount;// + angleAmount;
	    }
	    else
	        transparencyAmount = 1f;

	    foreach (Material material in _materials)
	    {
	        Color color = material.color;
            color.a = Mathf.Lerp(0, 1, transparencyAmount);
            material.color = color;
        }

        _wasNearEnough = isNearEnough;
	}
}
