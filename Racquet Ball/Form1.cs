using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Racquet_Ball
{
    public partial class racquetBall : Form
    {
        Rectangle player1 = new Rectangle(220, 100, 10, 60);
        Rectangle player2 = new Rectangle(240, 230, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);


        int playerTurn = 1;
        
        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 4;
        int ballXSpeed = -7;
        int ballYSpeed = 7;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool aDown = false;
        bool dDown = false;
        bool leftDown = false;
        bool rightDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen whitePen = new Pen(Color.White, 2);

        public racquetBall()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball 
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            //move player 1 
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (aDown == true && player1.X < 600)
            {
                player1.X -= playerSpeed;
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }

            //move player 2
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            if (leftDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }

            if (rightDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
            }


            //check if ball hit top or bottom wall and change direction if it does 
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }

            //check if ball struck right side wall
            if (ball.X > 600)
            {
                ballXSpeed *= -1;
            }

            //check if ball hits either player. If it does change the direction 
            //and place the ball in front of the player hit 
            if (ballXSpeed < 0)
            {
                if (player1.IntersectsWith(ball) && playerTurn == 1)
                {
                    playerTurn = 2;
                    ballXSpeed *= -1;
                    ball.X = player1.X + ball.Width;
                }
                else if (player2.IntersectsWith(ball) && playerTurn == 2)
                {
                    playerTurn = 1;
                    ballXSpeed *= -1;
                    ball.X = player2.X + ball.Width;
                }
            }
           
            //check if a player missed the ball and if true add 1 to score of other player  
            if (ball.X < 0 && playerTurn == 1)
            {
                player1Score++;
                player1ScoreLabel.Text = $"{player1Score}";

                ball.X = 395;
                ball.Y = 175;

                player1.Y = 100;
                player2.Y = 230;
            }
            else if (ball.X < 0 && playerTurn == 2)
            {
                player2Score++;
                player2ScoreLabel.Text = $"{player2Score}";

                ball.X = 295;
                ball.Y = 195;

                player1.Y = 100;
                player2.Y = 230;
            }
            

            // check score and stop game if either player is at 3 
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                ballLabel.Visible = true;
                ballLabel.Text = "Player 1 Wins!!";
                
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                ballLabel.Visible = true;
                ballLabel.Text = "Player 2 Wins!!";
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
            
            if (playerTurn == 1)
            {
                player1.IntersectsWith(ball);
                e.Graphics.DrawRectangle(whitePen, player1);
            }
            else
            {
                player2.IntersectsWith(ball);
                e.Graphics.DrawRectangle(whitePen, player2);
            }
        }
    }
}
