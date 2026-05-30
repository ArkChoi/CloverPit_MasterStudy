using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextHighlit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GetComponent<TextMeshPro>().color = Color.white;
        }
        else
        {
            GetComponent<TextMeshPro>().color = Color.black;
        }
    }
}
