using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhispererController : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject rulesScroll;
    [SerializeField] private GameObject pagesScroll;
    [SerializeField] private GameObject pageSelectionPrefab;
    [SerializeField] private Transform pageSelectionParent;

    void Start()
    {
        backButton.onClick.AddListener(UnloadPage);

        for(int i = 0; i < 14; i++)
        {
            GameObject pageSelection = Instantiate(pageSelectionPrefab, pageSelectionParent);
            int pageNumber = i + 1;
            pageSelection.GetComponentInChildren<TextMeshProUGUI>().text = i==0 ? pageNumber + ". " + "Rules for " + pageNumber + " cat" : pageNumber + ". " + "Rules for " + pageNumber + " cats";
            pageSelection.GetComponent<Button>().onClick.AddListener(() => LoadPage(pageNumber));
        }
        GameObject appendix = Instantiate(pageSelectionPrefab, pageSelectionParent);
        appendix.GetComponentInChildren<TextMeshProUGUI>().text = "15. Appendix";
        appendix.GetComponent<Button>().onClick.AddListener(() => LoadPage(15));
    }

    private void LoadPage(int pageNumber)
    {
        backButton.gameObject.SetActive(true);
        pagesScroll.SetActive(false);
        rulesScroll.SetActive(true);
    }

    private void UnloadPage()
    {
        backButton.gameObject.SetActive(false);
        pagesScroll.SetActive(true);
        rulesScroll.SetActive(false);
    }
}
