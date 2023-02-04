using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelSceneChanger : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneLoder.LoadScene(name);
    }
}
