using UnityEngine;
using System.Collections;

public class BlocksController : MonoBehaviour
{
    public Transform flag;
    public float speed = 5f;
    public float startingBlockPosition = 91.5f;
    public float blocksDistance;
    public LevelController LC;
    public bool finish;
    public bool done;
    public GameObject[] Boxs;
    public int[] endIndexforEachLevel;
    //public GameObject[] startingBoxs;
    //public GameObject[] endingBoxs;
    private GameObject bZone = null;
    private GameObject aZone = null;
    private bool slideBool = false;
    int blocksCount;
    void Update()
    {
        if (slideBool)
            slide();
    }
    void slide()
    {
        if (bZone)
        {
            blocksDistance += speed * Time.deltaTime;
            aZone.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            bZone.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            flag.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            if (bZone.transform.position.x <= 0 && !done)
            {
                replaceZones();
            }
        }
    }
    void replaceZones()
    {
        Destroy(aZone);
        aZone = bZone;
        if (finish)
        {
            bZone = Instantiate(Boxs[endIndexforEachLevel[LC.currentLevel]], new Vector3(91.5f, 0, 0), transform.rotation) as GameObject;
            done = true;
        }
        else
        {
            if (LC.currentLevel == 0)
            {
                if ((blocksCount + 1) < endIndexforEachLevel[0])
                    bZone = Instantiate(Boxs[1 + blocksCount], new Vector3(91.5f, 0, 0), transform.rotation) as GameObject;
                else
                    bZone = Instantiate(Boxs[Random.Range(1, endIndexforEachLevel[0])], new Vector3(91.5f, 0, 0), transform.rotation) as GameObject;
            }
            else
            {
                if (endIndexforEachLevel[LC.currentLevel - 1] + 2 + blocksCount < endIndexforEachLevel[LC.currentLevel])
                    bZone = Instantiate(Boxs[endIndexforEachLevel[LC.currentLevel - 1] + 2 + blocksCount], new Vector3(91.5f, 0, 0), transform.rotation) as GameObject;
                else
                    bZone = Instantiate(Boxs[Random.Range(endIndexforEachLevel[LC.currentLevel - 1] + 2, endIndexforEachLevel[LC.currentLevel])], new Vector3(91.5f, 0, 0), transform.rotation) as GameObject;
            }
            blocksCount++;
        }
        bZone.transform.parent = transform;
    }
    public void startOrReset()
    {
        blocksCount = 0;
        slideBool = true;
        done = false;
        finish = false;
        blocksDistance = 0;
        if (bZone)
            Destroy(bZone);
        if (LC.currentLevel == 0)
        {
            if ((blocksCount + 1) < endIndexforEachLevel[0])
                bZone = Instantiate(Boxs[1 + blocksCount], new Vector3(214.6f, 0, 0), transform.rotation) as GameObject;
            else
                bZone = Instantiate(Boxs[Random.Range(1, endIndexforEachLevel[0])], new Vector3(214.6f, 0, 0), transform.rotation) as GameObject;
        }
        else
        {
            if (endIndexforEachLevel[LC.currentLevel - 1] + 2 + blocksCount < endIndexforEachLevel[LC.currentLevel])
                bZone = Instantiate(Boxs[endIndexforEachLevel[LC.currentLevel - 1] + 2 + blocksCount], new Vector3(214.6f, 0, 0), transform.rotation) as GameObject;
            else
                bZone = Instantiate(Boxs[Random.Range(endIndexforEachLevel[LC.currentLevel - 1] + 2, endIndexforEachLevel[LC.currentLevel])], new Vector3(214.6f, 0, 0), transform.rotation) as GameObject;
        }
        blocksCount++;
        if (aZone)
            Destroy(aZone);

        if (LC.currentLevel == 0)
            aZone = Instantiate(Boxs[0], new Vector3(122.3f, 0, 0), transform.rotation) as GameObject;
        else
            aZone = Instantiate(Boxs[endIndexforEachLevel[LC.currentLevel - 1] + 1], new Vector3(122.3f, 0, 0), transform.rotation) as GameObject;

        aZone.transform.parent = bZone.transform.parent = transform;
    }
    public void SetBestDistanceFlagPosition(int bestPosition)
    {
        flag.gameObject.SetActive(true);
        flag.position = new Vector3(bestPosition * 10, 0, 0);
    }
    public Transform getRespawnLocation()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Respawn");
        GameObject choosenObject = objects[0];
        foreach (GameObject ob in objects)
        {
            if ((ob.transform.position.x > 0 && ob.transform.position.x < choosenObject.transform.position.x) || (choosenObject.transform.position.x < 0))
            {
                choosenObject = ob;
            }
        }
        return choosenObject.transform;
    }
}
