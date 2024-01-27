using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuleSystem;

public class Cat : MonoBehaviour
{
    [SerializeField] private List<Sprite> colors;
    [SerializeField] private List<Sprite> builds;
    [SerializeField] private List<Sprite> ages;
    [SerializeField] private List<Sprite> statuses;
    [SerializeField] private List<Sprite> genders;

    [SerializeField] private SpriteRenderer color;
    [SerializeField] private SpriteRenderer build;
    [SerializeField] private SpriteRenderer age;
    [SerializeField] private SpriteRenderer status;
    [SerializeField] private SpriteRenderer gender;

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

        if (catStatus != CatStatus.Outside)
        {
            status.sprite = catStatus == CatStatus.Inside ? statuses[0] : statuses[1];
        }
        
        if (catGender == CatGender.Female)
        {
            gender.sprite = genders[(int)catAge];
        }

        else if (catGender == CatGender.Schrodinger)
        {
            gender.sprite = genders[3];
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
}
