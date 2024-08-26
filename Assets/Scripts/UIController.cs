using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Main UI Elements")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] TMP_Text log;
    private string logText;

    [Header("Search UI Elements")]
    [SerializeField] GameObject searchMenu;
    [SerializeField] TMP_InputField s_idField;
    [SerializeField] TMP_Text s_idText;
    [SerializeField] TMP_Text s_nameText;
    [SerializeField] Transform resultContainer;

    [Header("Add UI Elements")]
    [SerializeField] GameObject addMenu;
    [SerializeField] TMP_InputField a_idField;
    [SerializeField] TMP_InputField a_nameField;

    [Header("Delete UI Elements")]
    [SerializeField] GameObject deleteMenu;
    [SerializeField] TMP_InputField d_idField;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        logText = "";
        MainMenu();
    }

    private void Update()
    {
        log.text = logText;
    }

    public void UpdateLogText(string text)
    {
        logText = text;
    }


    //API Hook Functions
    public void Search()
    {
        string s_id = s_idField.text;

        APIController.instance.GetEntry(s_id);
    }

    public void UpdateSearchResuts(string id, string name)
    {
        s_idText.text = id;
        s_nameText.text = name;
    }

    public void Add()
    {
        string a_id = a_idField.text;
        string a_name = a_nameField.text;

        APIController.instance.PutEntry(a_id, a_name);
    }

    public void Delete()
    {
        string d_id = d_idField.text;

        APIController.instance.DeleteEntry(d_id);
    }


    //Menu Navigation Functions
    public void SearchMenu()
    {
        mainMenu.SetActive(false);
        searchMenu.SetActive(true);
        addMenu.SetActive(false);
        deleteMenu.SetActive(false);
    }

    public void AddMenu()
    {
        mainMenu.SetActive(false);
        searchMenu.SetActive(false);
        addMenu.SetActive(true);
        deleteMenu.SetActive(false);
    }

    public void DeleteMenu()
    {
        mainMenu.SetActive(false);
        searchMenu.SetActive(false);
        addMenu.SetActive(false);
        deleteMenu.SetActive(true);
    }

    public void MainMenu()
    {
        searchMenu.SetActive(false);
        addMenu.SetActive(false);
        deleteMenu.SetActive(false);
        mainMenu.SetActive(true);

        logText = "";
    }
}
