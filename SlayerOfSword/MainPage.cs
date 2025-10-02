using SlayerOfSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlayerOfSword
{
    class MainPage
    {
        public enum Menu
        {
            Start, Battle, Equipment, Inventory, Shop, Back, End
        }

        Menu menu { get; set; }
        TextVector vector = new TextVector();    //SlayerOfSword 네임스페이스 안에있는 구조체임. GameManager.cs파일에서 정의하였음

        public string SelectMainPage() //시작창 표시 및 게임시작 , 종료 선택
        {
            Console.Clear();
            Console.SetCursorPosition(0, 5);
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.Write(" _____ _                         _____  __   _____                       _ \r\n/  ___| |                       |  _  |/ _| /  ___|                     | |\r\n\\ `--.| | __ _ _   _  ___ _ __  | | | | |_  \\ `--.__      _____  _ __ __| |\r\n `--. \\ |/ _` | | | |/ _ \\ '__| | | | |  _|  `--. \\ \\ /\\ / / _ \\| '__/ _` |\r\n/\\__/ / | (_| | |_| |  __/ |    \\ \\_/ / |   /\\__/ /\\ V  V / (_) | | | (_| |\r\n\\____/|_|\\__,_|\\__, |\\___|_|     \\___/|_|   \\____/  \\_/\\_/ \\___/|_|  \\__,_|\r\n                __/ |                                                      \r\n               |___/  ");

            Console.SetCursorPosition(30, 18);
            Console.Write("┌━━━━━━━━━━━┒");

            Console.SetCursorPosition(30, 19);
            Console.Write(" 1. Start");

            Console.SetCursorPosition(30, 20);
            Console.Write("└━━━━━━━━━━━┚");

            Console.SetCursorPosition(30, 23);
            Console.Write("┌━━━━━━━━━━━┒");
            Console.SetCursorPosition(30, 24);
            Console.Write(" 2. Exit");
            Console.SetCursorPosition(30, 25);
            Console.Write("└━━━━━━━━━━━┚");


            bool isOff = false;
            bool isStart = false;
            string selectResult = null;


            while (isOff == false && isStart == false)    //종료버튼 누르면 종료 게임시작누르면 할일 선택
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)  //넘버패드 누르기 허용
                {
                    isStart = true;
                    Console.ResetColor();
                    
                    if (Player.isPlayerReady == false) //플레이어를 생성하지않았다면 CreatePlayer 반환.
                    {
                        selectResult = "CreatePlayer";
                    }
                    else
                    {
                        selectResult = "GameStart";
                    }
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    Console.ResetColor();
                    isOff = true;
                    selectResult = "Exit";
                }
            }
            return selectResult;

        }
        public void PrintMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;

            vector.x = 5;  //텍스트를 입력하기위한 커서좌표 지정
            vector.y = 8;

            for(int i= (int)Menu.Start+1; i< (int)Menu.End; i++)    //위 선언한 열거형 메뉴 출력
            {
             
                Console.SetCursorPosition(vector.x, vector.y);
                Console.Write("┌━━━━━━━━━━━┒");
                Console.SetCursorPosition(vector.x, vector.y + 1);

                
                    Console.Write($" {i}.{(Menu)i}");
                
                    //Console.Write($" {i}. {(Menu)i}");
                
                Console.SetCursorPosition(vector.x, vector.y + 2);
                Console.Write("└━━━━━━━━━━━┚");

                vector.y += 5;  //텍스트y좌표 +5
            }
        }
        public Menu SelectMenu()
        {
            bool isSelectMenu = false; //메뉴선택여부 확인
            //Menu menu = new Menu();

            while (isSelectMenu == false)      //전투페이지로 넘어가거나 메인으로 돌아가기 선택시 루프종료
            {
                //MainPage에서 BattlePage(전투페이지)로 이동
                ConsoleKeyInfo key = Console.ReadKey(true);

                //숫자키 1 과 넘버패드 사용
                string inputkey = key.KeyChar.ToString(); //읽어온 키값을 char형으로 변환후 다시 string으로 변환함(비교용이)

                for (int i = (int)Menu.Start + 1; i < (int)Menu.End; i++)   //열거형을 돌아가면서 입력한 키와 일치하는 열거형이 있는지 확인함
                {
                    if (inputkey == i.ToString())   //열거형이 일치한다면
                    {
                        menu = (Menu)i;
                        isSelectMenu=true;  //유효한 메뉴가 선택되어 While문 탈출. true할당
                        break;
                    }
                }
     
            }
            return menu;

        }
       
    }
}
