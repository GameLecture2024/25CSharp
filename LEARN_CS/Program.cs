using LEARN_CS;
using Newtonsoft.Json;

// (1) 폴더에서 파일 경로 가져오기
string folderpath = "C:\\Users\\Administrator\\Desktop\\25CSharp\\LEARN_CS";
string filename = "Item.json";

string fullpath = Path.Combine(folderpath, filename);

Console.WriteLine(fullpath);

// (2) 파일을 읽어오기
string text = File.ReadAllText(fullpath);
Console.WriteLine(text);

// (3) 파일을 데이터로 해석하기 => class 만들기
// 10개. 1000원. 어떤 아이템을 사겠다.
// 시작 아이템을 랜덤으로 주는 기능 구현.

// json파일을 클래스로 변환해줘. Nuget- Newtonsoft 다운받은 것
var ItemDB = JsonConvert.DeserializeObject<List<Item>>(text);

Item select = ItemDB[1];

Console.WriteLine($"LABEL : {select.LABEL}," +
    $" Name : {select.NAME}, " +
    $" Stat : {select.STAT}, " +
    $" Value : {select.VALUE}"); 

