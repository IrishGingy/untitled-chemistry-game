using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    // TODO: Can put different tilemaps that are affected by the player's gear and upgrades

    public GameObject startPosition;
    public BoxCollider2D endPositionCollider;
    public GameObject phase1Line;
    public GameObject player;
    public int currentPhase;
    public float speed = 1f;
    public GameObject tilemapGrid;
    public GameObject catchAreas;
    public GameObject tilemapGO;

    [Header("Don't set in inspector")]
    [SerializeField] public Transform chosenCatchArea;
    [SerializeField] private bool fishing;

    private Rigidbody2D rb;
    private FishingPhaseII walls;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        fishing = false;
        SetCatchArea();

        gm = FindObjectOfType<GameManager>();
        tilemapGrid.SetActive(true);
        tilemapGO.SetActive(true);
        walls = tilemapGO.GetComponent<FishingPhaseII>();

        tilemapGrid.SetActive(false);
        tilemapGO.SetActive(false);
        rb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        currentPhase = 0;
        player.transform.position = startPosition.transform.position;
        phase1Line.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhase == 0)
        {
            if (Input.GetMouseButton(0))
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * 20);
            }
            if (Input.GetMouseButton(1))
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * 5);
                rb.AddForce(Vector3.right * speed);
            }
        }
        if (fishing && Input.GetKeyDown(KeyCode.Space))
        {
            //tilemapGrid.SetActive(false);
            //ResetFishing();
            ExitFishing();
        }
        //// Calculate the new position based on the current position and the left direction
        //Vector3 newPosition = transform.position + Vector3.right * speed * Time.deltaTime;

        //// Update the position of the GameObject
        //transform.position = newPosition;
    }

    public void StartFishing()
    {
        // turn on fishing manager gameobject
        // turn off boatCam
        // turn off tilemap stuff (ensure it is off for phase 0)
        // lock cursor
        fishing = true;
        currentPhase = 0;
        // gravityScale = 1f;
        // SetCatchArea()

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
        tilemapGrid.SetActive(false);
        player.transform.position = startPosition.transform.position;
        catchAreas.SetActive(true); 
        phase1Line.SetActive(true);
        currentPhase = 0;
        rb.gravityScale = 1f;
    }

    public void FishOn()
    {
        catchAreas.SetActive(false);
        //Cursor.lockState = CursorLockMode.None;
        rb.velocity = Vector3.zero;
        //player.transform.position = tilemapGrid.transform.position;
        //tilemapGrid.SetActive(true);
        //tilemapGO.SetActive(true);
        rb.gravityScale = 0f;
        endPositionCollider.enabled = false;
        phase1Line.SetActive(false);
        walls.StartPhaseII(player, out currentPhase);
        //rb.gravityScale = 0f;
    }

    public void ExitFishing()
    {
        gm.player.thirdPersonMovement.gameObject.SetActive(true);
        gameObject.SetActive(false);
        ResetFishing();
        walls.StopFishing();
    }

    //private void OnDrawGizmos()
    //{
    //    Debug.Log("Maybe drawing");
    //    if (chosenCatchArea)
    //    {
    //        Debug.Log("Drawing!");
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawMesh(chosenCatchArea.gameObject., chosenCatchArea.localScale);
    //    }
    //}
}
