using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private float maxSpawnDistance = 100;
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() 
    {
        NavMeshPath path = new NavMeshPath();
        Vector3[] spawnPos = 
            {
            new Vector3(1.1f, 0.5f, 10.0f),
            new Vector3(-0.1f, 0.5f, 10.0f),
            new Vector3(5.0f, 0.5f, 3.0f),
            new Vector3(-5.0f, 0.5f, 3.0f),
            new Vector3(10.0f, 0.5f, 1.0f),
            new Vector3(0.5f, 0.5f, -5.0f),
            new Vector3(-10.0f, 0.5f, 1.0f)
        };
        for (int i = 0; i < spawnPos.Length; i++)
        {
            GameObject spawnedEnemy = Instantiate(enemy, new Vector3(Camera.main.ViewportToWorldPoint(spawnPos[i]).x, 1.15f, Camera.main.ViewportToWorldPoint(spawnPos[i]).z), Quaternion.identity, gameObject.transform);
            EnemyController enCtrlr = spawnedEnemy.transform.GetComponent<EnemyController>();
            NavMeshAgent enAgent = spawnedEnemy.transform.GetComponent<NavMeshAgent>();
            enCtrlr.target = GameObject.FindWithTag("Player").transform;

            NavMeshHit hit;
            if (!NavMesh.SamplePosition(spawnedEnemy.transform.position, out hit, 3, NavMesh.AllAreas)) 
            {
                Destroy(spawnedEnemy);
                break;
            }
            enAgent.CalculatePath(enCtrlr.target.position, path);
            if (path.status != NavMeshPathStatus.PathComplete || CalculatePathLength(enCtrlr.target.transform.position, path) > maxSpawnDistance)
            {
                Destroy(spawnedEnemy);
                //if (path.status != NavMeshPathStatus.PathComplete) spawnedEnemy.name = "Path"; ;
                //if (CalculatePathLength(enCtrlr.target.transform.position, path) > 40) spawnedEnemy.name = CalculatePathLength(enCtrlr.target.transform.position, path).ToString();
            }
            else 
            {
                enAgent.SetDestination(enCtrlr.target.position);
                break;
            }
        }

        yield return new WaitForSeconds(2);
        StartCoroutine(Spawn());
    }

    float CalculatePathLength(Vector3 targetPosition, NavMeshPath path)
    {
        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}
