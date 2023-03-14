using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlexUI;

public class LevelSelectionScript : MonoBehaviour
{

    [SerializeField] GameObject levelCardPrefab;

    [Header("UI")]
    [SerializeField] RectTransform background;
    // Start is called before the first frame update
    void Start()
    {
        setUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void setUI ()
    {
        Flex Background = new Flex(background, 1);

      
        for (int i = 0; i < 3; i ++)
        {
            GameObject card = Instantiate(levelCardPrefab, Background.UI);

            Background.addChild(card.GetComponent<LevelSelecCard>().flex);

        }

        Background.setVerticalPadding(0.1f, 1, 0.1f, 1);
        Background.setHorizontalPadding(0.3f, 1, 0.3f, 1);


        Background.setSpacingFlex(1, 1);

        Background.setSize(Flex.ScreenSize());

    }




}
