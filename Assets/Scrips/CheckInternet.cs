using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CheckInternet : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CheckInteretConnection());
    }

    IEnumerator CheckInteretConnection()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if (request.error != null)
        {

        }
    }
    public void TryAgain()
    {
        
    }
}
