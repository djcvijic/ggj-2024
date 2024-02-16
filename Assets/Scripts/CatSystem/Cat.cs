using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] protected List<Sprite> colors;
    [SerializeField] protected List<Sprite> builds;
    [SerializeField] protected List<Sprite> ages;
    [SerializeField] protected List<Sprite> statuses;
    [SerializeField] protected List<Sprite> genders;

    [SerializeField] protected SpriteRenderer color;
    [SerializeField] protected SpriteRenderer build;
    [SerializeField] protected SpriteRenderer age;
    [SerializeField] protected SpriteRenderer status;
    [SerializeField] protected SpriteRenderer gender;

    protected CatColor catColor;
    protected CatBuild catBuild;
    protected CatAge catAge;
    protected CatStatus catStatus;
    protected CatGender catGender;

    public virtual void Initialize()
    {
        catColor = Util.GetRandomEnumValue<CatColor>();
        catBuild = Util.GetRandomEnumValue<CatBuild>();
        catAge = Util.GetRandomEnumValue<CatAge>();
        catStatus = Util.GetRandomEnumValue<CatStatus>();
        catGender = Util.GetRandomEnumValue<CatGender>();

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
            gender.sprite = genders[0];
        }
    }
}