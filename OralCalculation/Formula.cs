using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace OralCalculation
{
    class Formula
    {
        public List<string> history = new List<string>();//做题记录
        public Formula(string formula)
        {
            history.Add(formula);
        }
        public Formula()
        {

        }

        public void AddHistory(string formula)
        {
            history.Add(formula);
        }

        public void PrintHistory()//有答案
        {
            List<string> historyWithoutAnswer = new List<string>();

            foreach (string piece in history)//无答案
            {
                string piece2 = piece.Substring(0, piece.IndexOf("="));
                historyWithoutAnswer.Add(piece2);
            }

            historyWithoutAnswer.Add("\n");

            foreach (string str in history)//加有答案的版本
            {
                historyWithoutAnswer.Add(str);
            }

            PrintList("History",historyWithoutAnswer);
        }

        public async void PrintList(string Type,List<string> list)//type是内容类型(history/practice)
        {
            string writingData = "";

            foreach (string piece in list)
            {
                writingData += piece + "\n";
            }


            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("文本文件", new List<string>() { ".txt" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = Type+"-" + DateTime.Now.Month + "-" + DateTime.Now.Day;

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file
                await FileIO.WriteTextAsync(file, writingData);
                // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status == FileUpdateStatus.Complete)
                {
                    //"File was saved."
                    ContentDialog SavedDialog = new ContentDialog
                    {
                        Title = "已保存",
                        Content = "已保存" + file.Path,
                        CloseButtonText = "好"
                    };

                    ContentDialogResult result = await SavedDialog.ShowAsync();
                }
                else
                {
                    //couldn't be saved.";
                    ContentDialog ErrorDialog = new ContentDialog
                    {
                        Title = "无法保存",
                        Content = "无法保存到" + file.Path,
                        CloseButtonText = "好"
                    };

                    ContentDialogResult result = await ErrorDialog.ShowAsync();
                }
            }
            else
            {
                ContentDialog CancelledDialog = new ContentDialog
                {
                    Title = "已取消",
                    Content = "操作已被取消",
                    CloseButtonText = "好"
                };

                ContentDialogResult result = await CancelledDialog.ShowAsync();

            }


        }

        public string[] GenerateArrayFormula(int range, bool plus, bool minus, bool multiply)
        {
            //计数
            int ValidNumber = 0;
            List<string> SymbolList = new List<string>();
            if (plus == true)
            {
                ValidNumber++;
                SymbolList.Add("+");
            }
            if (minus == true)
            {
                ValidNumber++;
                SymbolList.Add("-");
            }
            if (multiply == true)
            {
                ValidNumber++;
                SymbolList.Add("*");//后期输出要替换 乘号
            }

            Random rd = new Random();
            int SymbolNumber = rd.Next(0, ValidNumber);//[minValue,maxValue)
            //list序号从0开始，上面都 -1 操作

            return new string[] { rd.Next(0, range).ToString(), SymbolList[SymbolNumber], rd.Next(0, range).ToString() };
        }

        public string GenerateFormula(int range, bool plus, bool minus, bool multiply)
        {
            //计数
            int ValidNumber = 0;
            List<string> SymbolList = new List<string>();
            if (plus == true)
            {
                ValidNumber++;
                SymbolList.Add("+");
            }
            if (minus == true)
            {
                ValidNumber++;
                SymbolList.Add("-");
            }
            if (multiply == true)
            {
                ValidNumber++;
                SymbolList.Add("*");//后期输出要替换 乘号
            }

            Random rd = new Random();
            int SymbolNumber = rd.Next(0, ValidNumber);//[minValue,maxValue)
            //list序号从0开始，上面都 -1 操作

            return rd.Next(0, range).ToString() + SymbolList[SymbolNumber] + rd.Next(0, range).ToString();
        }


    }
}
