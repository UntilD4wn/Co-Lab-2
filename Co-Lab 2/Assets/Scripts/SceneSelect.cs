using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    public void Level1()
    {
        //Play Game after clicking button
        SceneManager.LoadSceneAsync(1);
        Debug.Log("Change Scene Operational");
    }
    public void Level2()
    {
        //Play Game after clicking button
        SceneManager.LoadSceneAsync(2);
        Debug.Log("Change Scene Operational");
    }

}
