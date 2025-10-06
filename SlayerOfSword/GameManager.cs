using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordMaker;
using EnumManager;

namespace SlayerOfSword
{
    delegate void GameStart();  //게임실행 델리게이트 생성
    struct TextVector      //텍스트 작성 위치 선정
    {
        public int x;
        public int y;

    }
    public class GameManager
    {
        struct WindowSize    //GameManager 내에서만 사용할것임
        {
            public int width;
            public int height;
        }

        WindowSize windowSize;

        private event GameStart GameStarter;

      

        //몬스터를 제외한 객체 생성 (페이지 이동을 위한..)
        public GameManager()
        {
            GameStart();    
        }

        static bool isGameStart;

        public void GameStart() //게임시작  콘솔창 세팅 및 브금재생
        {
            GameStarter += GameSetting;
            GameStarter += StartBGM;
            isGameStart = true;     //게임시작여부 정적 필드 true 로 변경 - 메인문에서 반복조건에 걸거임
            GameStarter?.Invoke();

            GameStarter -= GameSetting;
            GameStarter = StartBGM;
        }

        public void StartBGM() //브금 재생 및 콘솔창 크기 지정
        {
            string ProjectPath = Environment.CurrentDirectory.Replace("Debug", "").Replace("bin", "").Replace("\\\\", ""); //현재 프로젝트 경로 가져오기
            SoundPlayer musicPlayer = new SoundPlayer($"{ProjectPath}\\BGM\\battle-of-the-dragons-8037.wav");
            musicPlayer.PlayLooping(); //브금실행
        }

        public void GameSetting() //콘솔창 크기지정 및 초기화면 출력 및 객체 선언
        {
            Console.Title = "Slayer Of Sword"; //콘솔의 타이틀지정
            Console.CursorVisible = false; //커서 표시 안되게 지정

            windowSize.width = 80;
            windowSize.height = 50;

            Console.SetWindowSize(windowSize.width, windowSize.height);
            Console.SetBufferSize(windowSize.width, windowSize.height);  //스크롤 너비고정
            Console.SetWindowPosition(0, 0);
            Console.ResetColor();
        }

        public  void Loading(string text,int millLoad)        //로딩기능 - 텍스트는 인자값을 받아 접속중, 로딩중 등등
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            windowSize.width = 80;
            windowSize.height = 50;

            Console.Clear();
            
           
            Console.SetCursorPosition(windowSize.width / 2 - text.Length, windowSize.height / 2 - 3);
            Console.Write(text);

            for (int i =0; i<3; i++)
            {
                Console.Write(" .");
                Thread.Sleep(millLoad/3);
            }
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        public T MoveMenu<T>(T value) where T : Enum //열거형을 받아와서 선택한 키와 알맞은 열거형을 반환하는 메서드 - 페이지에서 메뉴선택할때 사용
        {
            bool isSelectMenu = false; //메뉴선택여부 확인

            T selectValue = default(T); //T(열거형)의 기본값을 할당함


            T[] values = (T[])Enum.GetValues(typeof(T));    //어떤 Enum형식이 들어올지 모르므로 메서드가 받은 enum타입을 가져와서 T배열로 만듬

            while (!isSelectMenu)      //메뉴 선택시 루프종료
            {
                //사용자의 키 받아오기
                ConsoleKeyInfo key = Console.ReadKey(true);

                string inputkey = key.KeyChar.ToString(); //읽어온 키값을 char형으로 변환후 다시 string으로 변환함(비교용이)

                //숫자키 1 과 넘버패드 사용

                foreach(T choice in values)
                {
                    int intVal = Convert.ToInt32(choice);

                    if(inputkey == intVal.ToString())
                    {
                        selectValue = choice;
                        isSelectMenu = true;
                        break;
                    }
                }


            }
            return selectValue;


        }




    }
}
