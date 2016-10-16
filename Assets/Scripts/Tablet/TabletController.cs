using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class TabletController : MonoBehaviour
{
    private enum Mode
    {
        Wand,
        Fixed
    }

    //public MaterialInfoDictionary Dictionary;
    public GameObject Tablet;
    public TabletView View;
    public GameObject MaterialCursor;
    public float maxMaterialDistance = 50.0f;
    public string HeadNodeName = "HeadNode";
	public string HandNodeName = "HandNode";
    public AutoNavigationManager AutoNavigationManager;

    private Mode _mode = Mode.Wand;
    private Material _currentMaterial;
	public GameObject _head;
	private GameObject _hand;

    private Quaternion originalRotation;

    void Start()
    {
        MaterialCursor.SetActive(false);

		_head = GameObject.Find(HeadNodeName);
		_hand = GameObject.Find(HandNodeName);

        originalRotation = Tablet.transform.localRotation;

        if (!(GetConfigFilename() == "Assets/default.vrx"
            || GetConfigFilename().Contains("gamepad")))
        {
			_hand.transform.localPosition = _head.transform.position;
            Tablet.transform.localRotation = Quaternion.AngleAxis(90, Vector3.right);
        }
        else
        {
            _mode = Mode.Fixed;
            Tablet.SetActive(false);
		}
    }

    void Update()
	{
        if (Input.GetKeyDown(KeyCode.T) || MiddleVR.VRDeviceMgr.IsWandButtonToggled(2, true))
        {
            InvertMode();
        }
        if (_mode == Mode.Fixed)
			_hand.transform.position = _head.transform.position;

        LayerMask layerMask = ~Physics.IgnoreRaycastLayer;
        var ray = new Ray(_head.transform.position, _head.transform.forward);
       // Debug.DrawLine(ray.origin, ray.origin + ray.direction.normalized * maxMaterialDistance, Color.black);
        RaycastHit hit;
		if (Physics.Raycast(ray, out hit, maxMaterialDistance, layerMask))
        {
            var materialRenderer = hit.collider.gameObject.GetComponent<Renderer>();
            if (materialRenderer != null)
            {
                //Debug.Log("hit : " + materialRenderer.name);
                //Debug.DrawLine(ray.origin, hit.point);
                if (materialRenderer.material != null && materialRenderer.material.mainTexture != null)
                {
                    if (MiddleVR.VRDeviceMgr.IsWandButtonToggled(1, true))
                    {
                        string materialName = materialRenderer.material.mainTexture.name.Replace("(Instance)", "").Trim();
                        string materialInfoFilename = Path.Combine("html/materials/", materialName + ".html");

                        if (File.Exists(Path.Combine(Application.dataPath, materialInfoFilename)))
                        {
                            _currentMaterial = materialRenderer.material;

                            View.ShowMaterialInfo(materialInfoFilename);

                            MaterialCursor.SetActive(true);
                            MaterialCursor.transform.position = hit.point + hit.normal * 0.01f;
                            MaterialCursor.transform.rotation = Quaternion.LookRotation(hit.normal);

                            return;
                        } else
                        {
                            Debug.Log("Could not find info for material : " + materialName);
                            StartCoroutine(ShowMaterialNotFound(materialName));
                        }
                    }

                }

                if (!MiddleVR.VRDeviceMgr.IsWandButtonPressed(1) && materialRenderer.material != null && _currentMaterial != null && _currentMaterial.mainTexture != materialRenderer.material.mainTexture)
                {
                    View.BackToDefault();
                    _currentMaterial = null;

                    MaterialCursor.gameObject.SetActive(false);
                }
            }
        }

        if (_mode == Mode.Fixed)
            Tablet.SetActive(_currentMaterial != null
               /* || MiddleVR.VRDeviceMgr.IsWandButtonPressed(2)*/ || (AutoNavigationManager.Enabled && !AutoNavigationManager.IsMoving));
    }

    private IEnumerator ShowMaterialNotFound(string materialName)
    {

        View.MapScreen.MaterialInfoMissingText.text = "Pas d'info sur le matériau : " + materialName;
        yield return new WaitForSeconds(3);
        View.MapScreen.MaterialInfoMissingText.text = "";
        yield return null;
    }

    private void InvertMode()
    {
        if (_mode == Mode.Fixed)
        {
            _mode = Mode.Wand;
            Tablet.transform.localRotation = Quaternion.AngleAxis(70, Vector3.right);
            Tablet.SetActive(true);
        } else
        {
            _mode = Mode.Fixed;
            Tablet.transform.localRotation = originalRotation;
            Tablet.SetActive(false);
        }
    }

    private string GetConfigFilename()
    {
        string configFileName = GameObject.Find("VRManager").GetComponent<VRManagerScript>().ConfigFile;
        bool nextArgIsConfigFile = false;
        for (uint i = 0, iEnd = MiddleVR.VRKernel.GetCommandLineArgumentsNb(); i < iEnd; ++i)
        {
            string cmdLineArg = MiddleVR.VRKernel.GetCommandLineArgument(i);

            if (cmdLineArg == "--config")
            {
                nextArgIsConfigFile = true;
            }
            else if (nextArgIsConfigFile)
            {
                configFileName = cmdLineArg;
                nextArgIsConfigFile = false;
            }
        }

        return configFileName;
    }
}
