using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public enum PlayerMoveStatus
{
    zeroState,
    Run,
    Jump,
    Crouch,
    Fall,
    DoubleJump,
    Die
}
;
public class PlayerController : MonoBehaviour
{
    public Transform groundCheck;
    public Transform ceilCheck;
    public GameObject impactPrefab;
    public GameObject powerGUI;
    public Text powerText;
    public float playerSpeed;
    public float powerUpScaleSpeed;
    public float canonSpeedScale;
    public float springSpeedScale;
    public float jumbPower;
    public float downPower;
    public float springPower;
    public float canonPower;
    public PlayerAnimation PA;
    public PlayerMoveStatus moveStatus;
    public bool upperLevel;
    public GameController GC;
    public BlocksController BC;
    public PlayerSounds PS;
    public bool canFinish;
    public GameObject glow;
    private int breakCharges;
    private bool powerUpBool;
    private bool Grounded;
    private Rigidbody2D mRigidbody;
    void Start()
    {
        canFinish = false;
        Grounded = false;
        powerUpBool = false;
        breakCharges = 0;
        // camPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        mRigidbody = GetComponent<Rigidbody2D>();
        mRigidbody.fixedAngle = true;
    }
    void FixedUpdate()
    {
        Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, .2f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                if (moveStatus != PlayerMoveStatus.Die && moveStatus != PlayerMoveStatus.Run && moveStatus != PlayerMoveStatus.Crouch && moveStatus != PlayerMoveStatus.Jump)
                {
                    Run();
                   // mRigidbody.velocity.y = 0;
                }

            }
        }
        if (!Grounded)
        {
            if (mRigidbody.velocity.y < 0)
            {
                GetComponent<Animation>().Play("PlayerNormal");
                PA.fall();
                moveStatus = PlayerMoveStatus.DoubleJump;

            }

        }
    }
    public void Up()
    {
        switch (moveStatus)
        {
            case PlayerMoveStatus.Run:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumbPower));
                moveStatus = PlayerMoveStatus.Jump;
                PA.jumb();
                break;
            case PlayerMoveStatus.Jump:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumbPower * .8f));
                moveStatus = PlayerMoveStatus.DoubleJump;
                PA.dJumb();
                break;
            case PlayerMoveStatus.Crouch:
                if (!Physics2D.OverlapCircle(ceilCheck.position, .01f))
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumbPower));
                    moveStatus = PlayerMoveStatus.Jump;
                    PA.jumb();
                    GetComponent<Animation>().Play("PlayerNormal");
                }
                break;
        }
    }
    public void Down()
    {
        if (moveStatus == PlayerMoveStatus.Run)
        {
            StartCoroutine(crouchCoroutine());

        }
        else if (moveStatus == PlayerMoveStatus.Crouch)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("UpperFloor");
            foreach (GameObject ob in objects)
            {
                ob.GetComponent<Collider2D>().enabled = false;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -downPower));
        }
    }
    IEnumerator canonJumb()
    {
        float speedtemp = BC.speed;
        BC.speed = 0;
      //  PS.playSound(3);
        yield return new WaitForSeconds(2);
       // PS.playSound(1);
        BC.speed = speedtemp * canonSpeedScale;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, canonPower));
        moveStatus = PlayerMoveStatus.Jump;
        PA.jumb();
        yield return new WaitForSeconds(1);
        //PS.playSound(4);
        BC.speed = speedtemp;
        Up();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            //case "Breakable":
            //    if (powerUpBool)
            //    {
            //        other.collider2D.enabled = false;
            //        other.GetComponent<animateScript>().runAnimation();
            //    }
            //    else
            //    {
            //        // GC.lose();
            //    }
            //    break;
            case "UpperFloor":
                GameObject[] objects = GameObject.FindGameObjectsWithTag("UpperFloor");
                foreach (GameObject ob in objects)
                {
                    ob.GetComponent<Collider2D>().enabled = false;
                }
                break;
            case "Lose":
                moveStatus = PlayerMoveStatus.Die;
                GC.lose();
                break;
            case "FloatCoin":
                GC.getFloatingCoin();
                Destroy(other.gameObject);
                break;
            case "NeedHappiness":
                GC.MakeAHappyMan(other.GetComponent<animateScript>());
                break;
            case "Finish":
                GC.FinishLevelRoutine();
                break;
            default:
                switch (other.name)
                {
                    case "PowerCoin":
                        powerGUI.SetActive(true);
                        breakCharges += 2;
                        powerText.text = "" + breakCharges;
                        GC.getBreakCoin();
                        Destroy(other.gameObject);
                        break;
                    case "ScoreCoin":
                       // PS.playSound(2);
                        GC.getGoldenCoin();
                        Destroy(other.gameObject);
                        break;
                    case "Magnite":
                        StartCoroutine(magniteRoutine());
                        Destroy(other.gameObject);
                        break;
                    case "LastWall":
                        GC.respawnButton.SetActive(false);
                        if (canFinish)
                        {
                            StartCoroutine(finishRoutine(other));
                        }
                        break;
                    case "spring":
                        StopCoroutine(PowerUpRoutine());
                        StartCoroutine(spring(other));
                        break;

                }
                break;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Breakable":
                if (powerUpBool && other.GetComponent<Collider2D>().enabled)
                {
                    other.GetComponent<Collider2D>().enabled = false;
                    other.GetComponent<animateScript>().runAnimation();
                }
                break;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "UpperFloor")
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("UpperFloor");
            foreach (GameObject ob in objects)
            {
                ob.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
    IEnumerator crouchCoroutine()
    {
        moveStatus = PlayerMoveStatus.Crouch;
        GetComponent<Animation>().Play("PlayerColliderCrouch");
        PA.crouch();
        yield return new WaitForSeconds(1);
        while (moveStatus == PlayerMoveStatus.Crouch)
        {
            if (!Physics2D.OverlapCircle(ceilCheck.position, .01f))
            {
                GetComponent<Animation>().Play("PlayerNormal");
                moveStatus = PlayerMoveStatus.Run;
                PA.run();
            }
            yield return null;

        }
    }
    IEnumerator PowerUpRoutine()
    {
        powerUpBool = true;
        float speedtemp = BC.speed;
        BC.speed *= powerUpScaleSpeed;
        powerGUI.GetComponent<Button>().interactable = false;
        StartCoroutine("returnToOrigin");
        yield return new WaitForSeconds(.3f);
        StopCoroutine("returnToOrigin");
        powerGUI.GetComponent<Button>().interactable = true;
        checkPowerButtonGUI();
        BC.speed = speedtemp;
        powerUpBool = false;
    }
    IEnumerator returnToOrigin()
    {
        while (transform.position.x != 0)
        {
            yield return new WaitForSeconds(.04f);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, transform.position.y), .6f);
        }
    }
    public void startOrReset()
    {
        glow.SetActive(false);
        BC.speed = playerSpeed;
        powerUpBool = false;
        breakCharges = 0;
        transform.position = new Vector3(0, -9.2f, 0);
        moveStatus = PlayerMoveStatus.zeroState;
        StopAllCoroutines();
        StartCoroutine(canonJumb());
        checkPowerButtonGUI();
    }
    public void powerUp()
    {
        StartCoroutine(PowerUpRoutine());
        powerText.text = "" + --breakCharges;
    }
    public void impactPressed()
    {
        PS.playSound(5);
        Instantiate(impactPrefab, transform.position, Quaternion.identity);
    }
    IEnumerator finishRoutine(Collider2D other)
    {
        other.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(.5f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumbPower * .8f));
        moveStatus = PlayerMoveStatus.Jump;
        PA.dJumb();
      //  PS.playSound(0);
        other.GetComponent<animateScript>().runAnimation();
        yield return new WaitForSeconds(.1f);
        Time.timeScale = .1f;
        yield return new WaitForSeconds(.1f);
        Time.timeScale = 1;
    }
    public IEnumerator respawn()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        Transform respawnLocation = BC.getRespawnLocation();
        moveStatus = PlayerMoveStatus.zeroState;
        GetComponent<Collider2D>().enabled = false;
        while (respawnLocation.position.x > 0)
        {
            yield return new WaitForSeconds(.02f);
            transform.position = Vector3.MoveTowards(transform.position, respawnLocation.position, 1);
        }
        transform.position = respawnLocation.position;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;

    }
    IEnumerator magniteRoutine()
    {
        glow.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            GameObject[] coins = GameObject.FindGameObjectsWithTag("Metal");
            foreach (GameObject coin in coins)
            {
                //print((coin.transform.position - transform.position).magnitude);
                if (coin.transform.position.x > transform.position.x && (coin.transform.position - transform.position).magnitude < 25)
                {
                    StartCoroutine(moveToMe(coin.transform));
                }
            }
            yield return new WaitForSeconds(.2f);
        }
        glow.SetActive(false);

    }
    IEnumerator moveToMe(Transform ob)
    {
        while (ob)
        {
            ob.position = Vector2.MoveTowards(ob.position, transform.position, 1.2f);
            yield return new WaitForSeconds(.02f);
        }
    }
    IEnumerator spring(Collider2D other)
    {
        powerGUI.GetComponent<Button>().interactable = false;
        other.GetComponent<animateScript>().runAnimation();
        float speedtemp = BC.speed;
        BC.speed = speedtemp * springSpeedScale;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, springPower));
        moveStatus = PlayerMoveStatus.Jump;
        PA.jumb();
        yield return new WaitForSeconds(2.5f);
        BC.speed = speedtemp;
        powerGUI.GetComponent<Button>().interactable = true;
    }

    private void checkPowerButtonGUI()
    {
        powerGUI.GetComponent<Button>().interactable = true;
        if (breakCharges <= 0)
        {
            powerGUI.SetActive(false);
        }
        else
        {
            powerGUI.SetActive(true);
        }
    }

    private void Run()
    {
        moveStatus = PlayerMoveStatus.Run;
        if (PA)
            PA.run();
    }
}