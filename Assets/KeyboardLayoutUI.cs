using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardLayoutUI : MonoBehaviour
{
    public GameControl gameControl;
    public GameObject Keyboardlayout;
    public Dictionary<char, Image> keyButtons = new Dictionary<char, Image>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 2; i < Keyboardlayout.transform.childCount; i++)
        {
            GameObject currentCharLayout = Keyboardlayout.transform.GetChild(i).gameObject;
            Button textBoxButton = currentCharLayout.GetComponent<Button>();
            textBoxButton.onClick.AddListener(() => OnKeyPress(currentCharLayout.name[0]));
            keyButtons[currentCharLayout.name[0]] = currentCharLayout.GetComponent<Image>();
            SetButtonColor(currentCharLayout.name[0], Color.gray);
        }
    }


    public void OnKeyPress(char key)
    {
        key = char.ToUpper(key);
        gameControl.AddLetter(key);
    }

    public void OnEnterPress()
    {

        gameControl.CheckGuess();
    }

    public void OnDeletePress()
    {
        gameControl.RemoveLetter();
    }

    public void SetButtonColor(char key, Color color)
    {
        if (keyButtons.ContainsKey(key))
        {
            Image img = keyButtons[key];
            img.color = color;
        }
    }
}
