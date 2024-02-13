using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    private Mesh mesh;

    private Vector3 origin;
    private float fov = 0f;
    private float startAngle = 0f;
    private float viewDistance = 0f;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void LateUpdate()
    {
        int rayCount = 200;
        float angle = startAngle;
        float angleIncrease = fov / rayCount;

        // +1 for the origin and +1 for ray 0
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (!raycastHit2D.collider)
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            else
                vertex = raycastHit2D.point;

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }
        

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
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

    public void setViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

    public float getViewDistance()
    {
       return viewDistance;
    }
}