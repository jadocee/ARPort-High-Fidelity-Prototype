using TMPro;
using UnityEngine;

public class DescriptionManager : MonoBehaviour
{
    public TextMeshPro DescriptionText;

    public string testdescription = "This is a sample Description";

    // Start is called before the first frame update
    private void Start()
    {
        DescriptionText.text = "test: " + testdescription;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}