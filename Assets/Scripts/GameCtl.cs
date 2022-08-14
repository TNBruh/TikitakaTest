using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtl : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemy1Prefab;
    [SerializeField] GameObject enemy2Prefab;
    [SerializeField] GameObject menuCanvas;

    GameObject[] instantiatedCharacters  = new GameObject[3]; //assumption: 0th = player, 1st = enemy1, 2nd = enemy2

    public static GameCtl singleton;

    private void Start()
    {
        if (singleton != null)
        {
            Destroy(this);
        } else
        {
            singleton = this;
        }
    }

    public void StartGameAPI()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        instantiatedCharacters = new GameObject[3];
        instantiatedCharacters[0] = Instantiate(playerPrefab, new Vector3(-5.45f, 0), Quaternion.identity);
        instantiatedCharacters[1] = Instantiate(enemy1Prefab, new Vector3(2f, 0), Quaternion.identity);
        instantiatedCharacters[2] = Instantiate(enemy2Prefab, new Vector3(5.45f, 3), Quaternion.identity);
        yield return new WaitUntil(() =>
        {
            return instantiatedCharacters[0] == null ||
            (instantiatedCharacters[1] == null && instantiatedCharacters[2] == null);
        });
        yield return new WaitForSeconds(2);
        foreach (GameObject character in instantiatedCharacters)
        {
            if (character != null) Destroy(character);
        }
        menuCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
