using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEditor;

public class MyInkScript : MonoBehaviour
{
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;
    public Image textboxSprite;
    public bool isTalking = false;
    public bool startDialogue;
    public KeyCode continueButtonKey;

    [SerializeField] private Sprite bubbleSprite;
    [SerializeField] private Sprite borderSprite;
    [SerializeField] bool showingChoices;
    [SerializeField] float letterWaitInSeconds;
    [SerializeField] private PlayerController playerController;
    
    DialogueManager dm;
    static Story story;
    TextMeshProUGUI nametag;
    TextMeshProUGUI message;
    TextMeshProUGUI continueMessage;
    List<string> tags;
    static Choice choiceSelected;
    bool letterCancel;
    bool typingSentence;
    GameManager gm;
    string continueButtonKeyString;

    GameObject temp;
    //bool dialoguePlaying;

    // Start is called before the first frame update
    void Start()
    {
        SetInkScript(inkFile);
        continueButtonKeyString = continueButtonKey.ToString().ToUpper();
        continueMessage.text = $"—— PRESS {continueButtonKeyString} TO CONTINUE ——";
        dm = GetComponent<DialogueManager>();
        gm = GetComponent<GameManager>();
        if (!gm.letterScrolling)
        {
            letterWaitInSeconds = 0;
        }
    }

    public void SetInkScript(TextAsset file)
    {
        story = new Story(file.text);
        nametag = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        message = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        continueMessage = textBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        tags = new List<string>();
        choiceSelected = null;
    }

    private void Update()
    {
        if (textBox.activeSelf && (startDialogue || Input.GetKeyDown(continueButtonKey)))
        {
            startDialogue = false;
            //dm.dialoguePlaying = true;

            //Is there more to the story?
            if (story.canContinue)
            {
                nametag.text = "Placeholder";
                AdvanceDialogue();

                //Is this the last line of dialogue?
                if (!story.canContinue && story.currentChoices.Count == 0)
                {
                    Debug.Log("This is the last line of dialogue!");
                    continueMessage.text = $"—— PRESS {continueButtonKeyString} TO FINISH DIALOGUE ——";
                }

                //Are there any choices?
                if (story.currentChoices.Count != 0)
                {
                    StartCoroutine(ShowChoices());
                }
            }
            else if (!story.canContinue && story.currentChoices.Count != 0 && showingChoices)
            {
                Debug.Log("Dialogue isn't finished, but player must make a choice to proceed.");
            }
            else if (!story.canContinue && story.currentChoices.Count != 0 && !showingChoices)
            {
                // Ink has hit a branch or knot, and needs to advance to show choices
                StartCoroutine(ShowChoices());
            }
            else
            {
                FinishDialogue();
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && typingSentence)
        {
            letterCancel = true;
        }
    }

    public void StartDialogue(TextAsset file, bool placeholder = false)
    {
        //if (dm.dialoguePlaying) { return; }
        //gm.HidePrompt();
        // turn off player movement.
        //playerController.enabled = false;
        StartCoroutine(DelayStart(file, placeholder));
    }

    // Delay the start of the dialogue so that when E is pressed, the dialogue doesn't skip typing out the sentence
    IEnumerator DelayStart(TextAsset file, bool placeholder)
    {
        yield return new WaitForSeconds(0.25f);
        SetInkScript(file);
        if (placeholder)
        {
            Debug.Log("Should add a smaller textbox right above the NPC without a name tag showing the placeholder text");
        }
        textBox.SetActive(true);
        startDialogue = true;
    }

    // Finished the Story (Dialogue)
    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");
        textBox.SetActive(false);
        dm.StopDialogue();
        //gm.ShowPrompt();
        // allow the player to move again.
        //playerController.enabled = true;
        //dm.dialoguePlaying = false;
    }

    // Advance through the story 
    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    // Type out the sentence letter by letter and make character idle if they were talking
    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            typingSentence = true;
            if (letterCancel)
            {
                message.text = sentence;
            }
            if (!letterCancel)
            {
                message.text += letter;
                yield return new WaitForSeconds(letterWaitInSeconds);
            }
        }
        /*CharacterScript tempSpeaker = GameObject.FindObjectOfType<CharacterScript>();
        if (tempSpeaker.isTalking)
        {
            SetAnimation("idle");
        }*/
        letterCancel = false;
        typingSentence = false;
        yield return null;
    }

    // Create then show the choices on the screen until one got selected
    IEnumerator ShowChoices()
    {
        Debug.Log("There are choices need to be made here!");
        showingChoices = true;

        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject buttonObj = Instantiate(customButton, optionPanel.transform);
            temp = buttonObj;
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.AddComponent<ContentSizeFitter>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { buttonObj.GetComponent<Selectable>().Decide(); });
            switch (i)
            {
                case 0:
                    Debug.Log("First Choice = " + temp.name);
                    temp.transform.position = new Vector2(optionPanel.transform.position.x, optionPanel.transform.position.y);
                    break;
                case 1:
                    Debug.Log("Second Choice = " + temp.name);
                    temp.transform.position = new Vector2(optionPanel.transform.position.x, optionPanel.transform.position.y - 100);
                    break;
                case 2:
                    Debug.Log("Third Choice = " + temp.name);
                    temp.transform.position = new Vector2(optionPanel.transform.position.x, optionPanel.transform.position.y - 200);
                    break;
            }
        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    // Tells the story which branch to go to
    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    // After a choice was made, turn off the panel and advance from that choice
    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        showingChoices = false;
        AdvanceDialogue();
    }

    /* Tag Parser */
    /// In Inky, you can use tags which can be used to cue stuff in a game.
    /// This is just one way of doing it. Not the only method on how to trigger events. 
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch (prefix.ToLower())
            {
                /*case "anim":
                    SetAnimation(param);
                    break;
                case "color":
                    SetTextColor(param);
                    break;*/
                case "speaker":
                    SetSpeaker(param);
                    break;
                default:
                    Debug.LogWarning("Tag not recognized. There may need to be a case statment added.");
                    break;
            }
        }
    }
    /*void SetAnimation(string _name)
    {
        CharacterScript cs = GameObject.FindObjectOfType<CharacterScript>();
        cs.PlayAnimation(_name);
    }*/
    void SetTextColor(string _color)
    {
        switch (_color)
        {
            case "red":
                message.color = Color.red;
                break;
            case "blue":
                message.color = Color.cyan;
                break;
            case "green":
                message.color = Color.green;
                break;
            case "white":
                message.color = Color.white;
                break;
            default:
                Debug.Log($"{_color} is not available as a text color");
                break;
        }
    }

    void SetSpeaker(string _speaker)
    {
        nametag.text = _speaker;
        if (_speaker == "Thought")
        {
            textboxSprite.sprite = bubbleSprite;
        }
        else
        {
            textboxSprite.sprite = borderSprite;
        }
        /*switch (_speaker)
        {
            case "Tim":
                nametag.text = _speaker;
                break;
            case "Cashier":
                break;
            default:
                Debug.Log($"{_speaker} is not an available speaker");
                break;
        }*/
    }

}