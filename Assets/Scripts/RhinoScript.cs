using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoScript : MonoBehaviour
{
    bool initError = false;
    private Rigidbody2D myRigidbody;
    
    [SerializeField]
    private Transform[] checkpoints;
    private int checkpointIndex = 0;

    [SerializeField]
    private FieldOfViewScript fieldOfViewScript;
    [SerializeField]
    private detectPlayerScript detectPlayerScript;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        checkInitError();

        if (initError)
            return;
        
        fieldOfViewScript.setFOV(90f);
        fieldOfViewScript.setViewDistance(5f);
        detectPlayerScript.setFOV(90f);
        transform.position = checkpoints[0].transform.position;
    }

    void checkInitError()
    {
        if (!myRigidbody)
        {
            Debug.LogError("RhinoScript: rigidbody not initialized");
            initError = true;
        }
        if (!fieldOfViewScript)
        {
            Debug.LogError("RhinoScript: field of view not initialized");
            initError = true;
        }
        if (!detectPlayerScript)
        {
            Debug.LogError("RhinoScript: detect player not initialized");
            initError = true;
        }
        if (checkpoints.Length <= 1)
        {
            Debug.LogError("RhinoScript: need at least 2 checkpoints");
            initError = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (initError)
            return;
        
        updatePosition();
        fieldOfViewScript.setOrigin(transform.position);
        detectPlayerScript.setOrigin(transform.position);
    }

    private void updatePosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, checkpoints[checkpointIndex].transform.position, Time.deltaTime);
        if (transform.position != checkpoints[checkpointIndex].transform.position)
            return;
        
        checkpointIndex++;
        if (checkpointIndex == checkpoints.Length)
            checkpointIndex = 0;

        Vector3 newDirection = checkpoints[checkpointIndex].transform.position - transform.position;
        fieldOfViewScript.setStartAngle(newDirection);
        detectPlayerScript.setStartAngle(newDirection);
        
    }
}
