using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    public void loadScene()
    {
        Application.LoadLevel(sceneName);
    }
}
