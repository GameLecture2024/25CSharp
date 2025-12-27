// (1) 폴더에서 파일 경로 가져오기
using Newtonsoft.Json;
using 달리기프로젝트;


string folderpath = Environment.CurrentDirectory;
string filename = "jelly.json";
string fullpath = Path.Combine(folderpath, filename);

string folderpath2 = Environment.CurrentDirectory;
string filename2 = "obs.json";
string fullpath2 = Path.Combine(folderpath2, filename2);


// (2) 파일을 읽어오기
string text = File.ReadAllText(fullpath);
string text2 = File.ReadAllText(fullpath2);

var jellys = JsonConvert.DeserializeObject<List<Jelly>>(text);
var obstacles = JsonConvert.DeserializeObject<List<Obstacle>>(text2);

Player player = new Player();
int score_x = 50;
int score_y = 2;
int currentScore = 0;

// 1번을 입력하면 게임을 시작합니다.
// 그 이외의 버튼을 입력하면 게임을 종료합니다.

// 게임 타이틀 
int cursor = 5;

Console.WriteLine("Press Any Key to Play!");

while(true)
{
    Console.CursorVisible = false;

    // 플레이어의 입력   화살표 ↑(-)  화살표 ↓(+)
    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.DownArrow)
        {
            cursor++;

            if(cursor >= 7)
            {
                cursor = 5;
            }
        }
        else if (key == ConsoleKey.UpArrow)
        {
            cursor--;

            if(cursor <= 4)
            {
                cursor = 6;
            }
        }
        else if(key == ConsoleKey.Enter) 
        {
            // cursor 값이 5일때,  6일때
            if(cursor == 5)
            {
                currentScore = GamePlay(jellys, obstacles, player, score_x, score_y, currentScore);
            }
            else if(cursor == 6)
            {
                break;
            }
        }

        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(" _ _   ____                        ____            _           _     _ _ \r\n( | ) |  _ \\ _   _ _ __           |  _ \\ _ __ ___ (_) ___  ___| |_  ( | )\r\n V V  | |_) | | | | '_ \\   _____  | |_) | '__/ _ \\| |/ _ \\/ __| __|  V V \r\n      |  _ <| |_| | | | | |_____| |  __/| | | (_) | |  __/ (__| |_       \r\n      |_| \\_\\\\__,_|_| |_|         |_|   |_|  \\___// |\\___|\\___|\\__|      \r\n                                                |__/                     ");

        Console.SetCursorPosition(30, 5);
        Console.WriteLine("게임 시작");
        Console.SetCursorPosition(30, 6);
        Console.WriteLine("게임 종료");
        Console.SetCursorPosition(28, cursor);
        Console.WriteLine("▶");

        Console.SetCursorPosition(0, 10);
        Console.WriteLine(" _ _   _____           _   _     _              _ _ \r\n( | ) | ____|_ __   __| | | |   (_)_ __   ___  ( | )\r\n V V  |  _| | '_ \\ / _` | | |   | | '_ \\ / _ \\  V V \r\n      | |___| | | | (_| | | |___| | | | |  __/      \r\n      |_____|_| |_|\\__,_| |_____|_|_| |_|\\___|      ");

        Console.WriteLine(Environment.CurrentDirectory);
    }
 


    //Console.WriteLine("게임을 시작하려면 1을 입력해주세요, 이외의 값을 입력하면 게임이 종료됩니다.");
    //int input = int.Parse(Console.ReadLine());
    //if (input == 1)
    //{
    //    currentScore = GamePlay(jellys, obstacles, player, score_x, score_y, currentScore);
    //}
    //else
    //{
    //    break;
    //}
}

// 게임 플레이
static int GamePlay(List<Jelly>? jellys, List<Obstacle>? obstacles, Player player, int score_x, int score_y, int currentScore)
{
    // 플레이어는 살아있습니다.
    // 초기화
    currentScore = 0;
    player.Start();
    foreach(var jelly in jellys)
    {
        jelly.Start();
    }
    foreach(var obs in obstacles)
    {
        obs.Start();
    }


    using (var renderer = new ConsoleRenderer(80, 50)) // IDiposable 패턴
    {
        while (true)
        {
            // 1. 플레이어의 입력
            player.Update();


            // 2. 플레이어 이외의 오브젝트의 기능을 구현

            jellys[0].y = 5;
            jellys[1].y = 6;
            jellys[2].y = 7;
            jellys[0].Update();
            jellys[1].Update();
            jellys[2].Update();

            foreach (var obs in obstacles)
            {
                obs.Update();
            }

            jellys[0].GetScore(player, ref currentScore);
            jellys[1].GetScore(player, ref currentScore);
            jellys[2].GetScore(player, ref currentScore);

            foreach (var obs in obstacles)
            {
                obs.Hit(player, ref currentScore);
            }


            renderer.Clear();    // 화면을 지워라
                                 // 3. 그림을 그려줘라  
            player.Draw(renderer);
            renderer.Print(score_x, score_y, $"score : {currentScore}");


            jellys[0].Draw(renderer);
            jellys[1].Draw(renderer);
            jellys[2].Draw(renderer);

            foreach (var obs in obstacles)
            {
                obs.Draw(renderer);
            }

            // 4. FLiping 해결 -> Screen Double buffer 더블 버퍼 
            renderer.Flipping();

            if (player.isDeath)
            {
                break;
            }


            Thread.Sleep(33);  // frame per second     60FPS 1초  0.016

        }
    }

    return currentScore;
}

