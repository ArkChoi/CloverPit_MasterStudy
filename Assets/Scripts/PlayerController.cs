using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;
	public Boundary boundary;

    public float interactRange = 3f; // 상호작용 가능 거리
    public LayerMask interactLayer; // 상호작용할 레이어 지정

    private Camera camera;

    void Start()
    {   // 카메라는 메인카메라 값을 넣어준다.
        camera = GetComponentInChildren<Camera>(); ;
        Debug.Log(camera + "Find");
    }

    void Update()
	{
        // 플레이어 정면으로 Raycast 발사
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactLayer))
        {
            Debug.Log("물체감지");
            // 부딪힌 오브젝트의 Tag 확인
            if (hit.collider.CompareTag("interactable"))
            {
                SlotMachine SM = hit.collider.GetComponent<SlotMachine>();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    SM.OnHighlight();
                    Debug.Log("SlotMachineTouch");
                }
            }
        }
    }

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().linearVelocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3 
			(
				Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
				);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().linearVelocity.x * -tilt);
	}
}