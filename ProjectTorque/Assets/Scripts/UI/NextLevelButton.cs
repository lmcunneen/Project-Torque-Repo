using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public void NextLevelInBuildIndex()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(buildIndex + 1);
    }
}
