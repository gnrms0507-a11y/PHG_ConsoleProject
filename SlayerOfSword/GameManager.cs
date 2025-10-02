using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordMaker;

namespace SlayerOfSword
{
    delegate void GameStart();
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

        public void Loading(string text,int millLoad)        //로딩기능 - 텍스트는 인자값을 받아 접속중, 로딩중 등등
        {

            windowSize.width = 80;
            windowSize.height = 50;

            Console.Clear();
            Console.ResetColor();
           
            Console.SetCursorPosition(windowSize.width / 2 - text.Length, windowSize.height / 2 - 3);
            Console.Write(text);

            for (int i =0; i<3; i++)
            {
                Console.Write(" .");
                Thread.Sleep(millLoad/3);
            }
            
        }

    }
}
