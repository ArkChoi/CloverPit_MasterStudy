using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private SlotMachine SM = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = GameObject.Find("SlotMachine").GetComponent<SlotMachine>();
    }

    // 버튼 클릭 시 실행할 함수
    public void OnButtonClick()
    {
        SM.OnRun();
    }
}