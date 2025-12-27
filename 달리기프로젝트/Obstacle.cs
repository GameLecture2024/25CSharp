using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 달리기프로젝트
{
    public record class Obstacle
    {
        [JsonProperty("Speed")] public int Speed;
        [JsonProperty("Name")] public string Name;
        [JsonProperty("posY ")] public int y;
        int moveTick;  // 얼마 지나야 움직일 수 있나요?    Speed와 같아지면 움직여라 // moveTick1씩더하다가 ++
        int x; // x좌표

        public void Start()
        {
            moveTick = 0;
            x = 50;
        }

        public void Update()
        {
            moveTick++;   // moveTick 1씩 증가시켜주세요

            if (Speed <= moveTick) // Speed와 moveTick 같아지면 움직여라 
            {
                x--;
                if (x <= 0)
                {
                    x = 50;
                }
                moveTick = 0;  // 한번 사이클 돌았으면 0으로 초기화.
            }
        }

        public void Draw(ConsoleRenderer renderer)
        {
            renderer.Print(x, y, Name);
        }

        public void Hit(Player player, ref int currentscore)
        {
            if (x == player.x && y == player.y)     // 플레이어의 x,y좌표 나의 w,y좌표 같을 때 -> 점수를 획득한다.
            {
                //currentscore -= Score; // 점수를 획득하고 나서
                // 플레이어가 사망했습니다.
                player.Die();
            }
        }
    };
}
