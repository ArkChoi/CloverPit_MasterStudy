using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    public float interactRange = 3f; // 상호작용 가능 거리
    public LayerMask interactLayer; // 상호작용할 레이어 지정

    private Camera camera;
    SlotMachine SM = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   // 카메라는 메인카메라 값을 넣어준다.
        camera = Camera.main;
        Debug.Log(camera + "Find");

    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 정면으로 Raycast 발사
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        RaycastHit RememberHit;

        if (Physics.Raycast(ray, out hit, interactRange, interactLayer))
        {
            // 부딪힌 오브젝트의 Tag 확인
            if (hit.collider.CompareTag("interactable"))
            {
                RememberHit = hit;

                SM = hit.collider.GetComponent<SlotMachine>();
                SM.OnHighlight();

                //Debug.Log("Cheack");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("SlotMachineTouch");
                }
            }
        }
    }
}
