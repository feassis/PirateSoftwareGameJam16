using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Utilities.StateMachine;

public class EnemyBase : MonoBehaviour, IDamageble
{
    [SerializeField] protected float MaxHealth = 100;
    [SerializeField] protected MeshRenderer meshRenderer;
    [SerializeField] protected Material damageMaterial;
    [SerializeField] protected CharacterController characterController;
    [SerializeField] protected Animator animator;
    [SerializeField] protected PlayerDetectionTrigglerSphere detectionTrigger;
    [SerializeField] protected PlayerDetectionTrigglerSphere attackTrigger;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float attackRange = 3;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected ForceReciever forceReciever;
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected float rotationSpeed = 5;
    

    [field: SerializeField] public float AttackCoolDown { get; protected set; } = 1.5f;

    public GenericStateMachine<EnemyBase> stateMachine;
    private Material defaultMaterial;

    private float currentHealth;

    private const string IDLE_ANIMATION = "idle";
    private const string WALK_ANIMATION = "walking";
    private const string ATTACK_ANIMATION = "attack";

    public PlayerView target { get; protected set;}
    public bool IsOnAttackRange { get; protected set; }

    protected virtual void Awake()
    {
        defaultMaterial = meshRenderer.material;
        currentHealth = MaxHealth;
        detectionTrigger.Setup(detectionRange);
        detectionTrigger.OnPlayerEnterRange += OnPlayerEnterRange;
        detectionTrigger.OnPlayerExitRange += OnPlayerExitRange;

        attackTrigger.Setup(attackRange);
        attackTrigger.OnPlayerEnterRange += OnAttackRangeEnter;
        attackTrigger.OnPlayerExitRange += OnAttackRangeExit;

        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
    }

    private void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }


    private void OnAttackRangeEnter(PlayerView view)
    {
        IsOnAttackRange = true;
    }

    private void OnAttackRangeExit(PlayerView view)
    {
        IsOnAttackRange = false;
    }

    private void OnPlayerEnterRange(PlayerView view)
    {
        target = view;
    }

    private void OnPlayerExitRange(PlayerView view)
    {
       target = null;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 finalMotion = motion + forceReciever.Movement;
        finalMotion *= deltaTime;

        characterController.Move(finalMotion);
    }

    public void MoveTo(Vector3 moveToPosition, float deltaTime)
    {
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.destination = moveToPosition;

            Move(navMeshAgent.desiredVelocity.normalized * moveSpeed, deltaTime);


        }
        navMeshAgent.velocity = characterController.velocity;

    }

    public void RotateTowardsDirection(Vector3 direction)
    {
        // Calculate the target rotation based on movement direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate the character towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void RotatesToMovePosition()
    {
        RotateTowardsDirection(characterController.velocity.normalized);
    }

    public void Heal(float heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);
        

        if(currentHealth == 0)
        {
            Die();
        }
        else
        {
            TakeDamageVisuals();
        }
    }

    protected virtual void TakeDamageVisuals()
    {
        StartCoroutine(DamageVisuals());
    }

    protected virtual IEnumerator DamageVisuals()
    {
        meshRenderer.material = damageMaterial;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material = defaultMaterial;
    }

    protected virtual void Die()
    {
        meshRenderer.material = damageMaterial;
        meshRenderer.transform.localPosition = new Vector3(0, 0.5f, 0);
        meshRenderer.transform.localRotation = Quaternion.Euler(90, 0, 0);
        characterController.detectCollisions = false;
    }

    public void PlayAnimation(EnemyAnimations anim)
    {
        switch(anim)
        {
            case EnemyAnimations.Idle:
                animator.CrossFadeInFixedTime(IDLE_ANIMATION,0.1f);
                break;
            case EnemyAnimations.Walking:
                animator.CrossFadeInFixedTime(WALK_ANIMATION, 0.1f);
                break;
            case EnemyAnimations.Attack:
                animator.CrossFadeInFixedTime(ATTACK_ANIMATION, 0.1f);
                CoroutineManager.Instance.WaitForAnimation(animator, ATTACK_ANIMATION, () => animator.CrossFadeInFixedTime(IDLE_ANIMATION, 0.1f));
                break;
            default:
                break;
        }
    }

    public void ResetNavAgent()
    {
        navMeshAgent.ResetPath();
        navMeshAgent.velocity = Vector3.zero;
        Move(Time.deltaTime);
    }

    public void FacePlayer()
    {
        if (target == null)
        {
            return;
        }

        Vector3 facinngDir = target.transform.position - transform.position;

        facinngDir.y = 0;

        transform.rotation = Quaternion.LookRotation(facinngDir);
    }

    public virtual State GetStateAfterPlayerDetection()
    {
        return State.CHASING;
    }
}
