using UnityEngine;
using System.Collections;

public class MovingObjectScript : MonoBehaviour
{
    public float speed;
    public Transform target;
    public Transform start;
    public Transform end;
    public bool direction;
    // Update is called once per frame
    void Update()
    {

        if (direction)
        {
            target.position = Vector2.MoveTowards(target.position, start.position, speed * Time.deltaTime);

            if (target.position == start.position)
                direction = false;
        }
        else
        {
            target.position = Vector2.MoveTowards(target.position, end.position, speed * Time.deltaTime);
            if (target.position == end.position)
                direction = true;
        }
    }
}
