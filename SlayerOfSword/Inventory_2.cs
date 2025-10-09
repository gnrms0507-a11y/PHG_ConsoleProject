using EnumManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlayerOfSword
{
    //장비 장착 / 해제 페이지
    public partial class Inventory
    {
        //장비제거
        public void RemoveItem(Player _player, out bool? isBack)
        {
            //백스페이스 선택여부
            isBack = false;
            textVector.x = 0;
            textVector.y = Console.WindowHeight - 20;
            InventoryWindowReset(textVector.y);

            //보유중인 아이템 리스트 출력
            Console.WriteLine("[제거할 아이템 선택]");

            //키의 역할을 하는 변수
            int itemCount = 1;

            //아이템이 하나라도 있을때 출력함
            if (playerInventory.Count > 0)
            {
                foreach (var item in playerInventory)
                {
                    //아이템이 등급에 따른 색상지정
                    ItemColor(item);

                    Console.WriteLine($"{itemCount}.{item.itemName} ");
                    if (item.itemCategory == "Weapon")
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"┗ 추가 공격력: {((Weapon)item).plusPower}");
                    }
                    else if (item.itemCategory == "Armor")
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"┗ 추가 방어력: {((Armor)item).plusDefense}");
                    }
                    itemCount++;
                }

                //글자색 초기화
                Console.ForegroundColor = ConsoleColor.Gray;
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
                        foreach (var a in playerInventory)
                        {
                            //키와 일치한다면
                            if (SelectItem == itemSelectCount.ToString())
                            {
                                //텍스트 지우기

                                InventoryWindowReset(textVector.y);

                                ItemColor(a);

                                Console.Write($"{a.itemName}");
                                Console.WriteLine(" 을 제거하시겠습니까 ?");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("1. 제거 2.취소");

                                Thread.Sleep(700);

                                ConsoleKeyInfo inputkey = Console.ReadKey(true);

                                //취소눌렀을때
                                if (inputkey.Key == ConsoleKey.D2 || inputkey.Key == ConsoleKey.NumPad2)
                                {
                                    //while문탈출
                                    isKeyInput = true;
                                    //다시 인벤토리 접속시 로딩없이 접속하게함
                                    isBack = true;
                                    break;
                                }
                                //제거눌렀을때
                                else if (inputkey.Key == ConsoleKey.D1 || inputkey.Key == ConsoleKey.NumPad1)
                                {
                                    playerInventory.RemoveAt(itemSelectCount-1);
                                    //while문탈출
                                    isKeyInput = true;
                                    //다시 인벤토리 접속시 로딩없이 접속하게함
                                    isBack = true;
                                    itemCount--; //인벤토리 카운트 감소
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
