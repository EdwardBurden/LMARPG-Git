using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField] private float MaxHeightFromPlayer;
    [SerializeField] private float MinHeightFromPlayer;
    [SerializeField] private Camera RoamCamera;

    private Vector3 CameraStartPos;
    private void Awake()
    {
        CameraStartPos = RoamCamera.transform.position;
    }

    private void Update()
    {
        Vector3 direcction = GameManager.Instance.ActivePlayer.transform.position - RoamCamera.transform.position;
        RoamCamera.transform.position += Input.mouseScrollDelta.y * direcction.normalized;



        RoamCamera.transform.rotation = Quaternion.LookRotation(GameManager.Instance.ActivePlayer.transform.position - RoamCamera.transform.position, Vector3.up);
        this.transform.position = new Vector3(GameManager.Instance.ActivePlayer.transform.position.x, this.transform.position.y, GameManager.Instance.ActivePlayer.transform.position.z);
    }
}
