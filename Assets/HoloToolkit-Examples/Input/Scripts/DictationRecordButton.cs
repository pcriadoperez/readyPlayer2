// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    public class DictationRecordButton : MonoBehaviour, IInputClickHandler, IDictationHandler
    {
        [SerializeField]
        [Range(0.1f, 5f)]
        [Tooltip("The time length in seconds before dictation recognizer session ends due to lack of audio input in case there was no audio heard in the current session.")]
        private float initialSilenceTimeout = 2f;

        [SerializeField]
        [Range(5f, 60f)]
        [Tooltip("The time length in seconds before dictation recognizer session ends due to lack of audio input.")]
        private float autoSilenceTimeout = 10f;

        [SerializeField]
        [Range(1, 60)]
        [Tooltip("Length in seconds for the manager to listen.")]
        private int recordingTime = 5;

        [SerializeField]
        private TextMesh speechToTextOutput;

        [SerializeField]
        private GameObject recordLight;

        private Renderer buttonRenderer;

        private bool isRecording;

        private void Awake()
        {
            buttonRenderer = GetComponent<Renderer>();
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            ToggleRecording();
        }

        private void ToggleRecording()
        {
            if (isRecording)
            {
                isRecording = false;
                StartCoroutine(DictationInputManager.StopRecording());
                speechToTextOutput.color = Color.white;
                buttonRenderer.enabled = true;
                recordLight.SetActive(false);
                Debug.LogWarning("stop recording");
            }
            else
            {
                isRecording = true;
                StartCoroutine(DictationInputManager.StartRecording(initialSilenceTimeout, autoSilenceTimeout, recordingTime));
                speechToTextOutput.color = Color.green;
                recordLight.SetActive(true);
                buttonRenderer.enabled = false;
                Debug.LogWarning("start recording");
            }
        }

        public void OnDictationHypothesis(DictationEventData eventData)
        {
            Debug.LogWarning("OnDictationHypothesis");
            speechToTextOutput.text = eventData.DictationResult;
        }

        public void OnDictationResult(DictationEventData eventData)
        {   
            Debug.LogWarning("OndictationResult");
            speechToTextOutput.text = eventData.DictationResult;
        }

        public void OnDictationComplete(DictationEventData eventData)
        {
            Debug.LogWarning("onComplete");
            speechToTextOutput.text = eventData.DictationResult;
        }

        public void OnDictationError(DictationEventData eventData)
        {
            Debug.LogWarning("onDictationError");
            isRecording = false;
            speechToTextOutput.color = Color.red;
            buttonRenderer.enabled = true;
            recordLight.SetActive(false);
            speechToTextOutput.text = eventData.DictationResult;
            Debug.LogError(eventData.DictationResult);
            StartCoroutine(DictationInputManager.StopRecording());
        }
    }
}
