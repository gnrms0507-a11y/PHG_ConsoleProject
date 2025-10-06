using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SlayerOfSword
{
    partial class Player    //플레이어 생성 및 기본 세팅
    {
        public static int gold = 500;   //첫 시작 골드 500원
        public int power { get; set; }
        public int armor { get; set; }
        public int maxHp { get; set; }
        public int maxMp { get; set; }
        public string playerName { get; }

        public static int currentHp;    //현재 Hp, Mp
        public static int currentMp;
        //public static int playerExp; //경험치는 추후에..

        public static bool isPlayerReady = false;   //플레이어가 이미 만들어있는지 check하기위한 변수

        TextVector vector;  //GameManager.cs의 TextVector 가져옴

       

        public Player(string _Name)     //플레이어 맨처음 생성시 스탯과 이름
        {
            power = 25;
            armor = 5;
            maxHp = 999;
            maxMp = 120;
            currentHp = 999;
            currentMp = 120;
            playerName = _Name;

            //플레이어 기본스킬 3개 등록
            PlayerBasicSkill();
            
        }

        public static Player CreatePlayer()   //사용자에게 이름을 입력받아 플레이어 생성
        {
             Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
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
                    Console.WriteLine($"SlayerName : {name}");
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
            Console.Title = "Slayer Of Sword : "+name; //콘솔의 타이틀지정
            Console.ForegroundColor = ConsoleColor.Gray;
            return player;

        }
        public void PrintPlayerInfo()       //플레이어 생성이 완료되었다면 플레이어의 정보를 입력할것
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            vector.x = 55;
            vector.y = 35;
            if (Player.isPlayerReady == true)      //만약 플레이어 생성했으면 플레이어 이름 출력
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(vector.x, vector.y-1);
                Console.Write($"Name:{playerName}");
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(vector.x, vector.y);  // 플레이어 정보 출력할것
            Console.Write($"Gold : {Player.gold}");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(vector.x, vector.y+1);  // 플레이어 공격력 방어력 출력
            Console.Write($"Attack: {power}");

            Console.SetCursorPosition(vector.x, vector.y + 2);
            Console.Write($"Defense: {armor}");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(vector.x, vector.y+3);  // 플레이어 Hp 출력할것
            Console.Write($"HP: {Player.currentHp}/{maxHp}");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(vector.x, vector.y+4);  // 플레이어 Mp 출력할것
            Console.Write($"MP: {Player.currentMp}/{maxMp}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void UpdatePlayer(int _power, int _armor, int _maxHp, int _maxMp)     //플레이어 능력치 업데이트
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


    }
   

    
}
