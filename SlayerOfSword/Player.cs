using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlayerOfSword
{
    class Player
    {
        public static int gold = 500;   //첫 시작 골드 500원
        public int power { get; set; }
        public int armor { get; set; }
        public int maxHp { get; set; }
        public int maxMp { get; set; }
        public string playerName { get; set; }

        public static int currentHp;    //현재 Hp, Mp
        public static int currentMp;

        public static bool isPlayerReady = false;   //플레이어가 이미 만들어있는지 check하기위한 변수


        public Player(string _Name)     //플레이어 맨처음 생성시 스탯과 이름
        {
            power = 10;
            armor = 5;
            maxHp = 300;
            maxMp = 100;
            currentHp = 300;
            currentMp = 100;
            playerName = _Name;
        }

        public Player() //기본생성자는 빈값
        {
            
        }

        public void UpdatePlayer(int _power,int _armor,int _maxHp,int _maxMp)
        {
            power += _power;
            armor += _armor;
            maxHp += _maxHp;
            maxMp += _maxMp;

            if (currentHp >= maxHp)  //현재 HP와 MP가 최대 HP ,MP 이상일시 현재체력을 최대 HP,MP로 지정
            {
                currentHp = maxHp;
            }
            else if (currentMp >= maxMp) 
            { 
                currentMp = maxMp;
            }

        }
        public static Player CreatePlayer()   //사용자에게 이름을 입력받아 플레이어 생성
        {
            Console.ResetColor();
            Console.Clear();

            string name = null;

            bool isPlayerName = false;


            while (isPlayerName == false)
            {
                Console.Clear();
                Console.WriteLine("당신의 이름은 무엇입니까 ? : ");
                Console.WriteLine("2글자이상 , 6글자이하로 입력");
                Console.SetCursorPosition("당신의 이름은 무엇입니까 ? : ".Length + 10, 0);
                name = Console.ReadLine();

                if (name != null && name.Length >= 2 && name.Length <= 6)
                {
                    Console.Clear();
                    Console.WriteLine($"플레이어명 : {name}");
                    Player.isPlayerReady = true;   //캐릭터생성완료
                    isPlayerName = true;    //유효한 플레이어이름 입력
                    
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("2글자이상 6글자이하로 입력 바랍니다");
                    Thread.Sleep(1000);
                }
            }

            Player player = new Player(name);
            return player;

        }


    }
   

    
}
