using UnityEngine;
using System.Collections;

public class animateScript : MonoBehaviour
{
    public string folderName;
    public string[] folersOfDiffernetAnimation;
    public float speed = .05f;
    public bool loop;
    public bool AutoStart;
    private Sprite[] framesSprite;
    private SpriteRenderer spriteRenderer;
    int counter;
    // Use this for initialization
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        framesSprite = Resources.LoadAll<Sprite>(folderName);
        if (AutoStart)
            runAnimation();
    }

    // Update is called once per frame
    public void loadDifferentAnimation(int index, bool loop)
    {
        StopCoroutine(runAnimationRoutine());
        framesSprite = Resources.LoadAll<Sprite>(folersOfDiffernetAnimation[index]);
        this.loop = loop;
        StartCoroutine(runAnimationRoutine());
    }
    public void runAnimation()
    {
        StartCoroutine(runAnimationRoutine());
    }
    IEnumerator runAnimationRoutine()
    {
        int counter = 0;
        while (counter < framesSprite.Length)
        {
            spriteRenderer.sprite = framesSprite[counter];
            yield return new WaitForSeconds(speed);
            if (++counter == framesSprite.Length && loop)
                counter = 0;
        }
    }
}
