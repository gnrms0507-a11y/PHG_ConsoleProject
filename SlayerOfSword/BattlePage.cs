using EnumManager;
using SlayerOfSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SlayerOfSword.MainPage;
using static SlayerOfSword.Monster;
using static SlayerOfSword.Player;
using static System.Net.Mime.MediaTypeNames;

namespace SlayerOfSword
{
    class BattlePage
    {
        
        int[] MonstercolorIndex = new int[(int)MonsterList.Back+1] {0,10,13,1,11,12,4,7};//던전(몬스터)와의 컬러매칭을 위한 ConsoleColor Indexing 시작 Start, 0으로 구별

        int[] DifficultycolorIndex = new int[(int)Difficulty.Back+1] {0,10,2,7,12,4,5,7};//난이도와의 컬러매칭을 위한 ConsoleColor Indexing 시작 Start, 0으로 구별


        public static int clearDragon = 0;  //드래곤슬레이어와 전투 드래곤 5회잡으면 잠금해제

        TextVector vector= new TextVector();    //SlayerOfSword 네임스페이스 안에있는 구조체임. GameManager.cs파일에서 정의하였음

        public void PrintMonsterList()
        {

            Console.Clear();
                vector.x = 5;
                vector.y = 3;

                for (int i = 0; i <= (int)MonsterList.Back; i++)      //몬스터 리스트 출력
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

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void SelectMonster(MonsterList monstertName,out bool isLock)
        {
            vector.x = 25;
            vector.y = 45;
            isLock = false;
            Console.SetCursorPosition(vector.x, vector.y);
           
            if (monstertName != MonsterList.Back)     //battlePage에서 Back을 선택하지 않은경우실행
            {
                for (int i = 1; i <= (int)MonsterList.Back; i++)
                {
                    if (monstertName == ((MonsterList)i))    //몬스터의 이름이 인자값과 같은 열거형을 찾아서 몬스터 이름색 지정
                    {
                        Console.ForegroundColor = (ConsoleColor)MonstercolorIndex[i];

                        if(((MonsterList)i)==MonsterList.DragonSlayer && clearDragon<5)     //드래곤슬레이어가 잠겨있으면아무것도안함
                        {
                            Console.Write($"현재 잠겨있습니다. 드래곤 처치: {clearDragon}/5");
                            Thread.Sleep(1000);
                            isLock = true;
                        }
                 
                        break;
                    }
                }
                
            }
           
          
        }

        public void BattleWindowReset(int y)
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

        public void Battle(Player _player, Monster _monster, out bool? isRun)  //전투 메서드
        {
            int trun = 0;   //턴이 홀수면 플레이어턴 , 짝수면 몬스터의턴
            isRun = false; //도망여부

            string monsterAttack = null;
            int playerDemage = 0;
            int monsterDemage = 0;
            int monsterSkillDemage;


            //무기등급이 레전드면 몬스터방어무시 , 몬스터방어력 =0으로만듦
            if (_player.playerWeapon[0].itemGrade == ItemGrade.Legend)
            {
                vector.x = 0;   //텍스트 작성위치 지정
                vector.y = Console.WindowHeight - 10;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(0, vector.y);
                string LegendWeaponText = ($"{_player.playerWeapon[0].itemName}의 힘으로 몬스터의 방어를 무시합니다..");

                for (int i = 0; i < LegendWeaponText.Length; i++)
                {
                    Console.Write(LegendWeaponText[i]);
                    Thread.Sleep(50);
                }

                Thread.Sleep(500);

                ((LegendWeapon)_player.playerWeapon[0]).guardBreak(_monster);
            }



            MonsterSKillList usemonsterSkill;   //몬스터의 턴에서 사용할 몬스터의 스킬저장

            while (Player.currentHp >= 0 && _monster.currentMonsterHp>=0)        //플레이어의 hp 혹은 몬스터 hp가 0이하로 가면 끝
            {

                _monster.PrintMonsterHp(_monster); //몬스터의 체력출력

                vector.x = 0;   //텍스트 작성위치 지정
                vector.y = Console.WindowHeight-10;
                Console.ForegroundColor = ConsoleColor.Gray;
                

                if (trun % 2 == 0)      //플레이어 턴 먼저시작(0시작, 짝수일경우 플레이어의턴)
                {
                    Console.SetCursorPosition(0, vector.y-1);   
                    for(int i =0; i<Console.WindowWidth; i++) //줄치기
                    {
                        Console.Write("━");
                    }

                    BattleWindowReset(vector.y);
                    Console.Write($"{_player.playerName}'s Turn");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" [Hp:{Player.currentHp}/{_player.maxHp}]");

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($" [Mp:{Player.currentMp}/{_player.maxMp}]");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("1. Attack");
                    Console.WriteLine();
                    Console.WriteLine("2. Skill");
                    Console.WriteLine();
                    Console.WriteLine("3. Run..");


                    ConsoleKeyInfo key = Console.ReadKey(true);

                    //NumPad 도 입력허용함 평타 쳤을때.
                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)      
                    {
                        BattleWindowReset(vector.y);    //배틀창 지우기
         
                        for (int i = 0; i < ($"{_player.playerName} 의 Attack!").Length; i++)
                        {
                            Console.Write($"{_player.playerName} 의 Attack!"[i]);
                            Thread.Sleep(50);
                        }
                        Thread.Sleep(300);

                        playerDemage = _player.power - _monster.MonsterDefense;
                        if (playerDemage <= 0) { playerDemage = 0; }    //플레이어 데미지가 몬스터방어력에의해 -될경우 0으로지정


                        Console.SetCursorPosition(0, vector.y);

                        for (int i =0; i<($"{_monster.MonsterName}에게 {playerDemage}만큼의 데미지를 입혔다!").Length; i++)
                        {
                            Console.Write($"{_monster.MonsterName}에게 {playerDemage}만큼의 데미지를 입혔다!"[i]);
                            Thread.Sleep(50);
                        }

                        Thread.Sleep(300);
                        Console.SetCursorPosition(0, vector.y);
                        Console.WriteLine("                                                                                         ");
                        
                        
                        _monster.currentMonsterHp -= playerDemage;     //몬스터의 방어력을제외
                        if (_monster.currentMonsterHp <= 0)     //몬스터의 Hp가 0이하면 0 으로 지정
                        {
                                _monster.currentMonsterHp = 0;
                                break;  //배틀종료 - 반복문탈출
                                 
                        }
                    

                        trun++; //턴 증감
                    }


                    //스킬사용
                    else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    {
                        string inputSkillKey;   //스킬키 입력받을 변수
                        bool? isUseSkill = null; //스킬 사용여부 받을 변수 

                        BattleWindowReset(vector.y);  //아래 텍스트 지울것임

                        Console.WriteLine($"{_player.playerName}'s Skill Select");


                        //스킬 리스트 출력
                        foreach (var a in _player.playerskills) 
                        {
                            Console.Write($"{a.Key} . {a.Value}");

                            if(_player.skillDemage.TryGetValue(a.Value,out int Demage))  //스킬뎀지 출력
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write($"  {Demage} Demage");
                                Console.ForegroundColor = ConsoleColor.Gray; 
                            }

                            if (_player.skillMp.TryGetValue(a.Value, out int mp)) //스킬소모 mp출력
                            {

                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write($"  {mp}Mp");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            Console.WriteLine(); //한줄띄우기
                        }


                        ConsoleKeyInfo skillKey = Console.ReadKey(true);

                        inputSkillKey = skillKey.KeyChar.ToString();


                        foreach (var skill in _player.playerskills)
                        {
                            if (inputSkillKey == skill.Key && skill.Value.ToString() !="Back")
                            {

                                BattleWindowReset(vector.y);  //아래 텍스트 지울것임

                                playerDemage = _player.useSkill(inputSkillKey,out isUseSkill) - _monster.MonsterDefense;     //데미지에 플레이어의 스킬데미지 입력
                                if (playerDemage <= 0) { playerDemage = 0; }    //데미지가 0이하면 0 으로 고정

                                BattleWindowReset(vector.y);  //아래 텍스트 지울것임

                                if (isUseSkill==true)
                                {
                                    for (int i = 0; i < ($"{_monster.MonsterName}에게 {playerDemage}만큼의 데미지를 입혔다!").Length; i++)
                                    {
                                        Console.Write($"{_monster.MonsterName}에게 {playerDemage}만큼의 데미지를 입혔다!"[i]);
                                        Thread.Sleep(50);
                                    }

                                    Thread.Sleep(300);
                                }

                            }
                           
                        }

                        BattleWindowReset(vector.y);  //아래 텍스트 지울것임

                        if (isUseSkill==true)   //스킬을 사용했을때만 턴증감 사용못함(mp부족)이면 턴증감없음
                        {
                            _monster.currentMonsterHp -= playerDemage;     //몬스터의 방어력을제외

                            if (_monster.currentMonsterHp <= 0)     //몬스터의 Hp가 0이하면 0 으로 지정
                            {
                                 _monster.currentMonsterHp = 0;
                                break;
                            }
                           

                            trun++; //턴 증감
                        }
                      
                    }


                    //도망침
                    else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)  
                    {
                        BattleWindowReset(vector.y);

                        Console.SetCursorPosition(0, vector.y);
                        for (int i = 0; i < ($"{_monster.MonsterName}에게서 도망쳤다..").Length; i++)
                        {
                            Console.Write($"{_monster.MonsterName}에게서 도망쳤다.."[i]);
                            Thread.Sleep(50);
                        }
                        Thread.Sleep(300);
                        isRun = true;
                        break;
                    }

                     
                }


