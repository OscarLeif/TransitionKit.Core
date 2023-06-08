using AtaGames.TransitionKit.runtime;
using System;
using UnityEngine;

namespace AtaGames.TransitionKit
{
    public class TransitionKit : MonoBehaviour
    {
        public static TransitionKit Get;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoInit()
        {
            if (Get == null)
            {
                GameObject gameObject = new GameObject(nameof(TransitionKit));
                Get = gameObject.AddComponent<TransitionKit>();
                DontDestroyOnLoad(gameObject);
            }
        }

        private FadeTransition fadeTransition;

        public bool isWorking = false;

        public string NextSceneName;
        public int NextSceneIndex;

        public System.Action OnTransitionStart;
        public System.Action OnTransitionEnd;

        public System.Action BeforeSceneLoad;
        public System.Action AfterSceneLoad;

        private void Awake()
        {
            GameObject FadeTransition = new GameObject(nameof(FadeTransition));
            FadeTransition.transform.parent = transform;
            fadeTransition = FadeTransition.AddComponent<FadeTransition>();
            fadeTransition.TransitionKit = this;
        }

        public void FadeScene(int sceneIndex, float duration, Color color)
        {
            if (isWorking) { return; }
            if (fadeTransition == null) { }
            NextSceneIndex = sceneIndex;
            NextSceneName = string.Empty;
            fadeTransition.duration = duration;
            fadeTransition.ResetCounter();
            fadeTransition.gameObject.SetActive(true);
        }

        public void FadeScene(string sceneName, float duration, Color color)
        {
            if (isWorking) { return; }
            if (fadeTransition == null) { }
            NextSceneName = sceneName;
            NextSceneIndex = -1;
            fadeTransition.duration = duration;
            fadeTransition.ResetCounter();
            fadeTransition.gameObject.SetActive(true);
        }

        public void FadeScreen(float duration, Color color)
        {
            if (isWorking) { return; }
            NextSceneName = string.Empty;
            NextSceneIndex = -1;
            fadeTransition.ResetCounter();
        }

        public void CompletedTransition()
        {
            OnTransitionEnd?.Invoke();
            isWorking = false;
            NextSceneName = string.Empty;
            NextSceneIndex = -1;
        }
    }
}