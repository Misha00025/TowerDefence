using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestRoutes : MonoBehaviour
{
    [SerializeField] Navigator navigator;

    // Update is called once per frame
    void Update()
    {
        Vector2[] vectors = navigator.Route.ToArray();

        Vector2 lastVector = Vector2.zero;
        foreach (Vector2 vector in vectors)
        {
            Debug.DrawLine(lastVector, vector, new Color(1, 0, 0, 1));
            lastVector = vector;
        }
    }
}
