using RuleSystem;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WhispererController : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject rulesScroll;
    [SerializeField] private GameObject rulesPrefab;
    [SerializeField] private Transform rulesParent;

    [SerializeField] private GameObject pagesScroll;
    [SerializeField] private GameObject pageSelectionPrefab;
    [SerializeField] private Transform pageSelectionParent;

    [SerializeField] private GameObject catHolderPrefab;
    [SerializeField] private GameObject catPrefab;

    private List<GameObject> rulesList = new List<GameObject>();

    void Start()
    {
        backButton.onClick.SetListener(UnloadPage);
        closeButton.onClick.SetListener(BackToMainMenu);

        for(int i = 0; i < 14; i++)
        {
            GameObject pageSelection = Instantiate(pageSelectionPrefab, pageSelectionParent);
            int pageNumber = i + 1;
            pageSelection.GetComponentInChildren<TextMeshProUGUI>().text = i==0 ? pageNumber + ". " + "Rules for " + pageNumber + " cat" : pageNumber + ". " + "Rules for " + pageNumber + " cats";
            pageSelection.GetComponent<Button>().onClick.SetListener(() => LoadPage(pageNumber));
        }
        GameObject tableOfContents = Instantiate(pageSelectionPrefab, pageSelectionParent);
        tableOfContents.GetComponentInChildren<TextMeshProUGUI>().text = "15. Table Of Contents";
        tableOfContents.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(128, 35, 17, 255);
        tableOfContents.GetComponent<Button>().onClick.SetListener(() => LoadTableOfContentsPage());
    }

    private void LoadPage(int pageNumber)
    {
        var pageText = RuleBook.Instance.GetPageText(pageNumber);
        var rules = pageText.rules;
        var finalInstruction = pageText.elseInstruction;

        for(int i = 0; i < rules.Count; i++)
        {
            var ruleNumber = i + 1;
            GameObject ruleTextObject = Instantiate(rulesPrefab, rulesParent);
            ruleTextObject.GetComponentInChildren<TextMeshProUGUI>().text = ruleNumber + ". " + (ruleNumber == 1 ? "" : "Otherwise, ") + rules[i].rule + " " + rules[i].instruction.instruction;
            rulesList.Add(ruleTextObject);
        }

        GameObject finalInstructionObject = Instantiate(rulesPrefab, rulesParent);
        finalInstructionObject.GetComponentInChildren<TextMeshProUGUI>().text = (rules.Count + 1) + ". Otherwise, " + finalInstruction.instruction;
        rulesList.Add(finalInstructionObject);

        backButton.gameObject.SetActive(true);
        pagesScroll.SetActive(false);
        rulesScroll.SetActive(true);
    }

    private void LoadTableOfContentsPage()
    {

        backButton.gameObject.SetActive(true);
        pagesScroll.SetActive(false);
        rulesScroll.SetActive(true);

        GameObject title = Instantiate(rulesPrefab, rulesParent);
        title.GetComponentInChildren<TextMeshProUGUI>().text = "Your audience is inahabited by a diverse group of cats, here's how to diferentiate between them";
        rulesList.Add(title);

        GameObject colorTitle = Instantiate(rulesPrefab, rulesParent);
        colorTitle.GetComponentInChildren<TextMeshProUGUI>().text = "Cat Colors:";
        rulesList.Add(colorTitle);

        GameObject ageTitle = Instantiate(rulesPrefab, rulesParent);
        ageTitle.GetComponentInChildren<TextMeshProUGUI>().text = "Cat Ages:";
        rulesList.Add(ageTitle);

        GameObject genderTitle = Instantiate(rulesPrefab, rulesParent);
        genderTitle.GetComponentInChildren<TextMeshProUGUI>().text = "Cat Genders:";
        rulesList.Add(genderTitle);

        GameObject statusTitle = Instantiate(rulesPrefab, rulesParent);
        statusTitle.GetComponentInChildren<TextMeshProUGUI>().text = "Cat Statuses:";
        rulesList.Add(statusTitle);

        GameObject buildTitle = Instantiate(rulesPrefab, rulesParent);
        buildTitle.GetComponentInChildren<TextMeshProUGUI>().text = "Cat Builds:";
        rulesList.Add(buildTitle);
    }

    private void UnloadPage()
    {
        foreach (var rule in rulesList)
        {
            Destroy(rule);
        }   
        rulesList.Clear();
        backButton.gameObject.SetActive(false);
        pagesScroll.SetActive(true);
        rulesScroll.SetActive(false);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }
}
