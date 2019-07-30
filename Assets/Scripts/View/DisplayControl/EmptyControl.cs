using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control another object to enable and disable with this.
/// Used to control an empty background
/// </summary>
public class EmptyControl : MonoBehaviour
{
    public GameObject Empty;

    private void OnEnable()
    {
        Empty.SetActive(true);
    }

    private void OnDisable()
    {
        Empty.SetActive(false);
    }
}
