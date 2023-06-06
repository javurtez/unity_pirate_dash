using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    [SerializeField]
    private InputActionReference playerAction;

    public delegate void ControlHandler();
    public static event ControlHandler Tap;

    private static ControlManager instance;

    public static ControlManager Instance => instance;

    private void Awake()
    {
        instance = this;

        //PlayerManager.CallGameOver += GameOver;

        playerAction.action.Enable();
    }
    private void OnDestroy()
    {
        //PlayerManager.CallGameOver -= GameOver;
        playerAction.action.performed -= PlayerTap;

        playerAction.action.Disable();
    }
    private void Start()
    {
        playerAction.action.performed += PlayerTap;
    }

    private void GameOver()
    {
        playerAction.action.Disable();
    }

    private void PlayerTap(InputAction.CallbackContext context)
    {
        Tap?.Invoke();
    }
}