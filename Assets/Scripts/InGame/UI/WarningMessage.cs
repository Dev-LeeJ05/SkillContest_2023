using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningMessage : MonoBehaviour
{
    public TextMeshProUGUI Message;
    
    public IEnumerator SetMessage(string message, float wait)
    {
        Message.text = $"¡Ø {message} ¡Ø";
        gameObject.SetActive(true);
        yield return new WaitForSeconds(wait);
        gameObject.SetActive(false);
    }
}
