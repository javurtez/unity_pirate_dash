using Animancer;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    [SerializeField]
    private AnimancerComponent animancer;
    [SerializeField]
    private ClipTransition runAnimation;
    [SerializeField]
    private ClipTransition attackAnimation;

    [SerializeField]
    private GameObject swordTriggerObject;

    [SerializeField]
    private float speed;

    private void Awake()
    {
        ControlManager.Tap += AttackStart;
        //PlayerManager.WorldPositionReset += WorldPositionReset;
    }
    private void OnDestroy()
    {
        ControlManager.Tap -= AttackStart;
        //PlayerManager.WorldPositionReset -= WorldPositionReset;
    }
    private void Start()
    {
        attackAnimation.Events.OnEnd = AttackEnd;

        animancer.Play(runAnimation);
    }
    private void Update()
    {
        if (swordTriggerObject.activeInHierarchy) return;
        transform.position += speed * Time.unscaledDeltaTime * Vector3.right;

        if (transform.position.x >= PlayerManager.maxWorldX)
        {
            transform.position = new Vector3(transform.position.x - PlayerManager.maxWorldX, transform.position.y);
            PlayerManager.Instance.ResetPosition();
        }
    }

    private void AttackStart()
    {
        swordTriggerObject.SetActive(true);
        animancer.Play(attackAnimation);
    }
    private void AttackEnd()
    {
        swordTriggerObject.SetActive(false);
        animancer.Play(runAnimation);
    }
}