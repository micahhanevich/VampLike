using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControls : MonoBehaviour
{
    public GameObject BlankCard;
    public GameObject MultiShotCard;
    public GameObject HuntingBowCard;
    public GameObject PaddedBootsCard;
    public GameObject WideShaftsCard;
    public GameObject ThickCloakCard;
    public GameObject HeavyCloakCard;

    public static CardCollection.CARDS PickCard()
    {
        int result = Random.Range(1, System.Enum.GetNames(typeof(CardCollection.CARDS)).Length);
        
        switch (result)
        {
            case 1:
                return CardCollection.CARDS.MULTISHOT;
            case 2:
                return CardCollection.CARDS.HUNTINGBOW;
            case 3:
                return CardCollection.CARDS.PADDEDBOOTS;
            case 4:
                return CardCollection.CARDS.WIDESHAFTS;
            case 5:
                return CardCollection.CARDS.THICKCLOAK;
            case 6:
                return CardCollection.CARDS.HEAVYCLOAK;
            default:
                return CardCollection.CARDS.BLANK;
        }
    }

    public static CardCollection.CARDS[] PickCards(int numberOfOptions, bool smartPick = false)
    {
        CardCollection.CARDS[] cards = new CardCollection.CARDS[numberOfOptions];
        
        for (int i = 0; i < numberOfOptions; i++)
        {
            bool set = true;
            CardCollection.CARDS pickedCard = PickCard();
            foreach (CardCollection.CARDS card in cards)
            {
                if (card == pickedCard) { i--; set = false; break; }
            }
            if (set) { cards[i] = pickedCard; }
        }

        return cards;
    }

    public void SetupLevelUpMenu(int numberOfOptions = 3)
    {
        CardCollection.CARDS[] cards = PickCards(numberOfOptions);
        RectTransform[] cardobjects = new RectTransform[numberOfOptions];

        for (int i = 0; i < numberOfOptions; i++)
        {
            switch (cards[i])
            {
                case CardCollection.CARDS.MULTISHOT:
                    cardobjects[i] = Instantiate(MultiShotCard, ResourceControls.Player.LevelupMenu.transform, false).GetComponent<RectTransform>();
                    break;
                case CardCollection.CARDS.HUNTINGBOW:
                    cardobjects[i] = Instantiate(HuntingBowCard, ResourceControls.Player.LevelupMenu.transform, false).GetComponent<RectTransform>();
                    break;
                case CardCollection.CARDS.PADDEDBOOTS:
                    cardobjects[i] = Instantiate(PaddedBootsCard, ResourceControls.Player.LevelupMenu.transform, false).GetComponent<RectTransform>();
                    break;
                case CardCollection.CARDS.WIDESHAFTS:
                    cardobjects[i] = Instantiate(WideShaftsCard, ResourceControls.Player.LevelupMenu.transform, false).GetComponent<RectTransform>();
                    break;
                case CardCollection.CARDS.THICKCLOAK:
                    cardobjects[i] = Instantiate(ThickCloakCard, ResourceControls.Player.LevelupMenu.transform, false).GetComponent<RectTransform>();
                    break;
                case CardCollection.CARDS.HEAVYCLOAK:
                    cardobjects[i] = Instantiate(HeavyCloakCard, ResourceControls.Player.LevelupMenu.transform, false).GetComponent<RectTransform>();
                    break;
            }
        }

        switch (cardobjects.Length)
        {
            case 1:
                cardobjects[0].localPosition= new Vector3() { x = 0f, y = -85f, z = 0f };
                break;
            case 2:
                cardobjects[0].localPosition = new Vector3() { x = -180f, y = -85f, z = 0f };
                cardobjects[1].localPosition = new Vector3() { x = 180f, y = -85f, z = 0f };
                break;
            case 3:
                cardobjects[0].localPosition = new Vector3() { x = -280f, y = -85f, z = 0f };
                cardobjects[1].localPosition = new Vector3() { x = 0f, y = -85f, z = 0f };
                cardobjects[2].localPosition = new Vector3() { x = 280f, y = -85f, z = 0f };
                break;
        }
    }
}
