
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class IntroDialogue : MonoBehaviour
{
    [Header("Intro")]
    [SerializeField] private GameObject introPanel;

    [Header("Grandma")]
    [SerializeField] private GameObject grandmaBubble;
    [SerializeField] private TMP_Text grandmaText;

    [Header("Hipster")]
    [SerializeField] private GameObject hipsterBubble;
    [SerializeField] private TMP_Text hipsterText;

    [Header("Typing")]
    [SerializeField] private float charactersPerSecond = 45f;
    [SerializeField] private float introDelay = 3f;

    private int dialogueStep = 0;
    private bool isTyping = false;
    private bool isWaiting = true;

    private Coroutine typingCoroutine;
    private TMP_Text currentText;

    void Start()
    {
        dialogueStep = 0;
        isWaiting = true;
        isTyping = false;

        introPanel.SetActive(true);

        grandmaBubble.SetActive(true);
        grandmaText.gameObject.SetActive(true);

        hipsterBubble.SetActive(false);
        hipsterText.gameObject.SetActive(false);

        // Hide grandma's text until typing begins
        grandmaText.maxVisibleCharacters = 0;

        // Pause gameplay during the intro
        Time.timeScale = 0f;

        // Wait before starting the dialogue
        StartCoroutine(StartIntroWithDelay());
    }

    IEnumerator StartIntroWithDelay()
    {
        yield return new WaitForSecondsRealtime(introDelay);

        isWaiting = false;
        StartTyping(grandmaText);
    }

    void Update()
    {
        // Ignore clicks during the initial delay
        if (isWaiting)
            return;

        bool clicked = Mouse.current != null &&
                       Mouse.current.leftButton.wasPressedThisFrame;

        bool spacePressed = Keyboard.current != null &&
                            Keyboard.current.spaceKey.wasPressedThisFrame;

        if (!clicked && !spacePressed)
            return;

        // Reveal the full sentence if still typing
        if (isTyping)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            currentText.maxVisibleCharacters = int.MaxValue;
            isTyping = false;
            typingCoroutine = null;
            return;
        }

        // Switch from grandma to hipster
        if (dialogueStep == 0)
        {
            grandmaBubble.SetActive(false);
            grandmaText.gameObject.SetActive(false);

            hipsterBubble.SetActive(true);
            hipsterText.gameObject.SetActive(true);

            dialogueStep = 1;
            StartTyping(hipsterText);
        }
        // Close intro and start gameplay
        else if (dialogueStep == 1)
        {
            introPanel.SetActive(false);
            Time.timeScale = 1f;
            dialogueStep = 2;
        }
    }

    void StartTyping(TMP_Text text)
    {
        currentText = text;
        currentText.maxVisibleCharacters = 0;
        currentText.ForceMeshUpdate();

        isTyping = true;
        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        int totalCharacters = currentText.textInfo.characterCount;
        float delay = 1f / Mathf.Max(1f, charactersPerSecond);

        for (int i = 1; i <= totalCharacters; i++)
        {
            currentText.maxVisibleCharacters = i;
            yield return new WaitForSecondsRealtime(delay);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
