using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace DIComplete
{
	public partial class SplitButton
	{
        public Window1 mainobject;        
		public SplitButton()
		{         
			this.InitializeComponent();
			// Insert code required on object creation below this point.
		}
        public void initobject(Window1 mo)
        {
            mainobject = mo;
        }

        private void InsetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InsetComboBox.SelectedIndex == 0)
                InsetButton.Content = "Add";
            else if (InsetComboBox.SelectedIndex == 1)
                InsetButton.Content = "Finish";
            else
                InsetButton.Content = "Back";            
        }

        private void InsetButton_Click(object sender, RoutedEventArgs e)
        {
            if (InsetButton.Content.Equals("Add"))
                mainobject.MultibuttonAddClick();
            else if (InsetButton.Content.Equals("Finish"))
                mainobject.MultibuttonFinishClick();
            else
                mainobject.MultibuttonBackClick();
        }

        private void InsetButton_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key.ToString().Equals("Q")) || (e.Key.ToString().Equals("q")))
            {
                if (InsetButton.Content.Equals("Back"))
                    InsetButton.Content = "Add";
                else if (InsetButton.Content.Equals("Add"))
                    InsetButton.Content = "Finish";
                else
                    InsetButton.Content = "Back";
            }
        }        
    }
}