using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class FishingPhaseII : MonoBehaviour
{
    public float speed = 2f;
    public GameObject gridGO;
    public GameObject tilemapGO;
    public Tilemap tilemap;
    public Image[] collisionImages;
    public GameObject collisionUI;

    private bool waited;
    private Vector3Int max;
    // number of times the player hits an obstacle
    private int collisions;
    private int numTilesX;
    private GameManager gm;

    void Start()
    {
        collisions = 0;

        gridGO = transform.parent.gameObject;
        tilemapGO = gameObject;
        // Get the reference to the Tilemap component
        tilemap = GetComponent<Tilemap>();
        gm = FindObjectOfType<GameManager>(); 

        //StartPhaseII();
    }

    // Update is called once per frame
    void Update()
    {
        if (waited)
        {
            // Calculate the new position based on the current position and the left direction
            Vector3 newPosition = transform.position + Vector3.left * speed * Time.deltaTime;

            // Update the position of the GameObject
            transform.position = newPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisions++;
        // TODO: Give user feedback that they are colliding with an obstacle
        Debug.Log($"Hit an obstacle x{collisions}");
        collisionImages[collisions - 1].enabled = true;

        // restart scene
        if (collisions >= 3)
        {
            collisions = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gm.fm.ResetFishing();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        waited = true;
    }

    public void StartPhaseII(GameObject player, out int currentPhase)
    {
        // reset collision count
        collisionUI.SetActive(true);
        collisions = 0;
        foreach (Image i in collisionImages)
        {
            i.enabled = false;
        }

        currentPhase = 1;
        Cursor.lockState = CursorLockMode.None;
        gridGO.SetActive(true);
        tilemapGO.SetActive(true);
        // Resets the tilemap position
        transform.localPosition = Vector3.zero;
        tilemap.CompressBounds();
        player.transform.position = gridGO.transform.position;

        // Calculate and display the number of tiles on the Tilemap
        numTilesX = tilemap.cellBounds.size.x;
        max = tilemap.cellBounds.max;
        Vector3 rightMostBoundPosition = tilemap.CellToWorld(max);
        //Debug.Log(numTilesX);
        //Debug.Log(rightMostBoundPosition);

        waited = false;
        StartCoroutine(Wait());
    }

    public void StopFishing()
    {
        waited = false;
        // Resets the tilemap position
        transform.localPosition = Vector3.zero;
        tilemap.CompressBounds();
        collisionUI.SetActive(false);

        StopAllCoroutines();
    }
}
