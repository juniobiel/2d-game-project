using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction", menuName = "ScriptableObjects/Interaction/CreateObject", order = 1)]
public class InteractionObjectSO : ScriptableObject
{
    public List<string> InitialPhrase;
    public List<string> InteractionMessage;
    public List<Sprite> InteractionSprites;
    public CollectableItem CollectableItem;
    public InteractionItems ItemName;
}
