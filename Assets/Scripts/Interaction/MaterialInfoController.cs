using UnityEngine;

public class MaterialInfoController : MonoBehaviour
{
    public MaterialInfoDictionary Dictionary;
    public MaterialInfoView View;
    public string WandNodeName = "HandNode";

    private Material _currentMaterial;
    private GameObject _wand;

    void Start()
    {
        _wand = GameObject.Find(WandNodeName);
    }

	void Update ()
    {
        var ray = new Ray(_wand.transform.position, _wand.transform.forward);
	    
        RaycastHit hit;
	    if (Physics.Raycast(ray, out hit))
	    {
	        var materialRenderer = hit.collider.gameObject.GetComponent<Renderer>();
            if (materialRenderer != null)
            {
                if (MiddleVR.VRDeviceMgr.IsWandButtonPressed(1))
                {
                    MaterialInfo materialInfo = Dictionary[materialRenderer.material];
                    if (materialInfo != null)
                    {
                        View.Show(materialInfo, hit);
                        _currentMaterial = materialRenderer.material;
                        return;
                    }
                }

                if (_currentMaterial != null && _currentMaterial.mainTexture != materialRenderer.material.mainTexture)
                {
                    View.Hide();
                }
            }
        }
	}
}
