using UnityEngine;
using System.Collections;

public class KeyBoardControls : MonoBehaviour
{
    public PlayerController PC;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PC.Up();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PC.Down();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PC.powerUp();
        }
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            PC.impactPressed();
        }
    }
}