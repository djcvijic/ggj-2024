using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGenerator : MonoBehaviour
{
    [SerializeField] private int startCatCount;
    [SerializeField] private Cat catPrefab;
    [SerializeField] private List<Transform> catSeats;

    private Transform newCatPosition;
    private Transform previousCatPosition;

    private void GenerateCats()
    {
        for (int i = 0; i < startCatCount; i++)
        {
            Cat cat = Instantiate(catPrefab);

            newCatPosition = catSeats[Random.Range(0, catSeats.Count)];

            while (newCatPosition == previousCatPosition)
            {
                newCatPosition = catSeats[Random.Range(0, catSeats.Count)];
            }

            previousCatPosition = newCatPosition;

            cat.transform.position = newCatPosition.position;
        }
    }
}
