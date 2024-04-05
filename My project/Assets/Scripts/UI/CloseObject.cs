using System.Collections;
using UnityEngine;

public class CloseObject : MonoBehaviour
{
    public void CloseAfterAnim()
    {
        StartCoroutine(DelayClose(0.25f));
    }

    private IEnumerator DelayClose(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
