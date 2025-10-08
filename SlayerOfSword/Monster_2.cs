using EnumManager;
using SlayerOfSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SlayerOfSword.Monster;

namespace SlayerOfSword
{
    abstract partial class Monster //골렘 ~ 드래곤슬레이어2페이즈
    {

        public static bool isDragonSlayer2Page = false;  //드래곤슬레이어의 2페이즈 여부

        public static Monster CreateMonster(MonsterList _monsterList)  //몬스터 생성기 - 정적할당
        {
            Monster monster = null;
            switch(_monsterList)
            {
                case MonsterList.Goblin:
                    monster = new Goblin();
                    break;

                case MonsterList.Ghoul:
                    monster = new Ghoul();
                    break;

                case MonsterList.SnowMan:
                    monster = new SnowMan();
                    break;

                case MonsterList.Golem:
                    monster = new Golem();
                    break;

                case MonsterList.Dragon:
                    monster = new Dragon();
                    break;

                case MonsterList.DragonSlayer:
                    monster = new DragonSlayer();
                    break;
            }

            return monster;
        }

        class Golem : Monster
        {
            public Golem()
            {
                this.MonsterName = MonsterList.Golem.ToString();
                this.MonsterHp = 450;
                this.MonsterMp = 280;
                this.MonsterPower = 30;
                this.MonsterDefense = 45;

                this.currentMonsterHp = 450;
                this.currentMonsterMp = 280;

                this.monsterRewardGold = 150;

                this.monsterCreateText = "적 발견.. 제거 ..";
                this.playerLoseText = "생체반응 없음.. 제거 완료..";

                //골렘의 아이템 보상목록
                this.monsterRewardItem = new Item[5];
                this.monsterRewardItem[0] = new Weapon(WeaponList.Frostbrand, 16, ItemGrade.Epic);
                this.monsterRewardItem[1] = new Armor(ArmorList.CelestialMail, 10, PlusHp: 100, PlusMp: 60, ItemGrade.Rare);
                this.monsterRewardItem[2] = new Weapon(WeaponList.Thunderclash, 18, ItemGrade.Epic);
                this.monsterRewardItem[3] = new Armor(ArmorList.Stormplate, 15, PlusHp: 150, PlusMp: 100, ItemGrade.Epic);
                this.monsterRewardItem[4] = new Armor(ArmorList.FrostSentinel, 20, PlusHp: 180, PlusMp: 120, ItemGrade.Unique);



                PrintMonsterText(this.monsterCreateText, ConsoleColor.Cyan);     // 몬스터 대사치기

                Console.Clear();
                Console.WriteLine("                #**##%                     \r\n                ###%%%%%@@@@               \r\n               ##%%%%#%%@@@@#%@            \r\n               ##%@@%%%%@%@@*#%@@@ @@%%    \r\n               %%%@##%%%@@@%##%%@@@@@%##%  \r\n              %%@@%#*#%%@@%####%@@@@@%%%%# \r\n            %%%@@@%#*#%%@@%####%@@@@%%%### \r\n        %%%%@@@@@@@###%%%@%####%@@%%%%##   \r\n    %%%%%%@@@@@@   %##%#%@%%###%%##%#%%@@  \r\n  ###%%@@@@@@@@    %##%%@@%%##%%%##%%%@@@@ \r\n %%%@%#%@@@@@@      @@@%@@@@##%@@ %%%%@@@% \r\n%##%%%%@@@@@        @@@@@%%%@@@%   %%#%@@@@\r\n%%@@@@@@           @@@%%###%##%%@@%%#%@@@@@\r\n   %%              @@@@@@@%%#%%@%#%#%@@@@@@\r\n                   @@@@@@@@%#%@%###%@@@@@@@\r\n                   @@@@@@@%#@@@####@@@@@@@@\r\n                  @@@@@@@%#@@@####%@%@@@@@ \r\n                  @@@@@@@#%@@%##%%%%@@@@@  \r\n                   @@@@@%%%%%%##%%%%%@@@   \r\n                     @@@@%%%%% #%%%%%@@    \r\n                      @@@@%##%%##%%%%@     \r\n                      @@@@%##%%@           \r\n                      @@@@%##%%@@          \r\n                      %@@@%##%%%@          \r\n                     @%@@@%##%%@@          \r\n                     %%@@@%##%@@@          \r\n                     %#@@@%##%%@@@         \r\n                      %@@@%##%%@@          \r\n                      #%@@%###%%           \r\n                       %@@%%#%@            \r\n                        @@%###             \r\n                        %@%#%@             \r\n                        @@%#%              ");
                Thread.Sleep(1500);
            }

