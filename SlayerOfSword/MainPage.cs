using SlayerOfSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnumManager;

namespace SlayerOfSword
{
    class MainPage
    {
        TextVector vector = new TextVector();    //SlayerOfSword 네임스페이스 안에있는 구조체임. GameManager.cs파일에서 정의하였음

        public void SelectMainPage() //시작창 표시 및 게임시작 , 종료 선택
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

            Console.ResetColor();


        }
        public void PrintMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;

            vector.x = 5;  //텍스트를 입력하기위한 커서좌표 지정
            vector.y = 8;

            for(int i= 1; i<= (int)Menu.Back; i++)    //위 선언한 열거형 메뉴 출력
            {
             
                Console.SetCursorPosition(vector.x, vector.y);
                Console.Write("┌━━━━━━━━━━━┒");
                Console.SetCursorPosition(vector.x, vector.y + 1);

                Console.Write($" {i}.{(Menu)i}");
                
                Console.SetCursorPosition(vector.x, vector.y + 2);
                Console.Write("└━━━━━━━━━━━┚");

                vector.y += 5;  //텍스트y좌표 +5
            }
            Console.ResetColor();

            
        }

       
    }
}
