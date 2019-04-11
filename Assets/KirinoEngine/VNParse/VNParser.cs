using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace KirinoEngine
{
/*

    public class VNParser : MonoBehaviour
    {

        // 현재 어떤 스토리를 진행중
        public bool isPlaying
        {
            get;
            protected set;
        }

        const string syntax_background = "scene";
        const string syntax_show = "show";
        const string syntax_hide = "hide";
        const string syntax_voice = "voice";
        const string syntax_musicPlay = "play";
        const string syntax_musicStop = "stop_music";
        const string syntax_audio = "audio";
        const string syntax_loadScene = "load";

        protected Script loadedScript;

        private IEnumerator scriptIterator;
        private IEnumerator lineValidateIterator;

        // bool 을 리턴하는 함수를 넣는 곳. 참 거짓 평가가 필요한 키워드를 구현할때 쓴다.
        // 1. 단순하게 True False 가 이미 결정된 키워드를 넣는 방법
        // 2. True False 가 최종적으로 결정되는 어떤 처리를 지정하는 것도 된다.
        protected Dictionary<string, Func<bool>> conditions = new Dictionary<string, Func<bool>>();

        // 입력과 리턴이 없는 함수를 넣는 곳. 입력과 결과가 없는 수행만을 하는 키워드를 구현할때 쓴다.
        protected Dictionary<string, Action> actions = new Dictionary<string, Action>();

        // 입력과 리턴이 존재할 수 있는 함수를 넣는 곳. 입력을 넣어 무언가 수행하는 키워드를 구현할때 쓴다.
        protected Dictionary<string,Delegate> functions = new Dictionary<string,Delegate>();

        // 키리노 스크립트에서 리턴 키워드를 만났을 경우에 실행할 것들
        public delegate void ReturnEvent(); // IEnumerator OnReturn() 으로 나중에 대체해야함
        public event ReturnEvent onReturn = delegate {};


        // 시리얼라이즈화해서 저장할 이름
        const string serializedScripFileName = "script.bin";
        const string serializedStringFileName = "string.csv";

        const string editorRpyScriptsPath = "/RPY";


        // 경로에 있는 모든 스크립트를 가져와서 시리얼라이즈화 해주는 함수
        // 스크립트를 유니티 에디터에서 미리 파일로 시리얼라이즈 화를 먼저 해야 쓸수 있음
        // 시리얼라이즈화 해서 저장할 장소는 스트리밍애셋 패쓰를 사용하자
        // 시리얼라이즈화 안된 스크립트를 가져올 장소는 별개로 지정
        // 빌드시에 원본 스크립트는 나중에 따로 삭제하면 원본 스크립트 유출안될거임
        // 유니티 에디터상에서만 쓰는 함수
        public void SerializeScript()
        {
            string dataPath = Application.dataPath + editorRpyScriptsPath;

            loadedScript = Script.FromSource(dataPath);

            string savePath = Application.streamingAssetsPath;

            string scriptPath = Path.Combine(savePath,serializedScripFileName);
            string stringPath = Path.Combine(savePath,serializedStringFileName);

            loadedScript.ToFiles(scriptPath, stringPath);
        }

        // 역시리얼라이즈화로 바이너리로 부터 스크립트 오브젝트를 생성하고 사용
        // 유니티 에디터와 빌드 모두에서 사용하는 함수
        // 스크립트를 메모리에 로드하려면 최초 1회는 실행되야 함
        private void LoadScript()
        {
            string dataPath = Application.streamingAssetsPath;

            string scriptPath = Path.Combine(dataPath,serializedScripFileName);
            string stringPath = Path.Combine(dataPath,serializedStringFileName);


            // Deserialize
            loadedScript = Script.FromFiles(scriptPath, stringPath);



			loadedScript.Conditions = conditions;
			loadedScript.Actions = actions;
            loadedScript.Functions = functions;

			loadedScript.Prime();
			loadedScript.Validate();
		}


        void Awake()
        {
            InitSyntax();
            LoadScript();
        }


        public void MoveNext()
        {
            if(lineValidateIterator.MoveNext())
            {
                StartCoroutine(lineValidateIterator.Current as IEnumerator);
            }
            else
            {
                Debug.Log("End of Line");
                if(scriptIterator.MoveNext())
                {
                    lineValidateIterator = scriptIterator.Current as IEnumerator;                   
                }
                else
                {
                    Debug.Log("End of Label");
                }
            }
        }

        // 리턴을 만났을때
        protected virtual IEnumerator OnReturn()
        {
            Debug.Log("Return");
            isPlaying = false;
			VNController.textDisplayer.HideDialogueHolder();
            onReturn();

            yield return null;
        }

        // 선택지가 선택된 순간에 무엇을 할까
        // 선택에 의해 스토리 분기가 달라지는 것은 VGPrompter에 이미 구현되있음
        // 여기서는 선택지에 Hook 되어 같이 발동될 무언가를 구현하는 곳
        // 입력: 선택된 것이 온다
        private IEnumerator OnChoiceSelected(Script.Menu menu, Script.Choice choice)
        {
            // Do nothing for now

            // Show the choice text after the choice has been selected
            Debug.Log(choice.ToString());
            yield return null;
        }


        protected virtual void InitSyntax()
        {
            conditions.Add("True", () => true);
            conditions.Add("False", () => false);

            actions.Add("DoNothing", () => { });
            actions.Add(syntax_musicStop, () => { new VNMusicStop().Invoke(); });
        }

        // 어떤 레이블에서 읽어내려가기를 시작
        public void Play(string labelName = "start")
        {
            Debug.Log("Label Play: " + labelName);
            isPlaying = true;

            LoadScript(); // 이터레이터가 리셋이 안되서, 일단은 다시 재로드하는 것으로.

            scriptIterator = loadedScript.GetLabelEnumerator(labelName, OnLine, SelectChoice, OnChoiceSelected, OnReturn);
            scriptIterator.MoveNext();

            lineValidateIterator = scriptIterator.Current as IEnumerator;

            MoveNext();
            
        }



        // 코루틴을 쓰지 않고 스크립트 루틴을 돌리도록 변경했으나
        // 선택지 부분에서는 집역적으로 입력을 대기하기 위해 루프를 돌려야함
        IEnumerator AutoMoveNextForWaitSelect()
        {
            while(true)
            {
                scriptIterator.MoveNext();
                yield return null;
            }
        }


        // 선택을 어떻게 입력할까, 입력을 구현하는 곳
        // 정확히는 여러 인덱스 중 하나를 골라서 내보내는 것을 구현
        protected IEnumerator<int?> SelectChoice(Script.Menu menu)
        {

            // 선택을 표시하기 위한 버튼들 인스턴스화
            for (int i = 0; i < menu.TrueCount; i++)
            {
                VNController.menuDisplayer.AddNewSelection(menu.TrueChoices[i].Text, menu.TrueChoices[i].Index);
            }

            StartCoroutine("AutoMoveNextForWaitSelect");

            while(true)
            {
                if (VNController.menuDisplayer.selectedButton)
                {
                    break;
                }

                yield return null;
            }

            StopCoroutine("AutoMoveNextForWaitSelect");

            // 결정된 선택지를 가져오고 버튼들 화면에서 지우기
            int selectedIndex = VNController.menuDisplayer.selectedButton.index;
            VNController.menuDisplayer.RemoveAllSelections();

            // 선택된 것을 리턴
            yield return menu.TrueChoices[selectedIndex].Index;
        }


        // 본격적인 파싱 부분.
        // 라인 by 라인으로 이터레이터의 MoveNext를 통해 읽어 나감
        protected IEnumerator OnLine(Script.DialogueLine line)
        {
            AudioClip clip;

            // 공백 자르기
            string[] words = Regex.Split(line.Text,"\\s+");

            switch (line.Tag)
            {
                case syntax_background:
                    new VNBackground(VNDataController.Instance.GetDisplayable(words[0],words[1])).Invoke();
                    break;

                case syntax_show:
                    new VNShow(VNDataController.Instance.GetDisplayable(words[0],words[1])).Invoke();
                    break;
                case syntax_hide:
                    new VNHide(line.Text).Invoke();
                    break;

                case syntax_voice:
                    new VNVoice(line.Text).Invoke();
                    break;

                case syntax_musicPlay:
                    clip = VNDataController.Instance.GetAudioClip(line.Text);
                    new VNMusicPlay(clip).Invoke();
                    break;

                case syntax_audio:
                    clip = VNDataController.Instance.GetAudioClip(line.Text);
                    new VNAudio(clip).Invoke();
                    break;

                case syntax_loadScene:
                    new VNLoadScene(line.Text).Invoke();
                    break;

                default:
                    new VNSay(line.Tag, line.Text).Invoke();
                    break;
            }

            yield return null;
        }
    }
    */
}