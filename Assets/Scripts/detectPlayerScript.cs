using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayerScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    private Vector3 origin;
    private float fov = 0f;
    private float startAngle = 0f;

    void LateUpdate()
    {
        int rayCount = 200;
        float angle = startAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 5f;

        for (int i = 0; i <= rayCount; i++)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            angle -= angleIncrease;
            if (!raycastHit2D.collider || !raycastHit2D.rigidbody || raycastHit2D.rigidbody.tag != "Player")
                continue;
            GameObject player = raycastHit2D.rigidbody.gameObject;
            if (!player.GetComponent<monkeyScript>().isHiding())
                player.GetComponent<monkeyScript>().endGame(false);

        }
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return (new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad)));
    }

    public void setOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void setFOV(float fov)
    {
        this.fov = fov;
    }

    public void setStartAngle(Vector3 direction)
    {
        this.startAngle = calculateAngleFromVector(direction) + 45;
    }

    private float calculateAngleFromVector(Vector3 direction)
    {
        direction = direction.normalized;
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;
        return (n);
    }
}
