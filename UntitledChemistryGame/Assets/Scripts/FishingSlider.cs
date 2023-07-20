using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Slider))]
public class FishingSlider : MonoBehaviour
{
    [Header("Rect Transform Values")]
    [SerializeField] public float left = 200f;
    [SerializeField] public float right = 600f;
    [SerializeField] public float height = 50f;

    [SerializeField] private bool flip;

    public float speed;
    public float catchSectionPivot;
    public float catchSectionLength;
    public GameObject catchSectionObj;
    public RectTransform gameObjectRect;
    public TextMeshProUGUI statusTextMesh;
    public Item fishItem;

    private Slider _slider;
    private GameObject initObj;
    private RectTransform sliderRect;
    private bool handleStopped;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        statusTextMesh.gameObject.SetActive(false);
        _slider = GetComponent<Slider>();

        sliderRect = _slider.GetComponent<RectTransform>();
        initObj = Instantiate(catchSectionObj, _slider.transform);
        gameObjectRect = initObj.GetComponent<RectTransform>();
        gameObjectRect.anchoredPosition = new Vector2(-200, 0);
        gameObjectRect.anchorMax = new Vector2(1, 0.5f);
        gameObjectRect.anchorMin = new Vector2(0, 0.5f);
        gameObjectRect.offsetMax = new Vector2(-right, height);
        gameObjectRect.offsetMin = new Vector2(left, -height);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Anchor position: " + gameObjectRect.anchoredPosition);
            Debug.Log("Anchor max: " + gameObjectRect.anchorMax);
            Debug.Log("Anchor min: " + gameObjectRect.anchorMin);
            Debug.Log("Offset max: " + gameObjectRect.offsetMax);
            Debug.Log("Offset min: " + gameObjectRect.offsetMin);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Slider value: " + _slider.value);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Recalculate
            gameObjectRect = initObj.GetComponent<RectTransform>();
            gameObjectRect.anchoredPosition = new Vector2(-200, 0);
            gameObjectRect.anchorMax = new Vector2(1, 0.5f);
            gameObjectRect.anchorMin = new Vector2(0, 0.5f);
            gameObjectRect.offsetMax = new Vector2(-right, height);
            gameObjectRect.offsetMin = new Vector2(left, -height);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Reeling in! (Pressed Spacebar)");
            if (_slider.value >= (left * 0.001) && _slider.value <= ((1000 - right) * 0.001))
            {
                inventory.Add(fishItem);
                statusTextMesh.text = "FISH CAUGHT!";
                statusTextMesh.color = Color.green;
                statusTextMesh.gameObject.SetActive(true);
            }
            else
            {
                statusTextMesh.text = "FISH LOST!";
                statusTextMesh.color = Color.red;
                statusTextMesh.gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetSlider();
        }

        if (!handleStopped)
        {
            MoveHandle();
        }
    }

    void MoveHandle()
    {
        if (!flip)
        {
            if (_slider.value < _slider.maxValue)
            {
                _slider.value += speed * Time.deltaTime;
            }
            else
            {
                flip = true;
            }
        }
        else
        {
            if (_slider.value > _slider.minValue)
            {
                _slider.value -= speed * Time.deltaTime;
            }
            else
            {
                flip = false;
            }
        }
    }

    void ResetSlider()
    {
        _slider.value = _slider.minValue;
        statusTextMesh.gameObject.SetActive(false);
        _slider.gameObject.SetActive(false);
        handleStopped = true;
    }
}
