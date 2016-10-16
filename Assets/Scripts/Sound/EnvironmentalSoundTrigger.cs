using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EnvironmentalSoundTrigger : MonoBehaviour
{
    public AudioClip clip;
    // Note: Ajouter variable pour différencier les salles

    void OnTriggerEnter(Collider col)
    {
        EnvironmentalSound player = col.gameObject.GetComponent<EnvironmentalSound>();
        if (player != null)
        {
            player.PlaySound(this);
        }
    }

    void OnDrawGizmos()
    {
        Bounds bounds = GetComponent<BoxCollider>().bounds;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

}
