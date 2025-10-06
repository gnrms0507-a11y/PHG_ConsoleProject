using EnumManager;
using System;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace SlayerOfSword
{
    
     abstract partial class Monster      //추상클래스 Monster 생성 길이가 길어서 partical로.
    {
        
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


        //몬스터의 스킬 소모 mp
        //public int[] monsterSkillMp = new int[(int)MonsterSKillList.End]
        //{ 0, 10,
        //    0, 15, 10,
        //    0, 30, 20,
        //    0, 45, 20,
        //    0, 50, 35, 30,
        //    0, 50, 30,
        //    0, 70, 100, 150, 300 };  //맨끝은 End =0

        //몬스터 스킬의 데미지 나열
        public int[] monsterSkillDemage = new int[(int)MonsterSKillList.End]
        { 0, 18,
            0, 24, 22,
            0, 35, 32,
            0, 38, 40,
            0, 100, 78, 75,
            0, 80, 68,
            0, 120, 135, 185}; //맨끝은 End =0

        public string MonsterName { get; set; }
        public int MonsterHp { get; set; }
        public int MonsterMp { get; set; }
        public int MonsterPower { get; set; }
        public int MonsterDefense { get; set; }
        public int currentMonsterHp { get; set; }
        public int currentMonsterMp { get; set; }
        
        public int monsterRewardGold { get; set; }

        public string monsterCreateText { get; set; }   //몬스터 생성시 텍스트

        public string playerLoseText {  get; set; }  //플레이어가 졌을때 텍스트

        public abstract MonsterSKillList MonsterTurn(out int _mobSkillDemage);  //추상메서드 생성 - 몬스터 턴

        TextVector textVector = new TextVector();   //텍스트 출력 좌표지정

        public void PrintMonsterText(string text,ConsoleColor _consoleColor)      //몬스터 대사 출력
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
        public void PrintMonsterText(string mobName ,int reward)       //몬스터 보상 출력 , 오버로딩
        {

            textVector.x = 80;
            textVector.y = 50;

            string rewardText = mobName + " 과의 전투에서 승리하였다.";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(textVector.x / 2 - rewardText.Length, textVector.y / 2 - 3);

            for(int i =0; i< rewardText.Length; i++)    //승리 텍스트 출력
            {
                Console.Write(rewardText[i]);
                Thread.Sleep(60);
            }

            Console.SetCursorPosition(textVector.x / 2 - "만큼의 Gold를 휙득!".Length-3, textVector.y / 2 - 2);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{reward.ToString()} 만큼의 Gold를 휙득!");
            Player.gold += reward;  //보상골드 증가
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
            this.MonsterPower = 15;
            this.MonsterDefense = 4;
            this.currentMonsterHp = 100;
            this.currentMonsterMp = 50;
            this.monsterRewardGold = 20;

            this.monsterCreateText = "인간은 참 어리석단말야.. 크크크";
            this.playerLoseText = "역시 인간은 어리석어 크크크크";

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
            this.MonsterPower = 20;
            this.MonsterDefense = 6;
            this.currentMonsterHp = 150;
            this.currentMonsterMp = 65;
            this.monsterRewardGold = 35;

            this.monsterCreateText = "인간이다.. 맛있는 냄새..";
            this.playerLoseText = "인간 피 .. 맛있다 크큭..";

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
            this.MonsterPower = 28;
            this.MonsterDefense = 10;

            this.currentMonsterHp = 270;
            this.currentMonsterMp = 100;
            this.monsterRewardGold = 60;

            this.monsterCreateText = "조그만녀석, 눈사람으로 만들어주마 !";
            this.playerLoseText = "눈사람이 되었구나 부숴지거라!!";

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
