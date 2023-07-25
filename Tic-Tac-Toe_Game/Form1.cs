using System;
using Tic_Tac_Toe_Game.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1;
        enum enPlayer
        {
            Player1,Player2
        }
        enum enWinner
        {
            Player1,Player2,Draw, GameInProgress
        }
        struct stGameStatus 
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }
        public void EndGame()
        {
            lblWinner.Text = "Game Over";
            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player1";
                    break;
                case enWinner.Player2:
                    lblWinner.Text = "Player2";
                    break;
                default:
                    lblWinner.Text = "Draw";
                    break;
            }
            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public bool ChecKValues(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString()== btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;
                if (btn2.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.GameOver = true;
                    GameStatus.Winner = enWinner.Player2;
                    EndGame();
                    return true;
                }
            }
            else
            {
               // MessageBox.Show("")
            }
            GameStatus.GameOver = false;
            return false;
        }
        public void CheckWinner()
        {
            if (ChecKValues(button1,button2,button3))
                return;
            if (ChecKValues(button6, button5, button4))
                return;
            if (ChecKValues(button9, button8, button7))
                return;
            if (ChecKValues(button1, button6, button9))
                return;
            if (ChecKValues(button2, button5, button8))
                return;
            if (ChecKValues(button3, button4, button7))
                return;
            if (ChecKValues(button7, button5, button1))
                return;
            if (ChecKValues(button3, button5, button9))
                return;
        }
        public void ChangeButton(Button btn)
        {
            if (btn.Tag.ToString() == "?")
            {
                switch (PlayerTurn) {
                    case enPlayer.Player1:
                        btn.Image = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        lblTurn.Text = "Player2";
                        btn.Tag = "X";
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;
                    case enPlayer.Player2:
                        btn.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        lblTurn.Text = "Player1";
                        btn.Tag = "O";
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;
                }

            }
            else
            {
                MessageBox.Show("Wrong Choice","Wrong",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if(GameStatus.PlayCount == 9)
            {
                GameStatus.Winner = enWinner.Draw;
                GameStatus.GameOver = true;
                EndGame();
            }

        }
        public void RestButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
        }
        public void RestartGame()
        {
            RestButton(button1);
            RestButton(button2);
            RestButton(button3);
            RestButton(button4);
            RestButton(button5);
            RestButton(button6);
            RestButton(button7);
            RestButton(button8);
            RestButton(button9);

            PlayerTurn = enPlayer.Player1;
            lblTurn.Text = "Player1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            lblWinner.Text = "In Progress";
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(148, 137, 137);

            Pen whitePen = new Pen(White);
            whitePen.Width = 15;

            //Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //draw horizental lines
            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);
            //draw vertical lines
            e.Graphics.DrawLine(whitePen, 610,140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);

        }
        private void button_Click(object sender, EventArgs e)
        {
            ChangeButton((Button)sender);
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
