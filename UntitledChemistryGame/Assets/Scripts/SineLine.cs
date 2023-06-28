using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SineLine : MonoBehaviour
{
    public List<GameObject> nodes = new List<GameObject>();
    public RectTransform startPosition;
    public RectTransform endPosition;
    public GameObject nodeObject;
    public int nodeCount = 10;
    public float radius = 1f;
    public float speed = 1f;

    private LineRenderer lineRenderer;
    private Vector3[] nodePositions;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        nodePositions = new Vector3[nodeCount];
        CreateNodes();
        Debug.Log(nodePositions.Length);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == lineRenderer.GetComponent<MeshCollider>())
                {
                    Debug.Log("Mouse is over line!");
                }
            }
        }
    }

    private void CreateNodes()
    {
        for (int i = 0; i < nodeCount; i++)
        {
            GameObject node = Instantiate(nodeObject);
            RectTransform rectTransform = node.GetComponent<RectTransform>();
            Vector3 position = Vector3.Lerp(startPosition.position, endPosition.position, i / (float)(nodeCount - 1));
            rectTransform.position = position;
            rectTransform.SetParent(transform);
            nodes.Add(node);
            nodePositions[i] = position;
        }

        lineRenderer.positionCount = nodeCount;
        lineRenderer.SetPositions(nodePositions);
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse is over line!");
    }


    //private void CreateNodes()
    //{
    //    float distance = Vector3.Distance(endPosition, startPosition);
    //    Debug.Log("Distance: " + distance);
    //    float increment = distance / nodeCount;
    //    Debug.Log("Increment: " + increment);

    //    for (int i = 0; i < nodeCount; i++)
    //    {
    //        GameObject node = Instantiate(nodeObject);
    //        if (i > 0)
    //        {
    //            node.transform.position = startPosition;
    //        }
    //        else
    //        {
    //            node.transform.position = new Vector3(startPosition.x + increment, startPosition.y, startPosition.z);
    //        }
    //        node.transform.parent = transform;
    //        nodes.Add(node);
    //    }

    //    //float offset = 0.5f;
    //    //for (int i = 0; i < nodeCount; i++)
    //    //{
    //    //    GameObject node = Instantiate(nodeObject);
    //    //    if (i > 0)
    //    //    {
    //    //        node.transform.position = new Vector3(nodes[i - 1].transform.position.x + node.transform.localScale.x + offset, 0, 0);
    //    //    }
    //    //    else
    //    //    {
    //    //        node.transform.position = Vector3.zero;
    //    //    }
    //    //    node.transform.parent = transform;
    //    //    nodes.Add(node);
    //    //}
    //    Debug.Log("Nodes: " + nodes.Count);
    //}
}
