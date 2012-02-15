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
        //bool okToScroll = false;

        // 3 states, 
        // 1 -> edit ; 2 -> save ; 3 -> delete
        int state = 0;

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
            scrollJump();
        }

        private void appBar_Edit(object sender, EventArgs e)
        {
            if (state != 1)
            {

            }
            noteTextBlock.Focus();
            noteTextBlock.Select(noteTextBlock.Text.Length, 0);
            scrollJump();
            state = 1;
        }

        private void appBar_Save(object sender, EventArgs e)
        {
            if (state != 2)
            {
               
            }
            this.Focus();
            state = 2;
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

        public void scrollJump()
        {
            //if (okToScroll && (noteTextBlock.SelectionStart == noteTextBlock.Text.Length))
            //{
            noteScrollViewer.ScrollToVerticalOffset(noteTextBlock.ActualHeight);
            //}
            //okToScroll = true;
        }

    }

}