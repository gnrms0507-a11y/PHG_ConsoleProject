using SlayerOfSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordMaker
{
    enum MonsterList
    {
        고블린의숲=1,구울의소굴,거인의설원,골렘사원,드래곤둥지,드래곤무덤,메인으로, End
    }
 
    public struct Vector      //던전 몬스터 위치 선정
    {
        public int x;
        public int y;

    }
    class BattlePage
    {
        public static int clearDragon;  //드래곤슬레이어와 전투 잠금해제 요건 드래곤 5회잡으면 잠금해제

        public  MonsterList monsterList { get; set; }
        Vector vector= new Vector();
        

        public void SelectMonster()
        {
            bool isInputKey = false;   //메인으로 돌아가기 버튼 선택여부
            bool isMain = false;
            
            Console.ResetColor();
            Console.Clear();
            

            while (isInputKey == false)
            {
                vector.x = 10;
                vector.y = 5;

                for (int i = 1; i < (int)MonsterList.End; i++)      //몬스터 리스트 출력
                {

                    if (clearDragon >= 5 && (MonsterList)i == MonsterList.드래곤무덤)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.SetCursorPosition(vector.x, vector.y);
                        Console.Write("┌━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒");
                        Console.SetCursorPosition(vector.x, vector.y + 1);
                        Console.Write($" {i}.{(MonsterList)i}");
                        Console.SetCursorPosition(vector.x, vector.y + 2);
                        Console.Write("└━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┚");
                    }

                    else if (clearDragon < 5 && (MonsterList)i == MonsterList.드래곤무덤)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(vector.x, vector.y);
                        Console.Write("┌━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒");
                        Console.SetCursorPosition(vector.x, vector.y + 1);
                        Console.Write($" {i}.???????");
                        Console.SetCursorPosition(vector.x, vector.y + 2);
                        Console.Write("└━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┚");
                    }
                    else
                    {
                        if ((MonsterList)i == MonsterList.고블린의숲)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if ((MonsterList)i == MonsterList.구울의소굴)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        }
                        else if ((MonsterList)i == MonsterList.거인의설원)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                        else if ((MonsterList)i == MonsterList.골렘사원)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if ((MonsterList)i == MonsterList.드래곤둥지)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if ((MonsterList)i == MonsterList.메인으로)
                        {
                            Console.ResetColor();
                        }
  
                        Console.SetCursorPosition(vector.x, vector.y);
                        Console.Write("┌━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒");
                        Console.SetCursorPosition(vector.x, vector.y + 1);
                        Console.Write($" {i}.{(MonsterList)i}");
                        Console.SetCursorPosition(vector.x, vector.y + 2);
                       
                        Console.Write("└━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┚");
                    }

                    vector.y += 5;
                }

                //Console.ForegroundColor = ConsoleColor.DarkYellow;
                //Console.SetCursorPosition(65, 40);  //고정좌표임. 플레이어 정보 출력할것
                //Console.Write($"현재 보유중인 골드 : {Player.gold} Gold");
                //Console.ForegroundColor = ConsoleColor.Gray;
                //Console.SetCursorPosition(65, 41);  //고정좌표임. 플레이어 공격력 방어력 출력
                //Console.Write($"공격력: {Player.power} 방어력: {Player.armor}");
                //Console.ForegroundColor = ConsoleColor.DarkRed;
                //Console.SetCursorPosition(65, 42);  //고정좌표임. 플레이어 Hp 출력할것
                //Console.Write($"현재 HP: {Player.currentHp}/{Player.maxHp}");
                //Console.ForegroundColor = ConsoleColor.DarkBlue;
                //Console.SetCursorPosition(65, 43);  //고정좌표임. 플레이어 Mp 출력할것
                //Console.Write($"현재 MP: {Player.currentMp}/{Player.maxMp}");
                //Console.ResetColor();

                BattleMonster(Console.ReadKey());   //몬스터 배틀 호출
            }
        }

        public void BattleMonster(ConsoleKeyInfo keyInfo)
        {
           
            
        }



    }
}
