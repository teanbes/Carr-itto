using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent enemyNavMesh;
    private GameObject playerRef;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject deathParticles;
    private EnemiesSpawner enemiesSpawner;

    public bool canSeePlayer;
    private Transform Player;
    private Rigidbody rb;

    [Header("Settings")]
    [SerializeField] private float radius;
    [Range(0, 360)]
    [SerializeField] private float angle;
    [SerializeField] private float rangeOfAttack;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float attackrate = 1.5f;

    [Header("Health")]
    [SerializeField] private Health monsterHealth;
    [SerializeField] private  bool monsterDead;


    private float lastAttackTime = 0f;
    private int currentEnemiesAmount;

    RaycastHit hit;
    Vector3 direction;

    // Animation Vars"
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private readonly int GetHitHash = Animator.StringToHash("Armature|Take_Damage_1");
    private readonly int DieHash = Animator.StringToHash("Armature|Die");
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        if (playerRef )
            Player = playerRef.transform;

        enemiesSpawner = FindObjectOfType<EnemiesSpawner>();
        currentEnemiesAmount = enemiesSpawner.currentEnemiesAmount; 
        rb = GetComponent<Rigidbody>();
        animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        monsterDead = false;
        canSeePlayer = false;
 
              
        if (maxDistance <= 0f)
            maxDistance = 10f;


        StartCoroutine(FindPlayer());
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;
        Vector3 rayCastOrigin = transform.position + new Vector3(0f, -0.5f, 0f);
        // to make raycast only affect on a layer 
        int layermask = 1 << 6;
        bool tempraycast = Physics.Raycast(rayCastOrigin, transform.forward, out hit, maxDistance, layermask);


        if (canSeePlayer == true && monsterDead == false)
        {
            if (!Player) { return; }
            
            // Player and enemy positions vars
            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
            Vector3 directionToTarget = (Player.position - transform.position).normalized;
            float playerElevation = Player.position.y;

            // Follow player if can see and is not in range of attack
            if (distanceToPlayer >= rangeOfAttack && distanceToPlayer <= radius && playerElevation < 10)
            {
                animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, Time.deltaTime);
                enemyNavMesh.SetDestination(Player.position);
                    
            }
            else if (distanceToPlayer >= rangeOfAttack && distanceToPlayer <= radius && playerElevation > 10)
            {
                float randomPos = Random.Range(-1, -3);
                Vector3 walkAway = new Vector3(randomPos, 0, randomPos);
                animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, Time.deltaTime);
                enemyNavMesh.SetDestination(transform.position - walkAway);
            }
            else if (distanceToPlayer <= rangeOfAttack)
            {
                if (Time.time - lastAttackTime >= attackrate)
                {
                    transform.rotation = Quaternion.LookRotation(directionToTarget);
                    enemyNavMesh.ResetPath();
                    enemyNavMesh.velocity = Vector3.zero;
                    animator.CrossFadeInFixedTime(AttackHash, CrossFadeDuration);

                    lastAttackTime = Time.time;
                }
            }
            
        }
        if (canSeePlayer == false && monsterDead == false)
        {
            enemyNavMesh.ResetPath();
            enemyNavMesh.velocity = Vector3.zero;
            animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
            animator.SetFloat(SpeedHash, 0f, AnimatorDampTime, Time.deltaTime);
        }
        
    }

    private void OnEnable()
    {
        monsterHealth.OnTakeDamage += HandleTakeDamage;
        monsterHealth.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        monsterHealth.OnTakeDamage -= HandleTakeDamage;
        monsterHealth.OnDie -= HandleDie;
    }

    // Delays player finding in case of instatntiating at begining of runtime 
    private IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(1f); // wait for 1 second

        playerRef = GameObject.FindGameObjectWithTag("Player");
        if (playerRef)
            Player = playerRef.transform;

    }

    // Runs FieldOfViewCheck() for performance 
    private IEnumerator FOVRoutine()
    {

        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    // Checks if player is in field of view
    private void FieldOfViewCheck()
    {
        // Check for colitions in the radius of sphere cast
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        // If there is at least 1 collition player is in field of view
        if (rangeChecks.Length != 0 && monsterDead == false)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distancetoTarget = Vector3.Distance(transform.position, target.position);
                

                if (distancetoTarget <= maxDistance && !Physics.Raycast(transform.position, directionToTarget, distancetoTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else
            canSeePlayer = false;
    }

    private void HandleTakeDamage()
    {
        animator.CrossFadeInFixedTime(GetHitHash, CrossFadeDuration);
        StartCoroutine(AnimationDelay());
    }

    private void HandleDie()
    {
        monsterDead = true;
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        AudioManager.Instance.Play("BugSquished");
        Destroy(gameObject);
        enemiesSpawner.currentEnemiesAmount--;
        GameManager.instance.enemiesDestroyed++;

    }

    private IEnumerator AnimationDelay()
    {
        float delay = 2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
        }
    }

    // not used yet
    private void CarElevation()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        Vector3 directionToTarget = (Player.position - transform.position).normalized;

        float playerElevation = Player.position.y;

        if (playerElevation > 10f)
        {

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}