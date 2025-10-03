using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;
using WordMaker;
using EnumManager;

namespace EnumManager       //게임에 사용되는 열거형을 관리하는 네임스페이스 
{
    public enum MainMenu    //시작페이지의 열거형
    {
        Start, GameStart, Exit, End
    }

    public enum Menu    //선택페이지의 열거형
    {
        Start, Battle, Equipment, Inventory, Shop, Back, End
    }
    public enum MonsterList    //던전(몬스터) 리스트
    {
        Start, Goblin, Ghoul, SnowMan, Golem, Dragon, DragonSlayer, Back, End
    }
    enum Difficulty     //난이도 정렬
    {
        Start, VeryEasy, Easy, Noraml, Hard, Extreme, Nightmare, Back, End
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
            GameManager gm = new GameManager();   //GM생성  - 게임 세팅 및 시작 생성자에서 메서드실행
            MainPage mainPage = new MainPage(); //MainPage 생성
            BattlePage battlePage = new BattlePage();   //BattlePage
            Inventory inventory = new Inventory();  //Inventory 생성
            Store store = new Store();  //상점 생성
            Player player = null;  //플레이어 생성 (껍데기)

            string resultMainPage;  //메인페이지 메뉴 선택결과
            string resultMenuPage;  //메뉴페이지 메뉴 선택결과 
            string resultBattlePage;    //배틀페이지 몬스터 선택결과


            MainMenu mainmenu = default;
            Menu menu = default;
            MonsterList monsterList = default;


            while (true)    //게임시작. (무한반복시작!)
            {
                mainPage.SelectMainPage();
                resultMainPage = gm.MoveMenu(mainmenu).ToString();
                if (resultMainPage != "Exit")    //사용자가 종료를 선택하지 않았을경우
                {
                    if (resultMainPage == "GameStart" && Player.isPlayerReady == false)    //플레이어를 만들어야 할 경우
                    {
                        gm.Loading("Loading", 2000);    //로딩메서드 실행
                        player = Player.CreatePlayer();   //플레이어 생성 후 할당
                    }
                    else
                    {
                        gm.Loading($"{player.playerName} 접속중", 1500);    //로딩메서드 실행 캐릭터 생성이 되었다면 캐릭터명 출력
                    }

                    mainPage.PrintMenu(); //메뉴 출력
                    player.PrintPlayerInfo(); //생성된 플레이어정보 출력

                    resultMenuPage = gm.MoveMenu(menu).ToString();
                    switch (resultMenuPage)
                    {
                        case "Battle":
                            gm.Loading($"전투 페이지로 이동", 700);
                            battlePage.PrintMonsterList();  //배틀페이지의 몬스터리스트 출력
                            player.PrintPlayerInfo(); //생성된 플레이어정보 출력

                            resultBattlePage = gm.MoveMenu(monsterList).ToString();
                            battlePage.SelectMonster(resultBattlePage);      //배틀페이지의 SelectMonster 메서드에 선택한 mosterList값을 string타입으로 전달

                            if (resultBattlePage != "Back")     //전투창에서 Back 이 아닐경우 배틀시작
                            {
                               
                            }

                            else  //Back 일경우 메뉴선택창으로 이동
                            {
                                gm.Loading($"메뉴선택창으로 이동합니다", 1000);
                                mainPage.PrintMenu(); //메뉴 출력
                                player.PrintPlayerInfo(); //생성된 플레이어정보 출력
                            }










                                break;











                        case "Equipment":       //장비 창


                            break;













                        case "Inventory":       //인벤토리 창


                            break;













                        case "Shop":        //상점


                            break;









                        default:
                            gm.Loading($"메인으로 이동합니다", 1000);
                            break;
                    }


                }
                else
                {
                    break;  //게임종료
                }

                
            }
        }
    }
}
