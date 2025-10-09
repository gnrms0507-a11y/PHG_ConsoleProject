using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SlayerOfSword
{
    //캐릭터 피 , Mp회복하는곳
    class Recovery
    {
        //텍스트위치지정
        TextVector textVector;

        public void HealingStationWindowReset(int y)
        {
            for (int i = y; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);         //아래 텍스트 지울것임

                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write(" ");
                }
            }

            Console.SetCursorPosition(0, y);         //원래 입력받은 커서자리로 돌려놓음
        }

        public string PrintHealingStationPage(Player _player)
        {
            Console.Clear();

            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Console.WindowWidth; i++) //줄치기
            {
                Console.Write("━");
            }

            //플레이어 hp출력
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($" HP: {Player.currentHp}/{_player.maxHp} ");

            //플레이어 Mp출력
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write($"MP: {Player.currentMp}/{_player.maxMp} ");

            //플레이어 보유골드 출력
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Gold : {Player.gold}");

            //가운데 힐링스테이션 텍스트 출력
            textVector.x = 80;
            textVector.y = 50;

            Console.ForegroundColor = ConsoleColor.Green;

            string text = "어디가 불편하신가 ? 내가 함 봐드리지";

            Console.SetCursorPosition(textVector.x / 2 - text.Length, textVector.y / 2 - 3);
            
            Console.Write(text);


            // 행동 선택창 출력
            textVector.x = 0;
            textVector.y = Console.WindowHeight - 10; 
            Console.SetCursorPosition(0, textVector.y - 1);
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < Console.WindowWidth; i++) //줄치기
            {
                Console.Write("━");
            }

            HealingStationWindowReset(textVector.y);
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("1. Hp 회복");
            Console.WriteLine("2. Mp 회복");
            Console.WriteLine("3. Back");

            //선택받기
            bool isKeyInput = false;
            string SelectMenu = null;
            while (isKeyInput == false)
            {
                ConsoleKeyInfo consolekey = Console.ReadKey(true);

                if (consolekey.Key == ConsoleKey.D1 || consolekey.Key == ConsoleKey.NumPad1)
                {
                    isKeyInput = true;
                    SelectMenu = "Hp";
                }
                else if (consolekey.Key == ConsoleKey.D2 || consolekey.Key == ConsoleKey.NumPad2)
                {
                    isKeyInput = true;
                    SelectMenu = "Mp";
                }
                else if (consolekey.Key == ConsoleKey.D3 || consolekey.Key == ConsoleKey.NumPad3)
                {
                    isKeyInput = true;
                    SelectMenu = "Back";
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            return SelectMenu;
        }

        //Hp 회복
        public void RecoveryHpMp(Player _player,string HporMp , out bool? isRecovery)
        {
            //아래 변수를 밖으로 빼서 회복시 골드소모여부 에서 취소누른건지 확인
            isRecovery = false;

            textVector.x = 0;
            textVector.y = Console.WindowHeight - 10;
            Console.SetCursorPosition(0, textVector.y - 1);
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < Console.WindowWidth; i++) //줄치기
            {
                Console.Write("━");
            }

            Console.ForegroundColor = ConsoleColor.Gray;

            HealingStationWindowReset(textVector.y);
            //회복여부판단할 변수 최대피통에서 현재피통을뺐을때 0이면 true
            bool isHpFull = false;
            bool isMpFull = false;
            //Mp or Hp회복여부선택
            if (HporMp == "Hp")
            {
                if (_player.maxHp - Player.currentHp == 0)
                {
                    HealingStationWindowReset(textVector.y);
                    Console.WriteLine($"Hp를 회복할필요가 없습니다.");
                    isHpFull = true;
                    isRecovery = true;
                    Thread.Sleep(500);
                }

                //플레이어의 골드가 100아래일때
                else if(Player.gold<100)
                {
                    HealingStationWindowReset(textVector.y);
                    Console.WriteLine($"Gold가 부족합니다 . (100Gold소모)");
                    isHpFull = true;
                    isRecovery = true;
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine($"Hp를 {_player.maxHp - Player.currentHp} 만큼 회복합니다.");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(" 100 Gold가 소모됩니다 ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("1. 회복 2.취소");
                }
              
            }
            else if(HporMp == "Mp")
            {
                if(_player.maxMp - Player.currentMp==0)
                {
                    HealingStationWindowReset(textVector.y);
                    Console.WriteLine($"Mp를 회복할필요가 없습니다.");
                    Thread.Sleep(500);
                    isMpFull = true;
                    isRecovery = true;
                }

                else if (Player.gold < 70)
                {
                    HealingStationWindowReset(textVector.y);
                    Console.WriteLine($"Gold가 부족합니다 . (70Gold소모)");
                    isHpFull = true;
                    isRecovery = true;
                    Thread.Sleep(500);
                }

                else
                {
                    Console.WriteLine($"Mp를 {_player.maxMp - Player.currentMp} 만큼 회복합니다.");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(" 70 Gold가 소모됩니다 ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("1. 회복 2.취소");
                }
            }

           
            if ( (isHpFull == false && HporMp=="Hp") || (isMpFull == false && HporMp=="Mp")) 
            {
                //위 선택란 키입력받기
                bool isInputKey = false;

                while (isInputKey==false)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                
                    //회복을 선택했을경우
                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    {
                        isInputKey = true;
                        textVector.y = Console.WindowHeight - 10;

                        if(HporMp=="Hp")
                        {
                            Player.currentHp = _player.maxHp;
                            Player.gold -= 100;
                        }
                        else if(HporMp=="Mp")
                        {
                            Player.currentMp = _player.maxMp;
                            Player.gold -= 70;
                        }

                        //아래하단창 지우기
                        HealingStationWindowReset(textVector.y);

                        Console.SetCursorPosition(0, textVector.y);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        for (int i = 0; i <($"{HporMp}가 모두 회복되었다 !").Length; i++) //줄치기
                        {
                            Console.Write($"{HporMp}가 모두 회복되었다 !"[i]);
                            Thread.Sleep(100);
                            isRecovery = true;
                        }
                        Thread.Sleep(500);
                        Console.ForegroundColor = ConsoleColor.Gray;

                    }

                    if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    {
                        //반복문 빠져나옴과 회복안하고 취소눌렀음을 확인
                        isInputKey = true;
                        isRecovery = true;
                    }

                }

            }

        }
    }
}
