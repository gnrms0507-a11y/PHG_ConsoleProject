using EnumManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlayerOfSword
{
    //장비 장착 / 해제 페이지
    public partial class Inventory
    {
        //텍스트 좌표지정
        TextVector textVector;

        //아이템 리스트 인스턴스 생성 10개 공간생성 - 정적할당 
        public static List<Item> playerInventory = new List<Item>(10);

       
        //텍스트 지우는함수
        public void InventoryWindowReset(int y)
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

        //아이템 등급에 따른 색 지정하는 함수
        public static void ItemColor(Item _item)
        {
            switch(_item.itemGrade)
            {
                case ItemGrade.Normal:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case
                        ItemGrade.Rare:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;

                case
                        ItemGrade.Epic:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;

                case
                        ItemGrade.Unique:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                case
                        ItemGrade.Legend:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
            }
        }


        //장착중인 장비 출력
        public string PrintInventory(Player _player)
        {

            Console.Clear();

            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Console.WindowWidth; i++) //줄치기
            {
                Console.Write("━");
            }

            //플레이어 hp출력
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"HP: {Player.currentHp}/{_player.maxHp}\t");

            //플레이어 Mp출력
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"MP: {Player.currentMp}/{_player.maxMp}");

            //플레이어 공/방 출력
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Attack : {_player.power}\t");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Defense : {_player.armor}");

            //아이템 등급 출력
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"ItemGrade:\tNormal ");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write($"Rare ");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"Epic ");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Unique ");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Legend ");

            Console.WriteLine();
            //장착중인 무기 출력
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("---장착중인 장비 목록입니다---");
            Console.WriteLine();

            if (_player.playerWeapon[0] == null)
            {
                Console.WriteLine($"장착중인 무기가 없습니다.");
            }
            else
            {
                Console.Write("장착중인 무기: ");
                //ItemGrade 별 색상지정
                ItemColor(_player.playerWeapon[0]);
              
                Console.WriteLine($"[{_player.playerWeapon[0].itemName}]");
                Console.ForegroundColor= ConsoleColor.Gray;
                Console.WriteLine($"┗ 추가 공격력: {_player.playerWeapon[0].plusPower}");
            }

            //장착중인 방어구 출력
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            if (_player.playerArmor[0] == null)
            {
                Console.WriteLine($"장착중인 방어구가 없습니다.");
            }
            else
            {
                Console.Write("장착중인 방어구: ");
                //ItemGrade 별 색상지정

                ItemColor(_player.playerArmor[0]);
                Console.WriteLine($"[{_player.playerArmor[0].itemName}]");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"┗ 추가 방어력: {_player.playerArmor[0].plusDefense} 추가 Hp: {_player.playerArmor[0].plusHp} 추가 Mp: {_player.playerArmor[0].plusMp}");
            }

            Console.WriteLine($"");
            Console.WriteLine($"");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"---Notice---");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Unique 이상 무기 장착시 DragonStrike스킬 사용가능");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Legned 이상 무기 장착시 DragonStrike,SwordJudgement 사용가능");
            Console.WriteLine("또한 적 방어력 무시");
            Console.ForegroundColor = ConsoleColor.Gray;

            // 인벤토리창 출력
            textVector.x = 0;
            textVector.y = Console.WindowHeight - 20;

            Console.SetCursorPosition(0, textVector.y-1);

            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < Console.WindowWidth; i++) //줄치기
            {
                Console.Write("━");
            }

            InventoryWindowReset(textVector.y);

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("1. 장비 장착");
            Console.WriteLine("2. 장비 제거");
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
                    SelectMenu = "Equip";
                }
                else if (consolekey.Key == ConsoleKey.D2 || consolekey.Key == ConsoleKey.NumPad2)
                {
                    isKeyInput = true;
                    SelectMenu = "Remove";
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


        //장비 장착 또는 교체
        public void EquipOrChange(Player _player, out bool? isBack)
        { 
            //백스페이스 선택여부
            isBack = false;
            textVector.x = 0;
            textVector.y = Console.WindowHeight - 20;
            InventoryWindowReset(textVector.y);

            //키의 역할을 하는 변수
            int itemCount = 1;

            //보유중인 아이템 리스트 출력
            Console.WriteLine("[ItemList]");
            //아이템이 하나라도 있을때 출력함
            if (playerInventory.Count > 0)
            {
                foreach (var item in playerInventory)
                {
                    //아이템이 등급에 따른 색상지정
                    ItemColor(item);
                    
                    Console.WriteLine($"{itemCount}.{item.itemName} ");
                    if(item.itemCategory=="Weapon")
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"┗ 추가 공격력: {((Weapon)item).plusPower}");
                    }
                    else if(item.itemCategory == "Armor")
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"┗ 추가 방어력: {((Armor)item).plusDefense} 추가 Hp: {((Armor)item).plusHp} 추가 Mp: {((Armor)item).plusMp}");
                    }
                    itemCount++;
                }
                
                //글자색 초기화
                Console.ForegroundColor= ConsoleColor.Gray;
                Console.WriteLine();
                Console.WriteLine($"백스페이스(←)를 눌러 뒤로가기");

                bool isKeyInput = false;
                string SelectItem = null;

            
                while (isKeyInput == false)
                {
                    //키와 일치시킬 변수 지정
                    int itemSelectCount = 1;

                    ConsoleKeyInfo consolekey = Console.ReadKey(true);

                    SelectItem = consolekey.KeyChar.ToString();

                    //백스페이스를 눌렀다면 반복문 탈출
                    if (consolekey.Key == ConsoleKey.Backspace)
                    {
                        isKeyInput = true;
                        isBack = true;
                    }

                    //백스페이스가 아니라면
                    else 
                    {
                        foreach(var a in playerInventory)
                        {
                            //키와 일치한다면
                            if ((SelectItem == itemSelectCount.ToString()))
                            {
                                //텍스트 지우기
                                InventoryWindowReset(textVector.y);

                                //무기 or 방어구 장착
                                //장착중인 무기 없고 선택한 아이템이 무기일경우
                                if(a.itemCategory=="Weapon")
                                {
                                    Console.Write($"무기를 ");
                                    ItemColor(a);
                                    Console.Write($"{a.itemName} ");
                                    Console.ForegroundColor= ConsoleColor.Gray;
                                    Console.Write("로 교체합니다.");

                                    Thread.Sleep(700);

                                    //아이템 교체 후 교체된아이템 인벤토리에서 제거
                                    playerInventory.RemoveAt(itemSelectCount-1);
                                    playerInventory.Add(_player.playerWeapon[0]);

                                    //플레이어 아이템 갈아끼우기 후 반복탈출
                                    _player.playerWeapon[0] = (Weapon)a;

                                    //플레이어 능력치 업데이트
                                    _player.UpdatePlayer(_player.playerWeapon[0], _player.playerArmor[0]);
                                    _player.PlayerBasicSkill(_player);

                                    //유니크장비 이상일시 스킬추가함

                                    if(_player.playerWeapon[0].itemGrade == ItemGrade.Unique)
                                    {
                                        _player.PlayerBasicSkill(_player);
                                        ((UniqueWeapon)_player.playerWeapon[0]).NewSkill(ItemGrade.Unique, _player);
                                      
                                    }
                                    else if (_player.playerWeapon[0].itemGrade == ItemGrade.Legend)
                                    {
                                        _player.PlayerBasicSkill(_player);
                                        ((LegendWeapon)_player.playerWeapon[0]).NewSkill(ItemGrade.Legend,_player);
                                        
                                    }
                                    else
                                    {
                                        _player.PlayerBasicSkill(_player);
                                    }

                                    //while문탈출
                                    isKeyInput = true;
                                    //다시 인벤토리 접속시 로딩없이 접속하게함
                                    isBack = true;
                                    break;

                                }
                                else if(a.itemCategory == "Armor")
                                {
                                    Console.Write($"방어구를 ");
                                    ItemColor(a);
                                    Console.Write($"{a.itemName} ");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.Write("로 교체합니다.");

                                    Thread.Sleep(700);
                                    //아이템 교체 후 교체된아이템 인벤토리에서 제거
                                    playerInventory.RemoveAt(itemSelectCount - 1);
                                    playerInventory.Add(_player.playerArmor[0]);

                                    //플레이어 아이템 갈아끼우기 후 반복탈출
                                    _player.playerArmor[0] = (Armor)a;

                                    //플레이어 능력치 업데이트
                                    _player.UpdatePlayer(_player.playerWeapon[0], _player.playerArmor[0]);

                                    //무기가 유니크 이상이면 스킬추가

                                    if (_player.playerWeapon[0].itemGrade == ItemGrade.Unique)
                                    {
                                        _player.PlayerBasicSkill(_player);
                                        ((UniqueWeapon)_player.playerWeapon[0]).NewSkill(ItemGrade.Unique, _player);

                                    }
                                    else if (_player.playerWeapon[0].itemGrade == ItemGrade.Legend)
                                    {
                                        _player.PlayerBasicSkill(_player);
                                        ((LegendWeapon)_player.playerWeapon[0]).NewSkill(ItemGrade.Legend, _player);

                                    }
                                    else
                                    {
                                        _player.PlayerBasicSkill(_player);
                                    }
                                   

                                    //while문탈출
                                    isKeyInput = true;
                                    //다시 인벤토리 접속시 로딩없이 접속하게함
                                    isBack = true;
                                    break;
                                }
                            }
                            itemSelectCount++;
                        }
                    }


                }
                Console.ForegroundColor = ConsoleColor.Gray;


            }

            //보유한 아이템이 없을때
            else
            {
                Console.WriteLine($"보유중인 아이템이 없습니다.");
                Console.WriteLine();
                Console.WriteLine($"백스페이스(←)를 눌러 뒤로가기");

                bool isKeyInput = false;

                while (isKeyInput == false)
                {
                    ConsoleKeyInfo consolekey = Console.ReadKey(true);

                    //백스페이스를 눌렀다면 반복문 탈출
                    if (consolekey.Key == ConsoleKey.Backspace)
                    {
                        isKeyInput = true;
                        isBack = true;
                    }
                  
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }


        }
    }
}
