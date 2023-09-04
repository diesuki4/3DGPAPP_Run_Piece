using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualButton : MonoBehaviour
{
    void OnDisable()
    {
        GameManager.Instance.manualOK = true;
    }
}
