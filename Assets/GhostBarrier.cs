using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostBarrier : MonoBehaviour
{
	public float NearDistance = 1.5f;
	public float FarDistance = 2.5f;
	private IEnumerable<Material> _materials;
	private Camera _userCamera;
	private bool _wasNearEnough;

	void Start()
	{
		_userCamera = Camera.allCameras.First();

		_materials = GetComponentsInChildren<Renderer>().Select(x => x.material).ToArray();

		foreach (Material material in _materials)
		{
			Color color = material.color;
			color.a = 0;
			material.color = color;
		}

		GetComponent<Renderer>().enabled = false;
	}

	void Update()
	{
		Vector3 closestPoint = GetComponent<MeshCollider> ().ClosestPointOnBounds (_userCamera.transform.position);
		Vector3 distance = closestPoint - _userCamera.transform.position;

		bool isNearEnough = distance.magnitude <= FarDistance;
		GetComponent<Renderer>().enabled = isNearEnough;
		if (!_wasNearEnough || isNearEnough != _wasNearEnough)
		{
			_wasNearEnough = isNearEnough;
			return;
		}

		float transparencyAmount;
		if (isNearEnough)
			transparencyAmount = Mathf.InverseLerp (NearDistance, FarDistance, distance.magnitude);
		else
			transparencyAmount = 1f;

		foreach (Material material in _materials)
		{
			Color color = material.color;
			color.a = Mathf.Lerp(1, 0, transparencyAmount);
			material.color = color;
		}

		_wasNearEnough = isNearEnough;
	}
}

