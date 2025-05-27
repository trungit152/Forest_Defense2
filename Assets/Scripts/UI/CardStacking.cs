using UnityEngine;
using System.Collections.Generic;

public class CardStacking : MonoBehaviour
{
    [SerializeField] private float outerSpacing = 10f;
    [SerializeField] private float innerSpacing = -200f;
    [SerializeField] private int overlapStartIndex = 4; 
    private List<Transform> cards = new List<Transform>();

    void Start()
    {
        UpdateCardPositions();
    }

    void UpdateCardPositions()
    {
        cards.Clear();
        foreach (Transform child in transform)
        {
            cards.Add(child);
        }

        int cardCount = cards.Count;
        float totalWidth = 0f;
        bool useOverlap = cardCount > overlapStartIndex;

        if (cardCount <= overlapStartIndex)
        {
            // Khi thẻ ít, sắp xếp cách đều
            totalWidth = (cardCount - 1) * outerSpacing;
        }
        else
        {
            // Khi thẻ nhiều, các thẻ ngoài cách đều, thẻ trong xếp chồng
            totalWidth = (overlapStartIndex - 1) * outerSpacing + (cardCount - overlapStartIndex) * innerSpacing;
        }

        float startX = -totalWidth / 2f; // Căn giữa hệ thống thẻ

        for (int i = 0; i < cardCount; i++)
        {
            float xOffset;

            if (cardCount <= overlapStartIndex)
            {
                xOffset = startX + outerSpacing * i;
            }
            else
            {
                xOffset = startX + ((i < overlapStartIndex) ? outerSpacing * i :
                                    outerSpacing * (overlapStartIndex - 1) + innerSpacing * (i - overlapStartIndex + 1));
            }


            cards[i].localPosition = new Vector3(xOffset, 0, 0);
        }
    }
    void Update()
    {
        UpdateCardPositions();
    }
}
