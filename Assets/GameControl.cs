using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public TextMeshProUGUI ResultText;
    public TextMeshProUGUI CorrectText;
    public GameObject LetterPrefab;
    public Transform GridParent;
    public KeyboardLayoutUI KeyboardLayout;
    public GameObject ResetButton;
    public Color GreenColor;
    public Color YellowColor;
    private string selectedWord;
    private int currentRow = 0;
    private int currentCol = 0;
    private TextMeshProUGUI[,] letterBlocks = new TextMeshProUGUI[6, 5];
    private Image[,] imageBlocks = new Image[6, 5];
    private List<string> currentGuess = new List<string>();
    private string[] words = { "MATCH", "SHARK", "SHEEP", "APPLE", "HOUSE" };
   
    void Start()
    {
        
        selectedWord = words[Random.Range(0, words.Length)];
        CorrectText.text = "Correct : " + selectedWord;
        SetupGrid();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    char letter = GetLetterFromKey(kcode);
                    if (letter != ' ') AddLetter(letter);
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
                RemoveLetter();

            if (Input.GetKeyDown(KeyCode.Return) && currentCol == 5)
                CheckGuess();
        }
    }

    char GetLetterFromKey(KeyCode key)
    {
        string keyString = key.ToString();
        if (keyString.Length == 1 && char.IsLetter(keyString[0]))
        {
            return char.ToUpper(keyString[0]);
        }
        return ' ';
    }

    void SetupGrid()
    {

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject newLetterBlock = Instantiate(LetterPrefab, GridParent);
                TextBox textBox = newLetterBlock.GetComponent<TextBox>();
                letterBlocks[i, j] = textBox.TextMesh;
                imageBlocks[i, j] = textBox.BGImage;
            }
        }
    }

    public void AddLetter(char letter)
    {
        if (currentCol < 5)
        {
            letterBlocks[currentRow, currentCol].text = letter.ToString();
            currentGuess.Add(letter.ToString());
            currentCol++;
        }
    }

    public void RemoveLetter()
    {
        if (currentCol > 0)
        {
            currentCol--;
            letterBlocks[currentRow, currentCol].text = "";
            currentGuess.RemoveAt(currentGuess.Count - 1);
        }
    }

    public void CheckGuess()
    {

        if (currentCol < 5)
        {
            return;
        }

        string guessWord = string.Join("", currentGuess.ToArray());
        currentGuess.Clear();
        CheckLetterColors(guessWord);
        currentRow++;
        currentCol = 0;

        if (guessWord == selectedWord)
        {
            ResultText.text = "Win";
            ResetButton.SetActive(true);
        }
        else if (currentRow >= 6)
        {
            ResultText.text = "lose : " + selectedWord;
            ResetButton.SetActive(true);
        }
    }

    void CheckLetterColors(string guessWord)
    {
        for (int i = 0; i < 5; i++)
        {
            char guessLetter = guessWord[i];
            if (guessLetter == selectedWord[i])
            {
                
                imageBlocks[currentRow, i].color = GreenColor;
                KeyboardLayout.SetButtonColor(guessLetter, GreenColor);
            }
            else if (selectedWord.Contains(guessLetter.ToString()))
            {
                imageBlocks[currentRow, i].color = YellowColor;
                KeyboardLayout.SetButtonColor(guessLetter, GreenColor);
            }
            else
            {
                imageBlocks[currentRow, i].color = Color.gray;
                KeyboardLayout.SetButtonColor(guessLetter, YellowColor);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       /* selectedWord = words[Random.Range(0, words.Length)];
        currentRow = 0;
        currentCol = 0;
        CorrectText.text = "Correct : " + selectedWord;
        ResultText.text = "";
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                letterBlocks[i, j].text = "";
                imageBlocks[i, j].color = Color.white;
            }
        }*/
    }
}
