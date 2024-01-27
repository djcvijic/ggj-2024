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

    //for testing
    private List<Cat> cats = new List<Cat>();

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

            catSeat = catSeats[Random.Range(0, catSeats.Count)];

            while (usedCatSeats.Contains(catSeat))
            {
                catSeat = catSeats[Random.Range(0, catSeats.Count)];
            }

            usedCatSeats.Add(catSeat);

            cat.SetSeat(catSeat);
            cats.Add(cat);

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

        if (cats.Count > 0)
        {
            foreach (Cat existingCat in cats)
            {
                Destroy(existingCat.gameObject);
            }
        }

        cats.Clear();

        GenerateCats();
    }
}
