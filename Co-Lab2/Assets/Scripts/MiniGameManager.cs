using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public GameObject promptUI;
    public KeyCode switchKey;
    public bool triggerBox = false;

    void Start()
    {
        promptUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Near Triggor
        if (Input.GetKeyDown(switchKey) && triggerBox)
        {
            SceneManager.LoadSceneAsync(2);
            Debug.Log("Change Scene Operational");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //show UI
            promptUI.SetActive(true);
            triggerBox = true;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //hide the UI
        promptUI.SetActive(false);
        triggerBox = false;
    }
}
