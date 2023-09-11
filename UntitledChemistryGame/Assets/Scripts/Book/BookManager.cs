using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    //public QuestList questList;
    //public GameObject questNotificationUI;
    public GameObject log;
    public GameObject leftPageObject;
    public Page leftPage;
    public GameObject rightPageObject;
    public Sprite addIcon;

    public List<GameObject> leftPageGOs;
    public List<GameObject> rightPageGOs;

    public Page rightPage;
    // the page we are currently editing when pulling up the log
    public Page currentPage;
    // the current image that was clicked on in the book
    public Image currentImage;

    [Header("Quests")]
    public Quest bookTutorialQuest;

    [Header("Modal")]
    //public int modalNameValue = 0;
    //public int modalLocationValue = 0;
    //public int modalWeightRangeValue = 0;
    public GameObject modalGO;
    public TextMeshProUGUI modalTitle;
    public TMP_Dropdown modalDropdown;
    public string selectedText;
    public int selectedIndex;
    public List<string> currentList;

    [Header("Modal lists")]
    public List<string> locations = new List<string> { "Swamps", "Reefs", "Shallow" };
    public List<string> weightRanges = new List<string> { "1-15", "15-50", "50-100", "100+" };
    public List<string> fishNames = new List<string>();

    [Header("Modal values")]
    public Dictionary<ModalType, int> dropdownValues = new Dictionary<ModalType, int>();

    // List of fish types which is the content shown for each page
    public FishType[] fishTypes;

    public PageContent[] pageContents;

    //[Header("Lists")]
    //public List<string> weightRanges = new List<string>();
    //public List<string> locations = new List<string>();
    //// List of fish types which is the content shown for each page
    //public FishType[] fishTypes;
    //public List<string> fishNames = new List<string>();

    private GameManager gm;
    private Modal modal;
    // This is used when input polling (don't want to move the pages if the children aren't active)
    private GameObject firstChild;
    //private List<string> modalTypes = new List<string> { "Location", "WeightRange", "Name" };

    public int leftPageIndex = 0;
    public int rightPageIndex = 1;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();

        gameObject.SetActive(true);
        firstChild = transform.GetChild(0).gameObject;
        firstChild.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        modal = modalGO.GetComponent<Modal>();
        foreach (FishType type in fishTypes)
        {
            fishNames.Add(type.name);
        }
        modal.modalLists.Add(ModalType.Location, locations);
        modal.modalLists.Add(ModalType.WeightRange, weightRanges);
        modal.modalLists.Add(ModalType.Name, fishNames);

        dropdownValues.Add(ModalType.Location, 0);
        dropdownValues.Add(ModalType.WeightRange, 0);
        dropdownValues.Add(ModalType.Name, 0);

        leftPageObject = transform.GetChild(0).gameObject;
        rightPageObject = transform.GetChild(1).gameObject;
        leftPage = leftPageObject.GetComponent<Page>();
        rightPage = rightPageObject.GetComponent<Page>();

        leftPage.content = pageContents[leftPageIndex];
        rightPage.content = pageContents[rightPageIndex];

        // TODO: THIS IS VERY MUCH HARDCODED, CHANGE LATER!!!
        pageContents[0].completed = true;
        pageContents[1].completed = true;
        pageContents[2].icon = addIcon;
        pageContents[2].description = "";
        pageContents[2].completed = false;
        pageContents[3].completed = true;

        //initialized = true;

        //foreach (FishType type in fishTypes)
        //{
        //    fishNames.Add(type.name);
        //}
    }

    private void Update()
    {
        if (firstChild.activeSelf && !(modalGO.activeSelf && log.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    // decrement the leftPageIndex by two if it's not the first page (otherwise don't change the index)
                    leftPageIndex = leftPageIndex != 0 ? leftPageIndex - 2 : leftPageIndex;
                    rightPageIndex = rightPageIndex != 1 ? rightPageIndex - 2 : rightPageIndex;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    // increment the rightPageIndex by two if it's not the last page (otherwise don't change the index)
                    Debug.Log(pageContents.Length);
                    leftPageIndex = leftPageIndex != pageContents.Length - 2 ? leftPageIndex + 2 : leftPageIndex;
                    rightPageIndex = rightPageIndex != pageContents.Length - 1 ? rightPageIndex + 2 : rightPageIndex;
                }

                leftPage.content = pageContents[leftPageIndex];
                rightPage.content = pageContents[rightPageIndex];
            }
        }
    }

    private void OnDropdownValueChanged(int index)
    {
        Debug.Log(index);
        selectedText = modalDropdown.options[index].text;
        selectedIndex = index;
        Debug.Log($"Selected Index: {selectedIndex}, Selected Text: {selectedText}");
    }

    public void Modal(string modalType)
    {
        int typeIndex = 0;
        DetermineType(modalType, out typeIndex);
        // Clear any existing options
        modalDropdown.ClearOptions();

        // Opening modal
        if (modalType != "")
        {
            ModalType mt = (ModalType)typeIndex;
            int chosenIndex = dropdownValues[mt];
            // Set the selected index to the first element
            modalDropdown.value = chosenIndex;
            // Subscribe to the onValueChanged event
            modalDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
            currentList = modal.modalLists[mt];

            // Call the event handler manually to reflect the initial selection
            //OnDropdownValueChanged(modalDropdown.value);

            modalGO.SetActive(true);
            modalTitle.text = modalType;
            modalDropdown.AddOptions(currentList);
            //// Location
            //if (typeIndex == modalTypes[0])
            //{
            //    modalTitle.text = "Location";
            //    modalDropdown.AddOptions(locations);
            //}
            //// WeightRange
            //if (modalType == modalTypes[1])
            //{
            //    modalTitle.text = "Weight Range";
            //    modalDropdown.AddOptions(weightRanges);
            //}
            //// Name
            //if (modalType == modalTypes[2])
            //{
            //    modalTitle.text = "Name";
            //    modalDropdown.AddOptions(fishNames);
            //}
        }
        // Closing modal
        else
        {
            modalDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
            Debug.Log($"Selected Index: {selectedIndex}, Selected Text: {selectedText}");


            //if (title == "Weight Range")
            //{
            //    modalWeightRangeValue = modalDropdown.value;
            //}
            //else if (title == "Name")
            //{
            //    modalNameValue = modalDropdown.value;
            //}
            //else if (title == "Location")
            //{
            //    modalLocationValue = modalDropdown.value;
            //}
            modalGO.SetActive(false);
            modalDropdown.ClearOptions();
        }
    }

    private void DetermineType(string type, out int typeIndex)
    {
        if (type == "Location") 
        {
            typeIndex = 0;
        }
        else if (type == "WeightRange") 
        {
            typeIndex = 1;
        }
        else if (type == "Name") 
        {
            typeIndex = 2;
        }
        else
        {
            typeIndex = -1;
            Debug.LogWarning($"The modalType {type} does not exist!");
        }
    }

    //public void LocationIcon(GameObject clickedPage)
    //{
    //    log.SetActive(true); 
    //    currentPage = clickedPage.GetComponent<Page>();
    //    if (currentPage.pageLeftOrRight == 0)
    //    {
    //        currentImage = leftPageGOs[1].transform.GetChild(0).GetComponent<Image>();
    //    }
    //    else
    //    {
    //        currentImage = rightPageGOs[1].transform.GetChild(0).GetComponent<Image>();
    //    }
    //}

    public void FishIcon(GameObject clickedPage)
    {
        // toggle the log UI (position in the middle)
        // make it so that on click it selects that fish type icon and closes the log
        log.SetActive(true);
        currentPage = clickedPage.GetComponent<Page>();
        if (currentPage.pageLeftOrRight == 0)
        {
            currentImage = leftPageGOs[0].transform.GetChild(0).GetComponent<Image>();
        }
        else
        {
            currentImage = rightPageGOs[0].transform.GetChild(0).GetComponent<Image>();
        }
    }

    public void SetIcon(GameObject obj)
    {
        InventorySlot slot = obj.transform.parent.GetComponent<InventorySlot>();
        if (slot.item)
        {
            Debug.Log(slot.item.fishType);
            currentPage.content.nameFishType = slot.item.fishType;
            currentPage.content.icon = slot.item.fishType.icon;
            currentImage.sprite = slot.item.fishType.icon;
        }
        log.SetActive(false);

        Debug.Log("CurrentPage.icon: " + currentPage.content.icon);
        Debug.Log("fishType[2]: " + fishTypes[2].name);
        Debug.Log("fishType[2].icon: " + fishTypes[2].icon);
        // check for completion
        if (currentPage.content.icon == fishTypes[2].icon)
        {
            currentPage.content.completed = true;
            gm.qm.CompleteQuest(bookTutorialQuest);
            currentPage.UpdateUI();
        }
    }

    //public void AddQuestToMenu(Quest q)
    //{
    //    questList.buttonGameObjects.TryGetValue(q, out GameObject button);
    //    if (button)
    //    {
    //        button.SetActive(true);
    //        StartCoroutine(ShowQuestNotification());
    //    }
    //    else
    //    {
    //        Debug.Log($"No quest button gameobject created for {q}");
    //    }
    //}

    //private IEnumerator ShowQuestNotification()
    //{
    //    questNotificationUI.SetActive(true);
    //    yield return new WaitForSeconds(3f);
    //    questNotificationUI.SetActive(false);
    //}

    //public void QuestCheck()
    //{
    //    // number of fish, 
    //    Debug.Log("Checking if a quest was completed...");
    //}
}
