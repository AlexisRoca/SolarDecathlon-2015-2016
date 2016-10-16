using UnityEngine;
using UnityEngine.UI;

public class NavigationAutoDisplay : MonoBehaviour
{
    public AutoNavigationManager AutoNavigationManager;
    public Text Text;

	void Update ()
    {
        if (AutoNavigationManager.Enabled && !AutoNavigationManager.IsMoving && AutoNavigationManager.TargetedKeyPoint != null)
	    {
	        Text.gameObject.SetActive(true);
	        Text.text = AutoNavigationManager.TargetedKeyPoint.name;
	    }
	    else
        {
            Text.gameObject.SetActive(false);
	    }
	}
}
