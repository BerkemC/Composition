using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralGUIControl : MonoBehaviour
{
    [SerializeField]
    private bool isMenuCube=false;

    private void Start()
    {
        if(isMenuCube)
        {
            GetComponent<MeshRenderer>().material.color = new Vector4(Random.value, Random.value, Random.value, 1);
        }
    }

    public void ReloadLevel()
   {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    public void ChangeText(string ntext)
    {
        GetComponent<Text>().text = ntext;
    }

    public void LoadNexLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