                //몬스터의 턴
                else
                {
                    Console.SetCursorPosition(0, vector.y - 1);  
                    for (int i = 0; i < Console.WindowWidth; i++) //줄치기 몬스터의 턴
                    {
                        Console.Write("━");   
                    }

                    BattleWindowReset(vector.y);
                

                    Console.Write($"{_monster.MonsterName}'s Turn");
                    Thread.Sleep(1000);

                    usemonsterSkill = _monster.MonsterTurn(out monsterSkillDemage); //몬스터 스킬사용 분기에 던져줄것임

                    monsterAttack = usemonsterSkill.ToString();    //몬스터 스킬을 담음 (일반공격포함)

                    BattleWindowReset(vector.y);

                    //몬스터의 일반공격
                    if (monsterAttack.Contains("NormalAttack"))
                    {
                        for (int i = 0; i < ($"{_monster.MonsterName} 의 Attack!").Length; i++)
                        {
                            Console.Write($"{_monster.MonsterName} 의 Attack!"[i]);
                            Thread.Sleep(50);
                        }
                        Thread.Sleep(300);

                        monsterDemage = _monster.MonsterPower - _player.armor;
                        if (monsterDemage <= 0) { monsterDemage = 0; }    //몹 데미지가 플레이어방어력에의해 -될경우 0으로지정


                        Console.SetCursorPosition(0, vector.y);

                        for (int i = 0; i < ($"{_player.playerName}이(가) {monsterDemage}만큼의 데미지를 입었다!").Length; i++)
                        {
                            Console.Write($"{_player.playerName}이(가) {monsterDemage}만큼의 데미지를 입었다!"[i]);
                            Thread.Sleep(50);
                        }

                        Thread.Sleep(300);
                        Console.SetCursorPosition(0, vector.y);
                        Console.WriteLine("                                                                                         ");

                        Player.currentHp -= monsterDemage;     //몬스터의 방어력을제외한 데미지 입힘

                        if (Player.currentHp <= 0)     //몬스터의 Hp가 0이하면 0 으로 지정
                        {
                            Player.currentHp = 0;
                        }
                        
                        trun++;
                    }


                    //몬스터의 스킬사용
                    else
                    {
                        for (int i = 0; i < ($"{_monster.MonsterName} 의 {monsterAttack}!").Length; i++)
                        {
                            Console.Write($"{_monster.MonsterName} 의 {monsterAttack}!"[i]);
                            Thread.Sleep(50);
                        }
                        Thread.Sleep(300);
    
                        monsterDemage = monsterSkillDemage - _player.armor;
                        if (monsterDemage <= 0) { monsterDemage = 0; }    //몹 데미지가 플레이어방어력에의해 -될경우 0으로지정


                        Console.SetCursorPosition(0, vector.y);

                        for (int i = 0; i < ($"{_player.playerName}이(가) {monsterDemage}만큼의 데미지를 입었다!").Length; i++)
                        {
                            Console.Write($"{_player.playerName}이(가) {monsterDemage}만큼의 데미지를 입었다!"[i]);
                            Thread.Sleep(50);
                        }

                        Thread.Sleep(300);
                        Console.SetCursorPosition(0, vector.y);
                        Console.WriteLine("                                                                                         ");

                        Player.currentHp -= monsterDemage;     //몬스터의 방어력을제외한 데미지 입힘

                        if (Player.currentHp <= 0)     //몬스터의 Hp가 0이하면 0 으로 지정
                        {
                            Player.currentHp = 0;
                        }
                        

                        trun++;
                    }

                }
            }

