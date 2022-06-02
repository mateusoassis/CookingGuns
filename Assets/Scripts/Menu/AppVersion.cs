using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppVersion : MonoBehaviour
{
    public string prefix;
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = prefix + Application.version.ToString();
    }
}
