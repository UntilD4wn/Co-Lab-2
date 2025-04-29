using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private float zCoord;
    public GameObject picnicEndUI;
    public KeyCode switchKey;

   

    [SerializeField] private Transform targetPosition;  // Assign the correct drop position in the Inspector
    [SerializeField] private Transform wrongTargetPosition;
    [SerializeField] private Transform orginalPosition;
    [SerializeField] private float snapThresholdOne = 1f; // Distance threshold for snapping
    [SerializeField] private float snapThresholdTwo = 1f;

    public bool isCorrectObject;      // Whether it belongs to the correct set
    public bool isCorrectlyPlaced = false;    // Whether it's placed correctly

    private void Start()
    {
        picnicEndUI.SetActive(false);

        snapThresholdTwo = 2f;
        snapThresholdOne = 2f;

    }

    private void OnMouseDown()
    {
        if (isCorrectlyPlaced) return;  // Prevent moving if already placed correctly

        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mousePosition = GetMouseWorldPos();
        offset = transform.position - mousePosition;
        dragging = true;
    }

    private void Update()
    {
        if (dragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }

      

    }

    private void OnMouseUp()
    {
        dragging = false;

        // Check if object is close to the target position
        if (Vector2.Distance(transform.position, targetPosition.position) < snapThresholdOne)
        {
            transform.position = targetPosition.position; // Snap to the correct place
            isCorrectlyPlaced = true;  // Mark as placed correctly
            CheckGameCompletion(); // Check if all objects are placed
        }
        if (Vector2.Distance(transform.position, wrongTargetPosition.position) < snapThresholdTwo)
        {
            transform.position = orginalPosition.position; // Snap to the correct place
          
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void CheckGameCompletion()
    {
        // Find all draggable objects in the scene
        Drag[] allDraggableObjects = FindObjectsOfType<Drag>();

        foreach (Drag obj in allDraggableObjects)
        {
            if (!obj.isCorrectlyPlaced)
                return;  // If any object is not placed correctly, exit early
        }

        picnicEndUI.SetActive(true);
    }

     
}
