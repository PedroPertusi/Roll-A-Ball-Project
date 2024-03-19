using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float startZ;
    public float endZ;
    public float speed;

    private float journeyLength;
    private Vector3 startPosition;
    private bool isMovingTowardsEnd = true;
    private bool isStopped = false; // Variable to control if the enemy is stopped.

    void Start()
    {
        startPosition = transform.position;
        journeyLength = Mathf.Abs(endZ - startZ);
    }

    void Update()
    {
        if (isStopped) return; // Stop updating position if the enemy is stopped.

        float timeFraction = Time.time * speed / journeyLength;
        float distCovered = Mathf.PingPong(timeFraction, 1);

        Vector3 currentPos = transform.position;
        currentPos.z = Mathf.Lerp(startZ, endZ, distCovered);
        transform.position = currentPos;

        if ((distCovered >= 0.99f && isMovingTowardsEnd) || (distCovered <= 0.01f && !isMovingTowardsEnd))
        {
            transform.Rotate(0, 180, 0);
            isMovingTowardsEnd = !isMovingTowardsEnd;
        }
    }

    public void StopMovement()
    {
        isStopped = true;
    }
}
