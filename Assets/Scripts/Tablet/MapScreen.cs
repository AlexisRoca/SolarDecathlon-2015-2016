using System;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : TabletScreen
{
    public AutoNavigationManager AutoNavigationManager;
    public RectTransform MapPanel;
    public Text FloorText;
    public Text DestinationText;
    public Text AutoNavText;
    public Text MaterialInfoMissingText;
    public Image CursorImage;
    public Image TargetPoint;
    public BoxCollider MapBounds;
	public string HeadNodeName = "HeadNode";
	public GameObject User;
    public float[] FloorsHeight;
    public string[] FloorsLabels;
    public Image[] FloorsMaps;
    private GameObject _head;

    void Start()
    {
        _head = GameObject.Find(HeadNodeName);
    }

    void Update()
    {
        Vector3 userPosition = MapBounds.ClosestPointOnBounds(_head.transform.position);
        Vector3 userForward = _head.transform.forward;


        CursorImage.rectTransform.localPosition = TransformToMapPosition(userPosition);
        
        Quaternion quaternion = Quaternion.FromToRotation(userForward, MapBounds.transform.forward);
        CursorImage.rectTransform.localRotation = Quaternion.Euler(0, 0, quaternion.eulerAngles.y);

		if (MapBounds.bounds.Contains(User.transform.position))
        {
            for (int i = 0; i < FloorsHeight.Length; i++)
				if (User.transform.position.y > FloorsHeight[i])
                {
                    FloorText.text = FloorsLabels[i];

                    for (int j = 0; j < FloorsMaps.Length; j++)
                        FloorsMaps[j].gameObject.SetActive(j == i);

                    break;
                }
        }
        else
            FloorText.text = "";

        if (AutoNavigationManager.Enabled && !AutoNavigationManager.IsMoving && AutoNavigationManager.TargetedKeyPoint != null)
        {
            DestinationText.gameObject.SetActive(true);
            AutoNavText.gameObject.SetActive(true);
            TargetPoint.gameObject.SetActive(true);
            DestinationText.text = string.Format("Cible : {0}", AutoNavigationManager.TargetedKeyPoint.pointName);
            
            Vector3 targetPosition = MapBounds.ClosestPointOnBounds(AutoNavigationManager.TargetedKeyPoint.transform.position);
            TargetPoint.rectTransform.localPosition = TransformToMapPosition(targetPosition);
            if (AutoNavigationManager.TargetedKeyPoint.gameObject.tag == "Stairs")
            {
                TargetPoint.color = new Color(1, 1, 1, 0.5f);
                DestinationText.text += " - Escalier";
            }
            else
                TargetPoint.color = new Color(1, 1, 1, 1f);
        }
        else
        {
            DestinationText.gameObject.SetActive(false);
            AutoNavText.gameObject.SetActive(false);
            TargetPoint.gameObject.SetActive(false);
        }
    }

    private Vector3 TransformToMapPosition(Vector3 transformPosition)
    {

        var scale = new Vector2(1f / MapBounds.bounds.size.x, 1f / MapBounds.bounds.size.z);
        transformPosition = transformPosition - (MapBounds.bounds.center - MapBounds.bounds.size / 2);
        var mapPosition = new Vector2(transformPosition.x, transformPosition.z);
        mapPosition.Scale(scale);
        mapPosition.Scale(MapPanel.rect.size);
        mapPosition -= MapPanel.rect.size / 2;
        return mapPosition;
    }
}
