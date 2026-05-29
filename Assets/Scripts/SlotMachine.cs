using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    bool bIsOffHighlight = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bIsOffHighlight)
        {
            OffHighlight();
            bIsOffHighlight = true;
        }
    }

    public Vector3 AttachCameraTransform()
    {
        return transform.Find("CameraPosition").position;
    }

    public void OnHighlight()
    {

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material currentMats = meshRenderer.materials[1];

        currentMats.SetFloat("_Scale", 1.08f);

        // 수정된 배열을 다시 렌더러에 적용
        meshRenderer.materials[1] = currentMats;

        bIsOffHighlight = false;
    }
    void OffHighlight()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material currentMats = meshRenderer.materials[1];

        currentMats.SetFloat("_Scale", 1.0f);

        // 수정된 배열을 다시 렌더러에 적용
        meshRenderer.materials[1] = currentMats;
    }

}
