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
using Microsoft.Phone.Shell;

namespace SimpleNote
{
    public partial class MainPage : PhoneApplicationPage
    {
        //bool okToScroll = false;

        // 1 -> edit ; 2 -> save ; 3 -> delete
        int state = 0;
        PhoneApplicationService phoneAppService = PhoneApplicationService.Current;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            object myState;
            if (phoneAppService.State.ContainsKey("MyState"))
            {
                if (phoneAppService.State.TryGetValue("MyState", out myState))
                {
                    this.state = (int) myState;
                }
            }

            //loadNote();
            noteTextBlock.Text = noteTextBlock.Text + Environment.NewLine + state.ToString();

            if (state == 1)
            {
                prepEdit();
            }
        }

        private void noteTextBlock_Changed(object sender, TextChangedEventArgs e)
        {
            scrollJump();
        }

        private void noteTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            updateState(1);
        }


        private void noteTextBlock_unFocus(object sender, RoutedEventArgs e)
        {
            updateState(0);
        }

        private void appBar_Edit(object sender, EventArgs e)
        {
            if (state != 1)
            {

            }
            prepEdit();
            updateState(1);
        }

        private void appBar_Save(object sender, EventArgs e)
        {
            if (state != 2)
            {
                saveNote();
            }
            this.Focus();
            updateState(2);
        }

        private void appBar_Delete(object sender, EventArgs e)
        {
            updateState(3);
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

        private void updateState(int stateNum)
        {
            this.state = stateNum;
            phoneAppService.State["MyState"] = this.state;
        }

        private void prepEdit()
        {
            noteTextBlock.Focus();
            noteTextBlock.Select(noteTextBlock.Text.Length, 0);
            scrollJump();
        }
    }

}