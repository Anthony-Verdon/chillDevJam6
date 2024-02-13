using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum EMonkeySprite {
    IDLE,
    HIDE
}

public class monkeyScript : MonoBehaviour
{
    [SerializeField]
    private FieldOfViewScript fieldOfViewScript;
    [SerializeField]
    private Sprite[] sprites;

    bool initError = false;
    private Rigidbody2D myRigidbody;
    private SpriteRenderer mySpriteRenderer;

    private string bananaName ="Banana";
    private bool bananaNear = false;
    private bool bananaPickedUp = false;

    private bool hiding = false;

    private string extractAreaName ="extractArea";
    private bool inExtractArea = false;

    private float maxViewDistance = 5f;
    private float minViewDistance = 0.5f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        checkInitError();

        if (initError)
            return;
        fieldOfViewScript.setFOV(360f);
        fieldOfViewScript.setViewDistance(5f);
        fieldOfViewScript.setStartAngle(Vector3.zero);
    }

    void checkInitError()
    {
        if (!myRigidbody)
        {
            Debug.LogError("MonkeyScript: rigidbody not initialized");
            initError = true;
        }
        if (!mySpriteRenderer)
        {
            Debug.LogError("MonkeyScript: spriteRenderer not initialized");
            initError = true;
        }
        if (sprites.Length != EMonkeySprite.GetNames(typeof(EMonkeySprite)).Length)
        {
            Debug.LogError("MonkeyScript: number of sprites not equal with enum declaration");
            initError = true;
        }
        if (!fieldOfViewScript)
        {
            Debug.LogError("MonkeyScript: field of view not initialized");
            initError = true;
        }
    }

    void Update()
    {
        if (initError)
            return;
        
        checkActions();
        if (!hiding)
            updatePosition();
        updateViewDistance();
        fieldOfViewScript.setOrigin(transform.position);
    }

    private void updatePosition()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        myRigidbody.velocity = direction.normalized * Time.deltaTime * 300;
    }

    private void checkActions()
    {
        if (Input.GetKeyDown("e"))
        {
            if (bananaNear)
                pickUpBanana();
            else if (bananaPickedUp && inExtractArea)
                endGame(true);
        }
        else if (Input.GetKeyDown("left shift"))
            updateSprite();
    }

    private void pickUpBanana()
    {
        bananaPickedUp = true;
        Destroy(GameObject.Find(bananaName));
        GameObject.Find(extractAreaName).GetComponent<extractAreaScript>().activate();
    }

    public void endGame(bool playerWin)
    {
        if (playerWin == false && SceneManager.GetActiveScene().name == "tutorial")
        {
            SceneManager.LoadScene("Scenes/tutorial");
            return;
        }
        SceneManager.LoadScene("Scenes/mainScene");
    }

    private void updateSprite()
    {
        hiding = !hiding;
        if (hiding)
            mySpriteRenderer.sprite = sprites[(int)EMonkeySprite.HIDE];
        else
            mySpriteRenderer.sprite = sprites[(int)EMonkeySprite.IDLE];
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == bananaName)
            bananaNear = true;
        else if (collider.name == extractAreaName)
            inExtractArea = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == bananaName)
            bananaNear = false;
        else if (collider.name == extractAreaName)
            inExtractArea = false;
    }

    public bool isHiding()
    {
        return hiding;
    }

    private void updateViewDistance()
    {
        if (hiding && fieldOfViewScript.getViewDistance() > minViewDistance)
        {
            float newViewDistance = fieldOfViewScript.getViewDistance() - Time.deltaTime;
            if (newViewDistance < minViewDistance)
                newViewDistance = minViewDistance;
            fieldOfViewScript.setViewDistance(newViewDistance);
        }
        else if (!hiding && fieldOfViewScript.getViewDistance() < maxViewDistance)
        {
            float newViewDistance = fieldOfViewScript.getViewDistance() + Time.deltaTime;
            if (newViewDistance < minViewDistance)
                newViewDistance = minViewDistance;
            fieldOfViewScript.setViewDistance(newViewDistance);
        }
    }
}
