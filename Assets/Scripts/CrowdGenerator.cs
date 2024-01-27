using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGenerator : MonoBehaviour
{
    [SerializeField] private int startCatCount;
    [SerializeField] private Cat catPrefab;
    [SerializeField] private List<Transform> catSeats;
    [SerializeField] private Transform crowd;

    private Transform catPosition;

    private List<Transform> usedCatPositions;

    private void Start()
    {
        GenerateCats();
    }

    private void GenerateCats()
    {
        for (int i = 0; i < startCatCount; i++)
        {
            Cat cat = Instantiate(catPrefab, crowd);

            catPosition = catSeats[Random.Range(0, catSeats.Count)];

            while (usedCatPositions.Contains(catPosition))
            {
                catPosition = catSeats[Random.Range(0, catSeats.Count)];
            }

            usedCatPositions.Add(catPosition);

            cat.SetPosition(catPosition);
        }
    }
}
