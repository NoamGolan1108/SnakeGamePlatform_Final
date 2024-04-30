using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using WMPLib;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Drawing2D;

namespace SnakeGamePlatform
{
    
    public class GameEvents:IGameEvents
    {
        //הגדרת משתנים כללים לכל התוכנה 
        GameObject [] snake;
        TextLabel lblScore;
        TextLabel scoreLabel;
        GameObject food;
        GameObject borderLeft;
        GameObject borderRight;
        GameObject borderUp;
        GameObject borderDown;
        TextLabel highestScore;
        Position ScorePosition = new Position(64, 80);
        Random rnd = new Random();
        int scoreCounter = 0;
        int timeInterval;


      
        public void GameInit(Board board)
        {
            //הגדרה של הלוח, גודל ורזולוציה
            Board.resolutionFactor = 1;
            board.XSize = 600;
            board.YSize = 800;

            //תוויות שמוצגות, טקסט פתיחה ולוח ניקוד
            Position labelPosition = new Position(24, 280);
            lblScore = new TextLabel("Welcome to Galina Snake!", labelPosition);
            lblScore.SetFont("Ariel", 14);
            board.AddLabel(lblScore);
            scoreLabel = new TextLabel($"{scoreCounter}", ScorePosition);
            scoreLabel.SetFont("Ariel", 16);
            board.AddLabel(scoreLabel);


            //יצירה והוספה ללוח של עצמים במשחק- אוכל, נחש, גבולות
            Position foodPosition = new Position(200, 100);
            Position snakeFirstPosition = new Position(400, 50);
            Position snakeSecondPosition = new Position(400, 50);
            food = new GameObject(foodPosition, 25, 25);
            food.SetImage(Properties.Resources.strawberry);
            food.direction = GameObject.Direction.RIGHT;
            board.AddGameObject(food);
            snake = new GameObject[1];


            Position borderLeftPos = new Position(20, 30);
            borderLeft = new GameObject(borderLeftPos, 30, 520);
            borderLeft.SetBackgroundColor(Color.DarkGreen);
            board.AddGameObject(borderLeft);

            Position borderRightPos = new Position(20, 720);
            borderRight = new GameObject(borderRightPos, 30, 520);
            borderRight.SetBackgroundColor(Color.DarkGreen);
            board.AddGameObject(borderRight);

            Position borderUpPos = new Position(20, 30);
            borderUp = new GameObject(borderUpPos, 700, 30);
            borderUp.SetBackgroundColor(Color.DarkGreen);
            board.AddGameObject(borderUp);

            Position borderDownPos = new Position(530, 30);
            borderDown = new GameObject(borderDownPos, 720, 30);
            borderDown.SetBackgroundColor(Color.DarkGreen);
            board.AddGameObject(borderDown);

            snakeFirstPosition = new Position(300, 400);
            snake[0] = new GameObject(snakeFirstPosition, 20, 20);
            snake[0].SetBackgroundColor(Color.Aqua);
            board.AddGameObject(snake[0]);

            //הפעלת מוזיקת רקע
            board.PlayBackgroundMusic(@"\Images\galinaPhone.mp3");
           
            //הפעלת המשחק
            board.StartTimer(100);
        }
        
        //בחלק הבא עשינו כל מיני פעולות שצריכים לקרות במהלך המשחק
        