            public override MonsterSKillList MonsterTurn(out int mobSkillDemage)
            {
                int monsterSkillIndex;
                Random rnd = new Random();

                monsterSkillIndex = rnd.Next(((int)MonsterSKillList.GolemNormalAttack), ((int)MonsterSKillList.DragonNormalAttack));    //고블린과 구울사이의 스킬사용}
                mobSkillDemage = monsterSkillDemage[monsterSkillIndex]; //몬스터 스킬 데미지대입

                return (MonsterSKillList)monsterSkillIndex;
            }
        }

        class Dragon : Monster
        {
            public Dragon()
            {
                this.MonsterName = MonsterList.Dragon.ToString();
                this.MonsterHp = 750;
                this.MonsterMp = 480;
                this.MonsterPower = 70;
                this.MonsterDefense = 40;
                this.currentMonsterHp = 750;
                this.currentMonsterMp = 480;
                this.monsterRewardGold = 320;

                this.monsterCreateText = "인간이여 .. 두려움에 몸부림치거라";
                this.playerLoseText = "나약하기 짝이없군 인간녀석 크하하하";

                //드래곤의 아이템 보상목록
                this.monsterRewardItem = new Item[6];
                this.monsterRewardItem[0] = new Weapon(WeaponList.Thunderclash, 18, ItemGrade.Epic);
                this.monsterRewardItem[1] = new UniqueWeapon(WeaponList.Lumina, 25, ItemGrade.Unique);
                this.monsterRewardItem[2] = new UniqueWeapon(WeaponList.Ragnarok, 28, ItemGrade.Unique);
                this.monsterRewardItem[3] = new Armor(ArmorList.Stormplate, 15, PlusHp: 150, PlusMp: 100, ItemGrade.Epic);
                this.monsterRewardItem[4] = new Armor(ArmorList.FrostSentinel, 20, PlusHp: 180, PlusMp: 120, ItemGrade.Unique);
                this.monsterRewardItem[5] = new Armor(ArmorList.Thunderplate, 22, PlusHp: 200, PlusMp: 120, ItemGrade.Unique);

                PrintMonsterText(this.monsterCreateText, ConsoleColor.Red);     // 몬스터 대사치기

                Console.Clear();
                Console.WriteLine("              @      @@                                     \r\n        @      @@@@@   @@@                                  \r\n         @@      @@@@@@@@@@@                                \r\n           @@@@@@@@@@@@@@@@@@@    @@@@                      \r\n          @@@@@@@@@@@@@@@@@@@@@@ @@@@@@@@@@@@@@@@@          \r\n       @@@@@@@@@@@@@@@@@@@@@@@@@  @@@@@@@@@@@@@@@@@@@@      \r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    @@@@@@@@@@@@@@@@@@@@    \r\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   @@@@@@@@@@@@@@@@@@@@@@@@ \r\n@@@@@@ @@@@@@@@@@@@@@     @@@@@@@@@@ @@@@@@@@@@@@@@@       @\r\n    @@@@@@@@@@@@@@@@      @@@@@@@@@@@@@@@@@   @@  @         \r\n   @@@@@@  @@@@@@@@@@@@@@      @@@@@@@@@@@@@@@@             \r\n    @@@@  @@@@@@@@@@@@      @@@@@@@@@@@@@@@@@@@@@           \r\n          @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@          \r\n          @@@@@@@@@@@@@@@@@@@@@@@@@@@       @@@@@@@         \r\n          @@@@@@@@@@@@@@@@@@@@@@@@@         @@@@@@@@        \r\n              @@@@@@@@@@@@@@@@           @  @@@@@@@         \r\n               @@@@@@@@@@@@@@@           @ @@@@@@@@         \r\n                 @@@@@@@@@@@@@@@       @@@@@@@@@@@          \r\n                  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@          \r\n                   @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@           \r\n                   @@@@@@@@@@@@@@@@@@@@@@@@@@@@             \r\n                  @@@@@@@@@@@@@@@@@@@  @@@@@                \r\n          @@@@@  @@@@@@@   @@@@@@@@@@@@@                    \r\n          @@@@@@@@@@@@@@@ @@@@@@@@@@@@@@@       @@          \r\n           @@ @@@@@@@@ @@@@@@@@@@@@@@@@@@@     @            \r\n              @@@@ @@@@@@@@@@@@@@@@@@@@@@@@@  @@            \r\n                @@   @@ @@@@@@@@@@@@@@@@@@@@@@@@@           \r\n                     @@@@@@@@@@     @@@@@@@@@@@@@           \r\n                     @@@@@@@@        @@@@@@@  @@@           \r\n                      @@@@@@@ @@@    @@@@@@   @@@@          \r\n                      @@@@@@@   @@  @@@@@@@    @@@          \r\n                        @@@@@@@@@@@@@@@@@@    @@@@          \r\n                        @@@@@@@@@@@@@@@@@    @@@@           \r\n                          @@@@@@@@@@@@@     @@@@            \r\n                            @@@@@@@     @@@@@@              \r\n                              @@@@@@@@@@@@@@                ");
                Thread.Sleep(1500);
                
            }

