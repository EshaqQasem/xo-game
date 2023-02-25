using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add(GameOrder._3);
            comboBox1.Items.Add(GameOrder._11);
            comboBox1.Items.Add(GameOrder._6);
            comboBox1.SelectedIndex = 0;
            rdbOstart.Checked = true;

            this.panContainerLastSize = new Size(this.Width, this.Height);


            this.InitializeGame();

        }
        Button[] Squares;
        TicTacGame ticTac;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void InitializeGame()
        {
            int tmpOrder = (int)(GameOrder)comboBox1.SelectedItem;

            ticTac = new TicTacGame((GameOrder)tmpOrder);
            if (ticTac.gameOrder != GameOrder._3)
            {
                this.Height += 27*(int)ticTac.gameOrder;
                this.Width += 27 * (int)ticTac.gameOrder;
            }
            //ticTac.gameOrder = tmpOrder; 
            Squares = new Button[tmpOrder * tmpOrder];

            whoPlay = rdbXStart.Checked ? PlayerType.X : PlayerType.O;
            for (int i = 0; i < tmpOrder * tmpOrder; i++)
            {
                Squares[i] = new Button();
                Squares[i].Size = new System.Drawing.Size(panContainer.Width / tmpOrder, panContainer.Height / tmpOrder);
                Squares[i].Font = new System.Drawing.Font("Comic Sans MS", 20);
                Squares[i].ForeColor = Color.White;
                Squares[i].FlatStyle = FlatStyle.Flat;
                Squares[i].Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right;
                Squares[i].FlatAppearance.BorderColor = this.BackColor;
                Squares[i].Location = new Point(i % tmpOrder * Squares[i].Width, i / tmpOrder * Squares[i].Height);
                Squares[i].BackColor = Color.DarkSlateGray;// Color.CadetBlue;
                // Squares[i].Tag = (int)ticTac.playersPositions[i / tmpOrder, i % tmpOrder];
                Squares[i].Click += Form1_Click;
                Squares[i].Tag = i;
                this.panContainer.Controls.Add(Squares[i]);

            }
            panContainer.Enabled = true;
        }
        int xWins = 0, OWins = 0, equals = 0;

        PlayerType whoPlay = PlayerType.X;
        void Form1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            if (whoPlay == PlayerType.X)
            {
                btn.Text = "X";
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.Text = "O";
                btn.ForeColor = Color.Silver;
            }

            whoWin win = ticTac.playOn((int)btn.Tag / (int)ticTac.gameOrder, (int)btn.Tag % (int)ticTac.gameOrder, whoPlay);
            if (win == whoWin.X)
            {
                xWins++;
                lblXWins.Text = xWins.ToString();

                colorWinSqaure();
                MessageBox.Show("X Win");
            }
            else if (win == whoWin.O)
            {
                OWins++;
                lblOWins.Text = OWins.ToString();
                colorWinSqaure();
                MessageBox.Show("O Win");
            }
            else if (win == whoWin.equal)
            {
                equals++;
                lblEquals.Text = equals.ToString();
                MessageBox.Show("Nobody Win");
            }
            if (xWins > OWins)
            {
                lblXWins.ForeColor = Color.LightGreen;
                lblOWins.ForeColor = Color.Red;
            }
            else if (OWins > xWins)
            {
                lblOWins.ForeColor = Color.LightGreen;
                lblXWins.ForeColor = Color.Red;

            }
            else
                lblOWins.ForeColor = lblXWins.ForeColor = Color.Blue;
            whoPlay = whoPlay == PlayerType.X ? PlayerType.O : PlayerType.X;
        }

        private void colorWinSqaure()
        {
            foreach (Point p in ticTac.rowsAndColsWin)
            {
                if (p.X == (int)ticTac.gameOrder)
                {
                    for (int i = 0; i < (int)ticTac.gameOrder; i++)
                    {
                        Squares[i * (int)ticTac.gameOrder + i].BackColor = Color.Green;

                    }
                }
                else if (p.Y == (int)ticTac.gameOrder)
                {
                    for (int i = 1; i < (int)ticTac.gameOrder + 1; i++)
                    {
                        Squares[i * (int)ticTac.gameOrder - i].BackColor = Color.Green;

                    }
                }

                else if (p.X == -1)
                {
                    for (int i = 0; i < (int)ticTac.gameOrder; i++)
                    {
                        Squares[p.Y * (int)ticTac.gameOrder + i].BackColor = Color.Green;

                    }
                }
                else
                {
                    for (int i = 0; i < (int)ticTac.gameOrder; i++)
                    {
                        Squares[p.X + (int)ticTac.gameOrder * i].BackColor = Color.Green;

                    }
                }
            }
            this.panContainer.Enabled = false;        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < (int)ticTac.gameOrder * (int)ticTac.gameOrder; i++)
            {
                Squares[i].Dispose();
            }

            InitializeGame();
        }


        private void panContainer_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("ll");





        }

        Size panContainerLastSize;
        int hd;
        int wd;
        private void Form1_ResizeBegin(object sender, EventArgs e)
        {

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            /* hd = this.Height - panContainerLastSize.Height;
             wd = this.Width - this.panContainerLastSize.Width;
             this.Width+=hd;
             this.Height+=wd;
             this.panContainerLastSize = new Size(this.Width, this.Height);
             * */
        }

    }
}
