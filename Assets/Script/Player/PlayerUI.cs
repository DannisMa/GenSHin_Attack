using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private new Camera camera = null;
    private Transform mCanvas;
    private Transform interactUI;
    private PlayerControl playerControl;
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
        playerControl = new PlayerControl();
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
            playerControl.InteractionUI.Enable();
            interactUI.gameObject.SetActive(true);
            if (lookAtItem == null) 
            { 
                lookAtItem = hit.transform;
                playerControl.InteractionUI.InteractionItem.performed += lookAtItem.GetComponent<Item>().Effect;
            }
            interactUI.GetComponent<Text>().text = $"F : {hit.transform.name}";

        }
        else
        {
            playerControl.InteractionUI.Disable();
            interactUI.gameObject.SetActive(false);
            if(lookAtItem != null)
            {
                playerControl.InteractionUI.InteractionItem.performed -= lookAtItem.GetComponent<Item>().Effect;
                lookAtItem = null;
            }
        }
    }

    public void SetCamera(Camera _c)
    {
        camera = _c;
    }
}
