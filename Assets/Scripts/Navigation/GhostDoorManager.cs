using System.Linq;
using UnityEngine;

/* HOW TO USE :
 * 
 * 1) Modify doors material shader to Standard/Transparent :
 * 
 * - Bois grain pin alpha_1_1_masked.mat
 * - kaso_dark_oak_1_1_masked.mat
 * - GDLM152_kaso Aluminium.mat
 * - GDLM152_kaso Black.mat
 * - Métal-Inox.mat
 * - Plomb_1_1.mat
 * 
 * 2) Add white texture on materials without texture
 */

public class GhostDoorManager : MonoBehaviour
{
    public GameObject DoorsRootNode;
    public GameObject[] IgnoredDoors;
    public Material[] IgnoredMaterials;

    void Awake()
    {
        foreach (Transform child in DoorsRootNode.transform)
        {
            if (child.childCount == 0)
                continue;

            GameObject door = child.gameObject;
            if (IgnoredDoors.Contains(door))
                continue;

            var ghostDoor = door.AddComponent<GhostDoor>();
            ghostDoor.IgnoredMaterials = IgnoredMaterials;

            foreach (MeshCollider meshCollider in door.GetComponentsInChildren<MeshCollider>())
                meshCollider.enabled = false;
        }
    }
}
