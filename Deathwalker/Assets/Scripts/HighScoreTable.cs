using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HScoreEntry> hScoreEntryList;
    private List<Transform> hScoreEntryTransformList;

    private void Awake() {
        entryContainer = gameObject.transform.Find("HScoreEntryContainer");
        entryTemplate = entryContainer.transform.Find("HScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        // hScoreEntryList = new List<HScoreEntry>() {
        //     new HScoreEntry{score=23424, name="John Doe"},
        //     new HScoreEntry{score=234234, name="John De"},
        //     new HScoreEntry{score=23434524, name="John oe"},
        //     new HScoreEntry{score=2342234, name="Jon Doe"}
        // };

        AddHScoreEntry(100, "Johnny");

        string jsonString = PlayerPrefs.GetString("hScoreTable");
        HScores hScores = JsonUtility.FromJson<HScores>(jsonString);

        // sort by score
        for(int i = 0; i < hScores.hScoreEntryList.Count; i++) {
            for(int j = i + 1; j < hScores.hScoreEntryList.Count; j++) {
                if (hScores.hScoreEntryList[j].score > hScores.hScoreEntryList[i].score) {
                    HScoreEntry tmp = hScores.hScoreEntryList[i];
                    hScores.hScoreEntryList[i] = hScores.hScoreEntryList[j];
                    hScores.hScoreEntryList[j] = tmp;
                }
            }
        }

        hScoreEntryTransformList = new List<Transform>();
        foreach (HScoreEntry hScoreEntry in hScores.hScoreEntryList) {
            if (hScoreEntryTransformList.Count < 5) {
                CreateHScoreEntryTransform(hScoreEntry, entryContainer, hScoreEntryTransformList);
            }
        }

    }

    private void CreateHScoreEntryTransform(HScoreEntry hScoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        int score = hScoreEntry.score;
        string name = hScoreEntry.name;
        entryTransform.Find("posText").GetComponent<Text>().text = rank.ToString();
        entryTransform.Find("nameText").GetComponent<Text>().text = name;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        transformList.Add(entryTransform);
    }

    public void AddHScoreEntry (int score, string name) {
        // Create HighScore Entry
        HScoreEntry hScoreEntry = new HScoreEntry { score = score, name = name };
        // Load saved HighScores
        string jsonString = PlayerPrefs.GetString("hScoreTable");
        HScores hScores = JsonUtility.FromJson<HScores>(jsonString);
        // Add new entry to high score
        hScores.hScoreEntryList.Add(hScoreEntry);
        // Save updated high score
        string json = JsonUtility.ToJson(hScores);
        PlayerPrefs.SetString("hScoreTable", json);
        PlayerPrefs.Save();
    }

    private class HScores {
        public List<HScoreEntry> hScoreEntryList;
    }

    [System.Serializable]
    private class HScoreEntry {
        public int score;
        public string name;
    }
}
