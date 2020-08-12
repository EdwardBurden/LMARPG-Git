using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderElement : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private SceneElementLoadingType LoadingType;
    [SerializeField] private int Distance;

    private bool IsLoaded;

    public void Start() { }


    private void Update()
    {
        if (LoadingType == SceneElementLoadingType.Distance)
        {
            if (Vector3.Distance(GameManager.Instance.ActivePlayer.transform.position, this.transform.position) < Distance)
            {
                LoadScene();
            }
            else
            {
                UnLoadScene();
            }

        }
    }

    private void UnLoadScene()
    {
        if (IsLoaded)
        {
            SceneManager.UnloadSceneAsync(SceneName);
            IsLoaded = false;
        }
    }

    private void LoadScene()
    {
        if (!IsLoaded)
        {
            SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            IsLoaded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCharcater>() != null && other.GetComponent<PlayerCharcater>() == GameManager.Instance.ActivePlayer)
        LoadScene();
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerCharcater>() != null && other.GetComponent<PlayerCharcater>() == GameManager.Instance.ActivePlayer)
            UnLoadScene();
    }

}