            public override MonsterSKillList MonsterTurn(out int mobSkillDemage)
            {
                int monsterSkillIndex;
                Random rnd = new Random();

                monsterSkillIndex = rnd.Next(((int)MonsterSKillList.DragonNormalAttack), ((int)MonsterSKillList.DragonSlayer1NormalAttack));    //고블린과 구울사이의 스킬사용}
                mobSkillDemage = monsterSkillDemage[monsterSkillIndex]; //몬스터 스킬 데미지대입

                return (MonsterSKillList)monsterSkillIndex;
            }
        }

        class DragonSlayer : Monster
        {
            public DragonSlayer()
            {
                this.MonsterName = MonsterList.DragonSlayer.ToString(); //페이즈 1
                this.MonsterHp = 600;
                this.MonsterMp = 350;
                this.MonsterPower = 65;
                this.MonsterDefense = 35;
                this.currentMonsterHp = 600;
                this.currentMonsterMp = 350;
                

                this.monsterCreateText = "드래곤은 지겹도록 죽여왔지.. 다음은 인간인가?";
                this.playerLoseText = "약하다.. 너무 약해.. 강한녀석은 없는건가";

                PrintMonsterText(this.monsterCreateText, ConsoleColor.Red);     // 몬스터 대사치기

                Console.Clear();
                Console.WriteLine("                                                \r\n                                                \r\n                         :-.                    \r\n                        %@@%. -@@@@@@@%*=-:.:.. \r\n                        #@@@. -@@@@@@@@@@@@@#.. \r\n                        .@@%-.-@@@@@@@@@@@%=..  \r\n                      .*@@@@@@@#. .:-==--..     \r\n                      *@@@@@@@@#.               \r\n    ..:---:.         .@@@@@@@@@@#.              \r\n.::-#@@@@@@@*:.      .@@@@@@@@@@%=.             \r\n=%@@@@@@@@@@@@*:.  ..%@@@@@@@@@@@%-.            \r\n -@@@@@@@@@@@@@@*:..%@@@@@@@@@@@@@*:            \r\n -@@@@@@@@@@@@@@@@+%@@@@@@@@@@@@@@@*.     :-..  \r\n -@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%*=-.:#@@@*. \r\n :@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%*@@#. \r\n -@@@@@%=-#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@+. \r\n -@@@@%=-=+%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@-. \r\n -@@@@+   .+@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@:. \r\n .:**=.   .-%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#.  \r\n         .=@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@=.  \r\n         .*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@:.  \r\n         .=@@@@@@@@@@@@@@@@@@@@@@%%@@@@@@@@*.   \r\n       ..+@@@@@@@@@@@@@@@@@@@@@*. .+@@@@@@@-.   \r\n       .+@@@@@@@@%*==@@@@@......   .=@@@%@#:.   \r\n     .:#@@@%=:....   .@@@@.         .*@@#*-.    \r\n    .=@@@%=.          .@@@%.        .*@@%-..    \r\n    :%@@+.             .@@@:        =@%%@=.     \r\n    .-@@#.              :@@%.     .=%%--%+.     \r\n     .:%@+               =@@.    :*@#:.-%*.     \r\n      .-@@*=-:.          .#@%.  :#@*: .*@=.     \r\n       .=%@@@%=.          .@@*..=@+. .+%%=.     \r\n          .:::.          ..@@*.-%@*.            \r\n                         .%@%. -@@+.            \r\n                         %@@%.                  ");
                Thread.Sleep(1500);

            }
            public override MonsterSKillList MonsterTurn(out int mobSkillDemage)
            {
                int monsterSkillIndex;
                Random rnd = new Random();
                if (!isDragonSlayer2Page)
                {
                    monsterSkillIndex = rnd.Next(((int)MonsterSKillList.DragonSlayer1NormalAttack), ((int)MonsterSKillList.DragonSlayer2NormalAttack));    //스킬인덱스 범위지정}
                }
                else
                {
                    monsterSkillIndex = rnd.Next(((int)MonsterSKillList.DragonSlayer1NormalAttack)+1, ((int)MonsterSKillList.End));    //스킬인덱스 범위지정}
                }

                mobSkillDemage = monsterSkillDemage[monsterSkillIndex]; //몬스터 스킬 데미지대입
                return (MonsterSKillList)monsterSkillIndex;

            }

