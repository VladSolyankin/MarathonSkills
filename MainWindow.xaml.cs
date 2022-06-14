using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MarathonSkills
{
    public partial class MainWindow : Window
    {
        public string[] Lines { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ManagerNavigation.MainFrame = mainFrame;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void tableButton_Click(object sender, RoutedEventArgs e)
        {
            tableButton.Visibility = Visibility.Hidden;
            mainDataGrid.Visibility = Visibility.Visible;
        }

        private void Checkpoint_Click(object sender, RoutedEventArgs e)
        {
            //проверка на пустоту списка с чекпоинтами
            if (ManagerModel.Checkpoints.Count == 0)
            {
                ErrorWindow error = new ErrorWindow();
                error.errorTextBlock.Text = "Вы не загрузили файл";
                error.Show();
            }
            //иначе отображение особенностей чекпоинта в зависимости от файла
            else 
            {
                Button button = (Button)sender;
                Checkpoint checkpoint = new Checkpoint();
                checkpoint.chkNumber_TextBlock.Text = "Checkpoint " + button.Name[1];
                int buttonNum = int.Parse(button.Name[1].ToString()) - 1;

                if (button.Name != "p1") checkpoint.startImg.Opacity = 0.5;

                List<CheckpointFeatures> checkpointFeatures = ManagerModel.Checkpoints;


                if (checkpointFeatures[buttonNum].Drinks == "No") checkpoint.drinkImg.Opacity = 0.5;
                if (checkpointFeatures[buttonNum].Bars == "No") checkpoint.barImg.Opacity = 0.5;
                if (checkpointFeatures[buttonNum].Toilet == "No") checkpoint.toiletImg.Opacity = 0.5;
                if (checkpointFeatures[buttonNum].Info == "No") checkpoint.infoImg.Opacity = 0.5;
                if (checkpointFeatures[buttonNum].Med == "No") checkpoint.medImg.Opacity = 0.5;

                ManagerNavigation.MainFrame.Navigate(checkpoint);
            }
        }

        //загрузка файла
        public void fileButton_Click(object sender, RoutedEventArgs e)
        {
            //очищение списка чекпоинтов и DataGrid'a при повторной загрузке файла
            ManagerModel.Checkpoints.Clear();
            mainDataGrid.Items.Clear();

            //открытие проводника для выбора файла
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "Файл данных (*.txt)|*.txt|Все файлы (*.*)|*.*",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                DefaultExt = "txt"
            };

            //загрузка данных в DataGrid из файла
            if (openFile.ShowDialog() == true)
            {
                using (var f = new StreamReader(openFile.FileName))
                {
                    string line;
                    while ((line = f.ReadLine()) != null)
                    {
                        string[] features = line.Split(' ');
                        CheckpointFeatures checkpoint = new CheckpointFeatures()
                        {
                            Number = Convert.ToString(features[0].Replace("Checkpoint", "")),
                            Drinks = Convert.ToString(features[1]),
                            Bars = Convert.ToString(features[2]),
                            Toilet = Convert.ToString(features[3]),
                            Info = Convert.ToString(features[4]),
                            Med = Convert.ToString(features[5]),
                        };

                        ManagerModel.Checkpoints.Add(checkpoint);
                        mainDataGrid.Items.Add(checkpoint);
                    }
                }
            }
        }
    }
}