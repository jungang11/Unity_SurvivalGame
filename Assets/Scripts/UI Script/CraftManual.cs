using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName;//이름
    public GameObject go_Prefab;// 실제 설치될 프리팹
    public GameObject go_PreviewPrefab;// 미리보기 프리팹
}

public class CraftManual : MonoBehaviour
{
    public static bool craftManualActivated = false;


    // 상태변수
    private bool isActivated = false;
    private bool isPreviewActivated = false;

    [SerializeField]
    private GameObject go_BaseUI;  // 기본 베이스  UI

    [SerializeField]
    private Craft[] craft_fire; // 모닥불용 탭

    private GameObject go_Preview;
    private GameObject go_Prefab; // 실제 생성될 프리팹을 담을 변수

    [SerializeField]
    private Transform tf_Player;

    // raycast 필요변수 선언
    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;

    public void SlotClick(int _slotNumber)
    {

        go_Preview = Instantiate(craft_fire[_slotNumber].go_PreviewPrefab, tf_Player.position + tf_Player.forward, Quaternion.identity);
        go_Prefab = craft_fire[_slotNumber].go_Prefab;
        isPreviewActivated = true;
        CloseWindow();
        //go_BaseUI.SetActive(false);

    }


    void Update()
    {

        Window();

        if (isPreviewActivated)
            PreviewPositionUpdate();

        if (Input.GetButtonDown("Fire1"))
            Build();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
    }

    private void Build()
    {
        if (isPreviewActivated)
        {
            Instantiate(go_Prefab, hitInfo.point, Quaternion.identity);
            Destroy(go_Preview);
            isActivated = false;
            isPreviewActivated = false;
            go_Prefab = null;
            go_Preview = null;
        }
    }

    private void PreviewPositionUpdate()
    {
        Debug.Log("Preview위치함수 실행");
        Debug.Log("physicsif문,");
        Debug.Log("th_Player.position" + tf_Player.position);
        Debug.Log("player.forward" + tf_Player.forward);
        Debug.Log("hitinfo" + hitInfo);
        Debug.Log("layermask" + layerMask);
        Debug.Log(Physics.Raycast(tf_Player.position, tf_Player.forward, out hitInfo, range, layerMask));
        if (Physics.Raycast(tf_Player.position, tf_Player.forward, out hitInfo, range, layerMask))
        {
            Debug.Log("광선이 어딘가에 맞는중");
            if (hitInfo.transform != null)
            {
                Vector3 _location = hitInfo.point;
                Debug.Log(hitInfo);
                go_Preview.transform.position = _location;
            }
        }
    }

    private void Window()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            craftManualActivated = !craftManualActivated;

            if (craftManualActivated)
                OpenWindow();
            else
                CloseWindow();
        }

    }

    private void Cancel()
    {
        if (isPreviewActivated)
            Destroy(go_Preview);

        isActivated = false;
        isPreviewActivated = false;
        go_Preview = null;
        go_Prefab = null;

        go_BaseUI.SetActive(false);

    }


    private void OpenWindow()
    {
        GameManager.isOpenCraftManual = true;
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        GameManager.isOpenCraftManual = false;
        isActivated = false;
        go_BaseUI.SetActive(false);
    }
}