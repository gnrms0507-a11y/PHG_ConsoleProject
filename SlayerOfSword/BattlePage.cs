using SlayerOfSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SlayerOfSword.MainPage;
using EnumManager;

namespace SlayerOfSword
{
    class BattlePage
    {
        MonsterList monsterList { get; set; }
        
        int[] MonstercolorIndex = new int[(int)MonsterList.End] {0,10,5,1,11,12,4,7};//던전(몬스터)와의 컬러매칭을 위한 ConsoleColor Indexing 시작 Start, 0으로 구별

        int[] DifficultycolorIndex = new int[(int)Difficulty.End] {0,10,2,7,12,4,5,7};//난이도와의 컬러매칭을 위한 ConsoleColor Indexing 시작 Start, 0으로 구별


        public static int clearDragon;  //드래곤슬레이어와 전투 드래곤 5회잡으면 잠금해제

        TextVector vector= new TextVector();    //SlayerOfSword 네임스페이스 안에있는 구조체임. GameManager.cs파일에서 정의하였음

        public void PrintMonsterList()
        {

            Console.Clear();
                vector.x = 5;
                vector.y = 3;

                for (int i = (int)MonsterList.Start+1; i < (int)MonsterList.End; i++)      //몬스터 리스트 출력
                {
                       Console.ForegroundColor = (ConsoleColor)MonstercolorIndex[i];
                        
                if (clearDragon < 5 && (MonsterList)i == MonsterList.DragonSlayer)  //드래곤슬레아어는 처음에 잠겨있음
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
                        Console.SetCursorPosition(vector.x, vector.y);      //Back일경우엔 난이도표기x
                        Console.Write("┌━━━━━━━━━━━━━━━━━━━━━━━━┒");
                        Console.SetCursorPosition(vector.x, vector.y + 1);
                        Console.Write($" {i}.{(MonsterList)i} ");
                        Console.SetCursorPosition(vector.x, vector.y + 2);
                        Console.Write("└━━━━━━━━━━━━━━━━━━━━━━━━┚");
                        if ((Difficulty)i != Difficulty.Back) { Console.ForegroundColor = (ConsoleColor)DifficultycolorIndex[i]; Console.Write($"      {(Difficulty)i}"); }
                }

                    vector.y += 5;
                
                }

            Console.ResetColor();
        }

        public void SelectMonster(string monstertName)
        {
            vector.x = 25;
            vector.y = 45;

            Console.SetCursorPosition(vector.x, vector.y);

            string inputkey = null;

            if (monstertName != "Back")     //battlePage에서 Back을 선택하지 않은경우실행
            {
                for (int i = (int)MonsterList.Start + 1; i < (int)MonsterList.End; i++)
                {
                    if (monstertName == ((MonsterList)i).ToString())    //몬스터의 이름이 인자값과 같은 열거형을 찾아서 몬스터 이름색 지정
                    {
                        Console.ForegroundColor = (ConsoleColor)MonstercolorIndex[i];

                            Console.Write($"{monstertName}");
                            Console.ResetColor();
                            Console.Write($" 과 전투를 실행하시겠습니까 ? ");
                            Console.SetCursorPosition(vector.x, vector.y + 1);
                            Console.Write("1. 실행한다  2. 취소");
                

                        ConsoleKeyInfo key = Console.ReadKey(true);

                        inputkey = key.KeyChar.ToString(); //읽어온 키값을 char형으로 변환후 다시 string으로 변환함(비교용이)

                        if (inputkey == "2")
                        {
                            Console.ResetColor();
                            Console.SetCursorPosition(vector.x, vector.y);
                            Console.Write($"                                                            ");
                            Console.SetCursorPosition(vector.x, vector.y + 1);
                            Console.Write("                                                             ");
                        }
                        break;
                    }
                }
            }
            
          
        }
    }
}
