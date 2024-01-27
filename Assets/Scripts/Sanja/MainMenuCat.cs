using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCat : MonoBehaviour
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

    private void Start()
    {
        CreateCat();
    }

    public void CreateCat()
    {
        catColor = GetRandomEnumValue<CatColor>();
        catBuild = GetRandomEnumValue<CatBuild>();
        catAge = GetRandomEnumValue<CatAge>();
        catStatus = GetRandomEnumValue<CatStatus>();
        catGender = GetRandomEnumValue<CatGender>();

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
    }
    T GetRandomEnumValue<T>()
    {
        System.Array enumValues = System.Enum.GetValues(typeof(T));
        T randomEnumValue = (T)enumValues.GetValue(Random.Range(0, enumValues.Length));
        return randomEnumValue;
    }

}
