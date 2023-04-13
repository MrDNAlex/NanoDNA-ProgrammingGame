using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;
using DNAMathAnimation;
using FlexUI;

public class InfoPanelController : MonoBehaviour
{
    public Flex Parent;
    public Flex Holder;

    public Vector3 OriginalPos;
    public Transform ParentTrans;

    public Language lang;
    public InfoPanelType panelType;


    public static void genPanel(InfoPanelType type)
    {
        GameObject panelParent = Camera.main.transform.GetChild(0).GetChild(2).gameObject;

        panelParent.SetActive(true);

        destroyChildren(panelParent);

        GameObject panel;

        switch (type)
        {
            case InfoPanelType.Quit:

                panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/InfoPanels/WarningPanel") as GameObject, panelParent.transform);

                panel.GetComponent<WarningInfoPanel>().setPanel(panelParent.transform, InfoPanelType.Quit);

                break;
            case InfoPanelType.Complete:

                panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/InfoPanels/WarningPanel") as GameObject, panelParent.transform);

                panel.GetComponent<WarningInfoPanel>().setPanel(panelParent.transform, InfoPanelType.Complete);

                break;
            case InfoPanelType.InfoTips:

                panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/InfoPanels/TipsPanel") as GameObject, panelParent.transform);

                panel.GetComponent<ExplainInfoPanel>().setPanel(panelParent.transform, InfoPanelType.InfoTips);
                break;
            case InfoPanelType.CollectibleDescription:

                panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/InfoPanels/ShortDescPanel") as GameObject, panelParent.transform);

                panel.GetComponent<DescriptionInfoPanel>().setPanel(panelParent.transform, InfoPanelType.CollectibleDescription);
                break;
            case InfoPanelType.LinesUsed:
                panel = GameObject.Instantiate(Resources.Load("Prefabs/EditPanels/InfoPanels/ShortDescPanel") as GameObject, panelParent.transform);

                panel.GetComponent<DescriptionInfoPanel>().setPanel(panelParent.transform, InfoPanelType.LinesUsed);
                break;
        }

    }

    public void closePanel()
    {
        ParentTrans.gameObject.SetActive(false);

        Destroy(this.gameObject);

        Scripts.programManager.updateVariables();
    }

    private void OnDestroy()
    {
        ParentTrans.localPosition = OriginalPos;
    }



    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void destroyChildren(GameObject Obj)
    {
        //Only deletes children under the one referenced
        foreach (Transform child in Obj.transform)
        {
            //Safe to delete
            GameObject.Destroy(child.gameObject);
        }
    }

}
