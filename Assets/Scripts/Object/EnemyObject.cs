using Animancer;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [SerializeField]
    private AnimancerComponent animancer;
    [SerializeField]
    private ClipTransition idleAnimation;
    [SerializeField]
    private ClipTransition attackAnimation;
    [SerializeField]
    private ClipTransition hitAnimation;

    [SerializeField]
    private Transform otherCharacter;

    private Transform cameraTransform;

    private void Awake()
    {
        PlayerManager.WorldPositionReset += WorldPositionReset;
    }
    private void OnDestroy()
    {
        PlayerManager.WorldPositionReset -= WorldPositionReset;
    }
    private void Start()
    {
        cameraTransform = Camera.main.transform;

        attackAnimation.Events.OnEnd = AttackEnd;
        hitAnimation.Events.OnEnd = Dead;

        animancer.Play(idleAnimation);
    }
    private void Update()
    {
        if (transform.position.x <= cameraTransform.position.x - 8)
        {
            transform.position = new Vector3(
                otherCharacter.position.x + 8f,
                transform.position.y);
        }
    }

    private void WorldPositionReset()
    {
        float relativeX = transform.position.x - PlayerManager.maxWorldX;
        transform.position = new Vector3(relativeX, transform.position.y);
    }

    private void Attack()
    {
        animancer.Play(attackAnimation);
    }
    private void AttackEnd()
    {
        // Reduce Life of player
    }
    private void Hit()
    {
        animancer.Play(hitAnimation);
    }
    private void Dead()
    {
        LeanTween.delayedCall(1, () =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            // Dead
            Hit();
        }
        else if (collision.CompareTag("Player"))
        {
            // Attack
            Attack();
        }
    }
}