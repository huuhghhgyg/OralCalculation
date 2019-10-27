using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //检查所有配置
            if (PlusToggle.IsChecked == false && MinusToggle.IsChecked == false & MultiplyToggle.IsChecked == false)
            {
                //没有选择运算法则
                ShowMessageDialog("请在右下角选择运算法则", "没有选择运算法则");
            }
            else
            {
                //开始
                RefreshPuzzle();
            }
        }

        private async void ShowMessageDialog(string msg, string title)
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog(msg) { Title = title };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定"));
            await msgDialog.ShowAsync();
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
                ConfirmAnswer();
            }
        }

        void DidPuzzle(bool AnswerIsTrue)
        {
            PuzzleDid++;
            if (AnswerIsTrue)
            {
                DidRight++;
            }
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
            ConfirmAnswer();
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
            FirstNumber = RandomMath(0, CalculationArea);
            LastNumber = RandomMath(0, CalculationArea);
            remake:
            Symbol = RandomMath(1, 3);//随机符号
            string sym = "";
            switch (Symbol)
            {
                case 1:
                    if (PlusToggle.IsChecked == false)
                    {
                        goto remake;
                    }
                    else
                    {
                        sym = "+";
                    }
                    break;
                case 2:
                    if (MinusToggle.IsChecked == false)
                    {
                        goto remake;
                    }
                    else
                    {
                        sym = "-";
                    }
                    break;
                case 3:
                    if (MultiplyToggle.IsChecked == false)
                    {
                        goto remake;
                    }
                    else
                    {
                        sym = "×";
                    }
                    break;
            }
            PuzzleText.Text = FirstNumber.ToString() + sym + LastNumber.ToString() + "=?";
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
                ShowMessageDialog("请在右下角选择运算法则", "没有选择运算法则");
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
                            DidPuzzle(true);//做对
                            InputBox.Text = "";
                            //刷新题目
                            RefreshPuzzle();
                        }
                        else
                        {
                            //验证为错误答案
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
    }
}
