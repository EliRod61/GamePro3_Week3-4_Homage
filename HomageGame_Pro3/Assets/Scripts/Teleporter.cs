using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player, destination;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player.position = destination.position;
        }
    }
}
