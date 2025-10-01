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
    public class GameManger
    {
        private event GameStart GameStarter;
        private string ProjectPath = Environment.CurrentDirectory.Replace("Debug", "").Replace("bin", "").Replace("\\\\", ""); //현재 프로젝트 경로 가져오기
        

        public GameManger() //생성자 생성시 게임세팅및 브금정보 로드 , 시작페이지 로드
        {
            GameStarter += GameSet;
            GameStarter += StartBGM;
            GameStarter += MainPage;
            //추후 다른 기능들 추가예정

            GameStarter?.Invoke();

        }
        ~GameManger()   //이벤트 제거
        {
            GameStarter = null;
        }

        public void StartBGM() //브금 재생 및 콘솔창 크기 지정
        {
            SoundPlayer musicPlayer = new SoundPlayer($"{ProjectPath}\\BGM\\battle-of-the-dragons-8037.wav");
            musicPlayer.PlayLooping(); //브금실행
        }
        public void GameSet() //콘솔창 크기지정 및 초기화면 출력
        {
            Console.Title = "Warrior of Sword"; //콘솔의 타이틀지정
            Console.CursorVisible = false; //커서 표시 안되게 지정

            Console.SetWindowSize(110, 50);
            Console.SetBufferSize(110, 50);  //스크롤 너비고정

        }

        public void MainPage() //시작창 표시 및 게임시작 , 종료 선택
        {
            bool isOff = false;
            bool isStart = false;

            Console.Clear();
            Console.SetCursorPosition(40, 5);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" __      __                     .__                       _____    _________                       .___\r\n/  \\    /  \\_____ ______________|__| ___________    _____/ ____\\  /   _____/_  _  _____________  __| _/\r\n\\   \\/\\/   /\\__  \\\\_  __ \\_  __ \\  |/  _ \\_  __ \\  /  _ \\   __\\   \\_____  \\\\ \\/ \\/ /  _ \\_  __ \\/ __ | \r\n \\        /  / __ \\|  | \\/|  | \\/  (  <_> )  | \\/ (  <_> )  |     /        \\\\     (  <_> )  | \\/ /_/ | \r\n  \\__/\\  /  (____  /__|   |__|  |__|\\____/|__|     \\____/|__|    /_______  / \\/\\_/ \\____/|__|  \\____ | \r\n       \\/        \\/                                                      \\/                         \\/ ");

            Console.SetCursorPosition(45, 14);
            Console.Write("===========");

            Console.SetCursorPosition(45, 15);
            Console.Write("1.게임시작");

            Console.SetCursorPosition(45, 16);
            Console.Write("===========");

            Console.SetCursorPosition(45, 19);
            Console.Write("===========");
            Console.SetCursorPosition(45, 20);
            Console.Write("2. 나가기");
            Console.SetCursorPosition(45, 21);
            Console.Write("===========");


            while (isOff == false && isStart == false)    //종료버튼 누르면 종료 게임시작누르면 할일 선택
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        isStart = true;
                        Console.ResetColor();
                        Loading("Loading...",2000);
                        //Player player = new Player(); //플레이어 생성
                        SelectMenu();
                        break;


                    case ConsoleKey.D2:
                        Console.ResetColor();
                        isOff = true;
                        break;
                }
            }

        }

        public void Loading(string text,int millLoad)        //로딩기능 - 텍스트는 인자값을 받아 접속중, 로딩중 등등
        {
            Console.Clear();
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(text);
            Thread.Sleep(millLoad);
            
        }

        public void SelectMenu()
        {
            
            bool isMoveMain = false;
            while (isMoveMain == false)     //메인메뉴로 돌아가기
            {
                Console.Clear();
               
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.SetCursorPosition(45, 9);
                Console.Write("===========");
                Console.SetCursorPosition(45, 10);
                Console.Write("1.던전입장");
                Console.SetCursorPosition(45, 11);
                Console.Write("===========");

                Console.SetCursorPosition(45, 14);
                Console.Write("===========");
                Console.SetCursorPosition(45, 15);
                Console.Write("2. 장비");
                Console.SetCursorPosition(45, 16);
                Console.Write("===========");

                Console.SetCursorPosition(45, 19);
                Console.Write("===========");
                Console.SetCursorPosition(45, 20);
                Console.Write("3.인벤토리");
                Console.SetCursorPosition(45, 21);
                Console.Write("===========");

                Console.SetCursorPosition(45, 24);
                Console.Write("===========");
                Console.SetCursorPosition(45, 25);
                Console.Write("4. 상점");
                Console.SetCursorPosition(45, 26);
                Console.Write("===========");

                Console.SetCursorPosition(45, 29);
                Console.Write("===========");
                Console.SetCursorPosition(45, 30);
                Console.Write("5.메인으로");
                Console.SetCursorPosition(45, 31);
                Console.Write("===========");


                Console.SetCursorPosition(65, 40);
                Console.Write($"현재 보유중인 골드 : {Player.gold}");



                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                       
                        Loading("던전 입장중..",2000);
                       
                        break;

                    case ConsoleKey.D2:
                       
                        break;

                    case ConsoleKey.D3:

                        break;

                    case ConsoleKey.D4:

                        break;

                    case ConsoleKey.D5:
                        Loading("메인 화면으로 이동중..",1000);
                        MainPage();
                        break;
                }
               
            }
        }
    }
}
