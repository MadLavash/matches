using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class levelBehaviour : MonoBehaviour
{
    public GameObject matchFab;

    private const float SPACE_BETWEEN_MATCHES = 5f;
    private const float AI_CHOOSING_TIME = 1f;
    private RectTransform canvasTransform;
    private controlBehaviour controlScript;
    private List<matchBehaviour> listOfSelectedElements = new List<matchBehaviour>();
    private List<matchBehaviour> allMatchesList = new List<matchBehaviour>();

    void Awake()
    {
        controlScript = Camera.main.GetComponent<controlBehaviour>();
    }

    public void CreateGamefield(int matchesCount)
    {
        float spawnDistance = matchFab.GetComponent<RectTransform>().rect.width + SPACE_BETWEEN_MATCHES;
        float startPointX = -(spawnDistance * matchesCount) / 2;
        canvasTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        Vector3 spawnPositon = new Vector3(startPointX, 0);
        GameObject createdMatch;

        for (int index = 0; index < matchesCount; index++)
        {
            createdMatch = Instantiate(matchFab, spawnPositon, Quaternion.identity) as GameObject;
            createdMatch.transform.SetParent(canvasTransform, false);

            allMatchesList.Add(createdMatch.GetComponent<matchBehaviour>());

            allMatchesList[index].matchSelected += MatchSelected;
            allMatchesList[index].matchSelected += controlScript.PlayerHasSelectedMatch;
            allMatchesList[index].matchDeselected += MatchDeselected;
            allMatchesList[index].matchDeselected += controlScript.PlayerHasDeselectedMatch;

            spawnPositon[0] += spawnDistance;
        }

    }

    void MatchSelected(object sender, EventArgs e)
    {
        listOfSelectedElements.Add((matchBehaviour)sender);
    }

    void MatchDeselected(object sender, EventArgs e)
    {
        listOfSelectedElements.Remove((matchBehaviour)sender);
    }

    public void DeactivateSelectedMatches()
    {
        for (int index = 0; index < listOfSelectedElements.Count; index++)
        {
            listOfSelectedElements[index].SetActive(false);
            allMatchesList.Remove(listOfSelectedElements[index]);
        }

        listOfSelectedElements.Clear();
    }

    public void ChoosingForAI(int selectedMatchesCount)
    {
        StartCoroutine(ChoosingForAITimer(selectedMatchesCount));
    }

    IEnumerator ChoosingForAITimer(int selectedMatchesCount)
    {
        for (int index = 0; index < selectedMatchesCount; index++)
        {
            yield return new WaitForSeconds(AI_CHOOSING_TIME);
            allMatchesList[index].ComputerSelectedMatch();
        }

        yield return new WaitForSeconds(AI_CHOOSING_TIME);
        controlScript.MoveIsMade();
    }

}
