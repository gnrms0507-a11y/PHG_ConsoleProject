using EnumManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordMaker;
using static System.Net.Mime.MediaTypeNames;

namespace EnumManager       //게임에 사용되는 열거형을 관리하는 네임스페이스 
{
    public enum MainMenu    //시작페이지의 열거형
    {
         GameStart=1, Exit
    }

    public enum Menu    //선택페이지의 열거형
    {
         Battle=1, Inventory, Recovery, Back
    }
    public enum MonsterList    //던전(몬스터) 리스트
    {
         Goblin=1, Ghoul, SnowMan, Golem, Dragon, DragonSlayer, Back
    }
    enum Difficulty     //난이도 정렬
    {
         VeryEasy=1, Easy, Noraml, Hard, Extreme, Nightmare, Back
    }
    public enum ItemGrade
    {
        Normal, Rare, Epic, Unique, Legend
    }

    //무기,방어구
    public enum WeaponList
    {
        TrainingSword, LongSword, IronBlade, CrimsonSaber, Frostbrand, Thunderclash, Lumina, Ragnarok, Eternity
    }

    public enum ArmorList
    {
        TrainingArmor, IronMail, ScoutArmor, CrimsonMail, CelestialMail, Stormplate, FrostSentinel, Thunderplate, DivineSentinel
    }
}

namespace SlayerOfSword
{
  
    internal class Program
    {
        /*
           (턴제 RPG) 제목 Slayer of sword
           */
        
