using UnityEngine;
using UnityEngine.AddressableAssets;


namespace TheGame
{
    [CreateAssetMenu(fileName = "PopupReferences", menuName = "ScriptableObjects/Popup References")]
    public class PopupReferencesAddressable : ScriptableObject, IPopupReferences
    {
        [field : SerializeField] public AssetReferenceGameObject MothershipUI { get; private set; }
    }

    public interface IPopupReferences
    {
        AssetReferenceGameObject MothershipUI { get; }
    }
}
