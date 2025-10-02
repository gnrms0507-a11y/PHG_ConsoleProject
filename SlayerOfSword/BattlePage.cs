using SlayerOfSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlayerOfSword
{
    class BattlePage
    {
        enum MonsterList    //던전(몬스터) 리스트
        {
            Start, Goblin, Ghoul, SnowMan, Golem, Dragon, DragonSlayer, Back, End   
        }
        enum Difficulty     //난이도 정렬
        {
            Start, VeryEasy, Easy, Noraml, Hard, Extreme, Nightmare, Back , End
        }

        int[] MonstercolorIndex = new int[(int)MonsterList.End] {0,10,5,1,11,12,4,7};//던전(몬스터)와의 컬러매칭을 위한 ConsoleColor Indexing 시작 Start, 0으로 구별

        int[] DifficultycolorIndex = new int[(int)Difficulty.End] {0,10,2,7,12,4,5,7};//난이도와의 컬러매칭을 위한 ConsoleColor Indexing 시작 Start, 0으로 구별


        public static int clearDragon;  //드래곤슬레이어와 전투 잠금해제 요건 드래곤 5회잡으면 잠금해제

        TextVector vector= new TextVector();    //SlayerOfSword 네임스페이스 안에있는 구조체임. GameManager.cs파일에서 정의하였음

        public void PrintMonsterList()
        {

            Console.Clear();
                vector.x = 5;
                vector.y = 3;

                for (int i = (int)MonsterList.Start+1; i < (int)MonsterList.End; i++)      //몬스터 리스트 출력
                {
                       Console.ForegroundColor = (ConsoleColor)MonstercolorIndex[i];
                        
                if (clearDragon < 5 && (MonsterList)i == MonsterList.DragonSlayer)
                    {
                        Console.ForegroundColor = (ConsoleColor)6;
                        Console.SetCursorPosition(vector.x, vector.y);
                        Console.Write("┌━━━━━━━━━━━━━━━━━━━━━━━━┒");
                        Console.SetCursorPosition(vector.x, vector.y + 1);
                        Console.Write($" {i}.??????? -Lock-");
                        Console.SetCursorPosition(vector.x, vector.y + 2);
                        Console.Write("└━━━━━━━━━━━━━━━━━━━━━━━━┚");
                    }
                    
                    else
                    {
                        Console.SetCursorPosition(vector.x, vector.y);
                        Console.Write("┌━━━━━━━━━━━━━━━━━━━━━━━━┒");
                        Console.SetCursorPosition(vector.x, vector.y + 1);
                        Console.Write($" {i}.{(MonsterList)i} ");
                        Console.SetCursorPosition(vector.x, vector.y + 2);
                        Console.Write("└━━━━━━━━━━━━━━━━━━━━━━━━┚");
                        if ((Difficulty)i != Difficulty.Back) { Console.ForegroundColor = (ConsoleColor)DifficultycolorIndex[i]; Console.Write($"      {(Difficulty)i}"); }
                }

                    vector.y += 5;
                
                }

        }


        public void SelectMonster()
        {

        }

    }
}
