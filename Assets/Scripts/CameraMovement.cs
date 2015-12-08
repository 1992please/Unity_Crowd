using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float speed;
    public bool gamePlayLock;
    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        temp.y = player.position.y;
        transform.position = Vector3.Lerp(transform.position, temp, speed * Time.deltaTime);
    }
}
