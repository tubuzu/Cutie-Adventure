// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MyMonoBehaviour
{
    [SerializeField] List<Transform> levelButtons;
    [SerializeField] GameObject levelButton;
    public int reachedLevel;
    [SerializeField] protected int levelCount = 0;

    protected override void Awake()
    {
        // PlayerPrefs.DeleteAll();
        base.Awake();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        levelButtons = new List<Transform>();
        reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);

        bool check = true;
        while (check)
        {
            if (SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Level_" + (levelCount + 1).ToString("00") + ".unity") != -1) levelCount++;
            else check = false;
        }

        for (int i = 1; i <= levelCount; i++)
        {
            GameObject button = Instantiate(levelButton, Vector3.zero, Quaternion.identity);
            button.transform.SetParent(transform, false);
            levelButtons.Add(button.transform);
            button.GetComponent<Button>().GetComponentInChildren<Text>().text = i.ToString("00");
            button.GetComponent<LevelButton>().level = i;
            button.GetComponent<LevelButton>().SetStar(PlayerPrefs.GetInt("LevelStar_" + i.ToString("00"), 0));
            if (i > reachedLevel)
            {
                button.GetComponent<Button>().interactable = false;
            }
        }
    }
}
