using UnityEngine;

public class PlayerMachineState : MonoBehaviour
{
    private int Coin = 0;
    private int CloverTicket = 0;
    private int Count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCoin(int InCoin)
    {
        Coin = InCoin;
    }

    public int GetCoin()
    {
        return Coin;
    }

    public void AddCoin(int InCoin)
    {
        Coin += InCoin;
    }

    public void UseCoin()
    {
        Coin--;
    }

    public void SetCloverTicket(int InCloverTicket)
    {
        CloverTicket = InCloverTicket;
    }

    public int GetCloverTicket()
    {
        return CloverTicket;
    }

    public void SetCount(int InCount)
    {
        Count = InCount;
    }

    public int GetCount()
    {
        return Count;
    }
}
