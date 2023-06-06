using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void ValueHandler();
    public static event ValueHandler WorldPositionReset;

    public const float maxWorldX = 6;

    private static PlayerManager instance;

    public static PlayerManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public void ResetPosition()
    {
        WorldPositionReset?.Invoke();
    }
}