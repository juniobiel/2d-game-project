using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction", menuName = "ScriptableObjects/Interaction/CreateObject", order = 1)]
public class InteractionObjectSO : ScriptableObject
{
    public string InitialPhrase;
    public string InteractionMessage;
    public List<Sprite> InteractionSprites;
    public CollectableItem CollectableItem;
}
