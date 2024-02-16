using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleSystem;

public class AudienceCat : Cat
{
    [SerializeField] private List<Sprite> agesLaugh;
    [SerializeField] private List<Sprite> blinks1;
    [SerializeField] private List<Sprite> blinks2;

    [SerializeField] private SpriteRenderer blink1;
    [SerializeField] private SpriteRenderer blink2;

    [SerializeField] private Animation anim;
    [SerializeField] private GameObject laughMouth;

    private Seat catSeat;

    public override void Initialize()
    {
        base.Initialize();

        blink1.sprite = blinks1[(int)catAge];
        blink2.sprite = blinks2[(int)catAge];

        StartCoroutine(Blink());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Laugh());
        }
    }

    public void SetSeat(Seat seat)
    {
        catSeat = seat;
        transform.position = seat.GetPosition;
    }

    public CatData GetCatData()
    {
        return new CatData(catSeat.GetRowNumber, catSeat.GetSeatNumber, catColor, catBuild, catAge,
            catStatus, catGender);
    }

    private IEnumerator Blink()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 3f));
        anim.Play("Blink");
    }

    public IEnumerator Laugh()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        anim.Play("CatLaugh");
        age.sprite = agesLaugh[(int)catAge];
        laughMouth.SetActive(true);
    }
}