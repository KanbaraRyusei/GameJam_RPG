using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour, IEncounter
{
    [SerializeField]
    private string _sceneName;

    private bool _isEncount = false;

    private void Awake()
    {
        if(_isEncount)
        {
            gameObject.SetActive(false);
        }
    }

    public void Encount()
    {
        _isEncount = true;
        SceneLoder.LoadScene(_sceneName);
    }
}
