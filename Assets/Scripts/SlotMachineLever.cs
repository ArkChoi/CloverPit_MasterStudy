using UnityEngine;

public class SlotMachineLever : MonoBehaviour
{
    private bool bIsOffHighlight = false;
    private SlotMachine SM = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = GameObject.Find("SlotMachine").GetComponent<SlotMachine>();
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

    public void OnSpinning()
    {
        SM.Run();
    }
}
