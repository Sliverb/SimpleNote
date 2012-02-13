using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;

namespace SimpleNote
{
    public partial class MainPage : PhoneApplicationPage
    {
        bool okToScroll = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void noteTextBlock_Changed(object sender, TextChangedEventArgs e)
        {
            //if (okToScroll && (noteTextBlock.SelectionStart == noteTextBlock.Text.Length))
            //{
                noteScrollViewer.ScrollToVerticalOffset(noteTextBlock.ActualHeight);
            //}
            //okToScroll = true;
        }

        private void appBar_Edit(object sender, EventArgs e)
        {

        }

        private void appBar_Save(object sender, EventArgs e)
        {

        }

        private void appBar_Delete(object sender, EventArgs e)
        {

        }

        private void loadNote()
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();

            using (var fileStream = appStorage.OpenFile("note.txt", System.IO.FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    noteTextBlock.Text = sr.ReadToEnd();
                }
            }
        }

        private void saveNote()
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();

            using (var fileStream = appStorage.OpenFile("note.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    sw.WriteLine(noteTextBlock.Text);
                }
            }


        }

    }

}