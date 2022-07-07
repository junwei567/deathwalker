using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;
    // Pool to store text
    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private FloatingText GetFloatingText()
    {
        FloatingText text = floatingTexts.Find(t => !t.active);
        // If no inactive text in text array
        if (text == null)
        {
            text = new FloatingText();
            text.go = Instantiate(textPrefab);
            // Set the parent transform for the game object
            text.go.transform.SetParent(textContainer.transform);
            text.text = text.go.GetComponent<Text>();

            floatingTexts.Add(text);
        }
        return text;
    }
    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        // Edit Text component
        floatingText.text.text = msg;
        floatingText.text.color = color;
        // Convert world space coordinates (Camera) to screen space coordinates (UI elements)
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        // Update motion and duration
        floatingText.motion = motion;
        floatingText.duration = duration;
        // Activate floating text
        floatingText.Show();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FloatingText text in floatingTexts) {
            text.UpdateFloatingText();
        }
    }
}