         //פעולה של תזוזת הנחש
        public void SnakeMove(Board board)
        {
            for (int i = snake.Length - 1; i >= 1; i--)
            {
                snake[i].SetPosition(snake[i - 1].GetPosition());
            }

            Position snakePosition = snake[0].GetPosition();
            if (snake[0].direction == GameObject.Direction.RIGHT)
                snakePosition.Y = snakePosition.Y + 20;
            if (snake[0].direction == GameObject.Direction.LEFT)
                snakePosition.Y = snakePosition.Y - 20;
            if (snake[0].direction == GameObject.Direction.UP)
                snakePosition.X = snakePosition.X - 20;
            if (snake[0].direction == GameObject.Direction.DOWN)
                snakePosition.X = snakePosition.X + 20;
            snake[0].SetPosition(snakePosition);

        





        }
        //פעולה שהנחש גודל אחרי שהוא אוכל תות, מפורט תיאור מדוייק בפעולה איך עשינו את זה
        public void SnakeGetBigger(Board board)
        {
            //שומרים את המיקום של התא האחרון
            Position positionLast = snake[snake.Length - 1].GetPosition();
            //קואים ל MoveSnake
            SnakeMove(board);

            //יוצרים מערך חדש גדול יותר ומעתיקים את תוכן המערך למערך החדש, 
            GameObject [] copy = new GameObject[snake.Length + 1];
            for (int i = 0; i < snake.Length; i++)
            {
                copy[i] = snake[i];
            }


            //יוצרים אובייקט חדש עם המיקום שנשמר
            copy[copy.Length - 1] = new GameObject(positionLast, 20, 20);
            copy[copy.Length - 1].SetBackgroundColor(Color.Aqua);
            board.AddGameObject(copy[copy.Length - 1]);

            snake = copy;

        }


        //פעולה בוליאנית שבודקת האם הנחש התנגש בעצמו, ישמש אחר כך לפסילה
        public bool SnakeHitSnake(Board board, GameObject[] snake)
        {
            for (int i = 1; i < snake.Length; i++)
            {
                if (snake[0].IntersectWith(snake[i]))
                {
                    return true;
                }
            }
            return false;
        }

        //איפוס מערך הנחש, גם שכשהמשתמש ייפסל והמשחק יתחיל מחדש
        public GameObject[] ResetSnake(GameObject[] snake)
        { 
        
            GameObject[] newSnake = new GameObject[2];
            newSnake[0] = snake[0];
            newSnake[1] = snake[1];
            return newSnake;
        }
        
        public void GameClock(Board board)
        {
            SnakeMove(board);


           

            // מה שקורה כשהנחש אוכל תות- התות משתגר במקום חדש בגבולות הלוח + הנחש גדל

            if (snake[0].IntersectWith(food) == true)
            {
                int foodPositionX = rnd.Next(45, 505);
                int foodPositionY = rnd.Next(55, 695);
                Position foodPosition = new Position(foodPositionX, foodPositionY);
                food.SetPosition(foodPosition);
                SnakeGetBigger(board);
                board.RemoveLabel(scoreLabel);
                scoreCounter++;
                scoreLabel = new TextLabel($"{scoreCounter}", ScorePosition);
                scoreLabel.SetFont("Ariel", 16);
                board.AddLabel(scoreLabel);

            }
            //מה שקורה כשהנחש נתקל באחד הגבולות או בעצמו- סיום המשחק והתחלה מחדש
            if (SnakeHitSnake(board, snake) || snake[0].IntersectWith(borderUp) || snake[0].IntersectWith(borderLeft) || snake[0].IntersectWith(borderDown) || snake[0].IntersectWith(borderRight))
            {
                for (int i = 0; i < snake.Length; i++)
                {
                    board.RemoveGameObject(snake[i]);
                }
                board.RemoveGameObject(food);
                food.SetImage(Properties.Resources.food);
                board.RemoveLabel(scoreLabel);
                scoreCounter = 0;
                timeInterval = 200;
                board.StartTimer(timeInterval);
                GameInit(board);

            }





        }

         //המקלדת ואיך המשתמש שולט בנחש
        public void KeyDown(Board board, char key)
        {
           
            if (key == (char)ConsoleKey.LeftArrow)
                snake[0].direction = GameObject.Direction.LEFT;
            if (key == (char)ConsoleKey.RightArrow)
                snake[0].direction = GameObject.Direction.RIGHT;
            if (key == (char)ConsoleKey.UpArrow)
                snake[0].direction = GameObject.Direction.UP;
            if (key == (char)ConsoleKey.DownArrow)
                snake[0].direction = GameObject.Direction.DOWN;
        }
    }
}
