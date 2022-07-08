using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyramidCard
{
    public partial class PyramidForm : StrangeForm
    {
        public Button btnBack;
        public Button btnMenu;
        public Button btnTrans;
        public Label lblScore;
        private PyramidChessboard pyramidChessboard;
        private CardPictureBox tempCard = null;
        private List<CardPictureBox> Pile;
        const int imgWidth = 108;
        const int imgHeight = 150;
        private List<CardPictureBox> cardPictureBoxes;
        public PyramidForm()
        {

            InitializeComponent();
            this.Text = "金字塔纸牌";
            pyramidChessboard = new PyramidChessboard();
            List<Card> deck = PyramidChessboard.getCorrectCards();
            pyramidChessboard.startGame(deck, true, 7, 2);
            this.Size = new Size(1050, 800);
            cardPictureBoxes = new List<CardPictureBox>();
            Pile = new List<CardPictureBox>();
            initBackButtonAndMenuButtonAndScoreLabel();
            makePyramidDomain();
            makePileDomain();
            foreach (CardPictureBox cd in cardPictureBoxes)
            {
                cd.BringToFront();
            }
            this.BackColor = Color.ForestGreen;


        }
        public void initBackButtonAndMenuButtonAndScoreLabel()
        {    //产生返回按钮和菜单按钮和分数显示区域
            btnBack = new Button();
            btnMenu = new Button();
            btnBack.Text = "返回";
            btnBack.Width = 70;
            btnBack.Height = 50;
            btnBack.Click += new EventHandler(back_clk);
            btnBack.Location = new Point(0, 0);
            btnMenu.Text = "菜单";
            btnMenu.Width = 70;
            btnMenu.Height = 50;
            btnMenu.Click += new EventHandler(menu_clk);
            btnMenu.Location = new Point(this.Width - btnMenu.Width - 1, 0);
            lblScore = new Label();
            lblScore.Text = "您的得分" + "0";
            lblScore.Width = 300;
            lblScore.Height = 50;
            lblScore.Font = new Font("微软雅黑", 20);
            lblScore.TextAlign = ContentAlignment.MiddleCenter;
            lblScore.BackColor = Color.Transparent;
            lblScore.Location = new Point((this.Width - lblScore.Width) / 2, lblScore.Height / 2 - 20);
            this.Controls.Add(btnMenu);
            this.Controls.Add(btnBack);
            this.Controls.Add(lblScore);

        }
        public void makeMenuDomain()
        { //点击菜单后，产生设置页面
            Panel panel = new Panel();
            panel.BackColor = Color.YellowGreen;
            panel.Size = new Size(150, this.Height);
            panel.Location = new Point(this.Width - 150, 0);
            panel.BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(panel);
            panel.BringToFront();

            Button btnPrompt = new Button();
            btnPrompt.Size = new Size(70, 50);
            btnPrompt.Location = new Point(panel.Location.X + 40, panel.Location.Y + 25);
            btnPrompt.Text = "提示";
            btnPrompt.Click += new EventHandler((object s, EventArgs e) =>
            {

                List<CardPictureBox> listCards = new List<CardPictureBox>();
                List<(int, int)> tempList = new List<(int, int)>();
                tempList = pyramidChessboard.getTopCards();
                foreach (CardPictureBox cd in cardPictureBoxes)
                {
                    foreach ((int, int) point in tempList)
                    {
                        if (point.Item1 == cd.getRowIndex() && point.Item2 == cd.getColIndex())
                            listCards.Add(cd);
                    }
                }

                for (int i = 0; i < listCards.Count; i++)
                {
                    for (int j = i; j < listCards.Count; j++)
                    {
                        if (listCards[i].getRowIndex() != -1 && listCards[j].getRowIndex() != -1)
                        {
                            if (pyramidChessboard.getCard(listCards[i].getRowIndex(), listCards[i].getColIndex()).getValue() == 13 || pyramidChessboard.getCard(listCards[i].getRowIndex(), listCards[i].getColIndex()).getValue() + pyramidChessboard.getCard(listCards[j].getRowIndex(), listCards[j].getColIndex()).getValue() == 13)
                            {
                                if (tempCard != null)
                                    tempCard.BorderStyle = BorderStyle.None;
                                listCards[i].BorderStyle = BorderStyle.Fixed3D;
                                listCards[j].BorderStyle = BorderStyle.Fixed3D;
                                MessageBox.Show("已将一对牌框出！");

                                return;
                            }
                        }
                        else if (listCards[i].getRowIndex() != -1)
                        {
                            if (pyramidChessboard.getCard(listCards[i].getRowIndex(), listCards[i].getColIndex()).getValue() == 13)
                            {
                                if (tempCard != null)
                                    tempCard.BorderStyle = BorderStyle.None;
                                listCards[i].BorderStyle = BorderStyle.Fixed3D;
                                MessageBox.Show("已经牌框出");

                                return;
                            }
                            else if (pyramidChessboard.getCardInPile(listCards[j].getColIndex()).getValue() == 13)
                            {
                                if (tempCard != null)
                                    tempCard.BorderStyle = BorderStyle.None;
                                listCards[j].BorderStyle = BorderStyle.Fixed3D;
                                MessageBox.Show("已经牌框出");

                                return;
                            }
                            else if (pyramidChessboard.getCard(listCards[i].getRowIndex(), listCards[i].getColIndex()).getValue() + pyramidChessboard.getCardInPile(listCards[j].getColIndex()).getValue() == 13)
                            {
                                if (tempCard != null)
                                    tempCard.BorderStyle = BorderStyle.None;
                                listCards[i].BorderStyle = BorderStyle.Fixed3D;
                                listCards[j].BorderStyle = BorderStyle.Fixed3D;
                                MessageBox.Show("已将一对牌框出！");

                                return;
                            }
                        }
                        else if (listCards[j].getRowIndex() != -1)
                        {
                            if (pyramidChessboard.getCard(listCards[j].getRowIndex(), listCards[j].getColIndex()).getValue() == 13)
                            {
                                if (tempCard != null)
                                    tempCard.BorderStyle = BorderStyle.None;
                                listCards[j].BorderStyle = BorderStyle.Fixed3D;
                                MessageBox.Show("已经牌框出");

                                return;
                            }
                            else if (pyramidChessboard.getCardInPile(listCards[i].getColIndex()).getValue() == 13)
                            {
                                if (tempCard != null)
                                    tempCard.BorderStyle = BorderStyle.None;
                                listCards[i].BorderStyle = BorderStyle.Fixed3D;
                                MessageBox.Show("已经牌框出");

                                return;
                            }
                            else if (pyramidChessboard.getCard(listCards[j].getRowIndex(), listCards[j].getColIndex()).getValue() + pyramidChessboard.getCardInPile(listCards[i].getColIndex()).getValue() == 13)
                            {
                                if (tempCard != null)
                                    tempCard.BorderStyle = BorderStyle.None;
                                listCards[i].BorderStyle = BorderStyle.Fixed3D;
                                listCards[j].BorderStyle = BorderStyle.Fixed3D;
                                MessageBox.Show("已将一对牌框出！");

                                return;
                            }
                        }
                        else
                        {
                            if (pyramidChessboard.getCardInPile(listCards[i].getColIndex()).getValue() + pyramidChessboard.getCardInPile(listCards[j].getColIndex()).getValue() == 13)
                            {
                                if (tempCard != null)
                                    tempCard.BorderStyle = BorderStyle.None;
                                listCards[i].BorderStyle = BorderStyle.Fixed3D;
                                listCards[j].BorderStyle = BorderStyle.Fixed3D;
                                MessageBox.Show("已将一对牌框出！");

                                return;
                            }
                        }
                    }
                }

                if (pyramidChessboard.getShuffleCount() < 2) { MessageBox.Show("请切牌或重新发牌"); }
                else MessageBox.Show("游戏已无法再进行下去了！");
            });
            Controls.Add(btnPrompt);
            btnPrompt.BringToFront();

            Button btnRestart = new Button();
            btnRestart.Size = new Size(70, 50);
            btnRestart.Location = new Point(panel.Location.X + 40, panel.Location.Y + 175);
            btnRestart.Text = "重新开始";
            btnRestart.Click += ((object s, EventArgs e) => { Controls.Clear(); PyramidForm f = new PyramidForm(); this.Close(); f.Show(); });
            Controls.Add(btnRestart);
            btnRestart.BringToFront();



            Button btnRule = new Button();
            btnRule.Size = new Size(70, 50);
            btnRule.Location = new Point(panel.Location.X + 40, panel.Location.Y + 250);
            btnRule.Text = "游戏规则";
            btnRule.Click += new EventHandler(rule_clk);
            Controls.Add(btnRule);
            btnRule.BringToFront();

            Button btnBack = new Button();
            btnBack.Size = new Size(70, 50);
            btnBack.Location = new Point(panel.Location.X + 40, panel.Location.Y + 100);
            btnBack.Text = "返回游戏";
            btnBack.Click += new EventHandler((object s, EventArgs e) => { Controls.Remove(panel); Controls.Remove(btnBack); Controls.Remove(btnPrompt); Controls.Remove(btnRestart); Controls.Remove(btnRule); });
            Controls.Add(btnBack);
            btnBack.BringToFront();

        }
        public void makePyramidDomain()
        {
            if (pyramidChessboard.getNumOfRows() < 0) throw new Exception("创建金字塔的行数不正确！！");
            int rowPoint = btnMenu.Height, colPoint = 0;
            for (int i = 0; i < pyramidChessboard.getNumOfRows(); i++)
            {
                rowPoint += imgHeight / 4;
                for (int j = 0; j < pyramidChessboard.getRowWidth(i); j++)
                {
                    if (i % 2 == 0) { colPoint = this.Width / 2 - imgWidth / 2 - (i / 2) * (imgWidth * 4 / 3) + j * (imgWidth * 4 / 3); }
                    if (i % 2 == 1) { colPoint = this.Width / 2 - imgWidth * 4 / 3 * (i + 1) / 2 + imgWidth / 6 + j * imgWidth * 4 / 3; }
                    CardPictureBox cardpicture = new CardPictureBox(i, j);
                    cardpicture.Size = new Size(imgWidth, imgHeight);
                    cardpicture.Load("image\\" + pyramidChessboard.getCard(i, j).toString() + ".png");
                    cardpicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    cardpicture.Location = new Point(colPoint, rowPoint);
                    cardpicture.Click += new EventHandler(card_clk);
                    cardPictureBoxes.Add(cardpicture);
                    Controls.Add(cardpicture);
                }
            }
        }
        public void makePileDomain()
        {
            if (pyramidChessboard.getNumOfPile() < 0) throw new Exception("参数设置不正确");
            btnTrans = new Button();

            int rowPoint = btnMenu.Height + imgHeight * pyramidChessboard.getNumOfRows() / 4 + imgHeight;
            int colPoint = this.Width / 2 - imgWidth * 7 / 4;
            Panel panel = new Panel();
            panel.Size = new Size(imgWidth * 7 / 2, imgHeight * 3 / 2);
            panel.Location = new Point(colPoint, rowPoint);
            panel.BorderStyle = BorderStyle.Fixed3D;

            btnTrans.Text = "切牌";
            btnTrans.Size = new Size(imgWidth / 2, imgWidth / 2);
            btnTrans.Location = new Point(colPoint + imgWidth * 3 / 2, rowPoint + imgHeight * 3 / 4 - imgWidth / 2);
            btnTrans.Click += new EventHandler(trans_clk);

            Controls.Add(panel);
            Controls.Add(btnTrans);
            panel.SendToBack();
            btnTrans.BringToFront();

            rowPoint += imgHeight / 4;
            colPoint += imgWidth / 4;
            for (int i = 0; i < pyramidChessboard.getNumOfPile(); i++)
            {
                CardPictureBox cardpicture = new CardPictureBox(-1, i);
                cardpicture.Size = new Size(imgWidth, imgHeight);
                if (pyramidChessboard.getCardInPile(i) != null)
                    cardpicture.Load("image\\" + pyramidChessboard.getCardInPile(i).toString() + ".png");
                else cardpicture.BorderStyle = BorderStyle.Fixed3D;
                cardpicture.SizeMode = PictureBoxSizeMode.StretchImage;
                cardpicture.Location = new Point(colPoint, rowPoint);
                cardPictureBoxes.Add(cardpicture);
                cardpicture.Click += new EventHandler(card_clk);
                Pile.Add(cardpicture);
                Controls.Add(cardpicture);
                cardpicture.BringToFront();
                colPoint += imgWidth * 2;
            }
        }
        public void loadImageInPile(int index)
        {
            CardPictureBox cd = Pile[index];
            if (pyramidChessboard.getCardInPile(index) != null)
            {
                if (cd.Image == null)
                    cd.BorderStyle = BorderStyle.None;
                cd.Load("image\\" + pyramidChessboard.getCardInPile(index).toString() + ".png");
            }
            else
            {
                cd.Image = null;
                cd.BorderStyle = BorderStyle.Fixed3D;
            }

        }
        public void rule_clk(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Size = new Size(400, 500);
            //f.Location = new Point(this.Location.X +this.Width/2 - 200, this.Location.Y - 200+this.Height/2);
            TextBox txt = new TextBox();
            //txt.Location = new Point(f.Location.X + 10, f.Location.Y + 10);
            txt.Size = new Size(f.Size.Width - 20, f.Size.Height - 20);
            txt.Multiline = true;
            txt.ReadOnly = true;
            txt.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            txt.Text = "1）打开游戏，上方是金字塔区，任务就是消除金字塔里所有的牌，下面是发牌区。";
            txt.Text += "\r\n";
            txt.Text += "2）两张牌之和等于13，就可以匹配消除，而且必须是处于金字塔区的牌堆顶的。如果上方的金字塔区的牌没法匹配，可以将下方的发牌区的两个牌和上方金字塔中的牌匹配。";
            txt.Text += "\r\n";
            txt.Text += "3)发牌区的两张也可以匹配。如果遇到K，因为它本身就是13，点击可以直接消除，但是必须在金字塔区的牌堆顶。";
            txt.Text += "\r\n";
            txt.Text += "4)如果当前没有可以匹配的，点击切牌，翻开新的牌，此时左侧的牌会到右侧去。当左侧所有牌都到右侧之后，如果金字塔中还有牌没有消除，点击重新发牌，可以重新发一遍剩下的牌。";
            txt.Text += "\r\n";
            txt.Text += "5)每局游戏共有两次重新发牌的机会，也就是下方的牌从左到右总共可走三遍。当三次机会用尽后，如果此时金字塔区中还有牌无法消去，则游戏失败结束；反之如果将金字塔区的所有牌消除之后，游戏获胜。";
            f.Controls.Add(txt);

            Button btnBack = new Button();
            btnBack.Size = new Size(70, 50);
            btnBack.Location = new Point(f.Location.X + 165, f.Location.Y + 400);
            btnBack.Text = "返回游戏";
            btnBack.Click += new EventHandler((object s, EventArgs ee) => { f.Controls.Remove(txt); f.Close(); });
            f.Controls.Add(btnBack);
            btnBack.BringToFront();

            f.Show();
        }
        public void card_clk(object sender, EventArgs e)
        {
            CardPictureBox cd = (CardPictureBox)sender;
            
            Timer timer1 = new Timer();
            Timer timer2 = new Timer();
            int x1 = 0;
            int x2 = 0;//timer执行次数
            timer1.Tick += new EventHandler((object senderr, EventArgs ee) => {
                
                if (x1 == 10)
                {
                    x1 = 0;
                    Controls.Remove(cd);
                    timer1.Stop();
                    
                }
                if (x1 == 2)//条件满足，改变图片大小
                {
                    cd.Size = new Size(cd.Size.Width * 2 / 3, cd.Height * 2 / 3);

                }
                if (x1 == 6)//条件满足，改变图片大小
                {
                    cd.Size = new Size(cd.Size.Width/ 3, cd.Height/ 3);
                }
                if (x1 == 8)//条件满足，改变图片大小
                {
                    cd.Size = new Size(0, 0);
                }
                x1 += 2;//每执行一次timer，执行次数x加2；
            });
            timer2.Tick += new EventHandler((object senderr, EventArgs ee) => {

                if (x2 == 10)
                {
                    x2 = 0;
                    Controls.Remove(tempCard);
                    tempCard = null;
                    timer2.Stop();

                }
                if (x2 == 2)//条件满足，改变图片大小
                {
                    tempCard.Size = new Size(imgWidth* 2 / 3, imgHeight * 2 / 3);


                }
                if (x2 == 6)//条件满足，改变图片大小
                {
                    tempCard.Size = new Size(imgWidth / 3, imgHeight / 3);
                }
                if (x2 == 8)//条件满足，改变图片大小
                {
                    tempCard.Size = new Size(0, 0);
                }
                x2 += 2;//每执行一次timer，执行次数x加2；
            });
            if (pyramidChessboard.isGameOver())
            {
                pyramidChessboard.changeToGameOver();
                MessageBox.Show("游戏已经结束");
                return;
            }
            if (cd.getRowIndex() == -1 && pyramidChessboard.getCardInPile(cd.getColIndex()) == null)
            {
                MessageBox.Show("这里什么都没有哦");
                return;
            }
            if (tempCard == null)
            {
                if (cd.getRowIndex() != -1)
                {
                    if (pyramidChessboard.getCard(cd.getRowIndex(), cd.getColIndex()).getValue() == 13)
                    {
                        try
                        {
                            pyramidChessboard.hideOneCard(cd.getRowIndex(), cd.getColIndex());
                            cardPictureBoxes.Remove(cd);
                            timer1.Start();
                            cardPictureBoxes.Remove(cd);
                        }
                        catch (Exception err)
                        {
                            System.IO.StringReader sr = new System.IO.StringReader(err.ToString());
                            MessageBox.Show(sr.ReadLine());
                        }
                    }
                    else
                    {
                        cd.BorderStyle = BorderStyle.Fixed3D;
                        tempCard = cd;
                    }
                }
                else
                {
                    if (pyramidChessboard.getCardInPile(cd.getColIndex()).getValue() == 13)
                    {
                        try
                        {
                            pyramidChessboard.hideOneCardInPile(cd.getColIndex());
                            loadImageInPile(cd.getColIndex());
                        }
                        catch (Exception err)
                        {
                            System.IO.StringReader sr = new System.IO.StringReader(err.ToString());
                            MessageBox.Show(sr.ReadLine());
                        }
                    }
                    else
                    {
                        cd.BorderStyle = BorderStyle.Fixed3D;
                        tempCard = cd;
                    }
                }
            }
            else if (tempCard == cd)
            {
                cd.BorderStyle = BorderStyle.None;
                tempCard = null;
            }
            else
            {
                if (cd.getRowIndex() == -1 && tempCard.getRowIndex() == -1)
                {
                    if (pyramidChessboard.getCardInPile(cd.getColIndex()).getValue() + pyramidChessboard.getCardInPile(tempCard.getColIndex()).getValue() == 13)
                    {
                        try
                        {
                            pyramidChessboard.hideTwoCardsInPile(cd.getColIndex(), tempCard.getColIndex());
                            cd.BorderStyle = BorderStyle.None;
                            tempCard.BorderStyle = BorderStyle.None;
                            loadImageInPile(cd.getColIndex());
                            loadImageInPile(tempCard.getColIndex());
                            tempCard = null;

                        }
                        catch (Exception err)
                        {
                            System.IO.StringReader sr = new System.IO.StringReader(err.ToString());
                            MessageBox.Show(sr.ReadLine());
                        }
                    }
                    else
                    {
                        MessageBox.Show("这两张牌的和不是13");
                    }
                }
                else if (cd.getRowIndex() == -1)
                {
                    try
                    {
                        pyramidChessboard.hideTwoCardsWithPile(cd.getColIndex(), tempCard.getRowIndex(), tempCard.getColIndex());
                        timer2.Start();
                        cardPictureBoxes.Remove(tempCard);
                        cd.BorderStyle = BorderStyle.None;
                        loadImageInPile(cd.getColIndex());
                    }
                    catch (Exception err)
                    {
                        System.IO.StringReader sr = new System.IO.StringReader(err.ToString());
                        MessageBox.Show(sr.ReadLine());
                    }
                }
                else if (tempCard.getRowIndex() == -1)
                {
                    try
                    {
                        pyramidChessboard.hideTwoCardsWithPile(tempCard.getColIndex(), cd.getRowIndex(), cd.getColIndex());
                        timer1.Start();
                        cardPictureBoxes.Remove(cd);
                        tempCard.BorderStyle = BorderStyle.None;
                        loadImageInPile(tempCard.getColIndex());
                        tempCard = null;
                    }
                    catch (Exception err)
                    {
                        System.IO.StringReader sr = new System.IO.StringReader(err.ToString());
                        MessageBox.Show(sr.ReadLine());
                    }
                }
                else
                {
                    try
                    {
                        pyramidChessboard.hideTwoCards(cd.getRowIndex(), cd.getColIndex(), tempCard.getRowIndex(), tempCard.getColIndex());
                        timer2.Start();
                        timer1.Start();
                        cardPictureBoxes.Remove(tempCard);
                        cardPictureBoxes.Remove(cd);
                    }
                    catch (Exception err)
                    {
                        System.IO.StringReader sr = new System.IO.StringReader(err.ToString());
                        MessageBox.Show(sr.ReadLine());
                    }
                }

            }
            loadScoreData();

            if (pyramidChessboard.heapCount() == 0 && pyramidChessboard.getCardInPile(0) == null)
            {
                btnTrans.Text = "重新发牌";
                btnTrans.Click -= new EventHandler(trans_clk);
                btnTrans.Click += new EventHandler(pileShuffle_clk);

            }

        }
        public void trans_clk(object sender, EventArgs e)
        {
            pyramidChessboard.transCardsInPile();
            for (int i = 0; i < pyramidChessboard.getNumOfPile(); i++)
            {
                loadImageInPile(i);
            }

            if (tempCard != null)
            {
                if (tempCard.getRowIndex() == -1)
                {
                    tempCard.BorderStyle = BorderStyle.None;
                    tempCard = null;

                }
            }
            if (pyramidChessboard.heapCount() == 0 && pyramidChessboard.getCardInPile(0) == null)
            {
                btnTrans.Text = "重新发牌";
                btnTrans.Click -= new EventHandler(trans_clk);
                btnTrans.Click += new EventHandler(pileShuffle_clk);
            }
        }
        public void pileShuffle_clk(object sender, EventArgs e)
        {
            if (pyramidChessboard.isGameOver())
            {
                pyramidChessboard.changeToGameOver();
                MessageBox.Show("游戏已经结束");
                btnTrans.Click -= new EventHandler(pileShuffle_clk);
                return;
            }

            if (pyramidChessboard.getShuffleCount() < 2)
            {
                try
                {
                    pyramidChessboard.shufflePileCards();
                }
                catch (Exception err)
                {
                    System.IO.StringReader sr = new System.IO.StringReader(err.ToString());
                    MessageBox.Show(sr.ReadLine());
                }
                for (int i = 0; i < pyramidChessboard.getNumOfPile(); i++)
                    loadImageInPile(i);
                pyramidChessboard.addShuffleCount();
                btnTrans.Text = "切牌";
                btnTrans.Click -= new EventHandler(pileShuffle_clk);
                btnTrans.Click += new EventHandler(trans_clk);
            }
            else
            {
                MessageBox.Show("您的洗牌机会已用尽");
                pyramidChessboard.addShuffleCount();
            }

        }
        public void back_clk(object sender, EventArgs e) //点击返回按钮触发事件
        {
            this.Close();
            //产生新窗体
        }
        public void menu_clk(object sender, EventArgs e)    //点击菜单按钮触发事件
        {
            makeMenuDomain();
        }
        public void loadScoreData()     //更新分数数据
        {
            lblScore.Text = "您当前的得分:" + pyramidChessboard.getScore().ToString();
        }


    }

}