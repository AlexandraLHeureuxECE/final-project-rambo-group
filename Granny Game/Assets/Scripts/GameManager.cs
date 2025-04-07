using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stuff")]
    public GameObject playerPrefab;
    private GameObject currentPlayer;

    [Header("Spawn")]
    public Transform spawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        SpawnPlayer();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSpawnPoint();
        SpawnPlayer();
    }

    void FindSpawnPoint()
    {
        Spawnpoint sp = FindObjectOfType<Spawnpoint>();
        if (sp != null)
            spawnPoint = sp.transform;
    }

    public void SpawnPlayer()
    {
        if (playerPrefab == null || spawnPoint == null) return;

        if (currentPlayer != null)
            Destroy(currentPlayer);

        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void RespawnPlayer()
    {
        SpawnPlayer();
    }

    public void LoadNextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}