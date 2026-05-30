using UnityEngine;
using TMPro; // TextMesh Pro 사용 시 필수

public class UIMachineManager : MonoBehaviour
{
    // 1. UI 컴포넌트 변수 선언
    public TextMeshProUGUI CoinText; // 최신 TextMesh Pro
    public TextMeshProUGUI CountText;
    public TextMeshProUGUI CloverTicketText;

    private PlayerMachineState PCState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PCState = GameObject.Find("Player").GetComponent<PlayerMachineState>();
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = PCState.GetCoin().ToString() + " Φ";
    }
}
