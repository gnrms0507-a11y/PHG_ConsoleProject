using SlayerOfSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordMaker
{
    class MainPage
    {
        private BattlePage battlePage = new BattlePage();   //배틀페이지로 넘어가기위한 객체생성
        private Player user;    //플레이어의 정보를 받은 객체
        
        public void PrintMainPage() //시작창 표시 및 게임시작 , 종료 선택
        {
            bool isOff = false;
            bool isStart = false;

            Console.Clear();
            Console.SetCursorPosition(0, 5);
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.Write(" _____ _                         _____  __   _____                       _ \r\n/  ___| |                       |  _  |/ _| /  ___|                     | |\r\n\\ `--.| | __ _ _   _  ___ _ __  | | | | |_  \\ `--.__      _____  _ __ __| |\r\n `--. \\ |/ _` | | | |/ _ \\ '__| | | | |  _|  `--. \\ \\ /\\ / / _ \\| '__/ _` |\r\n/\\__/ / | (_| | |_| |  __/ |    \\ \\_/ / |   /\\__/ /\\ V  V / (_) | | | (_| |\r\n\\____/|_|\\__,_|\\__, |\\___|_|     \\___/|_|   \\____/  \\_/\\_/ \\___/|_|  \\__,_|\r\n                __/ |                                                      \r\n               |___/  ");

            Console.SetCursorPosition(35, 18);
            Console.Write("===========");

            Console.SetCursorPosition(35, 19);
            Console.Write("1.게임시작");

            Console.SetCursorPosition(35, 20);
            Console.Write("===========");

            Console.SetCursorPosition(35, 23);
            Console.Write("===========");
            Console.SetCursorPosition(35, 24);
            Console.Write("2. 나가기");
            Console.SetCursorPosition(35, 25);
            Console.Write("===========");
            

            while (isOff == false && isStart == false)    //종료버튼 누르면 종료 게임시작누르면 할일 선택
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    isStart = true;
                    Console.ResetColor();
                    GameManager.Loading("Loading...", 2000);

                    if(Player.isPlayerReady==false)
                    {
                        user = Player.CreatePlayer();
                    }
                    
                    SelectMenu();
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    Console.ResetColor();
                    isOff = true;
                }

            }

        }

        public void SelectMenu()
        {

            bool isMoveMain = false;
            while (isMoveMain == false)     //메뉴선택 메인메뉴로 돌아가기
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.SetCursorPosition(40, 9);
                Console.Write("===========");
                Console.SetCursorPosition(40, 10);
                Console.Write("1. 전투");
                Console.SetCursorPosition(40, 11);
                Console.Write("===========");

                Console.SetCursorPosition(40, 14);
                Console.Write("===========");
                Console.SetCursorPosition(40, 15);
                Console.Write("2. 장비");
                Console.SetCursorPosition(40, 16);
                Console.Write("===========");

                Console.SetCursorPosition(40, 19);
                Console.Write("===========");
                Console.SetCursorPosition(40, 20);
                Console.Write("3.인벤토리");
                Console.SetCursorPosition(40, 21);
                Console.Write("===========");

                Console.SetCursorPosition(40, 24);
                Console.Write("===========");
                Console.SetCursorPosition(40, 25);
                Console.Write("4. 상점");
                Console.SetCursorPosition(40, 26);
                Console.Write("===========");

                Console.SetCursorPosition(40, 29);
                Console.Write("===========");
                Console.SetCursorPosition(40, 30);
                Console.Write("5.메인으로");
                Console.SetCursorPosition(40, 31);
                Console.Write("===========");


                if(Player.isPlayerReady==true)      //만약 플레이어 생성했으면 플레이어 이름 출력
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(65, 39);
                    
                    Console.Write($"플레이어명:{user.playerName}");
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(65, 40);
                Console.Write($"현재 보유중인 골드 : {Player.gold} Gold");
                Console.ResetColor();

                ConsoleKeyInfo key = Console.ReadKey(true);

                //숫자키 1 과 넘버패드 사용
                if(key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    GameManager.Loading("전투페이지 입장중..", 1800);
                    battlePage.SelectMonster();
                }
                else if(key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {

                }
                else if(key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {

                }
                else if(key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                {

                }
                else if(key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad5)
                {
                    GameManager.Loading("메인 화면으로 이동중..", 1000);
                    PrintMainPage();
                }

            }
        }
    }
}
