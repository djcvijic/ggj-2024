using System;
using System.Collections.Generic;
using UnityEngine;
using RuleSystem;
using Random = UnityEngine.Random;

public class CrowdGenerator : MonoBehaviour
{
    [SerializeField] private AudienceCat catPrefab;
    [SerializeField] private List<Seat> catSeats;
    [SerializeField] private Transform crowd;

    private Seat catSeat;

    private List<Seat> usedCatSeats = new List<Seat>();

    public int MaxCatCount => catSeats.Count;

    public List<AudienceCat> GenerateCats(int catCount, DayOfWeek dayOfWeek)
    {
        usedCatSeats.Clear();

        foreach(Transform go in crowd.transform)
        {
            Destroy(go.gameObject);
        }

        List<CatData> allCatsData = new List<CatData>();

        List<AudienceCat> cats = new List<AudienceCat>();

        for (int i = 0; i < catCount; i++)
        {
            AudienceCat cat = Instantiate(catPrefab, crowd);
            cat.Initialize();

            catSeat = catSeats[Random.Range(0, catSeats.Count)];

            while (usedCatSeats.Contains(catSeat))
            {
                catSeat = catSeats[Random.Range(0, catSeats.Count)];
            }

            cat.SetSeat(catSeat);
            usedCatSeats.Add(catSeat);

            allCatsData.Add(cat.GetCatData());
            cats.Add(cat);
        }

        RuleBook.Instance.Initialize(new AudienceData(allCatsData, dayOfWeek));

        return cats;
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
        GenerateCats(8, DateTime.Today.DayOfWeek);
    }
}
