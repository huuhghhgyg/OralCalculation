using CalCore_Universal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“内容对话框”项模板

namespace OralCalculation
{
    public enum PrintResult
    {
        Printed,
        Cancelled,
        Nothing
    }
    public sealed partial class ContentDialog1 : ContentDialog
    {
        public PrintResult Result { get; private set; }

        public ContentDialog1()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //取消
            this.Result = PrintResult.Cancelled;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //输出

            Formula formula = new Formula();
            List<string> PuzzleList = new List<string>();
            List<string> PuzzleListAnswer = new List<string>();
            List<string> PuzzleListProcessed = new List<string>();

            bool b1 = true, b2 = true, b3 = true;
            if (PlusCheckBox.IsChecked == false || PlusCheckBox.IsChecked == null)
            {
                b1 = false;
            }
            if (MinusCheckBox.IsChecked == false || MinusCheckBox.IsChecked == null)
            {
                b2 = false;
            }
            if (MultiplyCheckBox.IsChecked == false || MultiplyCheckBox.IsChecked == null)
            {
                b3 = false;
            }

            //生成
            for (int i = 0; i < NumberBox1.Value; i++)
            {
                PuzzleList.Add(formula.GenerateFormula((int)NumberBox2.Value, b1, b2, b3));
            }

            CalCore core = new CalCore();

            foreach (string str in PuzzleList)
            {
                PuzzleListAnswer.Add(str + "=" + core.Calculate(str));
            }

            foreach (string str in PuzzleList)
            {
                PuzzleListProcessed.Add(str + "=");
            }

            PuzzleList.Clear();

            foreach (string str in PuzzleListProcessed)
            {
                str.Replace("*", "×");
                PuzzleList.Add(str);
            }

            PuzzleList.Add("\n");

            foreach (string str in PuzzleListAnswer)
            {
                str.Replace("*","×");
                PuzzleList.Add(str);
            }

            formula.PrintList("Practice",PuzzleList);
        }
    }
}
