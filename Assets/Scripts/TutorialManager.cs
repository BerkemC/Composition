using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    bool once = false;
    [SerializeField]
    private GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        StopAllCoroutines();
        if(SceneManager.GetActiveScene().buildIndex.Equals(1))
        {
            StartCoroutine(ChangeInfoText("To collect and use them, just go touch them. You will see your resource count on bottom right.", 5f));
            StartCoroutine(ChangeInfoText("Yeap, use W A S D keys to move.", 10f));
        }
       
        else if (SceneManager.GetActiveScene().buildIndex.Equals(3))
        {
            StartCoroutine(ChangeInfoText("A hammer requires at least 9 resources, but the more resources you have the more powerful it will be", 10f));
        }
        else if (SceneManager.GetActiveScene().buildIndex.Equals(4))
        {
            StartCoroutine(ChangeInfoText("The more you hold the click, the more the gun will be charged. So, more projectiles!!", 10f));
        }
        else if (SceneManager.GetActiveScene().buildIndex.Equals(6))
        {
            wall.SetActive(false);
            StartCoroutine(ChangeInfoText("If you have more than 9 resources, you can form a shield by holding right click to protect yourself!", 7f));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:Tut1Check();break;
            case 2:Tut2Check();break;
            case 3:Tut3Check();break;
            case 4:Tut4Check(); break;
            case 5:Tut5Check(); break;
            case 6:Tut6Check();break;
        }
    }

    IEnumerator ChangeInfoText(string nText,float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = nText;
        yield return null;
    }

    IEnumerator LoadNextTutorial(float time)
    {

        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }

    IEnumerator PutWall(float time)
    {

        yield return new WaitForSeconds(time);
        wall.SetActive(true);
        yield return null;
    }

    private void Tut1Check()
    {
        if(GameObject.Find("Captures").transform.childCount == 20)
        {
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "Good job!! You collected them all. Let's move on with the next tutorial!. Loading in 8";
            StartCoroutine(LoadNextTutorial(8));
        }
    }

    private void Tut2Check()
    {
        if (!GameObject.Find("ShootingCube"))
        {
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "Good job!! Each destructable object generate number of resources equal to its health. Use more to build more!Next Tutorial in 8";
            StartCoroutine(LoadNextTutorial(8));
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !once)
        {
            once = true;
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "After firing your resources you can take them back by touching them again!";
        }
    }

    private void Tut3Check()
    {
        if (!GameObject.Find("ShootingCube"))
        {
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "Good job!! Next Tutorial in 8";
            StartCoroutine(LoadNextTutorial(8));
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "Use left mouse click to attack and destroy the wall! You can press R to deactivate hammer.";
        }
    }
    private void Tut4Check()
    {
        if (!GameObject.Find("ShootingCube"))
        {
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "Good job!! Next Tutorial in 8";
            StartCoroutine(LoadNextTutorial(8));
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "Let the click go whenever you want, but be careful about the charge!";
        }
    }

    private void Tut5Check()
    {
        if (!GameObject.Find("ShootingCube"))
        {
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "Good job!! Next Tutorial in 8";
            StartCoroutine(LoadNextTutorial(8));
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "And it will shoot 5 projectiles from the sky!";
        }
    }

    private void Tut6Check()
    {
        if (!wall)
        {
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "Good job!! Good luck with real life (or level)! Next Tutorial in 8";
            StartCoroutine(LoadNextTutorial(8));
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && !once)
        {
            once = true;
            GameObject.Find("Info Panel").GetComponentInChildren<Text>().text = "Release right click to deactivate the shield";
            StartCoroutine(ChangeInfoText("You can also use Shift button to dodge to a pressed direction to avoid incoming projectiles", 7f));
            StartCoroutine(ChangeInfoText("When you get hit, you will lose one of your resources for good. And when no resources available remains, you loose the game.", 17f));
            StartCoroutine(ChangeInfoText("Oh, and your aim is to destroy every destructable object in the level, count will be shown on top of the screen.", 27f));
            StartCoroutine(ChangeInfoText("Destroy the wall to jump into the game", 37f));
            StartCoroutine(PutWall(37f));

        }
    }
}
