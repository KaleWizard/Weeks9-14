using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTrailMovement : MonoBehaviour
{
    List<Vector3> positions = new List<Vector3>();

    Vector3 direction = Vector3.zero;

    float speed = 5f;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            NewPoint(mousePos);
        }

        if (positions.Count == 0)
        {
            MoveToMouse(mousePos);
        } else
        {
            MoveToList();
        }

        UpdateLine(mousePos);
    }

    void NewPoint(Vector3 mousePos)
    {
        positions.Add(mousePos);
    }

    void MoveToMouse(Vector3 mousePos)
    {
        direction = mousePos - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void MoveToList()
    {
        direction = positions[0] - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
        if (Vector3.Distance(positions[0], transform.position) < 0.1f)
        {
            positions.RemoveAt(0);
        }
    }

    void UpdateLine(Vector3 mousePos)
    {
        List<Vector3> posList = new List<Vector3>(positions);
        posList.Add(mousePos);
        posList.Insert(0, transform.position);
        lineRenderer.positionCount = posList.Count;
        lineRenderer.SetPositions(posList.ToArray());
    }
}