            //몬스터가 죽었을경우 그리고 드래곤슬레이어일 경우엔 보상출력 X
            if (_monster.currentMonsterHp <= 0 && _monster.MonsterName != MonsterList.DragonSlayer.ToString())    
            {
                if (_monster.MonsterName == MonsterList.Dragon.ToString())
                {
                    BattlePage.clearDragon += 1;    //드래곤클리어시 드래곤클리어횟수1증감
                }
                _monster.PrintMonsterText(_monster, _monster.monsterRewardGold); //보상획득
              
            }
            //드래곤슬레이어 2페 잡으면 보상
            else if(_monster.currentMonsterHp <= 0 && _monster.MonsterName == MonsterList.DragonSlayer.ToString() && _monster.isPage2 == true)
            {
                _monster.PrintMonsterText(_monster, _monster.monsterRewardGold); //보상획득
            }

            //플레이어가 죽었을경우 패배문구출력
            else if (Player.currentHp <= 0)
            {
                BattleWindowReset(vector.y);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You Lose...");
                Thread.Sleep(1000);
                for (int i = 1; i <= (int)MonsterList.Back; i++)  //몬스터 텍스트색 찾기
                {
                    if (((MonsterList)i).ToString() == _monster.MonsterName)
                    {
                        _monster.PrintMonsterText(_monster.playerLoseText, (ConsoleColor)MonstercolorIndex[i]);
                        break;
                    }

                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Player.currentHp = 1;  //피 1증가.
            }
        }
    }
}
