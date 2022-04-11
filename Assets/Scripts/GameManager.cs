using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject[] characters;

    private int m_charIndex;
    public int CharIndex
    {
        get { return m_charIndex; }
        set { m_charIndex = value; }
    }

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            // Do not destroy game object when loading new scene, so that we can keep using it
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Only one instance is allowed --> destroy the duplicate
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Subscribe to event
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        // Unsubscribe to event
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // Function to be executed when event is called
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Gameplay")
        {
            Instantiate(characters[CharIndex]);
        }
    }
} // class