            //드래곤슬레이어 2페이즈 진입
          
            public DragonSlayer(bool isPage)
            {
                this.MonsterName = MonsterList.DragonSlayer.ToString();
                this.MonsterHp = 1000;
                this.MonsterMp = 650;
                this.MonsterPower = 85;
                this.MonsterDefense = 40;
                this.currentMonsterHp = 1000;
                this.currentMonsterMp = 650;
                this.isPage2 = true;
                this.monsterCreateText = "꽤 쓸만하군.. 이것도 받아보거라";
                this.playerLoseText = "꽤나 쓸만한녀석이었는데 아깝게되었군..";

                this.monsterRewardGold = 650;

                //드래곤슬레이어의 아이템 보상목록
                this.monsterRewardItem = new Item[6];
                this.monsterRewardItem[0] = new UniqueWeapon(WeaponList.Lumina, 25, ItemGrade.Unique);
                this.monsterRewardItem[1] = new UniqueWeapon(WeaponList.Ragnarok, 28, ItemGrade.Unique);
                this.monsterRewardItem[2] = new LegendWeapon(WeaponList.Eternity, 40, ItemGrade.Legend);
                this.monsterRewardItem[3] = new Armor(ArmorList.FrostSentinel, 20, PlusHp: 180, PlusMp: 120, ItemGrade.Unique);
                this.monsterRewardItem[4] = new Armor(ArmorList.Thunderplate, 22, PlusHp: 200, PlusMp: 120, ItemGrade.Unique);
                this.monsterRewardItem[5] = new Armor(ArmorList.DivineSentinel, 40, PlusHp: 280, PlusMp: 200, ItemGrade.Legend);

                PrintMonsterText(this.monsterCreateText, ConsoleColor.DarkRed);     // 몬스터 대사치기


                Console.Clear();
                Console.WriteLine("                       #*****#                    \r\n                      #+++++++*                   \r\n                     %******#**%                  \r\n                  ##%#****#****#                  \r\n                 **###********###                 \r\n                 ***#***#***#*++*                 \r\n             *#**#***%#*#****++**@                \r\n          #*++++***************++****#%           \r\n         *++++++++***********++++++++++*#         \r\n       #****+++++++****+****++++++++++++*%        \r\n      #*++******+++***+*****+++++++++++++#        \r\n    %#  #***+++*#****+**+****+++++++++++++%       \r\n    #*% %*+++*%##***++**+********+++++++++#       \r\n    #*# #***####*++++**++***+++++**#******+%      \r\n ******##*#####%#+***++++*+*+**+++++++++++*#      \r\n #****#*****%##% *+++++++*+*****+++**++++++*#     \r\n #****#**#+**#%  *+++++++*+***+++++#% #*+++**#    \r\n %#***#******#   #***********++++++*   #*+++*#%   \r\n   #*#******#    #************++++##    *+****#   \r\n  %#******#%    @****##*+*+****+++*@    #*****+*% \r\n%##%#*+**   @  ##*******+*******++**     %*****## \r\n    #****@    ##****#******++****++*%     #****** \r\n    #****#    **#+*#**+****+*+***#++#      ##****#\r\n    %*****   *++++++#*++++++++*****+*       #*****\r\n     ***+*@  #********++++++++++****+#      @****#\r\n     ***+*%  *******#*+++**+++++++***+@     #++**#\r\n     *+**** *+**+****+++****++++++**+*%    %**++*%\r\n     *+**+*%*#***++++++**+**+++++++*#+%     *##**#\r\n     #***+*% #+++++++++**+**+++++++**+#        #*%\r\n     #***+*  #++++++++*#+++**++++++**+#       ##% \r\n     #*+***% *++++++++***++**+++++++**@           \r\n      *+***# ++++++++**+##*+*+++++++*#            \r\n      #*****%++++++++**+++++*+++++++#             \r\n      #***+*#++++++++*#*++++*+++++++*             \r\n      #***+*#++++++++#      #++++++++             ");
                Thread.Sleep(1500);
            }
        }



    }
}
