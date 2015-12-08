using UnityEngine;
using System.Collections;

public enum AnimState
{
    run,
    fall,
    jumb,
    doubleJumb,
    crouch
}
public class PlayerAnimation : MonoBehaviour
{
    public string runFolder;
    public string jumbFolder;
    public string dJumbFolder;
    public string crouchFolder;
    public string fallFolder;

    public AnimState state;
    public float speed;

    private float frameTime;
    private int frameCount;
    private Sprite[] runSprites;
    private Sprite[] jumbSprites;
    private Sprite[] dJumbSprites;
    private Sprite[] fallSprites;
    private Sprite[] crouchSprites;
    private SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        runSprites = Resources.LoadAll<Sprite>(runFolder);
        jumbSprites = Resources.LoadAll<Sprite>(jumbFolder);
        dJumbSprites = Resources.LoadAll<Sprite>(dJumbFolder);
        fallSprites = Resources.LoadAll<Sprite>(fallFolder);
        crouchSprites = Resources.LoadAll<Sprite>(crouchFolder);
        StartCoroutine(Animation());
    }
    void FixedUpdate()
    {

    }
    // Update is called once per frame
    IEnumerator Animation()
    {
        while (true)
        {
            switch (state)
            {
                case AnimState.crouch:
                    if (++frameCount >= crouchSprites.Length)
                        frameCount = crouchSprites.Length - 1;
                    spriteRenderer.sprite = crouchSprites[frameCount];
                    break;
                case AnimState.doubleJumb:
                    if (++frameCount >= dJumbSprites.Length)
                        frameCount = dJumbSprites.Length - 1;
                    spriteRenderer.sprite = dJumbSprites[frameCount];
                    break;
                case AnimState.fall:
                    if (++frameCount >= fallSprites.Length)
                        frameCount = 0;
                    spriteRenderer.sprite = fallSprites[frameCount];
                    break;
                case AnimState.jumb:
                    if (++frameCount >= jumbSprites.Length)
                        frameCount = jumbSprites.Length - 1;
                    spriteRenderer.sprite = jumbSprites[frameCount];
                    break;
                case AnimState.run:
                    if (++frameCount >= runSprites.Length)
                        frameCount = 0;
                    spriteRenderer.sprite = runSprites[frameCount];
                    break;
            }
            yield return new WaitForSeconds(speed);
        }
    }
    //void Update()
    //{
    //    frameTime += Time.deltaTime;
    //    if (frameTime >= speed)
    //    {
    //        frameTime = 0;
    //        if (runBool)
    //        {
    //            if (++frameCount >= runSprites.Length)
    //                frameCount = 0;
    //            spriteRenderer.sprite = runSprites[frameCount];
    //        }
    //        else if (jumpBool)
    //        {
    //            if (++frameCount >= jumbSprites.Length)
    //                frameCount = jumbSprites.Length - 1;
    //            spriteRenderer.sprite = jumbSprites[frameCount];
    //        }
    //        else if (dJumpBool)
    //        {
    //            if (++frameCount >= dJumbSprites.Length)
    //                frameCount = dJumbSprites.Length - 1;
    //            spriteRenderer.sprite = dJumbSprites[frameCount];
    //        }
    //        else if (crouchBool)
    //        {
    //            if (++frameCount >= crouchSprites.Length)
    //                frameCount = crouchSprites.Length - 1;
    //            spriteRenderer.sprite = crouchSprites[frameCount];
    //        }
    //        else if (invcrouchBool)
    //        {
    //            if (--frameCount < 0)
    //                run();
    //            spriteRenderer.sprite = crouchSprites[frameCount];
    //        }
    //    }
    //}
    public void run()
    {
        state = AnimState.run;
        frameCount = 0;
    }
    public void jumb()
    {
        state = AnimState.jumb;
        frameCount = 0;

    }
    public void dJumb()
    {
        state = AnimState.doubleJumb;
        frameCount = 0;
    }
    public void crouch()
    {
        state = AnimState.crouch;
        frameCount = 0;
    }
    public void fall()
    {
        state = AnimState.fall;
        frameCount = 0;
    }
}
