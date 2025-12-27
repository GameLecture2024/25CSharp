using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace 달리기프로젝트
{
    public class Player
    {
        string shape = "p";
        public int x = 5;
        public int y = 7;
        public bool isDeath;

        public void Start()
        {
            isDeath = false;
            y = 7;
        }

        public void Update()
        {
            // 땅의 좌표와 다르면(점프 중) 중력을 받으세요.
            // y의 좌표를 1씩 더하세요.
            // 지금 y의 좌표가 땅 위에 있나요? GroundCheck
            if(GroundCheck() == false)
            {
                y = y + 1;
            }
            

            // space버튼을 입력했을 때
            if (Console.KeyAvailable && GroundCheck())
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Spacebar)
                {
                    y = y - 4;
                }
            }

            // jump를 하겠다.
        }

        public bool GroundCheck()
        {
            return y >= 7;
        }

        public void Draw(ConsoleRenderer renderer)
        {
            renderer.Print(x, y, shape);
        }

        public void Die()
        {
            isDeath = true;
        }

    }
}
