using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlatformManager : MonoBehaviour
{
    public static CPlatformManager Instance = null;

    [SerializeField]
    GameObject platformPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(platformPrefab, new Vector2(33.09f, 9.56f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(40.8f, 9.56f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(37.02f, 9.56f), platformPrefab.transform.rotation);
    }

    IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(5f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
