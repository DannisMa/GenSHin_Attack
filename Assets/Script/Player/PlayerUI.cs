using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private PlayerInputSystem instance;
    private PlayerControl mInputSystem;
    private new Camera camera = null;
    private Transform mCanvas;
    private Transform interactUI;
    private Transform lookAtItem = null;

    private void Awake()
    {
        mCanvas = transform.GetChild(0);
        interactUI = mCanvas.GetChild(0);
        interactUI.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = PlayerInputSystem.Instance;
        mInputSystem = instance.InputSystem;
        mInputSystem = instance.InputSystem;
    }

    // Update is called once per frame
    void Update()
    {
        if (camera == null)
            return;

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10.0f) && hit.transform.CompareTag("Item"))
        {
            interactUI.gameObject.SetActive(true);
            if (lookAtItem == null) 
            { 
                lookAtItem = hit.transform;
                mInputSystem.InteractionUI.InteractionItem.performed += lookAtItem.GetComponent<Item>().Effect;
            }
            interactUI.GetComponent<Text>().text = $"F : {hit.transform.name}";

        }
        else
        {
            interactUI.gameObject.SetActive(false);
            if(lookAtItem != null)
            {
                mInputSystem.InteractionUI.InteractionItem.performed -= lookAtItem.GetComponent<Item>().Effect;
                lookAtItem = null;
            }
        }
    }

    public void SetCamera(Camera _c)
    {
        camera = _c;
    }
}
