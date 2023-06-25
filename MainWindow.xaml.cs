using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;

namespace ListRecipes
{
    public partial class MainWindow : Window
    {
        static string dirRecipes = "Recipes";
        List<string> listRecipes = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            GetFileReceipt();
            LoadListBox();
        }

        private void GetFileReceipt()
        {
            foreach (string file in Directory.GetFiles(dirRecipes))
            {
                if (System.IO.Path.GetExtension(file) == ".xml")
                {
                    listRecipes.Add(System.IO.Path.GetFileNameWithoutExtension(file));  // сохраняем в список названия всех блюд
                }
            }
        }

        private void LoadListBox()
        {
            for (int i = 0; i < listRecipes.Count; i++)
            {
                listBox.Items.Add(listRecipes[i]);  // добавляем элемыеты в список
            }
        }

        private void LoadXML(string name)
        {
            using (XmlReader reader = XmlReader.Create($@"{dirRecipes}\{name}.xml"))  // создание объекта XmlReader для чтения XML-файла
            {
                FlowDocument flowDocument = (FlowDocument)XamlReader.Load(reader);  // загрузка XML-кода в объект FlowDocument
                scrollDoc.Document = flowDocument;  // отображение FlowDocument в элементе scrollDoc
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListBox? list = sender as ListBox;
                if (list != null)
                {
                    LoadXML(list.SelectedItem.ToString());  // загружаем содержимое xml файла
                }
            }
            catch (Exception) {}
        }
    }
}
