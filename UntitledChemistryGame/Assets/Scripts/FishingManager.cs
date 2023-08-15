using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    // TODO: Can put different tilemaps that are affected by the player's gear and upgrades

    public GameObject startPosition;
    public BoxCollider2D endPositionCollider;
    public GameObject player;
    public int currentPhase;
    public float speed = 1f;
    public GameObject tilemapGO;
    public GameObject catchAreas;

    [Header("Don't set in inspector")]
    [SerializeField] public Transform chosenCatchArea;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        SetCatchArea();

        tilemapGO.SetActive(false);
        rb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        currentPhase = 0;
        player.transform.position = startPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhase == 0)
        {
            if (Input.GetMouseButton(0))
            {
                rb.AddForce(Vector3.up * 5);
            }
            if (Input.GetMouseButton(1))
            {
                rb.AddForce(Vector3.up);
                rb.AddForce(Vector3.right * speed);
            }
        }
        //// Calculate the new position based on the current position and the left direction
        //Vector3 newPosition = transform.position + Vector3.right * speed * Time.deltaTime;

        //// Update the position of the GameObject
        //transform.position = newPosition;
    }

    private void SetCatchArea()
    {
        int catchAreaCount = catchAreas.transform.childCount;
        //Debug.Log(catchAreaCount);
        if (catchAreaCount > 0)
        {
            chosenCatchArea = catchAreas.transform.GetChild(Random.Range(0, catchAreaCount));
        }
    }

    public void ResetFishing()
    {
        player.transform.position = startPosition.transform.position;
    }

    public void FishOn()
    {
        rb.velocity = Vector3.zero;
        player.transform.position = Vector3.zero;
        tilemapGO.SetActive(true);
        currentPhase = 1;
        rb.gravityScale = 0f;
    }
}
