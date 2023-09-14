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
    public float forwardForce;
    public float upForce;
    public GameObject tilemapGrid;
    public GameObject catchAreas;
    public GameObject tilemapsParentGO;
    public AudioSource reelingSound;
    public GameObject collisionUI;
    public Sprite wormSprite;
    public Sprite fishSprite;
    public SpriteRenderer playerSprite;

    [Header("Don't set in inspector")]
    [SerializeField] public Transform chosenCatchArea;
    [SerializeField] public FishingPhaseII chosenObstacleTilemap;
    [SerializeField] private bool fishing;

    private Rigidbody2D rb;
    private FishingPhaseII[] obstacleTilemaps;
    private GameManager gm;
    private bool movingUp;
    private bool movingForward;
    private bool attachToCursor;

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(true);
        forwardForce = 2000f;
        upForce = 2000f;
        fishing = false;
        SetCatchArea();

        gm = FindObjectOfType<GameManager>();
        tilemapGrid.SetActive(true);
        tilemapsParentGO.SetActive(true);
        obstacleTilemaps = tilemapsParentGO.GetComponentsInChildren<FishingPhaseII>();
        foreach (Transform child in tilemapsParentGO.transform)
        {
            child.gameObject.SetActive(false);
        }
        //SetObstacles();

        tilemapGrid.SetActive(false);
        //tilemapsParentGO.SetActive(false);
        rb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        currentPhase = 0;
        player.transform.position = startPosition.transform.position;
        phase1Line.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhase == 1 && Input.GetKeyDown(KeyCode.O))
        {
            if (attachToCursor)
            {
                attachToCursor = false;
                player.SetActive(true);

                Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
            }
            else
            {
                attachToCursor = true;
                player.SetActive(false);
                // this centers the mouse
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.None;

                Cursor.SetCursor(fishSprite.texture, Vector3.zero, CursorMode.Auto);
            }
        }


        if (currentPhase == 0)
        {
            //if (collisionUI)
            //{
            //    collisionUI.SetActive(false);
            //}
            if (Input.GetMouseButtonDown(0))
            {
                movingUp = true;
            }
            if (Input.GetMouseButtonDown(1))
            {
                movingForward = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                movingUp = false;
            }
            if (Input.GetMouseButtonUp(1))
            {
                movingForward = false;
            }
            //if (Input.GetMouseButtonDown(1))
            //{
            //    reelingSound.Play();
            //}
            //if (Input.GetMouseButtonUp(1))
            //{
            //    reelingSound.Stop();
            //}
        }
        else
        {
            //if (!collisionUI)
            //{
            //    collisionUI.SetActive(true);
            //}
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

    private void FixedUpdate()
    {
        if (movingUp)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * Time.deltaTime * upForce);
        }
        if (movingForward)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * Time.deltaTime * (upForce / 5));
            rb.AddForce(Vector3.right * Time.deltaTime * forwardForce);
        }
    }

    public void StartFishing()
    {
        // turn on fishing manager gameobject
        // turn off boatCam
        // turn off tilemap stuff (ensure it is off for phase 0)
        // lock cursor
        player.SetActive(true);
        collisionUI.SetActive(false);
        fishing = true;
        currentPhase = 0;
        playerSprite.sprite = wormSprite;
        endPositionCollider.enabled = true;
        // gravityScale = 1f;
        SetCatchArea();

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

    private void SetObstacles()
    {
        if (obstacleTilemaps.Length > 0)
        {
            int index = Random.Range(0, obstacleTilemaps.Length);
            chosenObstacleTilemap = obstacleTilemaps[index];
        }
        else
        {
            Debug.LogWarning("There are no obstacle tilemaps to choose!");
        }
        //int tilemapCount = tilemapsParentGO.transform.childCount;
        //if (tilemapCount > 0)
        //{
        //    chosenObstacleTilemap = tilemapsParentGO.transform.GetChild(Random.Range(0, tilemapCount));
        //}
        //else 
        //{
        //    Debug.LogWarning("There are no obstacle tilemaps to choose!");
        //}
    }

    public void ResetFishing()
    {
        player.SetActive(true);
        movingUp = false;
        movingForward = false;
        tilemapGrid.SetActive(false);
        collisionUI.SetActive(false); 
        chosenObstacleTilemap.gameObject.SetActive(false);
        player.transform.position = startPosition.transform.position;
        catchAreas.SetActive(true); 
        phase1Line.SetActive(true);
        endPositionCollider.enabled = true;
        currentPhase = 0;
        playerSprite.sprite = wormSprite;
        rb.gravityScale = 1f;
    }

    public void FishOn()
    {
        movingUp = false;
        movingForward = false;
        catchAreas.SetActive(false);
        //Cursor.lockState = CursorLockMode.None;
        rb.velocity = Vector3.zero;
        //player.transform.position = tilemapGrid.transform.position;
        //tilemapGrid.SetActive(true);
        //tilemapGO.SetActive(true);
        rb.gravityScale = 0f;
        endPositionCollider.enabled = false;
        phase1Line.SetActive(false); 
        playerSprite.sprite = fishSprite;
        SetObstacles();
        chosenObstacleTilemap.StartPhaseII(player, out currentPhase);
        collisionUI.SetActive(true);
        //rb.gravityScale = 0f;
    }

    public void ExitFishing()
    {
        gm.player.thirdPersonMovement.gameObject.SetActive(true);
        gameObject.SetActive(false);
        ResetFishing();
        chosenObstacleTilemap.StopFishing();
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
