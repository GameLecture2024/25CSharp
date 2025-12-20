// (1) 폴더에서 파일 경로 가져오기
using Newtonsoft.Json;
using 달리기프로젝트;

string folderpath = "C:\\Users\\Administrator\\Desktop\\25CSharp\\달리기프로젝트";
string filename = "jelly.json";

string fullpath = Path.Combine(folderpath, filename);

Console.WriteLine(fullpath);

// (2) 파일을 읽어오기
string text = File.ReadAllText(fullpath);
Console.WriteLine(text);

var jellys = JsonConvert.DeserializeObject<List<Jelly>>(text);

//foreach(var j in jellys)
//{
//    Console.WriteLine(j);
//}

string p_shape = "p";
int p_x = 5;
int p_y = 5;
int score_x = 50;
int score_y = 2;
int currentScore = 0;


using(var renderer = new ConsoleRenderer(80,50)) // IDiposable 패턴
{
    while (true)
    {
        // 1. 플레이어의 입력
       


        // 2. 플레이어 이외의 오브젝트의 기능을 구현

        jellys[0].y = 5;
        jellys[1].y = 6;
        jellys[2].y = 7;
        jellys[0].Update();
        jellys[1].Update();
        jellys[2].Update();

        jellys[0].GetScore(p_x, p_y, ref currentScore);
        jellys[1].GetScore(p_x, p_y, ref currentScore);
        jellys[2].GetScore(p_x, p_y, ref currentScore);

        renderer.Clear();    // 화면을 지워라
        // 3. 그림을 그려줘라  
        renderer.Print(p_x, p_y, p_shape);
        renderer.Print(score_x, score_y, $"score : {currentScore}");


        jellys[0].Draw(renderer);
        jellys[1].Draw(renderer);
        jellys[2].Draw(renderer);

        // 4. FLiping 해결 -> Screen Double buffer 더블 버퍼 
        renderer.Flipping();

        Thread.Sleep(33);  // frame per second     60FPS 1초  0.016

    }
}


