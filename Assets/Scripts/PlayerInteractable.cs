using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInteractable : MonoBehaviour
{
    public float interactRange = 3f; // 상호작용 가능 거리
    public LayerMask interactLayer; // 상호작용할 레이어 지정
    public LayerMask groundLayer; // 바닥을 감지할 레이어

    private Camera camera;
    private Vector3 PlayerTransform;
    private SlotMachine SM = null;
    private SlotMachineLever SML = null;
    private GameObject FPSUI = null;

    private bool bIsPlayGame = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   // 카메라는 메인카메라 값을 넣어준다.
        camera = Camera.main;
        Debug.Log(camera + "Find");

        FPSUI = GameObject.Find("CanvasFPS");
        Debug.Log(FPSUI + "Find");
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsPlayGame)
        {
            //마우스 고정 해제
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 메인 카메라에서 마우스 위치로 향하는 광선 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 광선이 groundLayer와 충돌했는지 확인
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // 부딪힌 오브젝트의 Tag 확인
                if (hit.collider.CompareTag("Lever"))
                {
                    SML = hit.collider.GetComponent<SlotMachineLever>();
                    SML.OnHighlight();

                    if (Input.GetMouseButtonDown(0))
                    {
                        SML.OnSpinning();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {

            }
            // 아무 키나 처음 누르는 순간 감지
            else if (Input.anyKeyDown)
            {
                FPSCamera();
            }
        }
        else
        {
            //마우스 화면 중앙 고정
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

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

                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        Vector3 CameraPosition = SM.AttachCameraTransform();
                        AttachCamera(CameraPosition);
                        //Debug.Log("SlotMachineTouch");
                    }
                }
            }
        }
    }

    void AttachCamera(Vector3 InCameraTransform)
    {
        PlayerTransform = transform.position;
        transform.position = InCameraTransform;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<FirstPersonController>().enabled = false;
        FPSUI.SetActive(false);

        bIsPlayGame = true;
    }

    void FPSCamera()
    {
        transform.position = PlayerTransform;
        GetComponent<CharacterController>().enabled = true;
        GetComponent<FirstPersonController>().enabled = true;
        FPSUI.SetActive(true);

        bIsPlayGame = false;
    }
}
