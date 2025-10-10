using EnumManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace SlayerOfSword
{
    //무기,방어구
    public enum WeaponList
    {
        TrainingSword, LongSword, IronBlade, CrimsonSaber, Frostbrand, Thunderclash, Lumina, Ragnarok, Eternity
    }

    public enum ArmorList
    {
        TrainingArmor, IronMail, ScoutArmor, CrimsonMail, CelestialMail, Stormplate, FrostSentinel, Thunderplate, DivineSentinel
    }

    public abstract partial class Monster      //고블린 ~ 스노우맨 세팅
    {
        public static List<Weapon> dropWeapons = new List<Weapon>();

        public static List<Armor> dropArmors = new List<Armor>();

        //정적생성자로 몬스터 드랍아이템 리스트에 추가
        static Monster()
        {
            dropWeapons.Add(new Weapon(WeaponList.LongSword, 10, ItemGrade.Normal));
            dropWeapons.Add(new Weapon(WeaponList.IronBlade, 12, ItemGrade.Normal));

            dropWeapons.Add(new Weapon(WeaponList.CrimsonSaber, 20, ItemGrade.Rare));
            dropWeapons.Add(new Weapon(WeaponList.Frostbrand, 22, ItemGrade.Rare));

            dropWeapons.Add(new Weapon(WeaponList.Thunderclash, 33, ItemGrade.Epic));

            dropWeapons.Add(new UniqueWeapon(WeaponList.Lumina, 48, ItemGrade.Unique));
            dropWeapons.Add(new UniqueWeapon(WeaponList.Ragnarok, 50, ItemGrade.Unique));

            dropWeapons.Add(new LegendWeapon(WeaponList.Eternity, 65, ItemGrade.Legend));


            
            dropArmors.Add(new Armor(ArmorList.ScoutArmor, 8, ItemGrade.Normal));
            dropArmors.Add(new Armor(ArmorList.IronMail, 10, ItemGrade.Normal));

            dropArmors.Add(new Armor(ArmorList.CrimsonMail, 18, ItemGrade.Rare));
            dropArmors.Add(new Armor(ArmorList.CelestialMail, 20, ItemGrade.Rare));

            dropArmors.Add(new Armor(ArmorList.Stormplate, 30, ItemGrade.Epic));
            dropArmors.Add(new Armor(ArmorList.FrostSentinel, 32, ItemGrade.Epic));

            dropArmors.Add(new Armor(ArmorList.Thunderplate, 45, ItemGrade.Unique));

            dropArmors.Add(new Armor(ArmorList.DivineSentinel, 60, ItemGrade.Legend));
        }
        public enum MonsterSKillList       //몬스터들이 사용할 스킬리스트
        {
            GoblinNormalAttack,ThrowRock,

            GhoulNormalAttack,RottingTouch,Bite,

            SnowManNormalAttack, Blizzard,ThrowSnowBall,

            GolemNormalAttack, RockSmash , laserbeam,

            DragonNormalAttack, FireBreath, WingSweep, TailSlam,

            DragonSlayer1NormalAttack, DragonStrike, GuardBreak,

            DragonSlayer2NormalAttack,FlameSword, BladeTempest,SwordJudgement, End
        }

        //몬스터 스킬의 데미지 나열
        public int[] monsterSkillDemage = new int[(int)MonsterSKillList.End]
        { 0, 35,
            0, 38, 35,
            0, 50, 44,
            0, 52, 50,
            0, 115, 92, 88,
            0, 92, 80,
            0, 150, 160, 180}; //맨끝은 End =0

        public string MonsterName { get; set; }
        public int MonsterHp { get; set; }
        public int MonsterMp { get; set; }
        public int MonsterPower { get; set; }
        public int MonsterDefense { get; set; }
        public int currentMonsterHp { get; set; }
        public int currentMonsterMp { get; set; }
        
        public bool isPage2 { get; set; }   //2페이즈여부 확인
        public int monsterRewardGold { get; set; }

        public string monsterCreateText { get; set; }   //몬스터 생성시 텍스트

        public string playerLoseText {  get; set; }  //플레이어가 졌을때 텍스트

        public abstract MonsterSKillList MonsterTurn(out int _mobSkillDemage);  //추상메서드 생성 - 몬스터 턴

        TextVector textVector = new TextVector();   //텍스트 출력 좌표지정

        public Item[] monsterRewardItem;  //몬스터의 보상아이템

        //몬스터 대사 출력
        public void PrintMonsterText(string text,ConsoleColor _consoleColor)      
        {
            
            textVector.x = 80;
            textVector.y = 50;

            Console.Clear();
          
            Console.ForegroundColor = _consoleColor;
            Console.SetCursorPosition(textVector.x / 2 - text.Length, textVector.y / 2 - 3);
           
            for (int i =0; i<text.Length; i++)
            {
                Console.Write(text[i]);
                Thread.Sleep(100);
            }
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        //몬스터 보상 출력 , 오버로딩
        public void PrintMonsterText(Monster _monster ,int reward)       
        {

            textVector.x = 80;
            textVector.y = 50;

            string rewardText = _monster.MonsterName + " 과의 전투에서 승리하였다.";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(textVector.x / 2 - rewardText.Length, textVector.y / 2 - 3);

            for(int i =0; i< rewardText.Length; i++)    //승리 텍스트 출력
            {
                Console.Write(rewardText[i]);
                Thread.Sleep(60);
            }

            Console.SetCursorPosition(textVector.x / 2 - rewardText.Length, textVector.y / 2 - 2);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{reward.ToString()} 만큼의 Gold를 휙득!");
            Player.gold += reward;  //보상골드 증가

            bool isDropItem = (Inventory.playerInventory.Count >10) ? true : false;   //인벤토리가 꽉찼을 경우로 10개이상 인벤토리에 있으면 더이상못가짐

            if (!isDropItem)
            {
                Random itemDrop = new Random();
                int dropItemPerCent = itemDrop.Next(1, 4); //1~3까지 수 뽑음

                //int dropItemPerCent = 3; //테스트용

                //3을뽑을경우 당첨 - 아이템드랍
                if (dropItemPerCent == 3)
                {
                    //당첨되었을경우 (3)뽑음 , 몬스터의 보상개수만큼 난수를 받아 보상 받기

                    int dropIndex = itemDrop.Next(0, monsterRewardItem.Length);

                    Console.SetCursorPosition(textVector.x / 2 - rewardText.Length, textVector.y / 2 - 1);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{_monster.MonsterName} 에게서 ");

                    //아이템 등급에 따른 폰트색 지정
                    Inventory.ItemColor(_monster.monsterRewardItem[dropIndex]);

                    Console.Write($"{(_monster.monsterRewardItem[dropIndex]).itemName}");

                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.WriteLine(" 을 획득하였다");

                    //인벤토리에 아이템추가 Key 는 인벤토리카운트
                    Inventory.playerInventory.Add(_monster.monsterRewardItem[dropIndex]);
                }
            }
            else if (isDropItem)
            {
                Console.SetCursorPosition(textVector.x / 2 - rewardText.Length, textVector.y / 2 - 1);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"아이템을 더이상 획득할수 없습니다. 인벤토리를 비워주세요");
            }

                Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Gray;
            Thread.Sleep(1000);

        }

        public void PrintMonsterHp(Monster _monster)        //몬스터 피통출력
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Console.WindowWidth; i++) //줄치기
            {
                Console.Write("━");
            }
            textVector.x = Console.WindowWidth;
            textVector.y = 0;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(textVector.x / 2 - ($"{_monster.MonsterName} Hp:{_monster.currentMonsterHp}/{_monster.MonsterHp}").Length, textVector.y);
            Console.Write($"{_monster.MonsterName} Hp:{_monster.currentMonsterHp}/{_monster.MonsterHp}");

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //드래곤슬레이어 2페이즈 생성
        public static Monster DragonSlayer2Page()
        {
            Monster dragonSlayer2Page = new DragonSlayer(true);

            return dragonSlayer2Page;
        }

    }

    class Goblin : Monster 
    {
        public Goblin()
        {
            this.MonsterName = MonsterList.Goblin.ToString();
            this.MonsterHp = 100;
            this.MonsterMp = 50;
            this.MonsterPower = 25;
            this.MonsterDefense = 10;
            this.currentMonsterHp = 100;
            this.currentMonsterMp = 50;
            this.monsterRewardGold = 15;

            this.monsterCreateText = "인간은 참 어리석단말야.. 크크크";
            this.playerLoseText = "역시 인간은 어리석어 크크크크";



            //고블린의 아이템 보상목록
            this.monsterRewardItem = new Item[3];
            this.monsterRewardItem[0] = dropWeapons[0];
            this.monsterRewardItem[1] = dropArmors[0];
            this.monsterRewardItem[2] = dropArmors[1];


            PrintMonsterText(this.monsterCreateText, ConsoleColor.Green);     // 몬스터 대사치기
            Console.Clear();
            Console.WriteLine("                                                  \r\n                             +==                  \r\n                            **+=-+=               \r\n                         ***++*====               \r\n                    ****##+*+++++=+               \r\n                  #%%%#+*%#**++++++               \r\n                ###%%@@%**#*+**==++               \r\n               ######%@@@%###*#%++                \r\n               %%%%##%@%%*#####++                 \r\n              #%%@@%%%%%#* %#%@*+                 \r\n              ###%%%%# %**                        \r\n             %@@##%##% @%%%%                      \r\n            %%@@@%%#%%#  @%##@                    \r\n            ##%%%%%#%%%#%**%*+                    \r\n            %%%@@%%%##%##***++                    \r\n            @%%%%%@ %#%%###                       \r\n             @%%#%@%*%%#**+                       \r\n             %%*+#@@  %%#++                       \r\n              %#*+%%  %%##*=                      \r\n             ##%##   %%###*++                     \r\n             %##     %#****+*+                    \r\n            %#*     ##%#***++                     \r\n            #**#        %###***                   \r\n            ###*                                  \r\n           %#%###                                 \r\n           %%%%#                       ");
            Thread.Sleep(1500);
        }

        public override MonsterSKillList MonsterTurn(out int mobSkillDemage)
        {
            int monsterSkillIndex;
           
            Random rnd = new Random();

           monsterSkillIndex = rnd.Next(((int)MonsterSKillList.GoblinNormalAttack), ((int)MonsterSKillList.GhoulNormalAttack));    //고블린과 구울사이의 스킬사용}

            //this.currentMonsterMp = currentMonsterMp - monsterSkillMp[monsterSkillIndex];   //몬스터 mp소모
            mobSkillDemage = monsterSkillDemage[monsterSkillIndex]; //몬스터 스킬 데미지대입
            
            return (MonsterSKillList)monsterSkillIndex;
        }
      
    }

    class Ghoul : Monster
    {
        public Ghoul()
        {
            this.MonsterName = MonsterList.Ghoul.ToString();
            this.MonsterHp = 150;
            this.MonsterMp = 65;
            this.MonsterPower = 32;
            this.MonsterDefense = 14;
            this.currentMonsterHp = 150;
            this.currentMonsterMp = 65;
            this.monsterRewardGold = 25;

            this.monsterCreateText = "인간이다.. 맛있는 냄새..";
            this.playerLoseText = "인간 피 .. 맛있다 크큭..";

            //구울의 아이템 보상목록
            this.monsterRewardItem = new Item[4];
            this.monsterRewardItem[0] = dropWeapons[1];
            this.monsterRewardItem[1] = dropArmors[1];
            this.monsterRewardItem[2] = dropWeapons[2];
            this.monsterRewardItem[3] = dropArmors[2];


            PrintMonsterText(this.monsterCreateText, ConsoleColor.Magenta);     // 몬스터 대사치기
            Console.Clear();

            Console.WriteLine("                                                         \r\n                             ...                            \r\n                           :*%@%*-.                         \r\n                         .*@@@@@@@*.                        \r\n                         .%###@%#@#:                        \r\n                         .**.+++.=*.                        \r\n                          :%%@*%%#-.                        \r\n                           =++**=*.                         \r\n                           .%@@@@:                          \r\n                            .-@+.                           \r\n                            .=@*.                           \r\n                     :=*=*%*+*@#++##=+=-.                   \r\n                     -@-.+-:-+@#---#.-@#.                   \r\n                     =*.:+:::*@%-::=+.+#.                   \r\n                    .%-.+*--=*@%+=-+*.-@.                   \r\n                   .=%..*+:-+=@#=+:**:.@-                   \r\n                   .%- .*=+=-:@+:===+..#=.                  \r\n                  .=@. .***..-@*..=*#. **.                  \r\n                  .#@-  :=. .-@*. .=-..@%-                  \r\n                  -#%:      .=@#.     .#%+                  \r\n                  .+%.      .+@%:      :**.                 \r\n                  .+*   .+*+:+@#:-*#=. .=#:                 \r\n                  .*+   +@@@@#@%#@@@@=. :+*.                \r\n                  .*=   .=%@%*@%%#@%-.  .++:                \r\n                  .#+   :=#*=.+=.=#%:-.  :#+.               \r\n                  .#+. .+@+=+=#+++=+@*.  .##:               \r\n                  -#*+:.:%:  ....  .@=. .+**=               \r\n                 .#*=+...#:        .@- .:-+#+.              \r\n                 .+=+=. .#:        :@:  .+=+=               \r\n                  .:-.  .#-        -@.   ...                \r\n                        .*=        =%.                      \r\n                        .#+       .*%.                      \r\n                        :@@:     .:%@-.                     ");
            Thread.Sleep(1500);
        }

        public override MonsterSKillList MonsterTurn(out int mobSkillDemage)
        {
            int monsterSkillIndex;
            Random rnd = new Random();

            monsterSkillIndex = rnd.Next(((int)MonsterSKillList.GhoulNormalAttack) , ((int)MonsterSKillList.SnowManNormalAttack));    //구울과 스노우맨 사이의 스킬사용}

            mobSkillDemage = monsterSkillDemage[monsterSkillIndex]; //몬스터 스킬 데미지대입

            return (MonsterSKillList)monsterSkillIndex;
        }
    }

    class SnowMan : Monster
    {
        public SnowMan()
        {
            this.MonsterName = MonsterList.SnowMan.ToString();
            this.MonsterHp = 270;
            this.MonsterMp = 100;
            this.MonsterPower = 40;
            this.MonsterDefense = 20;

            this.currentMonsterHp = 270;
            this.currentMonsterMp = 100;
            this.monsterRewardGold = 40;

            this.monsterCreateText = "조그만녀석, 눈사람으로 만들어주마 !";
            this.playerLoseText = "눈사람이 되었구나 부숴지거라!!";

            //스노우맨의 아이템 보상목록
            this.monsterRewardItem = new Item[5];
            this.monsterRewardItem[0] = dropWeapons[1];
            this.monsterRewardItem[1] = dropWeapons[2];
            this.monsterRewardItem[2] = dropArmors[2];
            this.monsterRewardItem[3] = dropArmors[3];
            this.monsterRewardItem[4] = dropWeapons[3];

            PrintMonsterText(this.monsterCreateText, ConsoleColor.DarkBlue);     // 몬스터 대사치기
   

            Console.Clear();
            Console.WriteLine("           @@@@               \r\n         @@@@@@@@             \r\n        @@@@@@@@@             \r\n        @@@@@@@@@@            \r\n      @@@@@@@@@@@@@           \r\n   @@@@@@@@@@@@@@@@@@@        \r\n  @@@@@@@@@@@@@@@@@@@@@       \r\n  @@@@@@@@@@@@@@@@@@@@@@      \r\n @@@@@@@@@@@@@@@@@@@@@@@      \r\n @@@@@@@@@@@@@@@@@@@@@@@@     \r\n @@@@@@@@@@@@@@@@@@@@@@@@@    \r\n @@@@@@@@@@@@@@@@@@@@@@@@@@   \r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ \r\n@@@@@ @@@@@@@@@@@@@@@@ @@@@@@@\r\n@@@@@ @@@@@@@@@@@@@@@@   @@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@     @ \r\n @@@@@@@@@@@@@@@@@@@@@@       \r\n @@@@ @@@@@@@@@@@@@@@@@       \r\n      @@@@@@@@@@@@@@@@@@      \r\n      @@@@@@@@ @@@@@@@@@      \r\n      @@@@@@@  @@@@@@@@@      \r\n      @@@@@@@   @@@@@@@@      \r\n      @@@@@@@    @@@@@@@      \r\n      @@@@@@@    @@@@@@@      \r\n      @@@@@@     @@@@@@@      \r\n      @@@@@@     @@@@@@@      \r\n     @@@@@@@      @@@@@@      \r\n     @@@@@@@      @@@@@@      \r\n     @@@@@@@      @@@@@@@     \r\n  @@@@@@@@@@      @@@@@@@@@@  \r\n@@@@@@@@@@@@      @@@@@@@@@@@@");
            Thread.Sleep(1500);
        }     
        public override MonsterSKillList MonsterTurn(out int mobSkillDemage)
        {
            int monsterSkillIndex;
            Random rnd = new Random();

            monsterSkillIndex = rnd.Next(((int)MonsterSKillList.SnowManNormalAttack), ((int)MonsterSKillList.GolemNormalAttack));    //스킬 enum 범위지정

            mobSkillDemage = monsterSkillDemage[monsterSkillIndex]; //몬스터 스킬 데미지대입

            return (MonsterSKillList)monsterSkillIndex;
        }
    }

   
}
