using UnityEngine;

public class NavigateMenuBehaviour : MonoBehaviour
{
    public GameObject[] pages;
    [SerializeField] private GameObject currentPage;


    // Start is called before the first frame update
    private void Start()
    {
        pages[0].SetActive(true);
        currentPage = pages[0];
        for (var i = 1; i < pages.Length; i++) pages[i].SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private GameObject GetCurrentPage()
    {
        return currentPage;
    }

    private void SetCurrentPage(GameObject page)
    {
        currentPage = page;
        currentPage.SetActive(true);
    }

    public void SwitchPage(int index)
    {
        GetCurrentPage().SetActive(false);
        SetCurrentPage(pages[index]);
    }
}