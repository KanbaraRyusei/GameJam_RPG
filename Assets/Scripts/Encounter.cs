using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour, IEncounter
{
    [SerializeField]
    private string _sceneName;

    private bool _isEncount = false;

    public void Encount()
    {
        _isEncount = true;
    }
}
