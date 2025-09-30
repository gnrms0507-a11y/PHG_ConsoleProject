using System;                      
using System.Collections.Generic;  
using System.Diagnostics;          

namespace Day11
{
    internal class Program
    {
        // 플레이어 구조체 정의
        struct Player
        {
            public int x;              // 플레이어 x 좌표
            public int y;              // 플레이어 y 좌표
            public string playerChar;  // 플레이어 표시 문자
        }

        // 총알 구조체 정의
        struct Bullet
        {
            public int x;              // 총알 x 좌표
            public int y;              // 총알 y 좌표
            public bool active;        // 총알 활성 상태
            public string bulletChar;  // 총알 표시 문자
        }

        static Stopwatch watch = new Stopwatch(); // 프레임 제어용 스톱워치
        static Player player;                     // 플레이어 변수
        static List<Bullet> bullets = new List<Bullet>(); // 총알 리스트

        static void Main(string[] args)
        {
            Console.SetWindowSize(50, 25); // 콘솔 창 크기 50x25로 설정
            Console.CursorVisible = false; // 커서 숨김

            Start();  // 초기화 함수 호출
            while (true)
            {
                Update(); // 무한 반복으로 게임 루프 실행
            }
        }

        // 초기화 함수 (Unity의 Start와 유사)
        static void Start()
        {
            watch.Start(); // 스톱워치 시작 (프레임 제어용)

            player.x = 5;               // 초기 플레이어 x 좌표
            player.y = 10;              // 초기 플레이어 y 좌표
            player.playerChar = "▲";    // 플레이어 표시 문자 설정
        }

        // 반복 실행 함수 (Unity의 Update와 유사)
        static void Update()
        {
            if (watch.ElapsedMilliseconds >= 100) // 0.1초마다 실행
            {
                watch.Restart(); // 시간 초기화

                HandleInput();   // 키 입력 처리
                MoveBullets();   // 총알 이동 처리
                Render();        // 화면 렌더링
            }
        }

        // 키 입력 처리
        static void HandleInput()
        {
            if (Console.KeyAvailable) // 키가 눌렸는지 확인
            {
                var key = Console.ReadKey(true); // 키 입력 읽기, true = 콘솔에 표시 안함

                if (key.Key == ConsoleKey.W && player.y > 0) player.y--; // 위 이동
                if (key.Key == ConsoleKey.S && player.y < Console.WindowHeight - 1) player.y++; // 아래 이동
                if (key.Key == ConsoleKey.A && player.x > 0) player.x--; // 왼쪽 이동
                if (key.Key == ConsoleKey.D && player.x < Console.WindowWidth - 1) player.x++; // 오른쪽 이동

                if (key.Key == ConsoleKey.Spacebar) // 스페이스바 입력 시 총알 발사
                {
                    Bullet b = new Bullet(); // 총알 구조체 생성
                    b.x = player.x;          // 총알 초기 x 좌표 = 플레이어 x
                    b.y = player.y - 1;      // 총알 초기 y 좌표 = 플레이어 위쪽
                    b.active = true;         // 총알 활성화
                    b.bulletChar = "•";      // 총알 표시 문자
                    bullets.Add(b);          // 총알 리스트에 추가
                }
            }
        }

        // 총알 이동 처리
        static void MoveBullets()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].active)  // 총알이 활성 상태일 때
                {
                    Bullet b = bullets[i]; // 구조체 복사
                    b.y--;                 // 위로 이동

                    if (b.y < 0)           // 화면 밖으로 나가면
                    {
                        b.active = false;  // 비활성화
                    }

                    bullets[i] = b;        // 변경된 총알 좌표 적용
                }
            }

            // 비활성화된 총알 제거
            bullets.RemoveAll(b => b.active == false);
        }

        // 화면 렌더링
        static void Render()
        {
            ClearScreenByFilling(); // 화면 지우기

            // 플레이어 출력
            Console.SetCursorPosition(player.x, player.y); // 좌표 이동
            Console.Write(player.playerChar);              // 문자 출력

            // 총알 출력
            foreach (var b in bullets)
            {
                Console.SetCursorPosition(b.x, b.y); // 총알 좌표로 이동
                Console.Write(b.bulletChar);         // 총알 문자 출력
            }
        }

        // 화면 지우기 (빈 문자로 채우기)
        static void ClearScreenByFilling()
        {
            for (int i = 0; i < Console.WindowHeight; i++) // 세로 한 줄씩
            {
                Console.SetCursorPosition(0, i);                       // 줄 시작 위치
                Console.Write(new string(' ', Console.WindowWidth));   // 공백으로 채우기
            }
            Console.SetCursorPosition(0, 0); // 커서 원점으로 이동
        }
    }
}