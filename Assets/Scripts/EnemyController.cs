using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    [SerializeField]
    private Material dmgdMaterial;
    public float lookRadius = 10f;
    private int health = 2;
    private bool attackingRn;
    private bool startFollowing;
    private bool isFollowing = false;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            if (!isFollowing)
                startFollowing = true;

            isFollowing = true;

            agent.SetDestination(target.position);
            
            if (startFollowing)
            {
                var audioSource = GetComponent<AudioSource>();
                
                if (audioSource)
                    audioSource.Play();

                startFollowing = false;
            }

            if (distance <= agent.stoppingDistance && !attackingRn) 
            {
                //Rotate
                //Attack
                StartCoroutine(Attack());
            }
        }
        else
        {
            isFollowing = false;
        }
    }

    private IEnumerator Attack() 
    {
        attackingRn = true;
        target.GetComponent<HealthScript>().Damage(1);
        yield return new WaitForSeconds(1);
        attackingRn = false;
    }

    public void Damage(int amount) 
    {
        health -= amount;
        StartCoroutine(ChangeMaterial());

        if (health <= 0) Destroy(gameObject);
    }

    private IEnumerator ChangeMaterial() 
    {
        var temp = gameObject.GetComponentInChildren<MeshRenderer>().material;
        gameObject.GetComponentInChildren<MeshRenderer>().material = dmgdMaterial;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponentInChildren<MeshRenderer>().material = temp;
    }
}
