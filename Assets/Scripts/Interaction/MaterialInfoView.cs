using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MaterialInfoView : MonoBehaviour
{
    public Text Text;
    private Camera _camera;

    void Start()
    {
        _camera = GameObject.Find("Camera0").GetComponent<Camera>();
    }

    public void Show(MaterialInfo info, RaycastHit raycastHit)
    {
        gameObject.SetActive(true);
        Text.text = string.Format("{0}\n{1}", info.Name, info.Description);

        transform.position = new Vector3(raycastHit.point.x, _camera.transform.position.y, raycastHit.point.z);
        transform.forward = -raycastHit.normal;
        transform.Translate(0, 0, -0.1f);
    }

    public void Hide()
    {
        Text.text = "";
        gameObject.SetActive(false);
    }
}
