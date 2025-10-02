using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;
using WordMaker;

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

            string resultMainPage;  //메인페이지의 결과를 저장하는 변수
            string resultMenuPage;      //메뉴페이지의 결과를 저장하는 변수


            while (true)    //게임시작. (무한반복시작!)
            {
                resultMainPage = mainPage.SelectMainPage();  //메인페이지 출력 메서드 실행 , 반환값을 resultMainPage(string)변수에 담음

                if(resultMainPage != "Exit")    //사용자가 종료를 선택하지 않았을경우
                {
                    if (resultMainPage== "CreatePlayer")    //플레이어를 만들어야 할 경우
                    {
                        gm.Loading("Loading", 2000);    //로딩메서드 실행
                        player = Player.CreatePlayer();   //플레이어 생성 후 할당
                    }
                    else
                    {
                        gm.Loading($"{player.playerName} 접속중", 1500);    //로딩메서드 실행 캐릭터 생성이 되었다면 캐릭터명 출력
                    }

                    mainPage.PrintMenu(); //메뉴 출력
                    player.PrintPlayerInfo(); //생성된 정보 출력

                    resultMenuPage = mainPage.SelectMenu().ToString();

                    switch (resultMenuPage) 
                    {
                        case "Battle":
                            gm.Loading($"전투 페이지로 이동", 700);
                            battlePage.PrintMonsterList();  //배틀페이지의 몬스터리스트 출력
                            player.PrintPlayerInfo(); //생성된 정보 출력
                            break;

                        case "Equipment":


                            break;

                        case "Inventory":


                            break;

                        case "Shop":


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
