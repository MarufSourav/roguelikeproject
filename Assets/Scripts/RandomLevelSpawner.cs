using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomLevelSpawner : MonoBehaviour
{
    int randomVal;
    public void randomLevel() {
        randomVal = Random.Range(1, 3);
        SceneManager.LoadScene(randomVal);
    }
}
