using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerInputSystem inputInstance;
    private PlayerControl mInputSystem;
    
    private Camera playerCam = null;
    [SerializeField] private Transform playerUI;
    private Transform lookAtItem = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inputInstance = PlayerInputSystem.Instance;
        mInputSystem = inputInstance.InputSystem;
        mInputSystem.Pause.Esc.performed += Esc;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCam != null)
        {
            Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10.0f) && hit.transform.CompareTag("Item"))
            {
                playerUI.gameObject.SetActive(true);
                if (lookAtItem == null)
                {
                    lookAtItem = hit.transform;
                    mInputSystem.InteractionUI.InteractionItem.performed += lookAtItem.GetComponent<Item>().Effect;
                }
                playerUI.GetComponent<TMPro.TMP_Text>().text = $"F : {hit.transform.name}";

            }
            else
            {
                playerUI.gameObject.SetActive(false);
                if (lookAtItem != null)
                {
                    mInputSystem.InteractionUI.InteractionItem.performed -= lookAtItem.GetComponent<Item>().Effect;
                    lookAtItem = null;
                }
            }
        }
    }

    private void Esc(InputAction.CallbackContext obj)
    {
        if (Cursor.visible)
        {
            Debug.Log("Âê©w·Æ¹«");
            inputInstance.PlayingMode();
        }
        else
        {
            Debug.Log("¸Ñ©ñ·Æ¹«");
            inputInstance.ResumeMode();
        }
    }

    public void SetPlayerCamera(Camera _cam)
    {
        playerCam = _cam;
    }
}
