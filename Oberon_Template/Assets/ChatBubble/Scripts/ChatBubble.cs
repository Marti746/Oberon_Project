using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{

    public static void Create(Transform parent, Vector3 localPosition, IconType iconType, string text) {
        Transform chatBubbleTransform = Instantiate(GameAssets.i.pfChatBubble, parent);
        chatBubbleTransform.localPosition = localPosition;

        chatBubbleTransform.GetComponent<ChatBubble>().Setup(iconType, text);

        Destroy(chatBubbleTransform.gameObject, 4f);
    }

    public enum IconType {
        Flint,
        Guy,
    }


   [SerializeField] private Sprite flintIconSprite;
   [SerializeField] private Sprite guyIconSprite;

   private SpriteRenderer backgroundSpriteRenderer;
   private SpriteRenderer iconSpriteRenderer;
   private TextMeshPro textMeshPro;

   private void Awake() {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
   }

   private void Setup(IconType iconType, string text) {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate(true);
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(2f, 1f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(-2f, 0f);
        backgroundSpriteRenderer.transform.localPosition = 
            new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;

        iconSpriteRenderer.sprite = GetIconSprite(iconType);
   }

   private Sprite GetIconSprite(IconType iconType) {
        switch (iconType) {
            default:
            case IconType.Flint:    return flintIconSprite;
            case IconType.Guy:      return guyIconSprite;
        }
   }


}