        static void Main()
        {

            #region 인스턴스 및 필드
            GameManager gm = new GameManager();   //GM생성  - 게임 세팅 및 시작 생성자에서 메서드실행
            MainPage mainPage = new MainPage(); //MainPage 생성
            BattlePage battlePage = new BattlePage();   //BattlePage
            Inventory inventory = new Inventory(); //Inventory 생성
            Recovery recovery = new Recovery();  //회복센터
            Player player = null;  //플레이어 생성 (껍데기)
            Item item = new Item(); //아이템 생성
          

            MainMenu mainmenu = default;
            Menu menu = default;
            MonsterList monsterList = default;

            bool? isRun = null;

            Stack<Action> printer = new Stack<Action>();    //Stack 자료구조 (Action) 생성 , 반환값없음
            Action print = null;    //Action 변수생성 

            bool isGameOff = false;

            //페이지별로 메뉴이동여부 확인 - 메인페이지만 처음에 true
            bool isMainPage = true;
            bool isMenuPage = false;

            bool LockMonster = false;

            string resultMainPage;  //메인페이지 메뉴 선택결과
            string resultMenuPage = null;  //메뉴페이지 메뉴 선택결과 

            MonsterList resultBattlePage = default;    //배틀페이지 몬스터 선택결과


            bool? isRecovery = null;    //회복창에서 회복/취소 여부선택 혹은 회복할필요가없을때 , 기본값null

            bool? isBack = null; //인벤토리창에서 취소선택했을때 ,기본값 null


            #endregion

            //테스트용
            Inventory.playerInventory.Add(new Weapon(WeaponList.LongSword, 5, ItemGrade.Epic));
            Inventory.playerInventory.Add(new UniqueWeapon(WeaponList.Lumina, 6, ItemGrade.Unique));
            Inventory.playerInventory.Add(new LegendWeapon(WeaponList.Eternity, 15, ItemGrade.Legend));
            Inventory.playerInventory.Add(new Armor(ArmorList.CelestialMail, 5, 100, 100, ItemGrade.Rare));
     
            while (!isGameOff)    //게임시작. (무한반복시작!)
            {
                //메인페이지 출력
                if (isMainPage)
                {
                    printer.Push(mainPage.SelectMainPage);
                    print = printer?.Pop();     //처음 메인페이지 출력후 펑
                    print.Invoke();

                    resultMainPage = gm.MoveMenu(mainmenu).ToString();

                    if (resultMainPage == "GameStart")
                    {
                        if (Player.isPlayerReady == false)
                        {
                            gm.Loading("Loading", 1500);    //로딩메서드 실행
                            player = Player.CreatePlayer();   //플레이어 생성 후 할당
                                                            
                            isMenuPage = true;  //메뉴페이지 넘어감
                        }
                        else if (resultMainPage == "GameStart" && Player.isPlayerReady == true)
                        {
                            gm.Loading($"{player.playerName} 접속중", 1000);    //로딩메서드 실행 캐릭터 생성이 되었다면 캐릭터명 출력
                            isMenuPage = true;  //메뉴페이지로 넘어감
            
                        }
                    }
                    else if (resultMainPage == "Exit")
                    {
                        isGameOff = true;  //게임종료
                        break;
                    }
                    //GameStart와 Exit외의 메뉴 선택
                    else
                    {
                        isMenuPage = false;
                    }
                }

                //메뉴페이지 출력
                if (isMenuPage)
                {
                    //이전페이지 off
                    isMainPage = false;

                    printer.Push(player.PrintPlayerInfo);   //플레이어정보출력
                    printer.Push(mainPage.PrintMenu);
                    
                    print = printer.Pop();
                    print.Invoke();
                    print = printer.Pop();
                    print.Invoke();
                    resultMenuPage = gm.MoveMenu(menu).ToString();
                }

                //그다음 페이지
                switch (resultMenuPage)
                {
                    //전투페이지
                    case "Battle":

                        //배틀페이지 진입시 메인, 메뉴 페이지 전부 off Back을 눌러야만 메뉴페이지로 갈수있음
                        isMainPage = false; 
                        isMenuPage = false;

                       //잠겨있는 몬스터를 선택할경우엔 로딩이 필요없음
                        if(LockMonster==false)
                        {
                           gm.Loading($"전투 페이지로 이동", 700);
                        }

                        printer.Push(player.PrintPlayerInfo);   //플레이어정보출력.
                        printer.Push(battlePage.PrintMonsterList);       //메뉴출력
                        print = printer.Pop();
                        print.Invoke();
                        print = printer.Pop();
                        print.Invoke();

                        resultBattlePage = gm.MoveMenu(monsterList);

                        battlePage.SelectMonster(resultBattlePage,out bool isLock);      //배틀페이지의 SelectMonster 메서드에 선택한 mosterList값을 string타입으로 전달

                        LockMonster = isLock;

                        if (resultBattlePage != MonsterList.Back && isLock == false)     //전투선택창에서 Back 이 몬스터가 잠겨있지않을경우 시작
                        {
                            battlePage.Battle(player, Monster.CreateMonster(resultBattlePage),out isRun);    //입력받은 MonsterList형 resultBattlePage를 CreateMonster에 넘김,배틀시작
                            //드래곤슬레이어 2페이즈 시작
                            if (resultBattlePage == MonsterList.DragonSlayer && isRun == false)
                            {
                                Monster.isDragonSlayer2Page = true;
                                battlePage.Battle(player, Monster.DragonSlayer2Page(), out isRun);
                            }
                        }

                        //Back선택시 메뉴페이지 on
                        else if (resultBattlePage == MonsterList.Back)
                        {
                            gm.Loading($"메뉴 페이지로 이동", 700);
                            isMenuPage = true; //메인페이지는 생략하고 출력
                        }
                         break;

                    //인벤토리
                    case "Inventory":
                        //인벤토리에서 결과받을 string 변수지정
                        string resultInventoryPage;

                        isMainPage = false; //메인페이지는 생략하고 출력
                        isMenuPage = false;

                        //장비장착이나 제거에서 취소를 눌렀을때 로딩없이 바로 이동함
                        if (isBack != true)
                        {
                            gm.Loading($"인벤토리를 불러오는 중입니다", 1000);
                        }
                        else
                        {
                            isBack = false;
                        }

                        resultInventoryPage = inventory.PrintInventory(player);


                        switch (resultInventoryPage)
                        {
                            case "Equip":
                                inventory.EquipOrChange(player, out isBack);


                                break;

                            case "Remove":
                                inventory.RemoveItem(player, out isBack);


                                break;

                            case "Back":
                                gm.Loading($"메뉴 페이지로 이동", 700);
                                isMainPage = false; //메인페이지는 생략하고 출력
                                isMenuPage = true;
                                break;
                        }

                        break;


                    //회복 페이지
                    case "Recovery":
                        //회복페이지에서 결과받을 string 변수지정
                        string resultRecoveryPage;

                        isMainPage = false; //메인페이지는 생략하고 출력
                        isMenuPage = false; //메뉴페이지는 생략하고 출력

                        //회복확인여부에서 취소를 눌렀을경우 로딩없이 바로 회복페이지로 이동함
                        if (isRecovery != true)
                        {
                            gm.Loading($"회복공간으로 이동합니다", 800);
                        }
                        else
                        {
                            isRecovery = false; //취소여부 초기화
                        }

                        resultRecoveryPage = recovery.PrintHealingStationPage(player);

                        //Hp나 Mp (1번 아님 2번을선택) 일경우 회복선택하는 메서드 실행 회복을 마쳤을때도 isRecovery는 true가되어 로딩을 생략한다
                        if (resultRecoveryPage =="Hp" || resultRecoveryPage == "Mp")
                        {
                            recovery.RecoveryHpMp(player, resultRecoveryPage, out isRecovery);
                        }

                        else if(resultRecoveryPage == "Back")   
                        {
                            gm.Loading($"메뉴 페이지로 이동", 700);
                            isMainPage = false; //메인페이지는 생략하고 출력
                            isMenuPage = true;
                        }

                        if(resultRecoveryPage != "Back" && isRecovery==true)
                        {
                            isMainPage = false; //메인페이지는 생략하고 출력
                            isMenuPage = false; //메뉴페이지는 생략하고 출력
                        }


                            Console.WriteLine("");

                        break;


                    case "Back":
                        gm.Loading($"메인으로 이동합니다", 1000);
                        isMainPage = true; //메인페이지 출력
                        break;


                }
                
    
            }
        }

    }


    
}
