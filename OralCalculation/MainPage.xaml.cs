﻿using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace OralCalculation
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public int CalculationArea = 10;//10,50,100
        public bool plus = true;
        public bool minus = true;
        public bool multiply = false;

        public int FirstNumber = 1, LastNumber = 1, Symbol = 1;//symbol 1加法 2减法 3乘法
        public int PuzzleDid, DidRight = 0;
        public int TotalNumber = 0;
        public bool GameStarted = false;

        Formula formula = new Formula();

        public MainPage()
        {
            this.InitializeComponent();
            ExtendAcrylicIntoTitleBar();
            DidWrongBoard.Visibility = Visibility.Collapsed;
        }

        private void ExtendAcrylicIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar; titleBar.ButtonBackgroundColor = Colors.Transparent; titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameStarted == true)
            {
                GameRestartDialog.Visibility = Visibility.Visible;
                ContentDialogResult IsRestart = await GameRestartDialog.ShowAsync();
                if (IsRestart == ContentDialogResult.Primary)
                {
                    //重置
                    PuzzleDid = 0;
                    DidRight = 0;
                    GameStarted = false;
                    //开始
                    RefreshBoard();
                    //允许重新选择
                    FreezeOptions(false);
                }
            }
            else
            {
                EnterAnswer();
            }
        }

        bool IsConfigurationDone()
        {
            if (PlusToggle.IsChecked == false && MinusToggle.IsChecked == false & MultiplyToggle.IsChecked == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void EnterAnswer()
        {
            //检查所有配置
            if (IsConfigurationDone() == false)
            {
                //没有选择运算法则
                BottomCommandBar.IsOpen = true;
                StartTeachingTip.IsOpen = true;
            }
            else
            {
                if (GameStarted == false)
                {
                    //开始
                    RefreshPuzzle();
                    GameStarted = true;
                    InputBox.Text = "";

                    //显示范围
                    RangeBoard.Label = "0-" + CalculationArea.ToString();
                    RangeBoard.Visibility = Visibility.Visible;
                }
                else
                {
                    ConfirmAnswer();
                    RefreshBoard();
                }

                FreezeOptions(true);
            }

            TotalNumber++;
        }

        void FreezeOptions(bool selection)//运算过程中冻结选择
        {
            if (selection == true)
            {
                //禁止重新选择
                Area10Button.IsEnabled = false;
                Area20Button.IsEnabled = false;
                Area50Button.IsEnabled = false;
                Area100Button.IsEnabled = false;

                PlusToggle.IsEnabled = false;
                MinusToggle.IsEnabled = false;
                MultiplyToggle.IsEnabled = false;
            }
            else
            {
                //允许重新选择
                Area10Button.IsEnabled = true;
                Area20Button.IsEnabled = true;
                Area50Button.IsEnabled = true;
                Area100Button.IsEnabled = true;

                PlusToggle.IsEnabled = true;
                MinusToggle.IsEnabled = true;
                MultiplyToggle.IsEnabled = true;

            }
        }

        private void SetAlgorithmDialog()
        {
            BottomCommandBar.IsOpen = true;
            StartTeachingTip.IsOpen = true;
        }

        private void Area10Button_Click(object sender, RoutedEventArgs e)
        {
            CalculationArea = 10;
        }

        private void Area50Button_Click(object sender, RoutedEventArgs e)
        {
            CalculationArea = 50;
        }

        private void Area100Button_Click(object sender, RoutedEventArgs e)
        {
            CalculationArea = 100;
        }

        private void InputBox_KeyUp(object sender, KeyRoutedEventArgs e)
        
        {
            if (e.Key == VirtualKey.Enter)//enter键
            {
                EnterAnswer();
            }
        }

        void DidPuzzle(bool AnswerIsTrue)
        {
            PuzzleDid++;
            if (AnswerIsTrue)
            {
                DidRight++;
            }
            RefreshBoard();

            //更新磁贴
            if (PuzzleDid != 0 && DidRight != 0)
            {
                UpdatetTile("Practice",
                    "错误数:"+(PuzzleDid-DidRight),
                    "上次做题:" + TotalNumber.ToString());
            }
        }

        void RefreshBoard()
        {
            DidRightBoard.Label = DidRight.ToString();
            PuzzleBoard.Label = PuzzleDid.ToString();
            DidWrongBoard.Label = (PuzzleDid - DidRight).ToString();
            if (PuzzleDid - DidRight > 0)
            {
                DidWrongBoard.Visibility = Visibility.Visible;
            }
        }

        private void Area20Button_Click(object sender, RoutedEventArgs e)
        {
            CalculationArea = 20;
        }

        void KeyboardInput(string key)
        {
            InputBox.Text += key;
        }


        private void KeyButton1_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("1");
        }

        private void KeyButtonClear_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Text = "";
        }

        private void KeyButton2_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("2");
        }

        private void KeyButton3_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("3");
        }

        private void KeyButton4_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("4");
        }

        private void KeyButton5_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("5");
        }

        private void KeyButton6_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("6");
        }

        private void KeyButton7_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("7");
        }

        private void KeyButton8_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("8");
        }

        private void KeyButton9_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("9");
        }

        private void KeyButton0_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("0");
        }

        private void KeyButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            EnterAnswer();
        }

        private void KeyButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            KeyboardInput("-");
        }

        private void KeyboardVisible_Click(object sender, RoutedEventArgs e)
        {
            if (Keyboard_Grid.Visibility == Visibility.Collapsed)
            {
                Keyboard_Grid.Visibility = Visibility.Visible;
            }
            else
            {
                Keyboard_Grid.Visibility = Visibility.Collapsed;
            }
        }

        private void KeyboardRight_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_Grid.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void KeyboardLeft_Click(object sender, RoutedEventArgs e)
        {
            Keyboard_Grid.HorizontalAlignment = HorizontalAlignment.Left;
        }

        void RefreshPuzzle()
        {
            //    FirstNumber = RandomMath(0, CalculationArea);
            //    LastNumber = RandomMath(0, CalculationArea);
            //remake:
            //    Symbol = RandomMath(1, 3);//随机符号
            //    string sym = "";
            //    switch (Symbol)
            //    {
            //        case 1:
            //            if (PlusToggle.IsChecked == false)
            //            {
            //                goto remake;
            //            }
            //            else
            //            {
            //                sym = "+";
            //            }
            //            break;
            //        case 2:
            //            if (MinusToggle.IsChecked == false)
            //            {
            //                goto remake;
            //            }
            //            else
            //            {
            //                sym = "-";
            //            }
            //            break;
            //        case 3:
            //            if (MultiplyToggle.IsChecked == false)
            //            {
            //                goto remake;
            //            }
            //            else
            //            {
            //                sym = "×";
            //            }
            //            break;
            //    }
            //    PuzzleText.Text = FirstNumber.ToString() + sym + LastNumber.ToString() + "=?";


            string[] _puzzle = formula.GenerateArrayFormula(CalculationArea,
                PlusToggle.IsChecked,
                MinusToggle.IsChecked,
                MultiplyToggle.IsChecked);

            //注册
            FirstNumber = int.Parse(_puzzle[0]);
            LastNumber = int.Parse(_puzzle[2]);

            switch (_puzzle[1])
            {
                case "+":
                    Symbol = 1;
                    break;
                case "-":
                    Symbol = 2;
                    break;
                case "*":
                    Symbol = 3;
                    break;
            }

            PuzzleText.Text = _puzzle[0]+ _puzzle[1]+ _puzzle[2] + "=?";//生成算式
        }

        private async void PrintPractice_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog1 dialog = new ContentDialog1();
            await dialog.ShowAsync();
        }

        private void PrintHistory_Click(object sender, RoutedEventArgs e)
        {
            formula.PrintHistory();
        }

        int RandomMath(int from, int to)
        {
            Random rd = new Random();
            return rd.Next(from, to + 1);//(生成from~to之间的随机数，包括to,from)
        }

        public void ConfirmAnswer()
        {
            if (PlusToggle.IsChecked == false && MinusToggle.IsChecked == false & MultiplyToggle.IsChecked == false)

            {
                //没有选择运算法则
                SetAlgorithmDialog();
            }
            else
            {
                if (PuzzleText.Text.Substring(PuzzleText.Text.Length - 1) == "?")
                {
                    //正在显示问题，user提交答案
                    if (InputBox.Text != "")
                    {
                        int result = 0;
                        string sym = "";
                        switch (Symbol)
                        {
                            case 1://加法
                                result = FirstNumber + LastNumber;
                                sym = "+";
                                break;
                            case 2://减法
                                result = FirstNumber - LastNumber;
                                sym = "-";
                                break;
                            case 3:
                                result = FirstNumber * LastNumber;
                                sym = "×";
                                break;
                        }


                        if (InputBox.Text == result.ToString())
                        {
                            //验证为正确答案
                            formula.AddHistory(FirstNumber.ToString() 
                                + sym 
                                + LastNumber.ToString()
                                +"="+result.ToString());

                            DidPuzzle(true);//做对
                            InputBox.Text = "";
                            //刷新题目
                            RefreshPuzzle();
                        }
                        else
                        {
                            //验证为错误答案
                            formula.AddHistory(FirstNumber.ToString()
                                + sym
                                + LastNumber.ToString()
                                + "=" + result.ToString()
                                +" !");

                            DidPuzzle(false);
                            InputBox.Text = "";
                            //显示正确答案
                            PuzzleText.Text = FirstNumber.ToString() + sym + LastNumber.ToString() + "=" + result.ToString();
                        }
                    }
                }
                else
                {
                    //已显示答案,刷新题目
                    RefreshPuzzle();
                }
            }
        }


        private void UpdatetTile(string title, string subject, string body)//更新磁贴：title,subject(第一行),body(第二行)
        {
            // In a real app, these would be initialized with actual data

            // Construct the tile content
            TileContent content = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                {
                    new AdaptiveText()
                    {
                        Text = title
                    },

                    new AdaptiveText()
                    {
                        Text = subject,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    },

                    new AdaptiveText()
                    {
                        Text = body,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    }
                }
                        }
                    },

                    TileWide = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                {
                    new AdaptiveText()
                    {
                        Text = title,
                        HintStyle = AdaptiveTextStyle.Subtitle
                    },

                    new AdaptiveText()
                    {
                        Text = subject,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    },

                    new AdaptiveText()
                    {
                        Text = body,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    }
                }
                        }
                    }
                }
            };

            // Create the tile notification
            var notification = new TileNotification(content.GetXml());

            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }
    }
}
