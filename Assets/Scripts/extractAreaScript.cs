using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extractAreaScript : MonoBehaviour
{
    bool initError = false;
    private SpriteRenderer mySpriteRenderer;

    bool isActivated = false;
    bool isFadingIn = true;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        checkInitError();
    }

    private void checkInitError()
    {
        if (!mySpriteRenderer)
        {
            Debug.LogError("ExtractAreaScript: spriteRenderer not initialized");
            initError = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (initError || !isActivated)
            return;

        updateAlphaChannel();
    }

    private void updateAlphaChannel()
    {
        Color newColor = mySpriteRenderer.color;
        if (isFadingIn)
            newColor.a += Time.deltaTime;
        else
           newColor.a -= Time.deltaTime;
        mySpriteRenderer.color = newColor;
        if (mySpriteRenderer.color.a <= 0 || mySpriteRenderer.color.a >= 1){
            isFadingIn = !isFadingIn;
    }
    }

    public void activate()
    {
        isActivated = true;
    }
}
