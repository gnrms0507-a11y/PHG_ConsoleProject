using EnumManager;
using System;
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
         Battle=1, Equipment, Inventory, Shop, Back
    }
    public enum MonsterList    //던전(몬스터) 리스트
    {
         Goblin=1, Ghoul, SnowMan, Golem, Dragon, DragonSlayer, Back
    }
    enum Difficulty     //난이도 정렬
    {
         VeryEasy=1, Easy, Noraml, Hard, Extreme, Nightmare, Back
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
            Inventory inventory = new Inventory();  //Inventory 생성
            Store store = new Store();  //상점 생성
            Player player = null;  //플레이어 생성 (껍데기)

          


            MainMenu mainmenu = default;
            Menu menu = default;
            MonsterList monsterList = default;
           

            bool? isBattle = null; //몬스터배틀창에서 전투 / 취소 여부선택 기본값 true

            Stack<Action> printer = new Stack<Action>();    //Stack 자료구조 (Action) 생성 , 반환값없음
            Action print = null;    //Action 변수생성 

            bool isGameOff = false;

            //페이지별로 메뉴이동여부 확인 - 메인페이지만 처음에 true
            bool isMainPage = true;
            bool isMenuPage = false;
            bool isPage = false;    //유효페이지 확인여부


            string resultMainPage;  //메인페이지 메뉴 선택결과
            string resultMenuPage = null;  //메뉴페이지 메뉴 선택결과 

            MonsterList resultBattlePage = default;    //배틀페이지 몬스터 선택결과

            #endregion


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
                            gm.Loading("Loading", 2000);    //로딩메서드 실행
                            player = Player.CreatePlayer();   //플레이어 생성 후 할당
                            isMenuPage = true;  //메뉴페이지 넘어감
                        }
                        else if (resultMainPage == "GameStart" && Player.isPlayerReady == true)
                        {
                            gm.Loading($"{player.playerName} 접속중", 1500);    //로딩메서드 실행 캐릭터 생성이 되었다면 캐릭터명 출력
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
                    case "Battle":

                        if (isBattle != false)     //몬스터선택여부에서 취소를 눌렀을경우에는 로딩이존재하지않음
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

                        battlePage.SelectMonster(resultBattlePage, out isBattle);      //배틀페이지의 SelectMonster 메서드에 선택한 mosterList값을 string타입으로 전달


                        if (resultBattlePage != MonsterList.Back && isBattle == true)     //전투창에서 Back 이 아니고 배틀시작 전투실행여부에서 예를 눌렀을경우 시작
                        {
                            battlePage.Battle(player, Monster.CreateMonster(resultBattlePage));    //입력받은 MonsterList형 resultBattlePage를 CreateMonster에 넘김,배틀시작

                            //드래곤슬레이어 2페이즈 시작
                            if (resultBattlePage == MonsterList.DragonSlayer && isBattle == true)
                            {
                                Monster.isDragonSlayer2Page = true;
                                battlePage.Battle(player, Monster.DragonSlayer2Page());
                            }
                        }

                        else if (resultBattlePage != MonsterList.Back && isBattle == false)     //전투창에서 Back 이 아니고 배틀시작 전투실행여부에서 예를 눌렀을경우 시작
                        {
                            isMainPage = false; //메인페이지는 생략하고 출력
                            isMenuPage = false; //메뉴페이지는 생략하고 출력
                        }
                        else if (resultBattlePage == MonsterList.Back)
                        {
                            gm.Loading($"메뉴 페이지로 이동", 700);
                            isMainPage = false; //메인페이지는 생략하고 출력
                            isMenuPage = true;
                        }

                          
                         break;

                    //장비 페이지
                    case "Equipment":


                        break;



                    //인벤토리 페이지
                    case "Inventory":


                        break;


                    //상점 페이지
                    case "Shop":


                        break;


                    case "Back":
                        gm.Loading($"메인으로 이동합니다", 1000);
                        isMainPage = true; //메인페이지 출력
                        isPage = false; //유효페이지여부 초기화
                        break;


                }
                
    
            }
        }

    }


    
}
