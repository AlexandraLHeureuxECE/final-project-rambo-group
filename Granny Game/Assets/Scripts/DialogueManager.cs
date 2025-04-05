using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public static DialogueManager Instance;

    private Coroutine typingCoroutine;
    private Coroutine hideCoroutine;

    private void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string message, float duration = 3f, float typeSpeed = 0.03f)
    {
        dialoguePanel.SetActive(true);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message, typeSpeed));
        hideCoroutine = StartCoroutine(HideDialogueAfterDelay(duration));
    }

    IEnumerator TypeText(string message, float typeSpeed)
    {
        dialogueText.text = "";

        foreach (char c in message)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialoguePanel.SetActive(false);
    }

    public void HideDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        dialoguePanel.SetActive(false);
    }
}