using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleSystem;

public class Cat : MonoBehaviour
{
    [SerializeField] private List<Sprite> colors;
    [SerializeField] private List<Sprite> builds;
    [SerializeField] private List<Sprite> ages;
    [SerializeField] private List<Sprite> agesLaugh;
    [SerializeField] private List<Sprite> statuses;
    [SerializeField] private List<Sprite> genders;
    [SerializeField] private List<Sprite> blinks1;
    [SerializeField] private List<Sprite> blinks2;

    [SerializeField] private SpriteRenderer color;
    [SerializeField] private SpriteRenderer build;
    [SerializeField] private SpriteRenderer age;
    [SerializeField] private SpriteRenderer status;
    [SerializeField] private SpriteRenderer gender;
    [SerializeField] private SpriteRenderer blink1;
    [SerializeField] private SpriteRenderer blink2;

    [SerializeField] private Animation anim;
    [SerializeField] private GameObject laughMouth;

    private CatColor catColor;
    private CatBuild catBuild;
    private CatAge catAge;
    private CatStatus catStatus;
    private CatGender catGender;

    private Seat catSeat;

    public void CreateCat(CatColor ? specificColor = null, CatBuild ? specificBuild = null, CatAge ? specificAge = null, CatStatus ? specificStatus = null, CatGender ? specificGender = null)
    {
        catColor = specificColor == null ? GetRandomEnumValue<CatColor>() : specificColor.Value;
        catBuild = specificBuild == null ? GetRandomEnumValue<CatBuild>() : specificBuild.Value;
        catAge = specificAge == null ? GetRandomEnumValue<CatAge>() : specificAge.Value;
        catStatus = specificStatus == null ? GetRandomEnumValue<CatStatus>() : specificStatus.Value;
        catGender = specificGender == null ? GetRandomEnumValue<CatGender>() : specificGender.Value;

        color.sprite = colors[(int)catColor];

        if (catBuild != CatBuild.Skinny)
        {
            build.sprite = catBuild == CatBuild.Fat ? builds[0] : builds[1];
        }

        age.sprite = ages[(int)catAge];
        blink1.sprite = blinks1[(int)catAge];
        blink2.sprite = blinks2[(int)catAge];

        if (catStatus != CatStatus.Outside)
        {
            status.sprite = catStatus == CatStatus.Inside ? statuses[0] : statuses[1];
        }
        
        if (catGender == CatGender.Female)
        {
            gender.sprite = genders[0];
        }

        else if (catGender == CatGender.Schrodinger)
        {
            gender.sprite = genders[1];
        }

        StartCoroutine(Blink());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Laugh());
        }
    }

    public void SetSeat(Seat catSeat)
    {
        this.catSeat = catSeat;
        transform.position = catSeat.GetPosition;
    }

    public CatData GetCatData()
    {
        CatData catData = new CatData(catSeat.GetRowNumber, catSeat.GetSeatNumber, catColor, catBuild, catAge, catStatus, catGender);
        return catData;
    }

    T GetRandomEnumValue<T>()
    {
        System.Array enumValues = System.Enum.GetValues(typeof(T));
        T randomEnumValue = (T)enumValues.GetValue(Random.Range(0, enumValues.Length));
        return randomEnumValue;
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
