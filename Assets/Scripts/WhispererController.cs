using RuleSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private List<GameObject> rulesList = new List<GameObject>();

    void Start()
    {
        backButton.onClick.AddListener(UnloadPage);
        closeButton.onClick.AddListener(BackToMainMenu);

        for(int i = 0; i < 14; i++)
        {
            GameObject pageSelection = Instantiate(pageSelectionPrefab, pageSelectionParent);
            int pageNumber = i + 1;
            pageSelection.GetComponentInChildren<TextMeshProUGUI>().text = i==0 ? pageNumber + ". " + "Rules for " + pageNumber + " cat" : pageNumber + ". " + "Rules for " + pageNumber + " cats";
            pageSelection.GetComponent<Button>().onClick.AddListener(() => LoadPage(pageNumber));
        }
        GameObject tableOfContents = Instantiate(pageSelectionPrefab, pageSelectionParent);
        tableOfContents.GetComponentInChildren<TextMeshProUGUI>().text = "15. Table Of Contents";
        tableOfContents.GetComponent<Button>().onClick.AddListener(() => LoadTableOfContentsPage());
    }

    private void LoadPage(int pageNumber)
    {
        var pageText = RuleBook.Instance.GetPageText(pageNumber);
        var rules = pageText.rules;
        var finalInstruction = pageText.elseInstruction;

        foreach (var rule in rules)
        {
            GameObject ruleTextObject = Instantiate(rulesPrefab, rulesParent);
            ruleTextObject.GetComponentInChildren<TextMeshProUGUI>().text = rule.rule + " " + rule.instruction.instruction;
            rulesList.Add(ruleTextObject);
        }

        backButton.gameObject.SetActive(true);
        pagesScroll.SetActive(false);
        rulesScroll.SetActive(true);
    }

    private void LoadTableOfContentsPage()
    {

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
