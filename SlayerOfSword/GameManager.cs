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
    public delegate void GameStart();
    public class GameManager
    {
        private event GameStart GameStarter;
        private string ProjectPath = Environment.CurrentDirectory.Replace("Debug", "").Replace("bin", "").Replace("\\\\", ""); //현재 프로젝트 경로 가져오기

        //몬스터를 제외한 객체 생성 (페이지 이동을 위한..)
        Inventory inventory = new Inventory();
        Store store = new Store();
        MainPage mainPage = new MainPage();


        public GameManager() //생성자 생성시 게임세팅및 브금정보 로드 , 시작페이지 로드
        {
            GameStarter += GameSet;
            GameStarter += StartBGM;
            GameStarter += mainPage.PrintMainPage;
            //추후 다른 기능들 추가예정

            GameStarter?.Invoke();

        }
        ~GameManager()   //이벤트 제거
        {
            GameStarter = null;
        }

        public void StartBGM() //브금 재생 및 콘솔창 크기 지정
        {
            SoundPlayer musicPlayer = new SoundPlayer($"{ProjectPath}\\BGM\\battle-of-the-dragons-8037.wav");
            musicPlayer.PlayLooping(); //브금실행
        }
        public void GameSet() //콘솔창 크기지정 및 초기화면 출력 및 객체 선언
        {
            Console.Title = "Slayer of Sword"; //콘솔의 타이틀지정
            Console.CursorVisible = false; //커서 표시 안되게 지정

            Console.SetWindowSize(100, 55);
            Console.SetBufferSize(100, 55);  //스크롤 너비고정
            Console.SetWindowPosition(0, 0);

        }

        public static void Loading(string text,int millLoad)        //로딩기능 - 텍스트는 인자값을 받아 접속중, 로딩중 등등
        {
            Console.Clear();
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(text);
            Thread.Sleep(millLoad);
            
        }
    }
}
