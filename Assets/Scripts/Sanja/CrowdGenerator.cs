using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleSystem;

public class CrowdGenerator : MonoBehaviour
{
    [SerializeField] private int startCatCount;
    [SerializeField] private Cat catPrefab;
    [SerializeField] private List<Seat> catSeats;
    [SerializeField] private Transform crowd;

    private Seat catSeat;

    private List<Seat> usedCatSeats = new List<Seat>();

    private void Start()
    {
        GenerateCats();
    }

    private void GenerateCats()
    {
        List<CatData> allCatsData = new List<CatData>();

        for (int i = 0; i < startCatCount; i++)
        {
            Cat cat = Instantiate(catPrefab, crowd);
            cat.CreateCat();

            catSeat = catSeats[Random.Range(0, catSeats.Count)];

            while (usedCatSeats.Contains(catSeat))
            {
                catSeat = catSeats[Random.Range(0, catSeats.Count)];
            }

            cat.SetSeat(catSeat);
            usedCatSeats.Add(catSeat);

            allCatsData.Add(cat.GetCatData());
        }

        RuleBook.Instance.Initialize(allCatsData);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestGeneration();
        }
    }

    private void TestGeneration()
    {
        usedCatSeats.Clear();

        foreach(Transform go in crowd.transform)
        {
            Destroy(go.gameObject);
        }

        GenerateCats();
    }
}
