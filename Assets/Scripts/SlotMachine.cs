using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    //슬롯머신 상태 관련 변수
    private bool bIsOffHighlight = false;
    private bool bIsRun = false;
    private GameObject FirstUI = null;
    private GameObject MainGameUI = null;

    //슬롯머신 게임 관련 변수
    public Sprite[] Sprites; // 교체할 새로운 스프라이트
    private int[,] Map = new int[5, 3];
    private int MaxNumber = 67;
    private string[] UINames = { 
        "Image00", "Image01", "Image02", "Image03", "Image04",
        "Image10", "Image11", "Image12", "Image13", "Image14",
        "Image20", "Image21", "Image22", "Image23", "Image24"};
    //기록은 모두 기본을 기준으로 기록
    //0~12 13개
    private int Cherry   = 13;
    //13~25 13개
    private int Lemon    = 26;
    //26~35 10개
    private int Clover   = 36;
    //36~45 10개
    private int Bell     = 46;
    //45~53 8개
    private int Diamond  = 54;
    //54~61 8개
    private int Treasure = 62;
    //62~66 5개
    private int Seven    = 67;

    //슬롯머신 판정 관련 변수

    //
    private PlayerMachineState PCState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FirstUI = GameObject.Find("CanvasFirst");
        Debug.Log(FirstUI + "Find");
        //FirstUI.SetActive(false);

        MainGameUI = GameObject.Find("CanvasMainGame");
        Debug.Log(MainGameUI + "Find");
        //MainGameUI.SetActive(false);

        PCState = GameObject.Find("Player").GetComponent<PlayerMachineState>();

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsRun)
        {
            
        }

        if (!bIsOffHighlight)
        {
            OffHighlight();
            bIsOffHighlight = true;
        }
    }

    void Init()
    {
        FirstUI.SetActive(false);
        MainGameUI.SetActive(false);
    }

    public Vector3 AttachCameraPosition()
    {
        FirstUI.SetActive(true);
        return transform.Find("CameraPosition").position;
    }

    public Quaternion AttachCameraRotation()
    {
        FirstUI.SetActive(true);
        return transform.Find("CameraPosition").rotation;
    }

    public void OnRun()
    {
        FirstUI.SetActive(false);
        MainGameUI.SetActive(true);

        bIsRun = true;
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

    public void OffRun()
    {
        bIsRun = false;

        Init();
    }
    void OffHighlight()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material currentMats = meshRenderer.materials[1];

        currentMats.SetFloat("_Scale", 1.0f);

        // 수정된 배열을 다시 렌더러에 적용
        meshRenderer.materials[1] = currentMats;
    }

    public void Run()
    {
        PCState.UseCoin();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                switch (Random.Range(0, MaxNumber))
                {
                    //체리
                    case int n when n < Cherry:
                        Map[i, j] = 1;
                        break;
                    //레몬
                    case int n when n < Lemon:
                        Map[i, j] = 2;
                        break;
                    //클로버
                    case int n when n < Clover:
                        Map[i, j] = 3;
                        break;
                    //종
                    case int n when n < Bell:
                        Map[i, j] = 4;
                        break;
                    //다이아몬드
                    case int n when n < Diamond:
                        Map[i, j] = 5;
                        break;
                    //보물
                    case int n when n < Treasure:
                        Map[i, j] = 8;
                        break;
                    //세븐
                    case int n when n < Seven:
                        Map[i, j] = 7;
                        break;

                    default:
                        Map[i, j] = 0;
                        break;

                }
            }
        }

        //Map[0, 0] = 0; Map[1, 0] = 0; Map[2, 0] = 2; Map[3, 0] = 0; Map[4, 0] = 0;
        //Map[0, 1] = 0; Map[1, 1] = 2; Map[2, 1] = 0; Map[3, 1] = 2; Map[4, 1] = 0;
        //Map[0, 2] = 2; Map[1, 2] = 2; Map[2, 2] = 2; Map[3, 2] = 2; Map[4, 2] = 2;


        Print();
        Result();
    }

    void Print()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int Number = Map[i, j];
                GameObject.Find(UINames[j*5 + i]).GetComponent<Image>().sprite = Sprites[Number];
            }
        }

    }

    void Result()
    {
        bool bIsJackpot = JackpotCheack();
        bool bIsEye = EyeCheack(bIsJackpot);
        bool bIsHeaven = HeavenCheack(bIsJackpot);
        bool bIsGround = GroundCheack(bIsJackpot);
        bool bIsJag = JagCheack(bIsHeaven);
        bool bIsJig = JigCheack(bIsGround);

        bool bIsXL0 = WidthXLCheack(0, bIsHeaven);
        bool bIsXL1 = WidthXLCheack(1, false);
        bool bIsXL2 = WidthXLCheack(2, bIsGround);

        bool bIsL0 = WidthLCheack(0, bIsHeaven, bIsXL0);
        bool bIsL1 = WidthLCheack(1, false, bIsXL1);
        bool bIsL2 = WidthLCheack(2, bIsGround, bIsXL2);

        if (bIsJackpot)
        {
            int Value = ChangeValue(Map[0, 0]);
            Value *= 10;
            PCState.AddCoin(Value);
        }

        if (bIsEye)
        {
            int Value = ChangeValue(Map[2, 0]);
            Value *= 8;
            PCState.AddCoin(Value);
        }

        if (bIsHeaven)
        {
            int Value = ChangeValue(Map[2, 0]);
            Value *= 7;
            PCState.AddCoin(Value);
        }
        if (bIsGround)
        {
            int Value = ChangeValue(Map[2, 0]);
            Value *= 7;
            PCState.AddCoin(Value);
        }

        if (bIsJag)
        {
            int Value = ChangeValue(Map[2, 2]);
            Value *= 4;
            PCState.AddCoin(Value);
        }
        if (bIsJig)
        {
            int Value = ChangeValue(Map[2, 0]);
            Value *= 4;
            PCState.AddCoin(Value);
        }

        if (bIsXL0)
        {
            int Value = ChangeValue(Map[2, 0]);
            Value *= 3;
            PCState.AddCoin(Value);
        }
        if (bIsXL1)
        {
            int Value = ChangeValue(Map[2, 1]);
            Value *= 3;
            PCState.AddCoin(Value);
        }
        if (bIsXL2)
        {
            int Value = ChangeValue(Map[2, 2]);
            Value *= 3;
            PCState.AddCoin(Value);
        }

        if (bIsL0)
        {
            int Value = ChangeValue(Map[2, 0]);
            Value *= 2;
            PCState.AddCoin(Value);
        }
        if (bIsL1)
        {
            int Value = ChangeValue(Map[2, 1]);
            Value *= 2;
            PCState.AddCoin(Value);
        }
        if (bIsL2)
        {
            int Value = ChangeValue(Map[2, 2]);
            Value *= 2;
            PCState.AddCoin(Value);
        }

        if (DiagonalCheack(0, bIsJag))
        {
            int Value = ChangeValue(Map[0, 0]);
            Value *= 1;
            PCState.AddCoin(Value);
        }
        if(DiagonalCheack(1, false))
        {
            int Value = ChangeValue(Map[1, 0]);
            Value *= 1;
            PCState.AddCoin(Value);
        }
        if(DiagonalCheack(2, bIsJig))
        {
            {
                int Value = ChangeValue(Map[2, 0]);
                Value *= 1;
                PCState.AddCoin(Value);
            }
        }
        if(DiagonalCheack(3, false))
        {
            int Value = ChangeValue(Map[3, 0]);
            Value *= 1;
            PCState.AddCoin(Value);
        }
        if(DiagonalCheack(4, bIsJag))
        {
            int Value = ChangeValue(Map[4, 0]);
            Value *= 1;
            PCState.AddCoin(Value);
        }

        for (int i = 0; i < 5; i++)
        {
            if(LengthCheack(i, bIsEye))
            {
                int Value = ChangeValue(Map[i, 0]);
                Value *= 1;
                PCState.AddCoin(Value);
            }
        }

        if(WidthCheack(0, bIsHeaven, bIsXL0, bIsL0))
        {
            int Value = ChangeValue(Map[2, 0]);
            Value *= 1;
            PCState.AddCoin(Value);
        }
        if(WidthCheack(1, false, bIsXL1, bIsL1))
        {
            int Value = ChangeValue(Map[2, 1]);
            Value *= 1;
            PCState.AddCoin(Value);
        }
        if(WidthCheack(2, bIsGround, bIsXL2, bIsL2))
        {
            int Value = ChangeValue(Map[2, 2]);
            Value *= 1;
            PCState.AddCoin(Value);
        }

    }

    bool JackpotCheack()
    {
        int CheackNumber = Map[0, 0];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (CheackNumber != Map[i, j])
                {
                    return false;
                }
            }
        }

        Debug.Log("Jackpot");
        return true;
    }

    bool EyeCheack(bool bIsInJackpot)
    {
        if (bIsInJackpot)
        {
            Debug.Log("Eye");
            return true;
        }

        int CheackNumber = Map[1, 0];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == 0 && j == 0 || i == 4 && j == 0 || i == 0 && j == 2 || i == 4 && j == 2 || i == 2 && j == 1)
                {
                    continue;
                }
                else if (CheackNumber != Map[i, j])
                {
                    return false;
                }
            }
        }

        Debug.Log("Eye");
        return true;
    }

    bool HeavenCheack(bool bIsInJackpot)
    {
        if (bIsInJackpot)
        {
            Debug.Log("Heaven");
            return true;
        }

        int CheackNumber = Map[0, 0];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (j == 0)
                {
                    if (CheackNumber != Map[i, j])
                    {
                        return false;
                    }
                }
                else if (j == 1)
                {
                    if (i == 1 || i == 3)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
                else if (j == 2)
                {
                    if (i == 2)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        Debug.Log("Heaven");
        return true;
    }

    bool GroundCheack(bool bIsInJackpot)
    {
        if (bIsInJackpot)
        {
            Debug.Log("Ground");
            return true;
        }

        int CheackNumber = Map[0, 2];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (j == 2)
                {
                    if (CheackNumber != Map[i, j])
                    {
                        return false;
                    }
                }
                else if (j == 1)
                {
                    if (i == 1 || i == 3)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
                else if (j == 0)
                {
                    if (i == 2)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        Debug.Log("Ground");
        return true;
    }

    bool JagCheack(bool bIsInHeaven)
    {
        if (bIsInHeaven)
        {
            return false;
        }

        int CheackNumber = Map[0, 0];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (j == 0)
                {
                    if (i == 0 || i == 4)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
                else if (j == 1)
                {
                    if (i == 1 || i == 3)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
                else if (j == 2)
                {
                    if (i == 2)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        Debug.Log("Jag");
        return true;
    }

    bool JigCheack(bool bIsInGround)
    {
        if (bIsInGround)
        {
            return false;
        }

        int CheackNumber = Map[0, 2];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (j == 2)
                {
                    if (i == 0 || i == 4)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
                else if (j == 1)
                {
                    if (i == 1 || i == 3)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
                else if (j == 0)
                {
                    if (i == 2)
                    {
                        if (CheackNumber != Map[i, j])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        Debug.Log("Jig");
        return true;
    }

    //1행 체크
    bool WidthXLCheack(int Inj, bool bIsInHorG)
    {
        if (bIsInHorG)
        {
            return false;
        }

        int CheackNumber = Map[0, Inj];

        for (int i = 0; i < 5; i++)
        {
            if (CheackNumber != Map[i, Inj])
            {
                return false;
            }
        }

        Debug.Log("WidthXL");
        return true;
    }

    //1행 체크
    bool WidthLCheack(int Inj, bool bIsInHorG, bool bIsInXL)
    {
        if (bIsInHorG)
        {
            return false;
        }

        if (bIsInXL)
        {
            return false;
        }

        int CheackNumber = Map[0, Inj];

        if (CheackNumber == Map[1, Inj])
        {
            for (int i = 0; i < 4; i++)
            {
                if (CheackNumber != Map[i, Inj])
                {
                    return false;
                }
            }
        }
        else
        {
            CheackNumber = Map[1, Inj];

            for (int i = 1; i < 5; i++)
            {
                if (CheackNumber != Map[i, Inj])
                {
                    return false;
                }
            }
        }

        Debug.Log("WidthL");
        return true;
    }

    //1회 체크
    bool DiagonalCheack(int Ini, bool bIsInJigorJag)
    {
        if (bIsInJigorJag)
        {
            return false;
        }

        int J = 0;
        int CheackNumber = Map[Ini, J];

        if (Ini < 2)
        {
            if (CheackNumber == Map[Ini+1, J+1] && CheackNumber == Map[Ini + 2, J + 2])
            {
                Debug.Log("Diagonal");
                return true;
            }
        }
        else if (Ini == 2)
        {
            if (CheackNumber == Map[Ini + 1, J + 1] && CheackNumber == Map[Ini + 2, J + 2] || CheackNumber == Map[Ini - 1, J + 1] && CheackNumber == Map[Ini - 2, J + 2])
            {
                Debug.Log("Diagonal");
                return true;
            }
        }
        else
        {
            if (CheackNumber == Map[Ini - 1, J + 1] && CheackNumber == Map[Ini - 2, J + 2])
            {
                Debug.Log("Diagonal");
                return true;
            }
        }

        return false;
    }

    //1열 체크
    bool LengthCheack(int Ini, bool bIsInEye)
    {
        if (bIsInEye && Ini == 1 || Ini == 3)
        {
            return false;
        }

        int CheackNumber = Map[Ini, 0];

        for (int j = 0; j < 3; j++)
        {
            if (CheackNumber != Map[Ini, j])
            {
                return false;
            }
        }

        Debug.Log("Length");
        return true;
    }

    //1행 체크
    bool WidthCheack(int Inj, bool bIsInHorG, bool bIsInXL, bool bIsInL)
    {
        if (bIsInHorG || bIsInXL || bIsInL)
        {
            return false;
        }

        int CheackNumber = Map[0, Inj];

        if (CheackNumber == Map[1, Inj])
        {
            for (int i = 0; i < 3; i++)
            {
                if (CheackNumber != Map[i, Inj])
                {
                    return false;
                }
            }
        }
        else if (Map[1, Inj] == Map[2, Inj])
        {
            CheackNumber = Map[1, Inj];

            for (int i = 1; i < 4; i++)
            {
                if (CheackNumber != Map[i, Inj])
                {
                    return false;
                }
            }
        }
        else
        {
            CheackNumber = Map[2, Inj];

            for (int i = 2; i < 5; i++)
            {
                if (CheackNumber != Map[i, Inj])
                {
                    return false;
                }
            }
        }

        Debug.Log("Width");
        return true;
    }

    int ChangeValue(int InNumber)
    {
        int Value = 0;
        switch (InNumber)
        {
            //체리
            case 1:
                Value = 2;
                break;
            //레몬
            case 2:
                Value = 2;
                break;
            //클로버
            case 3:
                Value = 3;
                break;
            //종
            case 4:
                Value = 3;
                break;
            //다이아몬드
            case 5:
                Value = 5;
                break;
            //보물
            case 8:
                Value = 5;
                break;
            //세븐
            case 7:
                Value = 7;
                break;

            default:
                Value = 0;
                break;

        }

        return Value;
    }
}
