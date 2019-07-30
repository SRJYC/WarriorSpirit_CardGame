using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    bool once = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && once)
        {
            once = false;
            StartCoroutine(FirstCoroutine());
        }
    }

    IEnumerator FirstCoroutine()
    {
        for(int i=0; i< 3;i++)
        {
            Debug.Log("First [" + i + "]");
            yield return StartCoroutine(SecondCoroutine());
        }
    }

    IEnumerator SecondCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Second [" + i + "]");
            yield return null;
        }
    }
}
