using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace NoteSticker
{//creating a new note window upon initialization and adding controls
    class NoteWindowBuilder
    {
        
        /*
         * During debugging the files are saved inside the bin/Debug/netcoreapp3.1
         * when app is published files are saved inside the same location where the main
         * application is.
         */
        
        //Most are for testing at the moment
        public int wWidth { get; set; } = 350;
        public int wHeight { get; set; } = 430;
        public double wOpacity { get; set; } = 0.61;
        public double wTextBoxOpacity { get; set; } = .86;
        public bool wCanResize { get; set; } = false;
        public int wFontSize { get; set; } = 14;

        //checks if window can be moved
        public bool MoveWindow = false;
        //Store mouse position on click
        public Point Mos_LastPos = new Point();


        //Getting The Path to Application Folder as String
        string AppPath = MainWindow.PathToApplication;

        //Creating New Window
        Window nNote = new Window();

        Label 
            CCounter_Title,
            CCounter_NoteText;
        TextBox 
            NoteTitle,
            NoteTyping;
        Image
            MoveIcon;
        Button
            SaveNoteBtn;


        #region Constructor
        public NoteWindowBuilder()
        {//creating and showing window :: Setting up / configuring window's settings
            
            nNote.Width = wWidth;
            nNote.Height = wHeight;
            nNote.WindowStyle = WindowStyle.None;
            nNote.AllowsTransparency = true;



            //creating a transparent(ly) background
            nNote.Background = new SolidColorBrush(Colors.White) { Opacity = wOpacity };

            if (wCanResize)
            {
                nNote.ResizeMode = ResizeMode.CanResize;
            }
            else
            {
                nNote.ResizeMode = ResizeMode.NoResize;
            }

            //showing new window
            nNote.Show();

            #region Creating Grid and Configuring
            //create grid
            Grid nGrid = new Grid();
            //configure grid
            ColumnDefinition C1 = new ColumnDefinition(); //used for window's buttons at top (close, minimize)
            ColumnDefinition C2 = new ColumnDefinition(); //used for window's buttons at top (close, minimize)
            ColumnDefinition C3 = new ColumnDefinition(); //used for window's buttons at top (close, minimize)
            ColumnDefinition C4 = new ColumnDefinition(); //used for window's buttons at top (close, minimize)
            RowDefinition R1 = new RowDefinition();
            RowDefinition R2 = new RowDefinition();
            RowDefinition R3 = new RowDefinition();
            RowDefinition R4 = new RowDefinition();

            //configuring columns and rows
            C1.Width = new GridLength(25, GridUnitType.Star);
            C2.Width = new GridLength(25, GridUnitType.Star);
            C3.Width = new GridLength(25, GridUnitType.Star);
            C4.Width = new GridLength(25, GridUnitType.Star);
            

            R1.Height = new GridLength(20, GridUnitType.Pixel);
            R2.Height = new GridLength(48, GridUnitType.Pixel);
            R3.Height = new GridLength(nNote.Height / 2.8, GridUnitType.Pixel); //Textbox Row
            R4.Height = new GridLength(100, GridUnitType.Pixel);


            //adding columns and rows
            nGrid.ColumnDefinitions.Add(C1);
            nGrid.ColumnDefinitions.Add(C2);
            nGrid.ColumnDefinitions.Add(C3);
            nGrid.ColumnDefinitions.Add(C4);

            nGrid.RowDefinitions.Add(R1);
            nGrid.RowDefinitions.Add(R2);
            nGrid.RowDefinitions.Add(R3);
            nGrid.RowDefinitions.Add(R4);
            #endregion

            #region Creating Containers
            //adding controls into new window
            StackPanel nSPanel_TextBox = new StackPanel() {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(2, 4, 2, 4)
                
            };

            StackPanel nSPanel_NoteButtons = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(1, 48, 1, 2)
            };
            #endregion

            #region Creating Control(s)
            //create textbox for typing
            NoteTyping = new TextBox
            {
                FontSize = wFontSize,
                Padding = new Thickness(2),
                MaxLength = 450,
                Height = nNote.Height / 3,
                Opacity = wTextBoxOpacity,
                AcceptsReturn = true,
                TextWrapping = TextWrapping.Wrap,
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible
            };

            //Creating Textbox For Title TextBox
            NoteTitle = new TextBox
            {
                FontSize = wFontSize - 3,
                Padding = new Thickness(3),
                MaxLength = 48,
                MaxLines = 1,
                Opacity = wTextBoxOpacity,
                Height = R2.Height.Value / 2
            };

            //applying event to label/ notetyping
            NoteTyping.TextChanged += NoteTyping_TextChanged;
            NoteTitle.TextChanged += NoteTitle_TextChanged;



            //Labels\\
            CCounter_NoteText = new Label
            {
                FontSize = 14,
                Content = $"{NoteTyping.Text.Length} / {NoteTyping.MaxLength}",
                Padding = new Thickness(14,21,0,0)
            };
            

            CCounter_Title = new Label
            {
                FontSize = 14,
                Content = $"{NoteTitle.Text.Length} / {NoteTitle.MaxLength}",
                Padding = new Thickness(14, 21, 0, 0)
            };

            //Button(s)
            //Save
            SaveNoteBtn = new Button()
            {
                Content = "Save",
                Height = 35
                
            };

            SaveNoteBtn.Click += SaveNoteBtn_Click;
            

            //Getting and Creating a BitmapImage to Assign The image of MoveIcon   
            BitmapImage MILogo = new BitmapImage();
            MILogo.BeginInit();
            MILogo.UriSource = new Uri($"{AppPath}Images/MoveIcon_Small.png");
            MILogo.EndInit();

            //Creating New Image Controller
            MoveIcon = new Image()
            {
                Width = 61,
                Source = MILogo
                
            };

            //Applying Event(s) to new Image Control
            MoveIcon.MouseDown += MoveIcon_MouseDown;
            MoveIcon.MouseUp += MoveIcon_MouseUp;

            nNote.MouseMove += NNote_MouseMove;

            

            nSPanel_TextBox.Children.Add(NoteTyping);
            nSPanel_NoteButtons.Children.Add(SaveNoteBtn);
            #endregion


            #region Setting Control(s) into Correct Position
            //Assigning Image Icon to Top Row Last Column
            Grid.SetRow(MoveIcon, 0);
            Grid.SetColumn(MoveIcon, 3);

            //Setting Row/Column CCounter_Title
            Grid.SetRow(CCounter_Title, 1);
            Grid.SetColumn(CCounter_Title, 3);

            //Setting Gird Position For NoteTitle
            Grid.SetRow(NoteTitle, 1);
            Grid.SetColumn(NoteTitle, 0);
            Grid.SetColumnSpan(NoteTitle, 3);

            //Setting Row/Column CCounter_Note
            Grid.SetRow(CCounter_NoteText, 2);
            Grid.SetColumn(CCounter_NoteText, 3);


            //assigning textbox stackpanel to second row
            Grid.SetRow(nSPanel_TextBox, 2);
            Grid.SetColumn(nSPanel_TextBox, 0);
            Grid.SetColumnSpan(nSPanel_TextBox, 3);

            //Assigning Save Button Stack Panel to second row last column
            Grid.SetRow(nSPanel_NoteButtons, 2);
            Grid.SetColumn(nSPanel_NoteButtons, 3);
            #endregion


            //adding Children into Grid
            List<UIElement> ControlsToAdd = new List<UIElement>() {
                NoteTitle, MoveIcon, nSPanel_TextBox, CCounter_NoteText,
                CCounter_Title, nSPanel_NoteButtons
            };

            foreach (UIElement UIE in ControlsToAdd)
            {
                nGrid.Children.Add(UIE);
            }
            

            //adding grid to window
            nNote.Content = nGrid;

            //nGrid.Children.Add(ControlsToAdd);
            //nGrid.Children.Add(NoteTitle);
            //nGrid.Children.Add(MoveIcon);
            //nGrid.Children.Add(nSPanel_TextBox);
            //nGrid.Children.Add(CCounter_NoteText);


        }

        private void SaveNoteBtn_Click(object sender, RoutedEventArgs e)
        {//Call Method For Saving User's Note into a file

            //Creating Name For The File
            string FileSaveName = String.Concat(NoteTitle.Text.Trim().Replace(" ", ""), ".txt");
            string FileTargetPath = new Uri(@$"{AppPath}SaveFiles/{FileSaveName}").ToString();
            
            var output = JsonConvert.SerializeObject(new SaveNoteInfo
            {
                NoteName = NoteTitle.Text,
                NoteText = NoteTyping.Text,
                SaveFileName = FileSaveName
            });

            //Write to the same location of starting app
            File.WriteAllText(FileSaveName, output);

            Debug.WriteLine(output + " " + FileTargetPath);


        }

        private void NoteTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            CCounter_Title.Content = $"{NoteTitle.Text.Length} / {NoteTitle.MaxLength}";
        }

        private void NoteTyping_TextChanged(object sender, TextChangedEventArgs e)
        {
            CCounter_NoteText.Content = $"{NoteTyping.Text.Length} / {NoteTyping.MaxLength}";
        }

        private void NNote_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveWindow == true)
            {

                nNote.Left += (e.GetPosition(nNote).X - Mos_LastPos.X);
                nNote.Top += (e.GetPosition(nNote).Y - Mos_LastPos.Y);


                //controlling mouse position
                

            }
        }

        
        private void MoveIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MoveWindow = false;
        }

        //Allowing Window To Follow Mouse Position
        private void MoveIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MoveWindow = true;
            Mos_LastPos = e.GetPosition(nNote);

        }





        #endregion


    }
}
